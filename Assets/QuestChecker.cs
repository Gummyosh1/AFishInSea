using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Image = UnityEngine.UI.Image;

public class QuestChecker : MonoBehaviour
{
    public Color grey;
    public Color red;
    public Color green;

    public InventoryKing inventoryKing;
    public GameProgress gameProgress;
    public FishNames fishNames;
    public IslandScript islandScript;
    public IslandManager islandManager;
    public GameObject donationBox;
    public GameObject popUpWindow;
    public GameObject donateButton;
    public TMP_Text baitQText;
    public TMP_Text completeText;
    public TMP_Text popUpWindowText;
    public Image slotBacking;



    private int rarity = 0;
    private int index = 0;
    private int quantity = 0;
    private string fishRarityName = "";

    private int questIndex = 0;

    public void Update()
    {
        if (baitQText.gameObject.activeSelf && !popUpWindow.activeSelf)
        {
            questIndex = -1;

            updateInfo();

            for (int i = 0; i < 3; i++)
            {
                //Debug.Log("Our island is " + gameProgress.currentIslandIndex + " our house is " + islandScript.insideHouse + " our person is " + islandScript.insidePerson);
                //Debug.Log("Quest Type is " + islandManager.questTypeInfo[gameProgress.currentIslandIndex][islandScript.insideHouse][islandScript.insidePerson][i]);
                //Debug.Log("The type is " + islandManager.questTypeInfo[gameProgress.currentIslandIndex][islandScript.insideHouse][islandScript.insidePerson][0]);
                if (islandManager.questTypeInfo[gameProgress.currentIslandIndex][islandScript.insideHouse][islandScript.insidePerson][i] == 0)
                {
                    if (islandManager.questRarityInfo[gameProgress.currentIslandIndex][islandScript.insideHouse][islandScript.insidePerson][i] == rarity)
                    {
                        if (islandManager.questTracking[gameProgress.currentIslandIndex][islandScript.insideHouse][islandScript.insidePerson][i] > 0)
                        {
                            Debug.Log("Island is " + gameProgress.currentIslandIndex);
                            Debug.Log("House is " + islandScript.insideHouse);
                            Debug.Log("Person is " + islandScript.insidePerson);
                            Debug.Log("i is " + i);
                            
                            //Debug.Log("AHHHHHHHHHHHHHHHHHHHHHHHHH");
                            Debug.Log("0 index tracking is " + islandManager.questTracking[gameProgress.currentIslandIndex][islandScript.insideHouse][islandScript.insidePerson][i]);
                            questIndex = i;
                            donateButton.SetActive(true);
                            slotBacking.color = green;
                            break;
                        }
                    }
                }
            }
            if (questIndex == -1)
            {
                donateButton.SetActive(false);
                slotBacking.color = red;
            }
        }
        else if (!baitQText.gameObject.activeSelf)
        {
            slotBacking.color = grey;
            donateButton.SetActive(false);
        }
    }

    public void setupWindow()
    {
        int.TryParse(baitQText.text, out int q);
        popUpWindowText.text = "Confirm your donation of " + fishRarityName + " fish?";
        popUpWindow.SetActive(true);
    }

    public void close()
    {
        popUpWindow.SetActive(false);
    }

    public void donateFish()
    {
        //TWO SITUATIONS! 
        //FIRST -- QUEST_AMOUNT >= INVENTORY_AMOUNT
        int FishNeeded = islandManager.questTracking[gameProgress.currentIslandIndex][islandScript.insideHouse][islandScript.insidePerson][questIndex];
        if (FishNeeded >= quantity)
        {
            islandManager.updateQuest(islandScript.insideHouse, islandScript.insidePerson, questIndex, quantity);
            baitQText.text = "0";
            Destroy(donationBox.transform.GetChild(0).gameObject);
            baitQText.gameObject.SetActive(false);

        }
        //SECOND -- QUEST_AMOUNT < INVENTORY_AMOUNT
        if (FishNeeded < quantity)
        {
            baitQText.text = (quantity - islandManager.questTracking[gameProgress.currentIslandIndex][islandScript.insideHouse][islandScript.insidePerson][questIndex]).ToString();
            islandManager.updateQuest(islandScript.insideHouse, islandScript.insidePerson, questIndex, FishNeeded);
        }
        inventoryKing.ClearThenLoadInventory();
        islandScript.questCheck(0);
        close();
    }
    
    public void updateInfo()
    {
        int[] answer = islandScript.giveOneFishHelper(donationBox);
        rarity = answer[1];
        index = answer[0];
        int.TryParse(baitQText.text, out quantity);

        fishRarityName = "";
        switch (rarity)
        {
            case 0:
                //quantity = quantity * 40;
                fishRarityName = "Normal";
                break;
            case 1:
                //quantity = quantity * 80;
                fishRarityName = "Fancy";
                break;
            case 2:
                //quantity = quantity * 120;
                fishRarityName = "Extravagant";
                break;
            case 3:
                //quantity = quantity * 160;
                fishRarityName = "Pristine";
                break;
            case 4:
                //quantity = quantity * 1600;
                fishRarityName = "Elite";
                break;
        }
        if (rarity != 4)
        {
            //completeText.text = fishNames.nameLists[gameProgress.currentOceanIndex][rarity][index] + "\n" + fishRarityName + "\nQuantity = " + baitQText.text + "\n" + quantity + " gold";
        }
        else
        {
            //completeText.text = fishNames.nameLists[4][0][index] + "\n" + fishRarityName + "\nQuantity = " + baitQText.text + "\n" + quantity + " gold";
        }

    }
}
