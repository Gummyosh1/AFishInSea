using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FishCollectionBook : MonoBehaviour
{
    public GameObject background;
    public GameObject eliteHolder;
    public GameObject[] fishHolders;
    public GameProgress gameProgress;
    public GameObject navBar;

    public FishNames fishNames;
    public FishBios fishBios;
    public FishSpriteHolder fishSpriteHolderScript;

    public TMP_Text fishNameHolder;
    public TMP_Text quantityCaughtHolder;
    public TMP_Text fishBioHolder;
    public Image fishSpriteHolder;
    public GameObject fishPopWindowGO;

    private int[] eastBlueFishRegistered = new int[23];
    private int[] purpleFishRegistered = new int[23];
    private int[] lavaFishRegistered = new int[23];
    private int[] allBlueFishRegistered = new int[23];
    private int[] eliteEightRegistered = new int[23]; //only need 8 of these but we make it 23 so it can format correctly inside the oceanRegisters list of lists
    private int[][] oceanRegisters = new int[5][];

    public void CollectionInit()
    {
        loadCollection();
        oceanRegisters[0] = eastBlueFishRegistered;
        oceanRegisters[1] = purpleFishRegistered;
        oceanRegisters[2] = lavaFishRegistered;
        oceanRegisters[3] = allBlueFishRegistered;
        oceanRegisters[4] = eliteEightRegistered;
    }

    public void CollectionPopUp(){
        fishHolders[gameProgress.currentOceanIndex].SetActive(true);
        background.SetActive(true);
        navBar.SetActive(false);
    }

    public void specificFishPop(string conglomerate)
    {
        string[] parts = conglomerate.Split(',');

        int.TryParse(parts[0], out int ocean);
        int.TryParse(parts[1], out int rarity);
        int.TryParse(parts[2], out int index);

        int specialIndex = index;

        switch (rarity)
        {
            case 0:
                break;
            case 1:
                specialIndex = index + 10;
                break;
            case 2:
                specialIndex = index + 17;
                break;
            case 3:
                specialIndex = index + 21;
                break;
            case 4:
                //ELITE 8
                break;
        }

        if (oceanRegisters[ocean][specialIndex] > 0)
        {
            quantityCaughtHolder.text = oceanRegisters[ocean][specialIndex].ToString();
            fishNameHolder.text = fishNames.nameLists[ocean][rarity][index];
            //fishBioHolder.text = fishBios.bioLists[ocean][rarity][index]; TODO IN FUTURE UPDATE ABOUT BIOS
            fishBioHolder.text = "The story of this world is said to be contained in the fish of the vast oceans... Come back in a future update for the stories that each one tells!";
            fishSpriteHolder.sprite = fishSpriteHolderScript.spriteLists[ocean][rarity][index];
            fishSpriteHolder.color = Color.white;
        }
        else
        {
            quantityCaughtHolder.text = oceanRegisters[ocean][specialIndex].ToString();
            fishNameHolder.text = "?????????";
            fishBioHolder.text = "???????????????\n????????????????"; ;
            fishSpriteHolder.sprite = fishSpriteHolderScript.spriteLists[ocean][rarity][index];
            fishSpriteHolder.color = Color.black;
        }
        

        

        fishPopWindowGO.SetActive(true);
    }

    public void CollectionPopUpOff()
    {
        fishHolders[gameProgress.currentOceanIndex].SetActive(false);
        background.SetActive(false);
        navBar.SetActive(true);
    }

    //TO REGISTER AN ELITE 8 FISH, fishNumber = #, rarity = 4, currentOceanIndex = 4
    public void registerFish(int fishNumber, int rarity, int currentOceanIndex, int fishQuantity)
    {
        int index = 0;
        switch (rarity)
        {
            case 0:
                index = fishNumber;
                break;
            case 1:
                index = fishNumber + 10;
                break;
            case 2:
                index = fishNumber + 17;
                break;
            case 3:
                index = fishNumber + 21;
                break;
            case 4:
                index = fishNumber;
                //ELITE 8
                break;
        }
        oceanRegisters[currentOceanIndex][index] += fishQuantity;
        fishHolders[currentOceanIndex].transform.GetChild(index).GetChild(0).GetChild(0).gameObject.GetComponent<Image>().color = Color.white;

        collectionBookStorage CollectionBookStorage = new collectionBookStorage
        {
            EastBlueCollectionBook = eastBlueFishRegistered,
            PurpleCollectionBook = purpleFishRegistered,
            LavaCollectionBook = lavaFishRegistered,
            AllBlueCollectionBook = allBlueFishRegistered,
            EliteEightCollectionBook = eliteEightRegistered,
        };
        string jsonStorage = JsonUtility.ToJson(CollectionBookStorage);
        SaveSystem.SaveCollection(jsonStorage);
    }

    public void loadCollection(){
        string saveString = SaveSystem.LoadCollection();
        if (saveString != null){
            collectionBookStorage CollectionBookStorage = JsonUtility.FromJson<collectionBookStorage>(saveString);

            eastBlueFishRegistered = CollectionBookStorage.EastBlueCollectionBook;
            purpleFishRegistered = CollectionBookStorage.PurpleCollectionBook;
            lavaFishRegistered = CollectionBookStorage.LavaCollectionBook;
            allBlueFishRegistered = CollectionBookStorage.AllBlueCollectionBook;
            eliteEightRegistered = CollectionBookStorage.EliteEightCollectionBook;

            oceanRegisters[0] = eastBlueFishRegistered;
            oceanRegisters[1] = purpleFishRegistered;
            oceanRegisters[2] = lavaFishRegistered;
            oceanRegisters[3] = allBlueFishRegistered;
            oceanRegisters[4] = eliteEightRegistered;

            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 23; i++)
                {
                    if (oceanRegisters[j][i] >= 1)
                    {
                        fishHolders[j].transform.GetChild(i).GetChild(0).GetChild(0).gameObject.GetComponent<Image>().color = Color.white;
                    }
                    else
                    {
                        fishHolders[j].transform.GetChild(i).GetChild(0).GetChild(0).gameObject.GetComponent<Image>().color = Color.black;
                    }
                }
            }
            for (int j = 0; j < 8; j++){
                if (eliteEightRegistered[j] >= 1){
                    eliteHolder.transform.GetChild(j).GetChild(0).GetChild(0).gameObject.GetComponent<Image>().color = Color.white;
                }
                else{
                    eliteHolder.transform.GetChild(j).GetChild(0).GetChild(0).gameObject.GetComponent<Image>().color = Color.black;
                }
            }
        }
        else{
            
            oceanRegisters[0] = eastBlueFishRegistered;
            oceanRegisters[1] = purpleFishRegistered;
            oceanRegisters[2] = lavaFishRegistered;
            oceanRegisters[3] = allBlueFishRegistered;
            oceanRegisters[4] = eliteEightRegistered;

            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 23; i++)
                {
                    oceanRegisters[j][i] = 0;
                    fishHolders[j].transform.GetChild(i).GetChild(0).GetChild(0).gameObject.GetComponent<Image>().color = Color.black;
                }
            }

            for (int j = 0; j < 8; j++){
                    eliteEightRegistered[j] = 0;
                    eliteHolder.transform.GetChild(j).GetChild(0).GetChild(0).gameObject.GetComponent<Image>().color = Color.black;
            }
        }
    }
}

public class collectionBookStorage {
    public int[] EastBlueCollectionBook = new int[20];
    public int[] PurpleCollectionBook = new int[20];
    public int[] LavaCollectionBook = new int[20];
    public int[] AllBlueCollectionBook = new int[20];
    public int[] EliteEightCollectionBook = new int[8];
}
