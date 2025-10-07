using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonAdder : MonoBehaviour
{
    public FishNames fishNames;
    public GameProgress gameProgress;
    public IslandManager islandManager;
    public IslandScript islandScript;
    public GameObject giveFishButton;
    public GameObject baitQ;
    //private bool set = false;
    //private float  timer = 0;
    //private float cutoff = .5f;


    public GameObject donationBox;
    public TMP_Text fishInfoBox;
    public TMP_Text baitQText;


    //public GameObject FPInfo;
    //public GameObject currentFP;
    //public TMP_Text FPAmnt;

    //private bool called = false;

    public void Update()
    {
        if (gameObject.activeSelf)
        {
            //timer += Time.deltaTime;
            if (baitQ.activeSelf)
            {
                /*int curI = gameProgress.currentIslandIndex;
                int curH = islandScript.insideHouse;
                int curP = islandScript.insidePerson;
                if (islandManager.houseInfoStorage[curI][curH][curP] > 0)
                {*/
                giveFishButton.SetActive(true);

                updateInfo();
                //}
                /*else
                {
                    giveFishButton.SetActive(false);
                }*/
                //FPInfo.SetActive(false);
                //currentFP.SetActive(true);
            }
            else
            {
                fishInfoBox.text = "Put a fish here and I'll tell you what I'm offering for it!";
                giveFishButton.SetActive(false);
                //FPInfo.SetActive(true);
                //currentFP.SetActive(false);
            }
        }
    }

    public void updateInfo()
    {
        int[] answer = islandScript.giveOneFishHelper(donationBox);
        int rarity = answer[1];
        int index = answer[0];
        int.TryParse(baitQText.text, out int quantity);

        string fishRarityName = "";
        switch (rarity)
        {
            case 0:
                quantity = quantity * 40;
                fishRarityName = "Normal Rarity";
                break;
            case 1:
                quantity = quantity * 80;
                fishRarityName = "Fancy Rarity";
                break;
            case 2:
                quantity = quantity * 120;
                fishRarityName = "Extravagant Rarity";
                break;
            case 3:
                quantity = quantity * 160;
                fishRarityName = "Pristine Rarity";
                break;
            case 4:
                quantity = quantity * 1600;
                fishRarityName = "Elite Eight";
                break;
        }
        if (rarity != 4)
        {
            fishInfoBox.text = fishNames.nameLists[gameProgress.currentOceanIndex][rarity][index] + "\n" + fishRarityName + "\nQuantity = " + baitQText.text + "\n" + quantity + " gold";
        }
        else
        {
            fishInfoBox.text = fishNames.nameLists[4][0][index] + "\n" + fishRarityName + "\nQuantity = " + baitQText.text + "\n" + quantity + " gold";
        }
        
    }
}
