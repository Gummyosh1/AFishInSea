using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;
using TMPro;
using JetBrains.Annotations;
using System;

public class Merchant : MonoBehaviour
{
    public GameObject islandTracker;
    public NotEnoughScript notEnoughScript;
    public GameObject nextIslandButton;
    public TMP_Text playerGoldTotal;
    public TMP_Text itemPurchasedText;
    public TMP_Text ratMerchantName;
    public Image itemPurchasedSprite;
    public Image ratMerchantImage;
    public GameObject itemPurchasedPop;
    public GameObject merchantScreen;
    public GameObject navBar;
    public GameObject[] purchaseButton;
    public GameObject[] purchasedButton;
    public GameObject[] miniGoldCoins;

    public Image[] merchandiseImages; //0 IS THE MOST EXPENSIVE
    public TMP_Text[] goldAmounts;

    public Sprite[] RodPetMerchandise;
    public Sprite[] SailMerchandise;
    public Sprite[] DecorationMerchandise;
    public Sprite[] RatMerchants;
    [NonSerialized]
    public string[] RatMerchantNames =
    {"Borus", "Spike", "Gardenvoir", "Prisma", "White", "Dork", "Perry", "Duck", "Lucy",
    "Puffed", "Hop", "Wiz", "Jerry", "Jester", "Dinkle", "Beru"};


    public CharacterInit characterInit;
    public IslandScript islandScript;
    public GameProgress gameProgress;
    public GoldManager goldManager;
    public EquipmentScript equipmentScript;
    public PetWalk petWalk;
    public SailingTracker sailingTracker;
    public InventoryKing inventoryKing;
    public GoldBar goldBar;
    public SellLocks sellLocks;

    public int[][] costs = new int[16][];
    private int[] I1C = {700,300,100}; //1100
    private int[] I2C = {2000,800,400}; //3200
    private int[] I3C = {3200,1000,600}; //4800
    private int[] I4C = {4000,1500,900}; //6400
    private int[] I5C = {2000,800,400}; //3200
    private int[] I6C = {3400,1000,600}; //5000
    private int[] I7C = {5500,2000,500}; //8000
    private int[] I8C = {5500,4500,2000}; //12000
    private int[] I9C = {3000,1800,700}; //5500
    private int[] I10C = {5500,2250,750}; //8500
    private int[] I11C = {5500,4500,2500}; //12500
    private int[] I12C = {10000,4000,2000}; //16000
    private int[] I13C = {6000,2250,750}; //9000
    private int[] I14C = {10000,3750,1750}; //15500
    private int[] I15C = {12000,3500,2500}; //18000
    private int[] I16C = {15000,6400,4200}; //25600

    public void costsInit(){
        LoadCosts();

        costs[0] = I1C;
        costs[1] = I2C;
        costs[2] = I3C;
        costs[3] = I4C;
        costs[4] = I5C;
        costs[5] = I6C;
        costs[6] = I7C;
        costs[7] = I8C;
        costs[8] = I9C;
        costs[9] = I10C;
        costs[10] = I11C;
        costs[11] = I12C;
        costs[12] = I13C;
        costs[13] = I14C;
        costs[14] = I15C;
        costs[15] = I16C;
    }

