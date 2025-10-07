using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class InventoryKing : MonoBehaviour
{
    private bool beingDragged = false;
    private GameObject currentInventorySlot;
    private bool left = false;
    private bool swapSlot = false;
    public GameObject[] fishPrefabList;
    public GameObject[] InventorySlots;
    public GameObject[] InventoryNumbers;

    public GameObject[] FreeChestSlots;
    public GameObject[] FreeChestNumbers;

    public GameObject[] PaidChestSlots;
    public GameObject[] PaidChestNumbers;

    public GameObject[] donateSlot;

    [NonSerialized]
    public bool loaded = false;
    [NonSerialized]
    public float pickTimer = 0;
    [NonSerialized]
    public bool waiting = false;
    [NonSerialized]
    public bool bugFix = false;

    //Standard Inventory Empty Slots
    private int[] rarityIndex1 = new int[5];
    private int[] rarityIndex2 = new int[5];
    private int[] rarityIndex3 = new int[5];
    private int[] rarityIndex4 = new int[5];
    private int[] rarityIndex5 = new int[5];
    private int[] rarityIndex6 = new int[5];
    private int[] rarityIndex7 = new int[5];
    private int[] rarityIndex8 = new int[5];
    private int[] rarityIndex9 = new int[5];
    private int[] rarityIndex10 = new int[5];
    private int[] rarityIndex11 = new int[5];
    private int[] rarityIndex12 = new int[5];
    //Inventory End

    //Free Chest Holders
    private int[] freeChestHold1 = new int[5];
    private int[] freeChestHold2 = new int[5];
    private int[] freeChestHold3 = new int[5];
    private int[] freeChestHold4 = new int[5];
    private int[] freeChestHold5 = new int[5];
    private int[] freeChestHold6 = new int[5];
    private int[] freeChestHold7 = new int[5];
    private int[] freeChestHold8 = new int[5];
    private int[] freeChestHold9 = new int[5];
    private int[] freeChestHold10 = new int[5];
    private int[] freeChestHold11 = new int[5];
    private int[] freeChestHold12 = new int[5];
    private int[] freeChestHold13 = new int[5];
    private int[] freeChestHold14 = new int[5];
    private int[] freeChestHold15 = new int[5];
    private int[] freeChestHold16 = new int[5];
    //Free Chest End

    //Paid Chest Holders
    private int[] paidChestHold1 = new int[5];
    private int[] paidChestHold2 = new int[5];
    private int[] paidChestHold3 = new int[5];
    private int[] paidChestHold4 = new int[5];
    private int[] paidChestHold5 = new int[5];
    private int[] paidChestHold6 = new int[5];
    private int[] paidChestHold7 = new int[5];
    private int[] paidChestHold8 = new int[5];
    private int[] paidChestHold9 = new int[5];
    private int[] paidChestHold10 = new int[5];
    private int[] paidChestHold11 = new int[5];
    private int[] paidChestHold12 = new int[5];
    private int[] paidChestHold13 = new int[5];
    private int[] paidChestHold14 = new int[5];
    private int[] paidChestHold15 = new int[5];
    private int[] paidChestHold16 = new int[5];
    //Paid Chest End

    public int[][] dataStorage = new int[12][];
    public int[][] freeChestStorage = new int[16][];
    public int[][] paidChestStorage = new int[16][];
    public GameProgress gameProgress;
    public BattlePass battlePass;

    public bool TheOne = false;


    public void Update()
    {
        if (waiting)
        {
            pickTimer += Time.deltaTime;
        }
    }

    public void nonSetupLoad()
    {
        string saveString = SaveSystem.LoadInventory();
        if (saveString != null)
        {
            InventorySaveSlots loadedData = JsonUtility.FromJson<InventorySaveSlots>(saveString);
            dataStorage[0] = loadedData.InventorySlot1;
            dataStorage[1] = loadedData.InventorySlot2;
            dataStorage[2] = loadedData.InventorySlot3;
            dataStorage[3] = loadedData.InventorySlot4;
            dataStorage[4] = loadedData.InventorySlot5;
            dataStorage[5] = loadedData.InventorySlot6;
            dataStorage[6] = loadedData.InventorySlot7;
            dataStorage[7] = loadedData.InventorySlot8;
            dataStorage[8] = loadedData.InventorySlot9;
            dataStorage[9] = loadedData.InventorySlot10;
            dataStorage[10] = loadedData.InventorySlot11;
            dataStorage[11] = loadedData.InventorySlot12;
        }
        else
        {
            dataStorage[0] = rarityIndex1;
            dataStorage[1] = rarityIndex2;
            dataStorage[2] = rarityIndex3;
            dataStorage[3] = rarityIndex4;
            dataStorage[4] = rarityIndex5;
            dataStorage[5] = rarityIndex6;
            dataStorage[6] = rarityIndex7;
            dataStorage[7] = rarityIndex8;
            dataStorage[8] = rarityIndex9;
            dataStorage[9] = rarityIndex10;
            dataStorage[10] = rarityIndex11;
            dataStorage[11] = rarityIndex12;
        }
    }

    public void LoadInventory()
    {
        string saveString = SaveSystem.LoadInventory();
        if (saveString != null)
        {
            InventorySaveSlots loadedData = JsonUtility.FromJson<InventorySaveSlots>(saveString);
            dataStorage[0] = loadedData.InventorySlot1;
            dataStorage[1] = loadedData.InventorySlot2;
            dataStorage[2] = loadedData.InventorySlot3;
            dataStorage[3] = loadedData.InventorySlot4;
            dataStorage[4] = loadedData.InventorySlot5;
            dataStorage[5] = loadedData.InventorySlot6;
            dataStorage[6] = loadedData.InventorySlot7;
            dataStorage[7] = loadedData.InventorySlot8;
            dataStorage[8] = loadedData.InventorySlot9;
            dataStorage[9] = loadedData.InventorySlot10;
            dataStorage[10] = loadedData.InventorySlot11;
            dataStorage[11] = loadedData.InventorySlot12;

            for (int i = 0; i < dataStorage.Length; i++)
            {
                if (dataStorage[i] != null)
                {
                    invenItemSetup(dataStorage[i][0], dataStorage[i][1], dataStorage[i][2], i, dataStorage[i][3], dataStorage[i][4]);
                }
            }
        }
        else
        {
            dataStorage[0] = rarityIndex1;
            dataStorage[1] = rarityIndex2;
            dataStorage[2] = rarityIndex3;
            dataStorage[3] = rarityIndex4;
            dataStorage[4] = rarityIndex5;
            dataStorage[5] = rarityIndex6;
            dataStorage[6] = rarityIndex7;
            dataStorage[7] = rarityIndex8;
            dataStorage[8] = rarityIndex9;
            dataStorage[9] = rarityIndex10;
            dataStorage[10] = rarityIndex11;
            dataStorage[11] = rarityIndex12;
        }
    }

    public void LoadMerchantInventory(){
        if (!loaded){
            string saveString = SaveSystem.LoadInventory();
            if (saveString != null){
                InventorySaveSlots loadedData = JsonUtility.FromJson<InventorySaveSlots>(saveString);
                dataStorage[0] = loadedData.InventorySlot1;
                dataStorage[1] = loadedData.InventorySlot2;
                dataStorage[2] = loadedData.InventorySlot3;
                dataStorage[3] = loadedData.InventorySlot4;
                dataStorage[4] = loadedData.InventorySlot5;
                dataStorage[5] = loadedData.InventorySlot6;
                dataStorage[6] = loadedData.InventorySlot7;
                dataStorage[7] = loadedData.InventorySlot8;
                dataStorage[8] = loadedData.InventorySlot9;
                dataStorage[9] = loadedData.InventorySlot10;
                dataStorage[10] = loadedData.InventorySlot11;
                dataStorage[11] = loadedData.InventorySlot12;

                for (int i = 0; i < dataStorage.Length; i++){
                    if (dataStorage[i] != null){
                        invenItemSetup(dataStorage[i][0], dataStorage[i][1], dataStorage[i][2], i, dataStorage[i][3], dataStorage[i][4]);
                    }
                }
            }
            else{
                dataStorage[0] = rarityIndex1;
                dataStorage[1] = rarityIndex2;
                dataStorage[2] = rarityIndex3;
                dataStorage[3] = rarityIndex4;
                dataStorage[4] = rarityIndex5;
                dataStorage[5] = rarityIndex6;
                dataStorage[6] = rarityIndex7;
                dataStorage[7] = rarityIndex8;
                dataStorage[8] = rarityIndex9;
                dataStorage[9] = rarityIndex10;
                dataStorage[10] = rarityIndex11;
                dataStorage[11] = rarityIndex12;
            }
            loaded = true;
        }
        
    }

    public void ClearThenLoadInventory(){
        wipeInventory();
        string saveString = SaveSystem.LoadInventory();
        if (saveString != null){
            InventorySaveSlots loadedData = JsonUtility.FromJson<InventorySaveSlots>(saveString);
            dataStorage[0] = loadedData.InventorySlot1;
            dataStorage[1] = loadedData.InventorySlot2;
            dataStorage[2] = loadedData.InventorySlot3;
            dataStorage[3] = loadedData.InventorySlot4;
            dataStorage[4] = loadedData.InventorySlot5;
            dataStorage[5] = loadedData.InventorySlot6;
            dataStorage[6] = loadedData.InventorySlot7;
            dataStorage[7] = loadedData.InventorySlot8;
            dataStorage[8] = loadedData.InventorySlot9;
            dataStorage[9] = loadedData.InventorySlot10;
            dataStorage[10] = loadedData.InventorySlot11;
            dataStorage[11] = loadedData.InventorySlot12;

            for (int i = 0; i < dataStorage.Length; i++){
                if (dataStorage[i] != null){
                    invenItemSetup(dataStorage[i][0], dataStorage[i][1], dataStorage[i][2], i, dataStorage[i][3], dataStorage[i][4]);
                }
            }
        }
        else{
            dataStorage[0] = rarityIndex1;
            dataStorage[1] = rarityIndex2;
            dataStorage[2] = rarityIndex3;
            dataStorage[3] = rarityIndex4;
            dataStorage[4] = rarityIndex5;
            dataStorage[5] = rarityIndex6;
            dataStorage[6] = rarityIndex7;
            dataStorage[7] = rarityIndex8;
            dataStorage[8] = rarityIndex9;
            dataStorage[9] = rarityIndex10;
            dataStorage[10] = rarityIndex11;
            dataStorage[11] = rarityIndex12;
        }
    }

    /*public void ClearThenLoadFreeChest(){
        wipeChestOne();
        string saveString = SaveSystem.LoadFreeChest();
        if (saveString != null){
            ChestOneSaveSlots loadedData = JsonUtility.FromJson<ChestOneSaveSlots>(saveString);
            freeChestStorage[0] = loadedData.ChestSlot1;
            freeChestStorage[1] = loadedData.ChestSlot2;
            freeChestStorage[2] = loadedData.ChestSlot3;
            freeChestStorage[3] = loadedData.ChestSlot4;
            freeChestStorage[4] = loadedData.ChestSlot5;
            freeChestStorage[5] = loadedData.ChestSlot6;
            freeChestStorage[6] = loadedData.ChestSlot7;
            freeChestStorage[7] = loadedData.ChestSlot8;
            freeChestStorage[8] = loadedData.ChestSlot9;
            freeChestStorage[9] = loadedData.ChestSlot10;
            freeChestStorage[10] = loadedData.ChestSlot11;
            freeChestStorage[11] = loadedData.ChestSlot12;
            freeChestStorage[12] = loadedData.ChestSlot13;
            freeChestStorage[13] = loadedData.ChestSlot14;
            freeChestStorage[14] = loadedData.ChestSlot15;
            freeChestStorage[15] = loadedData.ChestSlot16;

            for (int i = 0; i < freeChestStorage.Length; i++){
                if (freeChestStorage[i] != null){
                    chestItemSetup(freeChestStorage[i][0], freeChestStorage[i][1], freeChestStorage[i][2], i, freeChestStorage[i][3], freeChestStorage[i][4]);
                }
            }
        }
        else{
            freeChestStorage[0] = freeChestHold1;
            freeChestStorage[1] = freeChestHold2;
            freeChestStorage[2] = freeChestHold3;
            freeChestStorage[3] = freeChestHold4;
            freeChestStorage[4] = freeChestHold5;
            freeChestStorage[5] = freeChestHold6;
            freeChestStorage[6] = freeChestHold7;
            freeChestStorage[7] = freeChestHold8;
            freeChestStorage[8] = freeChestHold9;
            freeChestStorage[9] = freeChestHold10;
            freeChestStorage[10] = freeChestHold11;
            freeChestStorage[11] = freeChestHold12;
            freeChestStorage[12] = freeChestHold13;
            freeChestStorage[13] = freeChestHold14;
            freeChestStorage[14] = freeChestHold15;
            freeChestStorage[15] = freeChestHold16;
        }
    }

    public void ClearThenLoadPaidChest(){
        wipeChestTwo();
        string saveString = SaveSystem.LoadPaidChest();
        if (saveString != null){
            ChestTwoSaveSlots loadedData = JsonUtility.FromJson<ChestTwoSaveSlots>(saveString);
            paidChestStorage[0] = loadedData.ChestSlot1;
            paidChestStorage[1] = loadedData.ChestSlot2;
            paidChestStorage[2] = loadedData.ChestSlot3;
            paidChestStorage[3] = loadedData.ChestSlot4;
            paidChestStorage[4] = loadedData.ChestSlot5;
            paidChestStorage[5] = loadedData.ChestSlot6;
            paidChestStorage[6] = loadedData.ChestSlot7;
            paidChestStorage[7] = loadedData.ChestSlot8;
            paidChestStorage[8] = loadedData.ChestSlot9;
            paidChestStorage[9] = loadedData.ChestSlot10;
            paidChestStorage[10] = loadedData.ChestSlot11;
            paidChestStorage[11] = loadedData.ChestSlot12;
            paidChestStorage[12] = loadedData.ChestSlot13;
            paidChestStorage[13] = loadedData.ChestSlot14;
            paidChestStorage[14] = loadedData.ChestSlot15;
            paidChestStorage[15] = loadedData.ChestSlot16;

            for (int i = 0; i < paidChestStorage.Length; i++){
                if (paidChestStorage[i] != null){
                    paidChestItemSetup(paidChestStorage[i][0], paidChestStorage[i][1], paidChestStorage[i][2], i, paidChestStorage[i][3], paidChestStorage[i][4]);
                }
            }
        }
        else{
            paidChestStorage[0] = paidChestHold1;
            paidChestStorage[1] = paidChestHold2;
            paidChestStorage[2] = paidChestHold3;
            paidChestStorage[3] = paidChestHold4;
            paidChestStorage[4] = paidChestHold5;
            paidChestStorage[5] = paidChestHold6;
            paidChestStorage[6] = paidChestHold7;
            paidChestStorage[7] = paidChestHold8;
            paidChestStorage[8] = paidChestHold9;
            paidChestStorage[9] = paidChestHold10;
            paidChestStorage[10] = paidChestHold11;
            paidChestStorage[11] = paidChestHold12;
            paidChestStorage[12] = paidChestHold13;
            paidChestStorage[13] = paidChestHold14;
            paidChestStorage[14] = paidChestHold15;
            paidChestStorage[15] = paidChestHold16;
        }
    }*/



    public void SaveInventory(){
        
        dataStorage[0] = rarityIndex1;
        dataStorage[1] = rarityIndex2;
        dataStorage[2] = rarityIndex3;
        dataStorage[3] = rarityIndex4;
        dataStorage[4] = rarityIndex5;
        dataStorage[5] = rarityIndex6;
        dataStorage[6] = rarityIndex7;
        dataStorage[7] = rarityIndex8;
        dataStorage[8] = rarityIndex9;
        dataStorage[9] = rarityIndex10;
        dataStorage[10] = rarityIndex11;
        dataStorage[11] = rarityIndex12;
        
        for (int i = 0; i < dataStorage.Length; i++){
            GameObject holder = InventorySlots[i];
            for (int j = 0; j < 5; j++){
                switch (j){
                    case 0:
                        if (holder.transform.childCount == 0){
                            dataStorage[i][j] = 0;
                        }
                        else{
                            dataStorage[i][j] = 1;
                        }
                        break;
                    case 1:
                        if (dataStorage[i][0] == 0){break;}
                        if (holder.transform.GetChild(0).name.Contains("Normal")){
                            dataStorage[i][j] = 0;
                        }
                        else if (holder.transform.GetChild(0).name.Contains("Fancy")){
                            dataStorage[i][j] = 1;
                        }
                        else if (holder.transform.GetChild(0).name.Contains("Extravagant")){
                            dataStorage[i][j] = 2;
                        }
                        else if (holder.transform.GetChild(0).name.Contains("Pristine")){
                            dataStorage[i][j] = 3;
                        }
                        else if (holder.transform.GetChild(0).name.Contains("Magical")){
                            dataStorage[i][j] = 4;
                        }
                        break;
                    case 2:
                        if (dataStorage[i][0] == 0){break;}
                        if (dataStorage[i][1] == 0){
                            dataStorage[i][j] = holder.transform.GetChild(0).GetComponent<NormalFish>().spriteIndex;
                        }
                        if (dataStorage[i][1] == 1){
                            dataStorage[i][j] = holder.transform.GetChild(0).GetComponent<FancyFish>().spriteIndex;
                        }
                        if (dataStorage[i][1] == 2){
                            dataStorage[i][j] = holder.transform.GetChild(0).GetComponent<ExtravagantFish>().spriteIndex;
                        }
                        if (dataStorage[i][1] == 3){
                            dataStorage[i][j] = holder.transform.GetChild(0).GetComponent<PristineFish>().spriteIndex;
                        }
                        if (dataStorage[i][1] == 4){
                            dataStorage[i][j] = holder.transform.GetChild(0).GetComponent<MagicalFishPrefab>().spriteIndex;
                        }
                        break;
                    case 3:
                        if (InventoryNumbers[i].GetComponent<TMP_Text>().text != "0"){
                            dataStorage[i][j] = int.Parse(InventoryNumbers[i].GetComponent<TMP_Text>().text);
                        }
                        else {dataStorage[i][j] = 0; dataStorage[i][0] = 0;}
                        break;
                    case 4:
                        if (dataStorage[i][0] == 0){break;}
                        if (dataStorage[i][1] == 0){
                            dataStorage[i][j] = holder.transform.GetChild(0).GetComponent<NormalFish>().oceanCaughtInIndex;
                        }
                        if (dataStorage[i][1] == 1){
                            dataStorage[i][j] = holder.transform.GetChild(0).GetComponent<FancyFish>().oceanCaughtInIndex;
                        }
                        if (dataStorage[i][1] == 2){
                            dataStorage[i][j] = holder.transform.GetChild(0).GetComponent<ExtravagantFish>().oceanCaughtInIndex;
                        }
                        if (dataStorage[i][1] == 3){
                            dataStorage[i][j] = holder.transform.GetChild(0).GetComponent<PristineFish>().oceanCaughtInIndex;
                        }
                        if (dataStorage[i][1] == 4){
                            dataStorage[i][j] = 4;
                        }
                        break;
                }
            }
        }
        InventorySaveSlots inventoryStorage = new InventorySaveSlots{
            InventorySlot1 = dataStorage[0],
            InventorySlot2 = dataStorage[1],
            InventorySlot3 = dataStorage[2],
            InventorySlot4 = dataStorage[3],
            InventorySlot5 = dataStorage[4],
            InventorySlot6 = dataStorage[5],
            InventorySlot7 = dataStorage[6],
            InventorySlot8 = dataStorage[7],
            InventorySlot9 = dataStorage[8],
            InventorySlot10 = dataStorage[9],
            InventorySlot11 = dataStorage[10],
            InventorySlot12 = dataStorage[11],
        };

        string jsonStorage = JsonUtility.ToJson(inventoryStorage);
        SaveSystem.SaveInventory(jsonStorage);
    }

    /*public void SaveFreeChest(){
        
        freeChestStorage[0] = freeChestHold1;
        freeChestStorage[1] = freeChestHold2;
        freeChestStorage[2] = freeChestHold3;
        freeChestStorage[3] = freeChestHold4;
        freeChestStorage[4] = freeChestHold5;
        freeChestStorage[5] = freeChestHold6;
        freeChestStorage[6] = freeChestHold7;
        freeChestStorage[7] = freeChestHold8;
        freeChestStorage[8] = freeChestHold9;
        freeChestStorage[9] = freeChestHold10;
        freeChestStorage[10] = freeChestHold11;
        freeChestStorage[11] = freeChestHold12;
        freeChestStorage[12] = freeChestHold13;
        freeChestStorage[13] = freeChestHold14;
        freeChestStorage[14] = freeChestHold15;
        freeChestStorage[15] = freeChestHold16;
        
        
        for (int i = 0; i < freeChestStorage.Length; i++){
            GameObject holder = FreeChestSlots[i];
            for (int j = 0; j < 5; j++){
                switch (j){
                    case 0:
                        if (holder.transform.childCount == 0){
                            freeChestStorage[i][j] = 0;
                        }
                        else{
                            freeChestStorage[i][j] = 1;
                        }
                        break;
                    case 1:
                        if (freeChestStorage[i][0] == 0){break;}
                        if (holder.transform.GetChild(0).name.Contains("Normal")){
                            freeChestStorage[i][j] = 0;
                        }
                        else if (holder.transform.GetChild(0).name.Contains("Fancy")){
                            freeChestStorage[i][j] = 1;
                        }
                        else if (holder.transform.GetChild(0).name.Contains("Extravagant")){
                            freeChestStorage[i][j] = 2;
                        }
                        else if (holder.transform.GetChild(0).name.Contains("Pristine")){
                            freeChestStorage[i][j] = 3;
                        }
                        break;
                    case 2:
                        if (freeChestStorage[i][0] == 0){break;}
                        if (freeChestStorage[i][1] == 0){
                            freeChestStorage[i][j] = holder.transform.GetChild(0).GetComponent<NormalFish>().spriteIndex;
                        }
                        if (freeChestStorage[i][1] == 1){
                            freeChestStorage[i][j] = holder.transform.GetChild(0).GetComponent<FancyFish>().spriteIndex;
                        }
                        if (freeChestStorage[i][1] == 2){
                            freeChestStorage[i][j] = holder.transform.GetChild(0).GetComponent<ExtravagantFish>().spriteIndex;
                        }
                        if (freeChestStorage[i][1] == 3){
                            freeChestStorage[i][j] = holder.transform.GetChild(0).GetComponent<PristineFish>().spriteIndex;
                        }
                        break;
                    case 3:
                        if (FreeChestNumbers[i].GetComponent<TMP_Text>().text != "0"){
                            freeChestStorage[i][j] = int.Parse(FreeChestNumbers[i].GetComponent<TMP_Text>().text);
                        }
                        else {freeChestStorage[i][j] = 0;}
                        break;
                    case 4:
                        if (freeChestStorage[i][0] == 0){break;}
                        if (freeChestStorage[i][1] == 0){
                            freeChestStorage[i][j] = holder.transform.GetChild(0).GetComponent<NormalFish>().oceanCaughtInIndex;
                        }
                        if (freeChestStorage[i][1] == 1){
                            freeChestStorage[i][j] = holder.transform.GetChild(0).GetComponent<FancyFish>().oceanCaughtInIndex;
                        }
                        if (freeChestStorage[i][1] == 2){
                            freeChestStorage[i][j] = holder.transform.GetChild(0).GetComponent<ExtravagantFish>().oceanCaughtInIndex;
                        }
                        if (freeChestStorage[i][1] == 3){
                            freeChestStorage[i][j] = holder.transform.GetChild(0).GetComponent<PristineFish>().oceanCaughtInIndex;
                        }
                        break;
                }
            }
        }
        ChestOneSaveSlots ChestOneStorage = new ChestOneSaveSlots{
            ChestSlot1 = freeChestStorage[0],
            ChestSlot2 = freeChestStorage[1],
            ChestSlot3 = freeChestStorage[2],
            ChestSlot4 = freeChestStorage[3],
            ChestSlot5 = freeChestStorage[4],
            ChestSlot6 = freeChestStorage[5],
            ChestSlot7 = freeChestStorage[6],
            ChestSlot8 = freeChestStorage[7],
            ChestSlot9 = freeChestStorage[8],
            ChestSlot10 = freeChestStorage[9],
            ChestSlot11 = freeChestStorage[10],
            ChestSlot12 = freeChestStorage[11],
            ChestSlot13 = freeChestStorage[12],
            ChestSlot14 = freeChestStorage[13],
            ChestSlot15 = freeChestStorage[14],
            ChestSlot16 = freeChestStorage[15],
        };

        string jsonStorage = JsonUtility.ToJson(ChestOneStorage);
        SaveSystem.SaveFreeChest(jsonStorage);
    }


    public void SavePaidChest(){
        
        paidChestStorage[0] = paidChestHold1;
        paidChestStorage[1] = paidChestHold2;
        paidChestStorage[2] = paidChestHold3;
        paidChestStorage[3] = paidChestHold4;
        paidChestStorage[4] = paidChestHold5;
        paidChestStorage[5] = paidChestHold6;
        paidChestStorage[6] = paidChestHold7;
        paidChestStorage[7] = paidChestHold8;
        paidChestStorage[8] = paidChestHold9;
        paidChestStorage[9] = paidChestHold10;
        paidChestStorage[10] = paidChestHold11;
        paidChestStorage[11] = paidChestHold12;
        paidChestStorage[12] = paidChestHold13;
        paidChestStorage[13] = paidChestHold14;
        paidChestStorage[14] = paidChestHold15;
        paidChestStorage[15] = paidChestHold16;
        
        
        for (int i = 0; i < paidChestStorage.Length; i++){
            GameObject holder = PaidChestSlots[i];
            for (int j = 0; j < 5; j++){
                switch (j){
                    case 0:
                        if (holder.transform.childCount == 0){
                            paidChestStorage[i][j] = 0;
                        }
                        else{
                            paidChestStorage[i][j] = 1;
                        }
                        break;
                    case 1:
                        if (paidChestStorage[i][0] == 0){break;}
                        if (holder.transform.GetChild(0).name.Contains("Normal")){
                            paidChestStorage[i][j] = 0;
                        }
                        else if (holder.transform.GetChild(0).name.Contains("Fancy")){
                            paidChestStorage[i][j] = 1;
                        }
                        else if (holder.transform.GetChild(0).name.Contains("Extravagant")){
                            paidChestStorage[i][j] = 2;
                        }
                        else if (holder.transform.GetChild(0).name.Contains("Pristine")){
                            paidChestStorage[i][j] = 3;
                        }
                        break;
                    case 2:
                        if (paidChestStorage[i][0] == 0){break;}
                        if (paidChestStorage[i][1] == 0){
                            paidChestStorage[i][j] = holder.transform.GetChild(0).GetComponent<NormalFish>().spriteIndex;
                        }
                        if (paidChestStorage[i][1] == 1){
                            paidChestStorage[i][j] = holder.transform.GetChild(0).GetComponent<FancyFish>().spriteIndex;
                        }
                        if (paidChestStorage[i][1] == 2){
                            paidChestStorage[i][j] = holder.transform.GetChild(0).GetComponent<ExtravagantFish>().spriteIndex;
                        }
                        if (paidChestStorage[i][1] == 3){
                            paidChestStorage[i][j] = holder.transform.GetChild(0).GetComponent<PristineFish>().spriteIndex;
                        }
                        break;
                    case 3:
                        if (PaidChestNumbers[i].GetComponent<TMP_Text>().text != "0"){
                            paidChestStorage[i][j] = int.Parse(PaidChestNumbers[i].GetComponent<TMP_Text>().text);
                        }
                        else {paidChestStorage[i][j] = 0;}
                        break;
                    case 4:
                        if (paidChestStorage[i][0] == 0){break;}
                        if (paidChestStorage[i][1] == 0){
                            paidChestStorage[i][j] = holder.transform.GetChild(0).GetComponent<NormalFish>().oceanCaughtInIndex;
                        }
                        if (paidChestStorage[i][1] == 1){
                            paidChestStorage[i][j] = holder.transform.GetChild(0).GetComponent<FancyFish>().oceanCaughtInIndex;
                        }
                        if (paidChestStorage[i][1] == 2){
                            paidChestStorage[i][j] = holder.transform.GetChild(0).GetComponent<ExtravagantFish>().oceanCaughtInIndex;
                        }
                        if (paidChestStorage[i][1] == 3){
                            paidChestStorage[i][j] = holder.transform.GetChild(0).GetComponent<PristineFish>().oceanCaughtInIndex;
                        }
                        break;
                }
            }
        }
        ChestTwoSaveSlots ChestTwoStorage = new ChestTwoSaveSlots{
            ChestSlot1 = paidChestStorage[0],
            ChestSlot2 = paidChestStorage[1],
            ChestSlot3 = paidChestStorage[2],
            ChestSlot4 = paidChestStorage[3],
            ChestSlot5 = paidChestStorage[4],
            ChestSlot6 = paidChestStorage[5],
            ChestSlot7 = paidChestStorage[6],
            ChestSlot8 = paidChestStorage[7],
            ChestSlot9 = paidChestStorage[8],
            ChestSlot10 = paidChestStorage[9],
            ChestSlot11 = paidChestStorage[10],
            ChestSlot12 = paidChestStorage[11],
            ChestSlot13 = paidChestStorage[12],
            ChestSlot14 = paidChestStorage[13],
            ChestSlot15 = paidChestStorage[14],
            ChestSlot16 = paidChestStorage[15],
        };

        string jsonStorage = JsonUtility.ToJson(ChestTwoStorage);
        SaveSystem.SavePaidChest(jsonStorage);
    }*/

    public bool getDrag(){
        return beingDragged;
    }

    public void startDrag(){
        beingDragged = true;
    }

    public void endDrag(){
        beingDragged = false;
    }
    
    public void inventorySlotTag(GameObject apple){
        currentInventorySlot = apple;
    }

    public GameObject getNewInventorySlot(){
        return currentInventorySlot;
    }

    public void leftSquare(){
        Debug.Log("We're inside the King!");
        left = false;
    }

    public bool getBattlePass(){
        return battlePass.passPurchased;
    }

    public void enteredSquare(){
        
        left = true;
    }

    public bool getSquare(){
        return left;
    }

    public bool getSwap(){
        return swapSlot;
    }

    public void setSwapTrue(){
        swapSlot = true;
    }

    public void setSwapFalse(){
        swapSlot = false;
    }

    public void catchFish(int rarity, int indexOfFish, int amnt){
        ClearThenLoadInventory();
        int a = -1;
        foreach (GameObject i in InventorySlots){
            a++;
            Sprite sprite_holder = null;
            switch(rarity){
                    case 0:
                        fishPrefabList[0].GetComponent<NormalFish>().InitCall();
                        sprite_holder = fishPrefabList[0].GetComponent<NormalFish>().normalFishSprites[gameProgress.currentOceanIndex][indexOfFish];
                        break;
                    case 1: 
                        fishPrefabList[1].GetComponent<FancyFish>().InitCall();
                        sprite_holder = fishPrefabList[1].GetComponent<FancyFish>().fancyFishSprites[gameProgress.currentOceanIndex][indexOfFish];
                        break;
                    case 2:
                        fishPrefabList[2].GetComponent<ExtravagantFish>().InitCall();
                        sprite_holder = fishPrefabList[2].GetComponent<ExtravagantFish>().extravagantFishSprites[gameProgress.currentOceanIndex][indexOfFish];
                        break;
                    case 3:
                        fishPrefabList[3].GetComponent<PristineFish>().InitCall();
                        sprite_holder = fishPrefabList[3].GetComponent<PristineFish>().pristineFishSprites[gameProgress.currentOceanIndex][indexOfFish];
                        break;
                    case 4:
                        fishPrefabList[4].GetComponent<MagicalFishPrefab>().InitCall();
                        sprite_holder = fishPrefabList[4].GetComponent<MagicalFishPrefab>().eliteEightSprites[indexOfFish];
                        break;
            }
            
            if (i.transform.childCount > 0 && i.transform.GetChild(0).gameObject.GetComponent<Image>().sprite == sprite_holder && int.Parse(InventoryNumbers[i.transform.GetSiblingIndex() - 1].GetComponent<TMP_Text>().text) < 9){
                if (int.Parse(InventoryNumbers[a].GetComponent<TMP_Text>().text) + amnt > 9){
                    amnt = int.Parse(InventoryNumbers[a].GetComponent<TMP_Text>().text) + amnt - 9;
                    InventoryNumbers[a].GetComponent<TMP_Text>().text = "9";
                }
                else{
                    InventoryNumbers[a].GetComponent<TMP_Text>().text = "" + (int.Parse(InventoryNumbers[a].GetComponent<TMP_Text>().text) + amnt);
                    break;
                }
                
                Debug.Log("FISH ADDED QUANTITY TO A STACK");
                
            }

            else if (i.transform.childCount == 0){
                GameObject newFish = Instantiate(fishPrefabList[rarity], i.transform, false);
                switch(rarity){
                    case 0:
                        newFish.GetComponent<Image>().sprite = sprite_holder;
                        newFish.GetComponent<NormalFish>().spriteIndex = indexOfFish;
                        newFish.GetComponent<NormalFish>().oceanCaughtInIndex = gameProgress.currentOceanIndex;
                        break;
                    case 1: 
                        newFish.GetComponent<Image>().sprite = sprite_holder;
                        newFish.GetComponent<FancyFish>().spriteIndex = indexOfFish;
                        newFish.GetComponent<FancyFish>().oceanCaughtInIndex = gameProgress.currentOceanIndex;
                        break;
                    case 2:
                        newFish.GetComponent<Image>().sprite = sprite_holder;
                        newFish.GetComponent<ExtravagantFish>().spriteIndex = indexOfFish;
                        newFish.GetComponent<ExtravagantFish>().oceanCaughtInIndex = gameProgress.currentOceanIndex;
                        break;
                    case 3:
                        newFish.GetComponent<Image>().sprite = sprite_holder;
                        newFish.GetComponent<PristineFish>().spriteIndex = indexOfFish;
                        newFish.GetComponent<PristineFish>().oceanCaughtInIndex = gameProgress.currentOceanIndex;
                        break;
                    case 4:
                        newFish.GetComponent<Image>().sprite = sprite_holder;
                        newFish.GetComponent<MagicalFishPrefab>().spriteIndex = indexOfFish;
                        break;
                }
                Debug.Log("NEW FISH MADE");
                InventoryNumbers[a].GetComponent<TMP_Text>().text = amnt.ToString();
                InventoryNumbers[a].SetActive(true);
                break;
            }
            if (a >= 3 && !battlePass.passPurchased){
                break;
            }
            else if (a >= 11 && battlePass.passPurchased){
                break;
            }
        }
        SaveInventory();
        
    }

    public bool checkFull(){
        int a = -1;
        foreach (GameObject i in InventorySlots){
            a++;
            if (i.transform.childCount == 0){
                return false;
            }
            if (a >= 3 && !battlePass.passPurchased){
                break;
            }
            else if (a >= 11 && battlePass.passPurchased){
                break;
            }
        }
        
        return true;
    }
    
    public bool checkEmpty(){
        int a = -1;
        foreach (GameObject i in InventorySlots){
            a++;
            if (i.transform.childCount != 0){
                return false;
            }
            if (a >= 3 && !battlePass.passPurchased){
                break;
            }
            else if (a >= 11 && battlePass.passPurchased){
                break;
            }
        }
        
        return true;
    }

    public void invenItemSetup(int fish, int rarity, int indexOfFish, int inventorySlotNumber, int quantityOfFish, int oceanCaught)
    {
        bool exists = false;
        if (fish == 1) { exists = true; }

        if (exists)
        {
            GameObject i = InventorySlots[inventorySlotNumber];
            GameObject newFish = Instantiate(fishPrefabList[rarity], i.transform, false);

            InventoryNumbers[inventorySlotNumber].SetActive(true);
            InventoryNumbers[inventorySlotNumber].GetComponent<TMP_Text>().text = "" + quantityOfFish;

            switch (rarity)
            {
                case 0:
                    newFish.GetComponent<NormalFish>().InitCall();
                    newFish.GetComponent<Image>().sprite = newFish.GetComponent<NormalFish>().normalFishSprites[oceanCaught][indexOfFish];
                    newFish.GetComponent<NormalFish>().spriteIndex = indexOfFish;
                    newFish.GetComponent<NormalFish>().oceanCaughtInIndex = oceanCaught;
                    break;
                case 1:
                    newFish.GetComponent<FancyFish>().InitCall();
                    newFish.GetComponent<Image>().sprite = newFish.GetComponent<FancyFish>().fancyFishSprites[oceanCaught][indexOfFish];
                    newFish.GetComponent<FancyFish>().spriteIndex = indexOfFish;
                    newFish.GetComponent<FancyFish>().oceanCaughtInIndex = oceanCaught;
                    break;
                case 2:
                    newFish.GetComponent<ExtravagantFish>().InitCall();
                    newFish.GetComponent<Image>().sprite = newFish.GetComponent<ExtravagantFish>().extravagantFishSprites[oceanCaught][indexOfFish];
                    newFish.GetComponent<ExtravagantFish>().spriteIndex = indexOfFish;
                    newFish.GetComponent<ExtravagantFish>().oceanCaughtInIndex = oceanCaught;
                    break;
                case 3:
                    newFish.GetComponent<PristineFish>().InitCall();
                    newFish.GetComponent<Image>().sprite = newFish.GetComponent<PristineFish>().pristineFishSprites[oceanCaught][indexOfFish];
                    newFish.GetComponent<PristineFish>().spriteIndex = indexOfFish;
                    newFish.GetComponent<PristineFish>().oceanCaughtInIndex = oceanCaught;
                    break;
                case 4:
                    newFish.GetComponent<MagicalFishPrefab>().InitCall();
                    newFish.GetComponent<Image>().sprite = newFish.GetComponent<MagicalFishPrefab>().eliteEightSprites[indexOfFish];
                    newFish.GetComponent<MagicalFishPrefab>().spriteIndex = indexOfFish;
                    break;
            }
        }
    }

    /*public void chestItemSetup(int fish, int rarity, int indexOfFish, int inventorySlotNumber, int quantityOfFish, int oceanCaught){
        bool exists = false;
        if (fish == 1) {exists = true;}

        if (exists){
            GameObject i = FreeChestSlots[inventorySlotNumber];
            GameObject newFish = Instantiate(fishPrefabList[rarity], i.transform, false);

            FreeChestNumbers[inventorySlotNumber].SetActive(true);
            FreeChestNumbers[inventorySlotNumber].GetComponent<TMP_Text>().text = "" + quantityOfFish;

            switch(rarity){
                    case 0:
                        newFish.GetComponent<NormalFish>().InitCall();
                        newFish.GetComponent<Image>().sprite = newFish.GetComponent<NormalFish>().normalFishSprites[oceanCaught][indexOfFish];
                        newFish.GetComponent<NormalFish>().spriteIndex = indexOfFish;
                        newFish.GetComponent<NormalFish>().oceanCaughtInIndex = oceanCaught;
                        break;
                    case 1: 
                        newFish.GetComponent<FancyFish>().InitCall();
                        newFish.GetComponent<Image>().sprite = newFish.GetComponent<FancyFish>().fancyFishSprites[oceanCaught][indexOfFish];
                        newFish.GetComponent<FancyFish>().spriteIndex = indexOfFish;
                        newFish.GetComponent<FancyFish>().oceanCaughtInIndex = oceanCaught;
                        break;
                    case 2:
                        newFish.GetComponent<ExtravagantFish>().InitCall();
                        newFish.GetComponent<Image>().sprite = newFish.GetComponent<ExtravagantFish>().extravagantFishSprites[oceanCaught][indexOfFish];
                        newFish.GetComponent<ExtravagantFish>().spriteIndex = indexOfFish;
                        newFish.GetComponent<ExtravagantFish>().oceanCaughtInIndex = oceanCaught;
                        break;
                    case 3:
                        newFish.GetComponent<PristineFish>().InitCall();
                        newFish.GetComponent<Image>().sprite = newFish.GetComponent<PristineFish>().pristineFishSprites[oceanCaught][indexOfFish];
                        newFish.GetComponent<PristineFish>().spriteIndex = indexOfFish;
                        newFish.GetComponent<PristineFish>().oceanCaughtInIndex = oceanCaught;
                        break;
                }  
        }
    }


    public void paidChestItemSetup(int fish, int rarity, int indexOfFish, int inventorySlotNumber, int quantityOfFish, int oceanCaught){
        bool exists = false;
        if (fish == 1) {exists = true;}

        if (exists){
            GameObject i = PaidChestSlots[inventorySlotNumber];
            GameObject newFish = Instantiate(fishPrefabList[rarity], i.transform, false);

            PaidChestNumbers[inventorySlotNumber].SetActive(true);
            PaidChestNumbers[inventorySlotNumber].GetComponent<TMP_Text>().text = "" + quantityOfFish;

            switch(rarity){
                    case 0:
                        newFish.GetComponent<NormalFish>().InitCall();
                        newFish.GetComponent<Image>().sprite = newFish.GetComponent<NormalFish>().normalFishSprites[oceanCaught][indexOfFish];
                        newFish.GetComponent<NormalFish>().spriteIndex = indexOfFish;
                        newFish.GetComponent<NormalFish>().oceanCaughtInIndex = oceanCaught;
                        break;
                    case 1: 
                        newFish.GetComponent<FancyFish>().InitCall();
                        newFish.GetComponent<Image>().sprite = newFish.GetComponent<FancyFish>().fancyFishSprites[oceanCaught][indexOfFish];
                        newFish.GetComponent<FancyFish>().spriteIndex = indexOfFish;
                        newFish.GetComponent<FancyFish>().oceanCaughtInIndex = oceanCaught;
                        break;
                    case 2:
                        newFish.GetComponent<ExtravagantFish>().InitCall();
                        newFish.GetComponent<Image>().sprite = newFish.GetComponent<ExtravagantFish>().extravagantFishSprites[oceanCaught][indexOfFish];
                        newFish.GetComponent<ExtravagantFish>().spriteIndex = indexOfFish;
                        newFish.GetComponent<ExtravagantFish>().oceanCaughtInIndex = oceanCaught;
                        break;
                    case 3:
                        newFish.GetComponent<PristineFish>().InitCall();
                        newFish.GetComponent<Image>().sprite = newFish.GetComponent<PristineFish>().pristineFishSprites[oceanCaught][indexOfFish];
                        newFish.GetComponent<PristineFish>().spriteIndex = indexOfFish;
                        newFish.GetComponent<PristineFish>().oceanCaughtInIndex = oceanCaught;
                        break;
                }  
        }
    }*/

    public void wipeInventory(){
        int w = -1;
        foreach (GameObject i in InventorySlots){
            w++;
            if (i.transform.childCount > 0){
                InventoryNumbers[w].SetActive(false);
                InventoryNumbers[w].GetComponent<TMP_Text>().text = "" + 0;
                GameObject newFish = i.transform.GetChild(0).gameObject;
                //Debug.Log("killed " +newFish.name);
                Destroy(newFish);
                

            }
            if (w >= 3 && !battlePass.passPurchased){
                break;
            }
            else if (w >= 11 && battlePass.passPurchased){
                break;
            }
            //Debug.Log("Object " + i.name + " has " + i.transform.childCount + " kids");
        }  
    }

    /*public void wipeChestOne(){
        int w = -1;
        foreach (GameObject i in FreeChestSlots){
            w++;
            if (i.transform.childCount > 0){
                FreeChestNumbers[w].SetActive(false);
                FreeChestNumbers[w].GetComponent<TMP_Text>().text = "" + 0;
                GameObject newFish = i.transform.GetChild(0).gameObject;
                Destroy(newFish);

            }
        }  
    }

    public void wipeChestTwo(){
        int w = -1;
        foreach (GameObject i in PaidChestSlots){
            w++;
            if (i.transform.childCount > 0){
                PaidChestNumbers[w].SetActive(false);
                PaidChestNumbers[w].GetComponent<TMP_Text>().text = "" + 0;
                GameObject newFish = i.transform.GetChild(0).gameObject;
                Destroy(newFish);

            }
        }  
    }*/

    public void leaveSellScreenOG(){
        if (InventorySlots.Length > 0){
        if (InventorySlots[12].transform.childCount > 0){
            foreach (GameObject i in InventorySlots){
                if (i.transform.childCount == 0){
                    InventorySlots[12].transform.GetChild(0).SetParent(i.transform);
                    InventoryNumbers[int.Parse(i.name)].GetComponent<TMP_Text>().text = InventoryNumbers[12].GetComponent<TMP_Text>().text;
                    InventoryNumbers[int.Parse(i.name)].SetActive(true);
                    InventoryNumbers[12].GetComponent<TMP_Text>().text = "0";
                    InventoryNumbers[12].SetActive(false);
                    SaveInventory();
                    break;
                }
            }
        }
    }
    }

    public void leaveSellScreen(){
        if (donateSlot.Length > 0){
        if (donateSlot[0].transform.childCount > 0){
            foreach (GameObject i in InventorySlots){
                if (i.transform.childCount == 0){
                    donateSlot[0].transform.GetChild(0).SetParent(i.transform);
                    InventoryNumbers[int.Parse(i.name)].GetComponent<TMP_Text>().text = InventoryNumbers[12].GetComponent<TMP_Text>().text;
                    InventoryNumbers[int.Parse(i.name)].SetActive(true);
                    InventoryNumbers[12].GetComponent<TMP_Text>().text = "0";
                    InventoryNumbers[12].SetActive(false);
                    SaveInventory();
                    break;
                }
            }
        }
    }
    }

    /*public void leaveFreeDonateScreen(){
        if (donateSlot.Length > 0){
        if (donateSlot[1].transform.childCount > 0){
            foreach (GameObject i in FreeChestSlots){
                if (i.transform.childCount == 0){
                    donateSlot[1].transform.GetChild(0).SetParent(i.transform);
                    FreeChestNumbers[int.Parse(i.name) - 12].GetComponent<TMP_Text>().text = FreeChestNumbers[16].GetComponent<TMP_Text>().text;
                    FreeChestNumbers[int.Parse(i.name) - 12].SetActive(true);
                    FreeChestNumbers[16].GetComponent<TMP_Text>().text = "0";
                    FreeChestNumbers[16].SetActive(false);
                    SaveFreeChest();
                    break;
                }
            }
        }
    }
    }

    public void leavePaidDonateScreen(){
        if (donateSlot.Length > 0){
        if (donateSlot[2].transform.childCount > 0){
            foreach (GameObject i in PaidChestSlots){
                if (i.transform.childCount == 0){
                    donateSlot[2].transform.GetChild(0).SetParent(i.transform);
                    PaidChestNumbers[int.Parse(i.name) - 28].GetComponent<TMP_Text>().text = PaidChestNumbers[16].GetComponent<TMP_Text>().text;
                    PaidChestNumbers[int.Parse(i.name) - 28].SetActive(true);
                    PaidChestNumbers[16].GetComponent<TMP_Text>().text = "0";
                    PaidChestNumbers[16].SetActive(false);
                    SavePaidChest();
                    break;
                }
            }
        }
    }
    }*/
    

    public void DonateBack(){
        if (donateSlot[0].transform.childCount > 0 && gameObject.name == "Inventory"){
            Debug.Log("Slot 1 is active");
            leaveSellScreen();
        }
        else if(donateSlot[1].transform.childCount > 0 && gameObject.name == "ChestInventoryHolder"){
            Debug.Log("Slot 2 is active");
            //leaveFreeDonateScreen();
        }
        else if (donateSlot[2].transform.childCount > 0 && gameObject.name == "PaidChestInventoryHolder") {
            Debug.Log("Slot 3 is active");
            //leavePaidDonateScreen();
        }
    }

    public void SpawnFishTest(){
        int randInt = UnityEngine.Random.Range(0, 5); // 0 to 9
        catchFish(randInt,0,1);
    }

    public void OnApplicationQuit(){
        if (donateSlot.Length > 0){
            DonateBack();
        }
    }

    void OnApplicationPause()
    {
        if (donateSlot.Length > 0){
            DonateBack();
        }
    }

}

