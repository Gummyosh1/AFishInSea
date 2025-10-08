using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using Random = System.Random;

public class BaitCollection : MonoBehaviour
{
    public TMP_Text baitCoinCount;
    public GameObject baitRewardScreen;
    [NonSerialized] public string[] baitPhrases = new string[6];
    public OceanScroll oceanScroll; 
    [NonSerialized]
    public int baitToBeCollected = 0;
    [NonSerialized]
    public int maxBaitStorage = 20;

    Random random = new Random();
    public GameObject baitCollectionPopUp;
    public GameObject baitCollectionPopUpFishing;
    public GameObject greyBack;
    public GameObject greyBackFishing;

    public DateTime oldCloseTime = DateTime.MinValue;
    public TMP_Text storageText;

    private float saver = 10;

    private float collectionInterval = 1800f; // 30 minutes in seconds
    private float timeSinceLastCollection = 0;

    public BaitQuantity baitQuantity;
    public BaitBuy baitBuy;

    public TMP_Text[] additions;
    public TMP_Text[] totals;
    public Random rando = new Random(); 
    public TMP_Text baitMsgTxt;
    

    public void Start()
    {
        SaveSystem.Init();
        baitBuy.LoadBait();
        loadCloseTime();
        loadBaitToBeCollected();
        Debug.Log("Old close time is: " + oldCloseTime.ToString());
        baitInit();
        
        if (oldCloseTime != DateTime.MinValue){
            TimeSpan timePassed = DateTime.Now - oldCloseTime;
            //Debug.Log(oldCloseTime);

            float totalSecondsPassed = (float)timePassed.TotalSeconds;
            //Debug.Log("total time passed + time spent in app last time: " + (totalSecondsPassed + timeSinceLastCollection));
            int resourcesToAdd = Mathf.FloorToInt((totalSecondsPassed + timeSinceLastCollection) / collectionInterval);
            //Debug.Log("Resources to Add: " + resourcesToAdd);
            float excessSeconds = (totalSecondsPassed + timeSinceLastCollection) % collectionInterval;
            baitToBeCollected = Mathf.Min(baitToBeCollected + resourcesToAdd, maxBaitStorage);
            timeSinceLastCollection = excessSeconds; //TODO
            //Debug.Log(timeSinceLastCollection);
            saveBaitToBeCollected();
        }
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus){
            saveCloseTime();
            Debug.Log("paused");
        };
    }

    public void openGame(){
        baitCoinCount.text = baitBuy.baitCoinTotal.ToString();
    }

    public void baitInit(){
        baitPhrases[0] = "Here's some bait for the hard work you're putting in!";
        baitPhrases[0] = "New bait just dropped chat is this reel (haha)?";
        baitPhrases[0] = "YIPEEEEEE";
        baitPhrases[0] = "Hey dude! I just want to say I'm proud of your hard work.";
        baitPhrases[0] = "You can use this to go fishing!!! Yay!";
        baitPhrases[0] = "It's a bird? It's a plane? No! It's bait for fishing!";
    }

    void OnApplicationQuit()
    {
        saveCloseTime();
    }

    public void Update(){
        saver += Time.deltaTime;
        if (saver >= 5){
            saveCloseTime();
            saver = 0;
        }

        inGameBaitCollection();
    }


    public void saveCloseTime(){
        DateTime closeTime = DateTime.Now;
        string storageTimeString = closeTime.ToString();
        baitTimeStorage baitTimeStorer = new baitTimeStorage{
            oldTime = storageTimeString,
            leftoverSeconds = timeSinceLastCollection,
        };
        string jsonStorage = JsonUtility.ToJson(baitTimeStorer);
        SaveSystem.SaveBaitTimer(jsonStorage);
        //Debug.Log("Time Since Last Collection: " + timeSinceLastCollection);
    }

    public void saveBaitToBeCollected(){
        BaitToBeCollectedClass baitCollection = new BaitToBeCollectedClass{
            baitToBeCollected = baitToBeCollected,
        };
        string jsonStorage = JsonUtility.ToJson(baitCollection);
        SaveSystem.SaveBaitCollectionStorage(jsonStorage);
    }

    public void loadCloseTime(){
        string saveString = SaveSystem.LoadBaitTimer();
        if (saveString != null){
            baitTimeStorage loadedData = JsonUtility.FromJson<baitTimeStorage>(saveString);
            oldCloseTime = DateTime.Parse(loadedData.oldTime);
            timeSinceLastCollection = loadedData.leftoverSeconds; //TODO
        }
        else{
            oldCloseTime = DateTime.MinValue;
            timeSinceLastCollection = 0;
        }
    }

    public void loadBaitToBeCollected(){
        string saveString = SaveSystem.LoadBaitCollectionStorage();
        if (saveString != null){
            BaitToBeCollectedClass loadedData = JsonUtility.FromJson<BaitToBeCollectedClass>(saveString);
            baitToBeCollected = loadedData.baitToBeCollected;
        }
        else{
            baitToBeCollected = 0;
        }
    }

    public void inGameBaitCollection(){
        timeSinceLastCollection += Time.deltaTime;

        if (timeSinceLastCollection >= collectionInterval)
        {
            timeSinceLastCollection = 0;
            if (baitToBeCollected < maxBaitStorage){
                //Debug.Log("Bait Collected!");
                baitToBeCollected++;
                saveBaitToBeCollected();
            }
        }
    }

    public void collectBait(){
        //baitCollectionPopUp.SetActive(true);
        //baitCollectionPopUpFishing.SetActive(true);
        //greyBack.SetActive(true);
        baitRewardScreen.SetActive(true);
        int hold = rando.Next(0,6);
        baitMsgTxt.text = baitPhrases[hold];

        baitBuy.WormTotal += 1; //TODO
        baitBuy.LadyBugTotal += 1; //TODO
        baitBuy.FireflyTotal += 1; //TODO
        baitBuy.MinnowTotal += 1; //TODO
        baitBuy.ButterflyTotal += 1;
        baitBuy.DragonflyTotal += 1;
        baitBuy.SaveBait();

        //THIS SECTION IS FOR THE VISUAL DISPLAYS OF BAIT
        /*baitQuantity.incrementNormal(); //TODO
        baitQuantity.incrementFancy(); //TODO
        baitQuantity.incrementExtravagant(); //TODO
        baitQuantity.incrementPristine(); //TODO*/
        
    }
}

public class baitTimeStorage
{
    public string oldTime;
    public float leftoverSeconds = 0;
}

public class BaitToBeCollectedClass
{
    public int baitToBeCollected = 0;
}