    public void merchantEnter()
    {
        //nextIslandButton.SetActive(false);
        merchantScreen.SetActive(true);
        navBar.SetActive(false);
        islandTracker.SetActive(false);
        goldBar.goldInit();
        sellLocks.toggleLockedSlots();
        merchandiseImages[0].sprite = RodPetMerchandise[gameProgress.currentIslandIndex];
        merchandiseImages[1].sprite = SailMerchandise[gameProgress.currentIslandIndex];
        merchandiseImages[2].sprite = DecorationMerchandise[gameProgress.currentIslandIndex];

        ratMerchantImage.sprite = RatMerchants[gameProgress.currentIslandIndex];
        ratMerchantName.text = RatMerchantNames[gameProgress.currentIslandIndex] + " the Rat Merchant";

        int cost0 = costs[gameProgress.currentIslandIndex][0];
        Debug.Log(gameProgress.currentIslandIndex);
        Debug.Log("current cost of 0 is " + costs[gameProgress.currentIslandIndex][0]);
        int cost1 = costs[gameProgress.currentIslandIndex][1];
        int cost2 = costs[gameProgress.currentIslandIndex][2];

        goldAmounts[0].text = cost0.ToString();
        goldAmounts[1].text = cost1.ToString();
        goldAmounts[2].text = cost2.ToString();
        playerGoldTotal.text = goldManager.getGold().ToString();

        if (cost0 > 0)
        {
            purchaseButton[0].SetActive(true);
            goldAmounts[0].gameObject.SetActive(true);
            miniGoldCoins[0].SetActive(true);
            purchasedButton[0].SetActive(false);
        }
        else
        {
            purchasedButton[0].SetActive(true);
            goldAmounts[0].gameObject.SetActive(false);
            miniGoldCoins[0].SetActive(false);
            purchaseButton[0].SetActive(false);
        }

        if (cost1 > 0)
        {
            purchaseButton[1].SetActive(true);
            goldAmounts[1].gameObject.SetActive(true);
            miniGoldCoins[1].SetActive(true);
            purchasedButton[1].SetActive(false);
        }
        else
        {
            purchasedButton[1].SetActive(true);
            goldAmounts[1].gameObject.SetActive(false);
            miniGoldCoins[1].SetActive(false);
            purchaseButton[1].SetActive(false);
        }

        if (cost2 > 0)
        {
            purchaseButton[2].SetActive(true);
            goldAmounts[2].gameObject.SetActive(true);
            miniGoldCoins[2].SetActive(true);
            purchasedButton[2].SetActive(false);
        }
        else
        {
            purchasedButton[2].SetActive(true);
            goldAmounts[2].gameObject.SetActive(false);
            miniGoldCoins[2].SetActive(false);
            purchaseButton[2].SetActive(false);
        }
        
        int a = costs[gameProgress.currentIslandIndex][0];
        int b = costs[gameProgress.currentIslandIndex][1];
        int c = costs[gameProgress.currentIslandIndex][2];

        int curI = gameProgress.currentIslandIndex;

        inventoryKing.ClearThenLoadInventory();
        bool empty = inventoryKing.checkEmpty();

        /*if ((goldManager.PlayerGold >= a && a != 0) || (goldManager.PlayerGold >= b && b != 0) || (goldManager.PlayerGold >= c && c != 0))
        {
            if (a == 0 && b == 0 && c == 0)
            {
                gameProgress.exclamationPoints[curI].transform.GetChild(gameProgress.exclamationPoints[curI].transform.childCount - 1).gameObject.SetActive(false);
            }
            else
            {
                gameProgress.exclamationPoints[curI].transform.GetChild(gameProgress.exclamationPoints[curI].transform.childCount - 1).gameObject.SetActive(true);
            }
            //CHECKS TO SEE IF ENOUGH GOLD TO BUY ANYTHING, IF SO, SETS EXCLAMATION POINT TO ACTIVE OVER THE MERCHANT. IF NOT, SETS INACTIVE  
        }
        else { gameProgress.exclamationPoints[curI].transform.GetChild(gameProgress.exclamationPoints[curI].transform.childCount - 1).gameObject.SetActive(false); }*/
    }

    public void merchantExit(){
        merchantScreen.SetActive(false);
        navBar.SetActive(true);
        islandTracker.SetActive(true);
        inventoryKing.leaveSellScreen();

        int a = costs[gameProgress.currentIslandIndex][0];
        int b = costs[gameProgress.currentIslandIndex][1];
        int c = costs[gameProgress.currentIslandIndex][2];
        //if(a == 0 && b == 0 && c == 0){nextIslandButton.SetActive(true);}
    }


    public void buyRodPet(){
        if (gameProgress.currentIslandIndex == 4 || gameProgress.currentIslandIndex == 8 || gameProgress.currentIslandIndex == 12) {
            buyRod();
        }
        else if (gameProgress.currentIslandIndex == 0 || gameProgress.currentIslandIndex == 2 || gameProgress.currentIslandIndex == 6
                || gameProgress.currentIslandIndex == 10 || gameProgress.currentIslandIndex == 14) {
            buyCharacter();
        }
        else
        {
            buyPet();
        }
        playerGoldTotal.text = goldManager.getGold().ToString();
        
    }

