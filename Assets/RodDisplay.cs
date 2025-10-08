using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class RodDisplay : MonoBehaviour
{
    public FishNames fishNames;
    public NotEnoughScript notEnoughScript;
    public MiniGameOne miniGameOne;
    public MiniGameTwo miniGameTwo;
    public MiniGameThree miniGameThree;
    public MiniGameFour miniGameFour;
    public Sprite noBait;
    public BaitBuy baitBuy;
    public EquipmentScript equipmentScript;
    public GameObject rodHolder;
    public Image rodImage;
    public Image baitImage;
    public Image baitBacking;
    public Color normal;
    public Color fancy;
    public Color extravagant;
    public Color pristine;
    public TMP_Text minigameTitle;
    public TMP_Text minigameTitleInfoScreen;
    public TMP_Text minigameDescription;
    private string[] minigameTitles = {"Fishy Timing","Whirlpool Walk","Memory Mania","Whack-A-Fish"};
    private string[] minigameDescriptions = {
        "In Fishy Timing you must tap the screen at the right time to line up your hook with the fish! The closer you are, the more fish you'll be rewarded with!",
        "In Whirlpool Walk you must move the fish using the arrows in order to collect the hooks! But be careful... Whenever you hit a whirlpool, your arrow's directions will be randomized.",
        "In Memory Mania you must remember 9 tiles and what order they appear in. Round 1 will be the first 3, Round 2 will be the first 6, and Round 3 will be the full 9.",
        "In Whack-A-Fish you must tap as many fish as you can before they all go back into hiding! The more fish you whack, the more fish you'll get!"};
    

    public void setupRod(){
        equipmentScript.popUpInit();
        rodImage.sprite = equipmentScript.rods[equipmentScript.equippedRod];
        Debug.Log("Equipped bait toal is " + baitBuy.baitTotals[(equipmentScript.equippedBaitRarity*3) + equipmentScript.equippedBaitIndex]);
        if (baitBuy.baitTotals[(equipmentScript.equippedBaitRarity*3) + equipmentScript.equippedBaitIndex] == 0){
            baitImage.sprite = noBait;
        }
        else{
            baitImage.sprite = equipmentScript.baits[equipmentScript.equippedBaitIndex];
        }
        minigameTitle.text = minigameTitles[equipmentScript.equippedRod];
        minigameTitleInfoScreen.text = minigameTitles[equipmentScript.equippedRod];
        minigameDescription.text = minigameDescriptions[equipmentScript.equippedRod];
        switch(equipmentScript.equippedBaitRarity){
            case 0:
                baitBacking.color = normal;
                break;
            case 1:
                baitBacking.color = fancy;
                break;
            case 2:
                baitBacking.color = extravagant;
                break;
            case 3:
                baitBacking.color = pristine;
                break;
        }
    }

    public void rodHolderOff(){
        rodHolder.SetActive(false);
    }
    
    public void rodHolderOn(){
        rodHolder.SetActive(true);
    }

    public void miniGamePlayButton()
    {
        Debug.Log("HITTING THE ROD BUTTON");
        int rodE = equipmentScript.equippedRod;
        Debug.Log("rod equipped is " + rodE);
        if (rodE == 0)
        {
            miniGameOne.miniGamePlay();
        }
        else if (rodE == 1)
        {
            miniGameTwo.miniGamePlay();
        }
        else if (rodE == 2)
        {
            miniGameThree.miniGamePlay();
        }
        else if (rodE == 3)
        {
            miniGameFour.miniGamePlay();
        }
    }
}
