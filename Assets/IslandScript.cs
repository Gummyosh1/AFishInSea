using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class IslandScript : MonoBehaviour
{
    public GameObject sellAllLock;
    public Button sellAllButton;
    public GameObject buyTFGPopUp;
    public GameObject sellAllPopUp;
    public TMP_Text sellAllText;
    public InventoryKing inventoryKing;
    public Slider slider;
    public Image fill;
    public Color green;
    public Color yellow;
    public GameObject popUpProgress;
    public GameObject lock1;
    public GameObject greyOutButton;
    public Button normalButton;
    public TMP_Text questProgress;

    public QuestSlider questSlider;
    public FishNames fishNames;
    public Merchant merchant;
    public GoldManager goldManager;
    public GameObject nextIslandButton;
    public GameObject claimButtonAndText;
    public GameObject fishGivenInfoScreen;
    //public FishCollected fishCollected;
    public Image houseResident;
    public SailingTracker sailingTracker;
    public GameProgress gameProgress;
    public HousingSprites housingSprites;
    public IslandManager islandManager;
    public TabScript tabScript;
    public Image yellowBox;
    public Color yellow1;
    public Color red1;


    public GameObject[] boats;
    public GameObject[] donationBoxes;
    public TMP_Text[] donationQuantity;
    public GameObject[] inventoryHolders;
    public GameObject[] giveAllBoxes;
    public TMP_Text[] giveAllQuantity;



    private IslandFishStorage islandFishStorage;
    [NonSerialized] public int insideHouse = 0;
    [NonSerialized] public int insidePerson = 0;
    [NonSerialized] public int[] fishGivenList = new int[16];

    /*public void loadHouseData(){
        currentIslandIndex = gameProgress.currentIslandIndex;

        //Resets all prior islands count to 0 that we've already visited
        for (int i = currentIslandIndex - 1; i >= 0; i--){
                islandManager.houseInfoStorage[i] = new int[] {0,0,0,0};
        }

        //Sets the current fish counts on the current island to the proper amounts
        islandFishStorage = islandManager.loadCountData();
        for (int j = 0; j < 4; j++){
            for (int i = 0; i < 5; i++){
                //Debug.Log(islandManager.houseInfoStorage[currentIslandIndex][j][i]);
                if (j == 0){
                    if (islandFishStorage != null){
                        islandManager.houseInfoStorage[currentIslandIndex][j][i] = islandFishStorage.House1[i];
                    }
                }
                else if (j == 1){
                    if (islandFishStorage != null){
                        islandManager.houseInfoStorage[currentIslandIndex][j][i] = islandFishStorage.House2[i];
                    }
                }
                else if (j == 2){
                    if (islandFishStorage != null){
                        islandManager.houseInfoStorage[currentIslandIndex][j][i] = islandFishStorage.House3[i];
                    }
                }
                else if (j == 3){
                    if (islandFishStorage != null){
                        islandManager.houseInfoStorage[currentIslandIndex][j][i] = islandFishStorage.House4[i];
                    }
                }
            }
        }
        //Debug.Log("And Island 4 has this many fish left: " + islandManager.houseInfoStorage[3][0][1]);
    }*/


    public void boatSetup()
    {
        for (int i = 0; i < boats.Length; i++)
        {
            if (i == gameProgress.currentIslandIndex)
            {
                boats[i].SetActive(true);
            }
            else
            {
                boats[i].SetActive(false);
            }
        }
    }

    public void houseOneTap()
    {
        insideHouse = 0;
        islandManager.HouseTap();

        //TODO SET THE TRIFECTA RESIDENTS TO THE PROPER SPRITES
        //houseResident.sprite = housingSprites.Islands[currentIslandIndex][0];
    }

    public void houseTwoTap()
    {
        insideHouse = 1;
        islandManager.HouseTap();
        //TODO SET THE TRIFECTA RESIDENTS TO THE PROPER SPRITES
        //houseResident.sprite = housingSprites.Islands[currentIslandIndex][1];
    }

    public void houseThreeTap()
    {
        insideHouse = 2;
        islandManager.HouseTap();
        //TODO SET THE TRIFECTA RESIDENTS TO THE PROPER SPRITES
        //houseResident.sprite = housingSprites.Islands[currentIslandIndex][2];
    }

    public void personOneTap()
    {
        insidePerson = 0;
        islandManager.HouseEnter();
        //fishCollected.InitializeProgressBar();
        //TODO
        // PUT FP REMAINING & FP TIL STIPEND TEXT UPDATE HERE
        // PUT THE POP UP MENU PERSON'S SPRITE TO DISPLAY
    }

    public void personTwoTap()
    {
        insidePerson = 1;
        islandManager.HouseEnter();
        //fishCollected.InitializeProgressBar();
        //TODO
        // PUT FP REMAINING & FP TIL STIPEND TEXT UPDATE HERE
        // PUT THE POP UP MENU PERSON'S SPRITE TO DISPLAY
    }

    public void personThreeTap()
    {
        insidePerson = 2;
        islandManager.HouseEnter();
        //fishCollected.InitializeProgressBar();
        //TODO
        // PUT FP REMAINING & FP TIL STIPEND TEXT UPDATE HERE
        // PUT THE POP UP MENU PERSON'S SPRITE TO DISPLAY
    }

    public void merchantTap()
    {

    }

    /*public void giveOneFish(){
        if (donationBoxes[0].transform.childCount == 0 && donationBoxes[1].transform.childCount == 0 && donationBoxes[2].transform.childCount == 0){
            return;
        }
        int rarity = 0;
        int open = 0;
        if (inventoryHolders[0].activeSelf){
            rarity = giveOneFishHelper(donationBoxes[0]);
            open = 0;
        }
        else if (inventoryHolders[1].activeSelf){
            rarity = giveOneFishHelper(donationBoxes[1]);
            open = 1;
        }
        else if (inventoryHolders[2].activeSelf){
            rarity = giveOneFishHelper(donationBoxes[2]);
            open = 2;
        }

        int fishStartCount = int.Parse(donationQuantity[open].text);
        
        if (rarity == 4){
            if (islandManager.houseInfoStorage[currentIslandIndex][insideHouse][4] > 0){
                islandManager.houseInfoStorage[currentIslandIndex][insideHouse][4]--;
                donationQuantity[open].text = (int.Parse(donationQuantity[open].text) - 1).ToString();
                magicalText.text = "Magical: " + islandManager.houseInfoStorage[currentIslandIndex][insideHouse][4];
            }
            else if (islandManager.houseInfoStorage[currentIslandIndex][insideHouse][3] > 0){
                islandManager.houseInfoStorage[currentIslandIndex][insideHouse][3]--;
                donationQuantity[open].text = (int.Parse(donationQuantity[open].text) - 1).ToString();
                pristineText.text = "Pristine: " + islandManager.houseInfoStorage[currentIslandIndex][insideHouse][3];
            }
            else if (islandManager.houseInfoStorage[currentIslandIndex][insideHouse][2] > 0){
                islandManager.houseInfoStorage[currentIslandIndex][insideHouse][2]--;
                donationQuantity[open].text = (int.Parse(donationQuantity[open].text) - 1).ToString();
                extravagantText.text = "Extravagant: " + islandManager.houseInfoStorage[currentIslandIndex][insideHouse][2];
            }
            else if (islandManager.houseInfoStorage[currentIslandIndex][insideHouse][1] > 0){
                islandManager.houseInfoStorage[currentIslandIndex][insideHouse][1]--;
                donationQuantity[open].text = (int.Parse(donationQuantity[open].text) - 1).ToString();
                fancyText.text = "Fancy: " + islandManager.houseInfoStorage[currentIslandIndex][insideHouse][1];
            }
            else if (islandManager.houseInfoStorage[currentIslandIndex][insideHouse][0] > 0){
                islandManager.houseInfoStorage[currentIslandIndex][insideHouse][0]--;
                donationQuantity[open].text = (int.Parse(donationQuantity[open].text) - 1).ToString();
                normalText.text = "Normal: " + islandManager.houseInfoStorage[currentIslandIndex][insideHouse][0];
            }
        }
        else if(rarity == 3){
            if (islandManager.houseInfoStorage[currentIslandIndex][insideHouse][3] > 0){
                islandManager.houseInfoStorage[currentIslandIndex][insideHouse][3]--;
                donationQuantity[open].text = (int.Parse(donationQuantity[open].text) - 1).ToString();
                pristineText.text = "Pristine: " + islandManager.houseInfoStorage[currentIslandIndex][insideHouse][3];
            }
            else if (islandManager.houseInfoStorage[currentIslandIndex][insideHouse][2] > 0){
                islandManager.houseInfoStorage[currentIslandIndex][insideHouse][2]--;
                donationQuantity[open].text = (int.Parse(donationQuantity[open].text) - 1).ToString();
                extravagantText.text = "Extravagant: " + islandManager.houseInfoStorage[currentIslandIndex][insideHouse][2];
            }
            else if (islandManager.houseInfoStorage[currentIslandIndex][insideHouse][1] > 0){
                islandManager.houseInfoStorage[currentIslandIndex][insideHouse][1]--;
                donationQuantity[open].text = (int.Parse(donationQuantity[open].text) - 1).ToString();
                fancyText.text = "Fancy: " + islandManager.houseInfoStorage[currentIslandIndex][insideHouse][1];
            }
            else if (islandManager.houseInfoStorage[currentIslandIndex][insideHouse][0] > 0){
                islandManager.houseInfoStorage[currentIslandIndex][insideHouse][0]--;
                donationQuantity[open].text = (int.Parse(donationQuantity[open].text) - 1).ToString();
                normalText.text = "Normal: " + islandManager.houseInfoStorage[currentIslandIndex][insideHouse][0];
            }
        }
        else if (rarity == 2){
            if (islandManager.houseInfoStorage[currentIslandIndex][insideHouse][2] > 0){
                islandManager.houseInfoStorage[currentIslandIndex][insideHouse][2]--;
                donationQuantity[open].text = (int.Parse(donationQuantity[open].text) - 1).ToString();
                extravagantText.text = "Extravagant: " + islandManager.houseInfoStorage[currentIslandIndex][insideHouse][2];
            }
            else if (islandManager.houseInfoStorage[currentIslandIndex][insideHouse][1] > 0){
                islandManager.houseInfoStorage[currentIslandIndex][insideHouse][1]--;
                donationQuantity[open].text = (int.Parse(donationQuantity[open].text) - 1).ToString();
                fancyText.text = "Fancy: " + islandManager.houseInfoStorage[currentIslandIndex][insideHouse][1];
            }
            else if (islandManager.houseInfoStorage[currentIslandIndex][insideHouse][0] > 0){
                islandManager.houseInfoStorage[currentIslandIndex][insideHouse][0]--;
                donationQuantity[open].text = (int.Parse(donationQuantity[open].text) - 1).ToString();
                normalText.text = "Normal: " + islandManager.houseInfoStorage[currentIslandIndex][insideHouse][0];
            }
        }
        else if (rarity == 1){
            if (islandManager.houseInfoStorage[currentIslandIndex][insideHouse][1] > 0){
                islandManager.houseInfoStorage[currentIslandIndex][insideHouse][1]--;
                donationQuantity[open].text = (int.Parse(donationQuantity[open].text) - 1).ToString();
                fancyText.text = "Fancy: " + islandManager.houseInfoStorage[currentIslandIndex][insideHouse][1];
            }
            else if (islandManager.houseInfoStorage[currentIslandIndex][insideHouse][0] > 0){
                islandManager.houseInfoStorage[currentIslandIndex][insideHouse][0]--;
                donationQuantity[open].text = (int.Parse(donationQuantity[open].text) - 1).ToString();
                normalText.text = "Normal: " + islandManager.houseInfoStorage[currentIslandIndex][insideHouse][0];
            }
        }
        else if (rarity == 0){
            if (islandManager.houseInfoStorage[currentIslandIndex][insideHouse][0] > 0){
                islandManager.houseInfoStorage[currentIslandIndex][insideHouse][0]--;
                donationQuantity[open].text = (int.Parse(donationQuantity[open].text) - 1).ToString();
                normalText.text = "Normal: " + islandManager.houseInfoStorage[currentIslandIndex][insideHouse][0];
            }
        }
        int fishDelta = fishStartCount - int.Parse(donationQuantity[open].text);
        fishGivenList[currentIslandIndex] += fishDelta;

        islandManager.saveData(islandManager.houseInfoStorage[currentIslandIndex][0], islandManager.houseInfoStorage[currentIslandIndex][1],
        islandManager.houseInfoStorage[currentIslandIndex][2], islandManager.houseInfoStorage[currentIslandIndex][3]);
        islandManager.saveFishGiveth();
        fishCollected.UpdateProgressBar();
        if (int.Parse(donationQuantity[open].text) == 0){
            Destroy(donationBoxes[open].transform.GetChild(0).gameObject);
            donationQuantity[open].gameObject.SetActive(false);
        }
    }*/


    //BEGIN OF MULTIPLE GIFTING
    //
    //
    //
    //
    //
    //

    //REWORK ALL THIS
    public void giveAllFish()
    {
        if (donationBoxes[0].transform.childCount == 0)
        {
            return;
        }
        int rarity = 0;
        int index = 0;
        int open = 0;
        if (inventoryHolders[0].activeSelf)
        {
            int[] holder = giveOneFishHelper(donationBoxes[0]);
            rarity = holder[1];
            index = holder[0];
            open = 0;
        }

        int fishStartCount = int.Parse(donationQuantity[open].text);

        donationQuantity[open].text = "0";
        //int beforeGold = islandManager.houseInfoStorage[gameProgress.currentIslandIndex][insideHouse][insidePerson];
        /*
        if (rarity == 4)
        {
            bool Remainder = true;
            int FPNeeded = islandManager.houseInfoStorage[gameProgress.currentIslandIndex][insideHouse][insidePerson];

            if (FPNeeded > 0)
            {
                //TWO SITUATIONS! 
                //FIRST -- remaning >= fishCount
                if (FPNeeded >= (int.Parse(donationQuantity[open].text) * 10000))
                {
                    islandManager.houseInfoStorage[gameProgress.currentIslandIndex][insideHouse][insidePerson] -= int.Parse(donationQuantity[open].text) * 10000;
                    donationQuantity[open].text = "0";
                    //TODO
                    //UPDATE TEXT HERE ABT REMAINING FP

                    Remainder = false;
                }
                //SECOND -- remaining < FP
                if (Remainder && FPNeeded < (int.Parse(donationQuantity[open].text) * 10000))
                {
                    donationQuantity[open].text = (int.Parse(donationQuantity[open].text) - 1).ToString();
                    islandManager.houseInfoStorage[gameProgress.currentIslandIndex][insideHouse][insidePerson] = 0;
                    //TODO
                    //UPDATE TEXT HERE ABT FP BEING 0 NOW & TRIGGER WHATEVER ENDSTEP EVENTS MUST HAPPEN

                    Remainder = true;
                }
            }

        }

        else if (rarity == 3)
        {
            bool Remainder = true;
            int FPNeeded = islandManager.houseInfoStorage[gameProgress.currentIslandIndex][insideHouse][insidePerson];

            if (FPNeeded > 0)
            {
                //TWO SITUATIONS! 
                //FIRST -- remaning >= fishCount
                if (FPNeeded >= (int.Parse(donationQuantity[open].text) * 4))
                {
                    islandManager.houseInfoStorage[gameProgress.currentIslandIndex][insideHouse][insidePerson] -= int.Parse(donationQuantity[open].text) * 4;
                    donationQuantity[open].text = "0";
                    //TODO
                    //UPDATE TEXT HERE ABT REMAINING FP

                    Remainder = false;
                }
                //SECOND -- remaining < fishCount
                if (Remainder && FPNeeded < (int.Parse(donationQuantity[open].text) * 4))
                {
                    donationQuantity[open].text = (int.Parse(donationQuantity[open].text) - Math.Ceiling(FPNeeded / 4f)).ToString();
                    islandManager.houseInfoStorage[gameProgress.currentIslandIndex][insideHouse][insidePerson] = 0;
                    //TODO
                    //UPDATE TEXT HERE ABT FP BEING 0 NOW & TRIGGER WHATEVER ENDSTEP EVENTS MUST HAPPEN

                    Remainder = true;
                }
            }
        }

        else if (rarity == 2)
        {
            bool Remainder = true;
            int FPNeeded = islandManager.houseInfoStorage[gameProgress.currentIslandIndex][insideHouse][insidePerson];

            if (FPNeeded > 0)
            {
                //TWO SITUATIONS! 
                //FIRST -- remaning >= fishCount
                if (FPNeeded >= (int.Parse(donationQuantity[open].text) * 3))
                {
                    islandManager.houseInfoStorage[gameProgress.currentIslandIndex][insideHouse][insidePerson] -= int.Parse(donationQuantity[open].text) * 3;
                    donationQuantity[open].text = "0";
                    //TODO
                    //UPDATE TEXT HERE ABT REMAINING FP

                    Remainder = false;
                }
                //SECOND -- remaining < fishCount
                if (Remainder && FPNeeded < (int.Parse(donationQuantity[open].text) * 3))
                {
                    donationQuantity[open].text = (int.Parse(donationQuantity[open].text) - Math.Ceiling(FPNeeded / 3f)).ToString();
                    islandManager.houseInfoStorage[gameProgress.currentIslandIndex][insideHouse][insidePerson] = 0;
                    //TODO
                    //UPDATE TEXT HERE ABT FP BEING 0 NOW & TRIGGER WHATEVER ENDSTEP EVENTS MUST HAPPEN

                    Remainder = true;
                }
            }
        }


        else if (rarity == 1)
        {
            bool Remainder = true;
            int FPNeeded = islandManager.houseInfoStorage[gameProgress.currentIslandIndex][insideHouse][insidePerson];

            if (FPNeeded > 0)
            {
                //TWO SITUATIONS! 
                //FIRST -- remaning >= fishCount
                if (FPNeeded >= (int.Parse(donationQuantity[open].text) * 2))
                {
                    islandManager.houseInfoStorage[gameProgress.currentIslandIndex][insideHouse][insidePerson] -= int.Parse(donationQuantity[open].text) * 2;
                    donationQuantity[open].text = "0";
                    //TODO
                    //UPDATE TEXT HERE ABT REMAINING FP

                    Remainder = false;
                }
                //SECOND -- remaining < fishCount
                if (Remainder && FPNeeded < (int.Parse(donationQuantity[open].text) * 2))
                {
                    donationQuantity[open].text = (int.Parse(donationQuantity[open].text) - Math.Ceiling(FPNeeded / 2f)).ToString();
                    islandManager.houseInfoStorage[gameProgress.currentIslandIndex][insideHouse][insidePerson] = 0;
                    //TODO
                    //UPDATE TEXT HERE ABT FP BEING 0 NOW & TRIGGER WHATEVER ENDSTEP EVENTS MUST HAPPEN

                    Remainder = true;
                }
            }
        }


        else if (rarity == 0)
        {
            bool Remainder = true;
            int FPNeeded = islandManager.houseInfoStorage[gameProgress.currentIslandIndex][insideHouse][insidePerson];

            if (FPNeeded > 0)
            {
                //TWO SITUATIONS! 
                //FIRST -- remaning >= fishCount
                if (FPNeeded >= int.Parse(donationQuantity[open].text))
                {
                    islandManager.houseInfoStorage[gameProgress.currentIslandIndex][insideHouse][insidePerson] -= int.Parse(donationQuantity[open].text);
                    donationQuantity[open].text = "0";
                    //TODO
                    //UPDATE TEXT HERE ABT REMAINING FP

                    Remainder = false;
                }
                //SECOND -- remaining < fishCount
                if (Remainder && FPNeeded < int.Parse(donationQuantity[open].text))
                {
                    donationQuantity[open].text = (int.Parse(donationQuantity[open].text) - FPNeeded).ToString();
                    islandManager.houseInfoStorage[gameProgress.currentIslandIndex][insideHouse][insidePerson] = 0;
                    //TODO
                    //UPDATE TEXT HERE ABT FP BEING 0 NOW & TRIGGER WHATEVER ENDSTEP EVENTS MUST HAPPEN

                    Remainder = true;
                }
            }
        }*/


        int fishDelta = fishStartCount - int.Parse(donationQuantity[open].text);

        if (rarity != 4 && fishDelta > 0)
        {
            islandManager.claimStipend(fishDelta, fishNames.nameLists[gameProgress.currentOceanIndex][rarity][index], rarity);
        }
        else if (fishDelta > 0)
        {
            islandManager.claimStipend(fishDelta, fishNames.nameLists[4][0][index], rarity);
        }


        fishGivenList[gameProgress.currentIslandIndex] += fishDelta;
        //islandManager.saveData();
        //islandManager.saveQuest();
        islandManager.saveFishGiveth();
        //fishCollected.UpdateProgressBar();



        if (int.Parse(donationQuantity[open].text) == 0)
        {
            Destroy(donationBoxes[open].transform.GetChild(0).gameObject);
            donationQuantity[open].gameObject.SetActive(false);
        }

        int curI = gameProgress.currentIslandIndex;
        int curH = insideHouse;
        int curP = insidePerson;
        //int localFP = islandManager.IslandFishTotals[curI][curH][curP];
        int curFP = islandManager.houseInfoStorage[curI][curH][curP];


        /*
        if ((localFP - curFP) >= (localFP / 3))
        {
            if (islandManager.claimed[curI][curH][curP][0] == 0)
            {
                claimButtonAndText.SetActive(true);
            }
        }

        //SECOND STIPEND
        if ((localFP - curFP) >= ((localFP / 3) * 2))
        {
            if (islandManager.claimed[curI][curH][curP][1] == 0)
            {
                claimButtonAndText.SetActive(true);
            }
        }


        //FULL PAYMENT
        if (curFP == 0)
        {
            if (islandManager.claimed[curI][curH][curP][2] == 0)
            {
                claimButtonAndText.SetActive(true);
            }
        }*/




        //UPDATES BAR PROGRESS WHENEVER A GIFT IS GIVEN
        //barInfo.text = (islandManager.houseInfoStorage[curI][curH][curP] * 40).ToString();

        //UPDATE RED BOX
        if (islandManager.houseInfoStorage[curI][curH][curP] > 0)
        {
            yellowBox.color = yellow1;
        }
        else
        {
            yellowBox.color = red1;
        }

    }

    public void giveAllForReal()
    {
        for (int i = 0; i < giveAllBoxes.Length; i++)
        {
            if (giveAllBoxes[i].transform.childCount == 0)
            {
            }
            else
            {
                int rarity = 0;
                int index = 0;

                int[] holder = giveOneFishHelper(giveAllBoxes[i]);
                rarity = holder[1];
                index = holder[0];

                int fishStartCount = int.Parse(giveAllQuantity[i].text);

                giveAllQuantity[i].text = "0";

                int fishDelta = fishStartCount;

                if (rarity != 4 && fishDelta > 0)
                {
                    islandManager.claimAll(fishDelta, fishNames.nameLists[gameProgress.currentOceanIndex][rarity][index], rarity);
                }
                else if (fishDelta > 0)
                {
                    islandManager.claimAll(fishDelta, fishNames.nameLists[4][0][index], rarity);
                }


                fishGivenList[gameProgress.currentIslandIndex] += fishDelta;
                islandManager.saveFishGiveth();

                if (int.Parse(giveAllQuantity[i].text) == 0)
                {
                    //Destroy(giveAllBoxes[i].transform.GetChild(0).gameObject);
                    giveAllQuantity[i].gameObject.SetActive(false);
                }
            }
        }
        inventoryKing.SaveInventory();
        inventoryKing.ClearThenLoadInventory();
        
    }

    public int calculateTotals()
    {
        int total = 0;
        int holder = 0;
        int fishRarity = 0;
        int numFishSold = 0;

        for (int i = 0; i < giveAllBoxes.Length; i++)
        {
            if (giveAllBoxes[i].transform.childCount != 0)
            {
                holder = 0;
                int[] amongus = giveOneFishHelper(giveAllBoxes[i]);
                fishRarity = amongus[1];
                numFishSold = int.Parse(giveAllQuantity[i].text);
                switch (fishRarity)
                {
                    case 0:
                        holder = numFishSold;
                        break;
                    case 1:
                        holder = numFishSold * 2;
                        break;
                    case 2:
                        holder = numFishSold * 3;
                        break;
                    case 3:
                        holder = numFishSold * 4;
                        break;
                    case 4:
                        holder = numFishSold * 40;
                        break;
                }
                holder *= 40;

                total += holder;
            }
        }
        return total;
    }

    public void sellPopUp()
    {
        if (gameProgress.battlePass.passPurchased)
        {
            sellAllPopUp.SetActive(true);
            int totalHolder = calculateTotals();
            sellAllText.text = $"Sell all the fish you've caught for \n{totalHolder}\ngold?";
        }
        else
        {
            buyTFGPopUp.SetActive(true);
        }
        

    }

    public void sellAllLockInit()
    {
        if (gameProgress.battlePass.passPurchased)
        {
            sellAllButton.enabled = true;
            sellAllLock.SetActive(false);
        }
        else
        {
            sellAllButton.enabled = false;
            sellAllLock.SetActive(true);
        }
    }
    //END OF MULTIPLE GIFTING
    //
    //
    //
    //
    //
    //

    public void questCheck(int questTypeIndex, int val1 = 0, int val2 = 0)
    {
        switch (questTypeIndex)
        {
            case 0:
                //EVERYTHING ELSE CODED IN QUESTCHECKER SCRIPT, ATTATCHED TO INVENTORY IN VILLAGER ON ISLANDS
                updateSliderLocal();
                break;

            case 1:
                //VAL1 == SCORE PLAYER SCORED
                //VAL2 == INDEX OF WHAT MINIGAME THEY WERE PLAYING

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        for (int a = 0; a < 3; a++)
                        {
                            if (islandManager.questTypeInfo[gameProgress.currentIslandIndex][i][j][a] == 1)
                            {
                                if (islandManager.questRarityInfo[gameProgress.currentIslandIndex][i][j][a] == val2)
                                {
                                    if (val1 > islandManager.questCaps[gameProgress.currentIslandIndex][i][j][a])
                                    {
                                        if (islandManager.questTracking[gameProgress.currentIslandIndex][i][j][a] != 0)
                                        {
                                            islandManager.updateQuest(i, j, a, islandManager.questCaps[gameProgress.currentIslandIndex][i][j][a]);
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                updateSliderLocal();
                break;

            case 2:
                //VAL1 == ITEM IN MERCHANT INDEX
                //VAL2 == N/A
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        for (int a = 0; a < 3; a++)
                        {
                            if (islandManager.questTypeInfo[gameProgress.currentIslandIndex][i][j][a] == 2)
                            {
                                if (islandManager.questRarityInfo[gameProgress.currentIslandIndex][i][j][a] == val1)
                                {
                                    if (islandManager.questTracking[gameProgress.currentIslandIndex][i][j][a] != 0)
                                    {
                                        islandManager.updateQuest(i, j, a, islandManager.questCaps[gameProgress.currentIslandIndex][i][j][a]);
                                    }
                                    break;

                                }
                            }
                        }
                    }
                }
                updateSliderLocal();
                break;

            case 3:
                //VAL1 == N/A
                //VAL2 == N/A
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        for (int a = 0; a < 3; a++)
                        {
                            if (islandManager.questTypeInfo[gameProgress.currentIslandIndex][i][j][a] == 3)
                            {
                                if (islandManager.questTracking[gameProgress.currentIslandIndex][i][j][a] != 0)
                                {
                                    islandManager.updateQuest(i, j, a, 1);
                                }
                                break;
                            }
                        }
                    }
                }
                updateSliderLocal();
                break;

            case 4:
                //VAL1 == BAIT RARITY
                //VAL2 == BAIT QUANTITY
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        for (int a = 0; a < 3; a++)
                        {
                            if (islandManager.questTypeInfo[gameProgress.currentIslandIndex][i][j][a] == 4)
                            {
                                if (islandManager.questRarityInfo[gameProgress.currentIslandIndex][i][j][a] == val1)
                                {
                                    if (islandManager.questTracking[gameProgress.currentIslandIndex][i][j][a] > 0)
                                    {
                                        islandManager.updateQuest(i, j, a, val2);
                                    }
                                    break;

                                }
                            }
                        }
                    }
                }

                updateSliderLocal();
                break;

            case 5:
                //VAL1 == CURRENT STREAK
                //VAL2 == N/A
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        for (int a = 0; a < 3; a++)
                        {
                            if (islandManager.questTypeInfo[gameProgress.currentIslandIndex][i][j][a] == 5)
                            {
                                islandManager.updateQuestStreak(i, j, a, val1);
                                break;
                            }
                        }
                    }
                }
                updateSliderLocal();
                break;

            case 6:
                //VAL1 == GOLD GAINED AMOUNT
                //VAL2 == N/A
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        for (int a = 0; a < 3; a++)
                        {
                            if (islandManager.questTypeInfo[gameProgress.currentIslandIndex][i][j][a] == 6)
                            {
                                islandManager.updateQuest(i, j, a, val1);
                                if (islandManager.questTracking[gameProgress.currentIslandIndex][i][j][a] < 0)
                                {
                                    islandManager.questTracking[gameProgress.currentIslandIndex][i][j][a] = 0;
                                    islandManager.saveQuest();
                                }
                                break;
                            }
                        }
                    }
                }
                updateSliderLocal();
                break;
        }
    }

    public void updateSliderLocal()
    {
        bool[] sliderInitBools = { false, false, false };

        for (int i = 0; i < 3; i++)
        {
            if (islandManager.questTracking[gameProgress.currentIslandIndex][insideHouse][insidePerson][i] <= 0)
            {
                sliderInitBools[i] = true;
            }
            else
            {
                sliderInitBools[i] = false;
            }
        }

        questSlider.updateSlider(gameProgress.currentIslandIndex, insideHouse, insidePerson, sliderInitBools[0], sliderInitBools[1], sliderInitBools[2]);
    }

    public void donateSetup()
    {

    }


    public int[] giveOneFishHelper(GameObject donationBox)
    {
        if (donationBox.transform.childCount > 0)
        {
            string kidName = donationBox.transform.GetChild(0).gameObject.name;

            if (kidName.Contains("Normal"))
            {
                int[] a = { donationBox.transform.GetChild(0).gameObject.GetComponent<NormalFish>().spriteIndex, 0 };
                return a;
            }
            else if (kidName.Contains("Fancy"))
            {
                int[] a = { donationBox.transform.GetChild(0).gameObject.GetComponent<FancyFish>().spriteIndex, 1 };
                return a;
            }
            else if (kidName.Contains("Extravagant"))
            {
                int[] a = { donationBox.transform.GetChild(0).gameObject.GetComponent<ExtravagantFish>().spriteIndex, 2 };
                return a;
            }
            else if (kidName.Contains("Pristine"))
            {
                int[] a = { donationBox.transform.GetChild(0).gameObject.GetComponent<PristineFish>().spriteIndex, 3 };
                return a;
            }
            else if (kidName.Contains("Magical"))
            {
                int[] a = { donationBox.transform.GetChild(0).gameObject.GetComponent<MagicalFishPrefab>().spriteIndex, 4 };
                return a;
            }
            else
            {
                int[] a = { -1, -1 };
                return a;
            }
        }

        //[INDEX, RARITY]
        int[] b = { -1, -1 };
        return b;

    }

    public void moveFishy()
    {
        if (inventoryHolders[0].activeSelf)
        {
            Debug.Log("Naught triggered");
            if (donationBoxes[1].transform.childCount > 0)
            {
                donationBoxes[1].transform.GetChild(0).SetParent(donationBoxes[0].transform);
                donationQuantity[0].text = donationQuantity[1].text;
                donationQuantity[0].gameObject.SetActive(true);
                donationQuantity[1].gameObject.SetActive(false);
                donationQuantity[1].text = "0";
            }
            else if (donationBoxes[2].transform.childCount > 0)
            {
                donationBoxes[2].transform.GetChild(0).SetParent(donationBoxes[0].transform);
                donationQuantity[0].text = donationQuantity[2].text;
                donationQuantity[0].gameObject.SetActive(true);
                donationQuantity[2].gameObject.SetActive(false);
                donationQuantity[2].text = "0";
            }
        }
        else if (inventoryHolders[1].activeSelf)
        {
            Debug.Log("Two triggered");
            if (donationBoxes[0].transform.childCount > 0)
            {
                donationBoxes[0].transform.GetChild(0).SetParent(donationBoxes[1].transform);
                donationQuantity[1].text = donationQuantity[0].text;
                donationQuantity[1].gameObject.SetActive(true);
                donationQuantity[0].gameObject.SetActive(false);
                donationQuantity[0].text = "0";
            }
            else if (donationBoxes[2].transform.childCount > 0)
            {
                donationBoxes[2].transform.GetChild(0).SetParent(donationBoxes[1].transform);
                donationQuantity[1].text = donationQuantity[2].text;
                donationQuantity[1].gameObject.SetActive(true);
                donationQuantity[2].gameObject.SetActive(false);
                donationQuantity[2].text = "0";
            }
        }
        else if (inventoryHolders[2].activeSelf)
        {
            //Debug.Log("Three triggered");
            if (donationBoxes[0].transform.childCount > 0)
            {
                donationBoxes[0].transform.GetChild(0).SetParent(donationBoxes[2].transform);
                donationQuantity[2].text = donationQuantity[0].text;
                donationQuantity[2].gameObject.SetActive(true);
                donationQuantity[0].gameObject.SetActive(false);
                donationQuantity[0].text = "0";
            }
            else if (donationBoxes[1].transform.childCount > 0)
            {
                donationBoxes[1].transform.GetChild(0).SetParent(donationBoxes[2].transform);
                donationQuantity[2].text = donationQuantity[1].text;
                donationQuantity[2].gameObject.SetActive(true);
                donationQuantity[1].gameObject.SetActive(false);
                donationQuantity[1].text = "0";
            }
        }

    }

    public void continueJourneyFirstTap()
    {
        fishGivenInfoScreen.SetActive(true);
        popUpProgress.SetActive(false);
        tabScript.navBar.SetActive(false);
        //nextIslandButton.SetActive(false);
    }

    public void continueJourneySecondTap()
    {
        fishGivenInfoScreen.SetActive(false);
        gameProgress.completeIsland();
        tabScript.navBar.SetActive(true);
    }

    public void continueJourneyCancel()
    {
        fishGivenInfoScreen.SetActive(false);
        tabScript.navBar.SetActive(true);
        //nextIslandButton.SetActive(true);
    }

    public void boatClick()
    {
        int completed = 0;
        int total = 0;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int a = 0; a < 3; a++)
                {
                    if (islandManager.questCaps[gameProgress.currentIslandIndex][i][j][a] != -1)
                    {
                        total++;
                    }
                    if (islandManager.questTracking[gameProgress.currentIslandIndex][i][j][a] == 0)
                    {
                        completed++;
                    }
                }
            }
        }

        Debug.Log("I AM HERE!");

        questProgress.text = completed + "/" + total;

        if (completed == total)
        {
            greyOutButton.SetActive(false);
            lock1.SetActive(false);
            Debug.Log("Button is enabled!");
            normalButton.enabled = true;
        }
        else
        {
            greyOutButton.SetActive(true);
            lock1.SetActive(true);
            Debug.Log("Button is disabled!");
            normalButton.enabled = false;
        }

        slider.maxValue = total;

        slider.value = completed;

        questProgress.SetText(completed + "/" + total);


        if (total == completed)
        {
            fill.color = green;
        }
        else
        {
            fill.color = yellow;
        }

        popUpProgress.SetActive(true);
    }

    public void boatExit()
    {
        popUpProgress.SetActive(false);
    }

    /*
    KILLED
    public void subtractTime(int rarity = 0, int quantity = 0){
        int curI = gameProgress.currentIslandIndex + 1;
        switch(rarity){
            case 0:
                gameProgress.totalTravelTimes[curI] -= quantity * .5;
                break;
            case 1:
                gameProgress.totalTravelTimes[curI] -= quantity * 1;
                break;
            case 2:
                gameProgress.totalTravelTimes[curI] -= quantity * 2;
                break;
            case 3:
                gameProgress.totalTravelTimes[curI] -= quantity * 3;
                break;
            case 4:
                gameProgress.totalTravelTimes[curI] -= quantity * 4;
                break;
        }
        gameProgress.saveTotalTravelTimes();
    }
    KILLED
    */

    /*
    KILLED
    public void timeTester(){
        for (int i = 0; i < 500; i++){
            gameProgress.sailDistance();
        }
        gameProgress.checkIslandStatus();
    }
    */

}