    public void buySails(){
        int cost = costs[gameProgress.currentIslandIndex][1];
        if (goldManager.getGold() >= cost){
            goldManager.BuyItem(cost);
            sailingTracker.sailsOwned[gameProgress.currentIslandIndex] = 1;
            costs[gameProgress.currentIslandIndex][1] = 0;
            SaveCosts();
            sailingTracker.saveSailing();
            
            itemPurchasedPop.SetActive(true);
            itemPurchasedSprite.sprite = SailMerchandise[gameProgress.currentIslandIndex];
            itemPurchasedText.text = "Equip it on the Boat screen!";
        }
        else
        {
            notEnoughScript.ActivateNotEnough();
        }
        playerGoldTotal.text = goldManager.getGold().ToString();

        islandScript.questCheck(2,1);
        merchantEnter();
    }

    public void buyDecorations(){
        int curI = gameProgress.currentIslandIndex;
        int cost = costs[curI][2];
        if (goldManager.getGold() >= cost){
            goldManager.BuyItem(cost);
            costs[curI][2] = 0;
            SaveCosts();

            if (curI == 0 || curI == 4 || curI == 8 || curI == 12){
                equipmentScript.barrelsOwned[curI/4] = 1;
                equipmentScript.equippedBarrel = curI/4;
            }
            else if(curI == 1 || curI == 5 || curI == 9 || curI == 13){
                equipmentScript.tablesOwned[curI/4] = 1;
                equipmentScript.equippedTable = curI/4;
            }
            else if(curI == 2 || curI == 6 || curI == 10 || curI == 14){
                equipmentScript.chairsOwned[curI/4] = 1;
                equipmentScript.equippedChair = curI/4;
            }
            else if(curI == 3 || curI == 7 || curI == 11 || curI == 15){
                equipmentScript.gobletsOwned[curI/4] = 1;
                equipmentScript.equippedGoblet = curI/4;
            }
            equipmentScript.saveDecorations();
            
            itemPurchasedPop.SetActive(true);
            itemPurchasedSprite.sprite = DecorationMerchandise[gameProgress.currentIslandIndex];
            itemPurchasedText.text = "Equip it on the Boat screen!";


            islandScript.questCheck(2,2);
            merchantEnter();
        }
        else
        {
            notEnoughScript.ActivateNotEnough();
        }
        playerGoldTotal.text = goldManager.getGold().ToString();
    }


    private void buyRod()
    {
        int cost = costs[gameProgress.currentIslandIndex][0];
        if (goldManager.getGold() >= cost)
        {
            goldManager.BuyItem(cost);
            costs[gameProgress.currentIslandIndex][0] = 0;
            SaveCosts();

            switch (gameProgress.currentIslandIndex)
            {
                case 4:
                    equipmentScript.rodsOwned[1] = 1;
                    break;
                case 8:
                    equipmentScript.rodsOwned[2] = 1;
                    break;
                case 12:
                    equipmentScript.rodsOwned[3] = 1;
                    break;
            }
            equipmentScript.SaveRod();

            itemPurchasedPop.SetActive(true);
            itemPurchasedSprite.sprite = RodPetMerchandise[gameProgress.currentIslandIndex];
            itemPurchasedText.text = "Equip it in the equipment menu on the Fishing screen!";

            islandScript.questCheck(2, 0);
            merchantEnter();
        }
        else
        {
            notEnoughScript.ActivateNotEnough();
        }
    }

