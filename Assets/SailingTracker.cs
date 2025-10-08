using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class SailingTracker : MonoBehaviour
{
    public TMP_Text popUpPrice;
    public GameObject[] popUps;
    public SailsEquipper sailsEquipper;
    public PetWalk petWalk;
    [NonSerialized] public int[] sailsOwned = new int[50];
    public Image sails;
    public GameProgress gameProgress;
    [NonSerialized] public int equippedSail = 0;
    public Sprite[] offSails;
    public Sprite[] onSails;
    public Transform timeHolder; // Children: 11, 12, 21, 22
    public TMP_Text OneOneText;
    public TMP_Text OneTwoText;
    public TMP_Text TwoOneText;
    public TMP_Text TwoTwoText;
    public TMP_Text ZeroOneText;
    public TMP_Text ZeroTwoText;
    public TMP_Text ZeroZeroOneText;
    public TMP_Text ZeroZeroTwoText;

    public GameObject[] SailMoving;
    public GameObject[] waterEffectOne;
    public GameObject[] waterEffectTwo;
    public GameObject[] Anchor;
    public GameObject[] AnchorUp;

    public GameObject speedUpButton;


    [NonSerialized]
    public bool sailing = false;
    [NonSerialized] public DateTime endTime;
    [NonSerialized] public TimeSpan remainingTime;
    [NonSerialized] public int tasksCompletedToday;
    [NonSerialized] public DateTime lastTaskCompleteDate;
    public DailyTaskGift dailyTaskGift;
    [NonSerialized] public bool claimed = false;
    [NonSerialized] public bool INITIATED = false;


    //SHOP FOR SAILS
    public GameObject[] shopSailsNOTPURCHASED;
    public GameObject[] shopSailsPURCHASED;
    public Button[] shopSailsButtons;
    public Sprite[] shopSprites;
    public GameObject popUpWindow;
    public GameObject notEnoughGems;
    public Image shopPopUpImage;
    public GemStorage gemStorage;
    public SailSwap sailSwap;
    private int skinsCost = 500;
    private int currentSail = 21;


    //SHOP FOR SAILS SECTION
    public void buySail(int x)
    {
        sailsOwned[x] = 1;
        saveSailing();
    }

    public void sailsShopInit()
    {
        for (int i = 0; i < shopSailsNOTPURCHASED.Length; i++)
        {
            if (sailsOwned[i + 16] == 1)
            {
                shopSailsPURCHASED[i].SetActive(true);
                shopSailsNOTPURCHASED[i].SetActive(false);
                shopSailsButtons[i].enabled = false;
            }
            else
            {
                shopSailsNOTPURCHASED[i].SetActive(true);
                shopSailsPURCHASED[i].SetActive(false);
                shopSailsButtons[i].enabled = true;
            }
        }
    }

    public void shopBuySail()
    {
        if (gemStorage.GemTotal >= skinsCost)
        {
            gemStorage.buyWithGems(skinsCost);
            buySail(currentSail);
            sailSwap.equipNewSail(currentSail);
            popUpWindow.SetActive(false);
            sailsShopInit();
            saveSailing();
        }
    }

    public void buyPopUp(int characterIndex)
    {
        switch (characterIndex)
        {
            case 16:
                skinsCost = 100;
                break;
            case 17:
                skinsCost = 150;
                break;
            case 18:
                skinsCost = 200;
                break;
            case 19:
                skinsCost = 250;
                break;
            case 20:
                skinsCost = 300;
                break;
        }
        if (gemStorage.GemTotal >= skinsCost)
        {
            popUpWindow.SetActive(true);
            currentSail = characterIndex;
            shopPopUpImage.sprite = shopSprites[characterIndex - 16];
            popUpPrice.text = skinsCost.ToString();
        }
        else
        {
            notEnoughGems.SetActive(true);
        }
        
    }

    //END SHOP FOR SAILS SECTION



    public void popUpReset()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            popUps[i].SetActive(false);
        }
    }

    void OnApplicationFocus(bool focus)
    {
        if (INITIATED)
        {
            Debug.Log("We are on FOCUS and the start is " + tasksCompletedToday);
            if (lastTaskCompleteDate.Date != DateTime.Now.Date)
            {
                SaveSystem.Init();
                claimed = false;
                tasksCompletedToday = 0;
                saveTasksCompletedToday();
            }
            dailyTaskGift.taskInit();    
            Debug.Log("We are on FOCUS and the end is " + tasksCompletedToday);
        }
        
    }

    void OnApplicationPause(bool pause)
    {
        if (INITIATED)
        {
            Debug.Log("We are on PAUSE and the start is " + tasksCompletedToday);
            if (lastTaskCompleteDate.Date != DateTime.Now.Date)
            {
                SaveSystem.Init();
                claimed = false;
                tasksCompletedToday = 0;
                saveTasksCompletedToday();
            }
            dailyTaskGift.taskInit();
            Debug.Log("We are on PAUSE and the end is " + tasksCompletedToday);
        }
        
    }


    public void loadSailing()
    {
        string saveString = SaveSystem.LoadSail();
        if (saveString != null)
        {
            SailStorage loadedData = JsonUtility.FromJson<SailStorage>(saveString);
            equippedSail = loadedData.currentSail;
            sailsOwned = loadedData.sailsOwnedStored;
            //for (int i = 0; i < 50; i++){
            //Debug.Log("Loaded value of " + i + " is: " + loadedData.sailsOwnedStored[i]);
            //}   

        }
        else
        {
            equippedSail = 21;
            sailsOwned = new int[50];
            sailsOwned[21] = 1;
            //saveSailing();
            //sailsOwned = new int[] {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1};
        }
    }

    public void saveSailing(){
        SailStorage savedData = new SailStorage{
            currentSail = equippedSail,
            sailsOwnedStored = sailsOwned,
        };
        //for (int i = 0; i < 50; i++){
                //Debug.Log("Saved value of " + i + " is: " + savedData.sailsOwnedStored[i]);
            //}   
        string jsonStorage = JsonUtility.ToJson(savedData);
        SaveSystem.SaveSail(jsonStorage);
    }

    public void saveTasksCompletedToday(){
        Debug.Log("The value we are writing to SAVE is " + tasksCompletedToday);
        TodayTaskClass todayTaskClass = new TodayTaskClass{
            tasksCompleted = tasksCompletedToday,
            lastTaskCompletedDate = DateTime.Now.ToString(),
            claimed = claimed,
        };
        string jsonStorage = JsonUtility.ToJson(todayTaskClass);
        SaveSystem.SaveTasksCompletedToday(jsonStorage);
    }

    public void loadTasksCompletedToday(){
        SaveSystem.Init();
        string saveString = SaveSystem.LoadTasksCompletedToday();
        if (saveString != null)
        {
            TodayTaskClass loadedData = JsonUtility.FromJson<TodayTaskClass>(saveString);
            tasksCompletedToday = loadedData.tasksCompleted;
            lastTaskCompleteDate = DateTime.Parse(loadedData.lastTaskCompletedDate);
            claimed = loadedData.claimed;
            if (lastTaskCompleteDate.Date != DateTime.Now.Date)
            {
                claimed = false;
                tasksCompletedToday = 0;
            }
            dailyTaskGift.taskInit();

            Debug.Log("We loaded and this is the amount weve done " + tasksCompletedToday);
        }
        else
        {
            Debug.Log("WERE HITTING THE ELSE");
            lastTaskCompleteDate = DateTime.MinValue;
            claimed = false;
            dailyTaskGift.taskInit();
        }
    }
}

public class SailStorage {
    public int currentSail = 0;
    public int[] sailsOwnedStored = new int[50];
}

public class TodayTaskClass
{
    public int tasksCompleted = 0;
    public string lastTaskCompletedDate = DateTime.MinValue.ToString();
    public bool claimed = false;
}