public class InventorySaveSlots{
    //EXAMPLE
    //[Is there something stored in the slot?, rarity of fish in slot, indexOfFish of that rarity, quantity of that fish, ocean it was caught in]
    //[0,0,0,0,0] [Nothing stored]
    //[1,2,2,3,0] [Fish here, Extravagant Rarity, At index 2, 3 fish, caught in East Blue]
    //public int[][] dataStorage = new int[8][];
    public int[] InventorySlot1 = new int[5];
    public int[] InventorySlot2 = new int[5];
    public int[] InventorySlot3 = new int[5];
    public int[] InventorySlot4 = new int[5];
    public int[] InventorySlot5 = new int[5];
    public int[] InventorySlot6 = new int[5];
    public int[] InventorySlot7 = new int[5];
    public int[] InventorySlot8 = new int[5];
    public int[] InventorySlot9 = new int[5];
    public int[] InventorySlot10 = new int[5];
    public int[] InventorySlot11 = new int[5];
    public int[] InventorySlot12 = new int[5];
}

public class ChestOneSaveSlots {
    //EXAMPLE
    //[Is there something stored in the slot?, rarity of fish in slot, indexOfFish of that rarity, quantity of that fish, ocean it was caught in]
    //[0,0,0,0,0] [Nothing stored]
    //[1,2,2,3,0] [Fish here, Extravagant Rarity, At index 2, 3 fish, caught in East Blue]
    //public int[][] dataStorage = new int[8][];
    public int[] ChestSlot1 = new int[5];
    public int[] ChestSlot2 = new int[5];
    public int[] ChestSlot3 = new int[5];
    public int[] ChestSlot4 = new int[5];
    public int[] ChestSlot5 = new int[5];
    public int[] ChestSlot6 = new int[5];
    public int[] ChestSlot7 = new int[5];
    public int[] ChestSlot8 = new int[5];
    public int[] ChestSlot9 = new int[5];
    public int[] ChestSlot10 = new int[5];
    public int[] ChestSlot11 = new int[5];
    public int[] ChestSlot12 = new int[5];
    public int[] ChestSlot13 = new int[5];
    public int[] ChestSlot14 = new int[5];
    public int[] ChestSlot15 = new int[5];
    public int[] ChestSlot16 = new int[5];
}


public class ChestTwoSaveSlots {
    //EXAMPLE
    //[Is there something stored in the slot?, rarity of fish in slot, indexOfFish of that rarity, quantity of that fish, ocean it was caught in]
    //[0,0,0,0,0] [Nothing stored]
    //[1,2,2,3,0] [Fish here, Extravagant Rarity, At index 2, 3 fish, caught in East Blue]
    //public int[][] dataStorage = new int[8][];
    public int[] ChestSlot1 = new int[5];
    public int[] ChestSlot2 = new int[5];
    public int[] ChestSlot3 = new int[5];
    public int[] ChestSlot4 = new int[5];
    public int[] ChestSlot5 = new int[5];
    public int[] ChestSlot6 = new int[5];
    public int[] ChestSlot7 = new int[5];
    public int[] ChestSlot8 = new int[5];
    public int[] ChestSlot9 = new int[5];
    public int[] ChestSlot10 = new int[5];
    public int[] ChestSlot11 = new int[5];
    public int[] ChestSlot12 = new int[5];
    public int[] ChestSlot13 = new int[5];
    public int[] ChestSlot14 = new int[5];
    public int[] ChestSlot15 = new int[5];
    public int[] ChestSlot16 = new int[5];
}