    private void buyCharacter()
    {
        int cost = costs[gameProgress.currentIslandIndex][0];
        if (goldManager.getGold() >= cost)
        {
            goldManager.BuyItem(cost);
            costs[gameProgress.currentIslandIndex][0] = 0;
            SaveCosts();
            switch (gameProgress.currentIslandIndex)
            {
                case 0:
                    characterInit.buyCharacter(0);
                    break;
                case 2:
                    characterInit.buyCharacter(1);
                    break;
                case 6:
                    characterInit.buyCharacter(2);
                    break;
                case 10:
                    characterInit.buyCharacter(3);
                    break;
                case 14:
                    characterInit.buyCharacter(4);
                    break;
            }

            equipmentScript.SaveRod();

            itemPurchasedPop.SetActive(true);
            itemPurchasedSprite.sprite = RodPetMerchandise[gameProgress.currentIslandIndex];
            itemPurchasedText.text = "Swap your character on the Tasks screen!";

            islandScript.questCheck(2, 0);
            merchantEnter();
        }
        else
        {
            notEnoughScript.ActivateNotEnough();
        }
    }

    private void buyPet()
    {
        int curI = gameProgress.currentIslandIndex;
        int cost = costs[curI][0];
        if (goldManager.getGold() >= cost)
        {
            goldManager.BuyItem(cost);
            costs[curI][0] = 0;
            SaveCosts();

            if (curI == 1)
            {
                petWalk.buyCatOne();
            }
            else if (curI == 3)
            {
                petWalk.buyPenguin();
            }
            else if (curI == 5)
            {
                petWalk.buyCatTwo();
            }
            else if (curI == 7)
            {
                petWalk.buyPurpleFox();
            }
            else if (curI == 9)
            {
                petWalk.buyFoxOne();
            }
            else if (curI == 11)
            {
                petWalk.buyLavaPenguin();
            }
            else if (curI == 13)
            {
                petWalk.buyCatThree();
            }
            else if (curI == 15)
            {
                petWalk.buyFoxTwo();
            }

            itemPurchasedPop.SetActive(true);
            itemPurchasedSprite.sprite = RodPetMerchandise[curI];
            itemPurchasedText.text = "Equip it in the equipment menu on the Fishing screen!";


            islandScript.questCheck(2, 0);
            merchantEnter();
        }
        else
        {
            notEnoughScript.ActivateNotEnough();
        }
    }

    public void SaveCosts(){
        costs costsStorage = new costs{
            I1C = costs[0],
            I2C = costs[1],
            I3C = costs[2],
            I4C = costs[3],
            I5C = costs[4],
            I6C = costs[5],
            I7C = costs[6],
            I8C = costs[7],
            I9C = costs[8],
            I10C = costs[9],
            I11C = costs[10],
            I12C = costs[11],
            I13C = costs[12],
            I14C = costs[13],
            I15C = costs[14],
            I16C = costs[15],
        };
        string jsonStorage = JsonUtility.ToJson(costsStorage);
        SaveSystem.SaveCosts(jsonStorage);
    }

    public void LoadCosts(){
        string saveString = SaveSystem.LoadCosts();
        if (saveString != null){
            costs loadedData = JsonUtility.FromJson<costs>(saveString);
            I1C = loadedData.I1C;
            I2C = loadedData.I2C;
            I3C = loadedData.I3C;
            I4C = loadedData.I4C;
            I5C = loadedData.I5C;
            I6C = loadedData.I6C;
            I7C = loadedData.I7C;
            I8C = loadedData.I8C;
            I9C = loadedData.I9C;
            I10C = loadedData.I10C;
            I11C = loadedData.I11C;
            I12C = loadedData.I12C;
            I13C = loadedData.I13C;
            I14C = loadedData.I14C;
            I15C = loadedData.I15C;
            I16C = loadedData.I16C;
        }
    }


}

public class costs
{
    public int[] I1C = new int[3];
    public int[] I2C = new int[3];
    public int[] I3C = new int[3];
    public int[] I4C = new int[3];
    public int[] I5C = new int[3];
    public int[] I6C = new int[3];
    public int[] I7C = new int[3];
    public int[] I8C = new int[3];
    public int[] I9C = new int[3];
    public int[] I10C = new int[3];
    public int[] I11C = new int[3];
    public int[] I12C = new int[3];
    public int[] I13C = new int[3];
    public int[] I14C = new int[3];
    public int[] I15C = new int[3];
    public int[] I16C = new int[3];

}
