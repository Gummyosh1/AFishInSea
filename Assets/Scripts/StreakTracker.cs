using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class StreakTracker : MonoBehaviour
{
    public TMP_Text currentGemsPopUpText;
    public GameObject buyGemsPopup;
    public TMP_Text valueSaveText;
    public TMP_Text streakSaveText;
    public GameObject streakSaveOBJ;
    public IslandScript islandScript;
    public TMP_Text streakUIText;
    public TMP_Text streakTaskText;
    public InventoryKing inventoryKing;
    public GemStorage gemStorage;
    public BaitCollection baitCollection;
    public BaitBuy baitBuy;
    public DateTime lastDate;
    [NonSerialized]
    public int currentStreak = 0;
    private int cost = 0;
    public TMP_Text streakText;
    public TMP_Text streakTextShip;
    public Slider slider;
    public TMP_Text streakCounterBarText;
    public Image fill;
    [NonSerialized] public int[] dailyChestLevels = {3, 10, 25, 75, 150, 365};
    public Sprite[] lvl1Rewards;
    public Sprite[] lvl2Rewards;
    public Sprite[] lvl3Rewards;
    public Sprite[] lvl4Rewards;
    public Sprite[] lvl5Rewards;
    public Sprite[] lvl6Rewards;
    public Sprite[] lvl7Rewards;

    public GameObject rewardPopUp;
    public GameObject chestPopUp;
    public GameObject backing;
    public GameObject backing2;

    public Image rewardSprite;
    public TMP_Text rewardQuantity;
    private Random random = new Random();
    private int rewardQ;
    private int rewardIndex;
    public GameObject waitingButton;
    public GameObject openChestButton;
    public TMP_Text chestTimerText;
    private float timing = 10;
    private DateTime chestClaimed = DateTime.MinValue;
    public FlameAnimate flameAnimateBar;
    public FlameAnimate flameAnimateShip;
    public FlameAnimate flameAnimateUI;
    public FlameAnimate flameAnimateTask;
    public Image boatChest;
    public Image progressChest;
    public DailyChest dailyChest;
    public crystalAnimation crystalAnimation;
    private bool initFocus = false;
    private bool isPaused = false;

    public void Start()
    {
        SaveSystem.Init();
        flameAnimateBar.initList();
        flameAnimateShip.initList();
        flameAnimateUI.initList();
        flameAnimateTask.initList();
        dailyChest.chestInit();
        crystalAnimation.CrystalInit();
        // RESET STREAK HERE IF IT'S BEEN MORE THAN A DAY
        loadStreak();
        loadDailyChest();
        TimeSpan dateDifference = DateTime.Now.Date - lastDate;
        if (dateDifference.TotalDays > 1 || lastDate == DateTime.MinValue)
        {
            if (lastDate == DateTime.MinValue)
            {
                currentStreak = 0;
                saveStreak();
            }
            else
            {
                double gemAmnt = dateDifference.TotalDays * 25;
                cost = (int)gemAmnt;
                streakSaveText.text = $"It's been {dateDifference.TotalDays} days since you completed a task\nSave your streak for {cost} gems?";
                valueSaveText.text = $"{cost}";
                streakSaveOBJ.SetActive(true);
            }
            
            //currentStreak = 0;
            //saveStreak();
        }
        streakText.text = "" + currentStreak;
        streakTextShip.text = "" + currentStreak;
        streakUIText.text = currentStreak.ToString();
        streakTaskText.text = currentStreak.ToString();

        InitializeStreakBar();

        initFocus = true;
    }
    
    void OnApplicationPause(bool pauseStatus)
    {
        isPaused = pauseStatus;
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (initFocus)
        {
            loadStreak();
            loadDailyChest();
            TimeSpan dateDifference = DateTime.Now.Date - lastDate;
            Debug.Log($"Date difference is {dateDifference.TotalDays}");
            if (dateDifference.TotalDays > 1 || lastDate == DateTime.MinValue)
            {
                if (lastDate == DateTime.MinValue)
                {
                    currentStreak = 0;
                    saveStreak();
                }
                else
                {
                    double gemAmnt = dateDifference.TotalDays * 25;
                    cost = (int)gemAmnt;
                    streakSaveText.text = $"It's been {dateDifference.TotalDays} days since you completed a task\nSave your streak for {cost} gems?";
                    valueSaveText.text = $"{cost}";
                    streakSaveOBJ.SetActive(true);
                }

                //currentStreak = 0;
                //saveStreak();
            }
            streakText.text = "" + currentStreak;
            streakTextShip.text = "" + currentStreak;
            streakUIText.text = currentStreak.ToString();
            streakTaskText.text = currentStreak.ToString();
            isPaused = !hasFocus;
            InitializeStreakBar();
        }
    }

    public void confirmStreakLoss()
    {
        lastDate = DateTime.Now.Date.AddDays(-1);
        currentStreak = 0;
        saveStreak();

        streakText.text = "" + currentStreak;
        streakTextShip.text = "" + currentStreak;
        streakUIText.text = currentStreak.ToString();
        streakTaskText.text = currentStreak.ToString();

        InitializeStreakBar();
    }

    public void healStreak()
    {
        if (gemStorage.GemTotal >= cost)
        {
            lastDate = DateTime.Now.Date.AddDays(-1);
            saveStreak();
            streakText.text = "" + currentStreak;
            streakTextShip.text = "" + currentStreak;
            streakUIText.text = currentStreak.ToString();
            streakTaskText.text = currentStreak.ToString();

            InitializeStreakBar();

            gemStorage.buyWithGems(cost);
            streakSaveOBJ.SetActive(false);
        }
        else
        {
            currentGemsPopUpText.text = $"{gemStorage.GemTotal}";
            buyGemsPopup.SetActive(true);
        }
    }

    public void Update()
    {
        timing += Time.deltaTime;

        if (timing > 10)
        {
            TimeSpan timeTilNextDay = DateTime.Now.Date.AddDays(1) - DateTime.Now;
            chestTimerText.text = "" + (timeTilNextDay.Hours + 1) + " hours until\nnext chest...";
            timing = 0;

            if (chestClaimed == DateTime.MinValue)
            {
                openChestButton.SetActive(true);
                waitingButton.SetActive(false);
            }
            if (chestClaimed.Date != DateTime.Now.Date)
            {
                openChestButton.SetActive(true);
                waitingButton.SetActive(false);
            }
            else
            {
                waitingButton.SetActive(true);
                openChestButton.SetActive(false);
            }

            saveDailyChest();
            InitializeStreakBar();
        }
    }
/**/
    public void InitializeStreakBar(){
        if (currentStreak < 3){
            slider.maxValue = dailyChestLevels[0];
            streakCounterBarText.SetText(currentStreak + "/" + dailyChestLevels[0]);
            flameAnimateTask.currentTorch = 0;
            flameAnimateBar.currentTorch = 0;
            flameAnimateShip.currentTorch = 0;
            flameAnimateUI.currentTorch = 0;
            flameAnimateTask.index = 0;
            flameAnimateBar.index = 0;
            flameAnimateShip.index = 0;
            flameAnimateUI.index = 0;
            dailyChest.chestInit();
            boatChest.sprite = dailyChest.chests[0][0];
            progressChest.sprite = dailyChest.chests[0][0];
            crystalAnimation.currentCrystal = 0;
            crystalAnimation.index = 0;
        }
        else if (currentStreak < 10){
            slider.maxValue = dailyChestLevels[1];
            streakCounterBarText.SetText(currentStreak + "/" + dailyChestLevels[1]);
            //flame colors
            flameAnimateTask.currentTorch = 1;
            flameAnimateBar.currentTorch = 1;
            flameAnimateShip.currentTorch = 1;
            flameAnimateUI.currentTorch = 1;
            flameAnimateTask.index = 0;
            flameAnimateBar.index = 0;
            flameAnimateShip.index = 0;
            flameAnimateUI.index = 0;
            boatChest.sprite = dailyChest.chests[1][0];
            progressChest.sprite = dailyChest.chests[1][0];
            crystalAnimation.currentCrystal = 1;
            crystalAnimation.index = 0;
            //flame color end
            fill.color = Color.white;
        }
        else if (currentStreak < 25){
            slider.maxValue = dailyChestLevels[2];
            streakCounterBarText.SetText(currentStreak + "/" + dailyChestLevels[2]);
            //flame colors
            flameAnimateTask.currentTorch = 2;
            flameAnimateBar.currentTorch = 2;
            flameAnimateShip.currentTorch = 2;
            flameAnimateUI.currentTorch = 2;
            flameAnimateTask.index = 0;
            flameAnimateBar.index = 0;
            flameAnimateShip.index = 0;
            flameAnimateUI.index = 0;
            boatChest.sprite = dailyChest.chests[2][0];
            progressChest.sprite = dailyChest.chests[2][0];
            crystalAnimation.currentCrystal = 2;
            crystalAnimation.index = 0;
            //flame color end
            fill.color = Color.blue;
        }
        else if (currentStreak < 75){
            slider.maxValue = dailyChestLevels[3];
            streakCounterBarText.SetText(currentStreak + "/" + dailyChestLevels[3]);
            //flame colors
            flameAnimateTask.currentTorch = 3;
            flameAnimateBar.currentTorch = 3;
            flameAnimateShip.currentTorch = 3;
            flameAnimateUI.currentTorch = 3;
            flameAnimateTask.index = 0;
            flameAnimateBar.index = 0;
            flameAnimateShip.index = 0;
            flameAnimateUI.index = 0;
            boatChest.sprite = dailyChest.chests[3][0];
            progressChest.sprite = dailyChest.chests[3][0];
            crystalAnimation.currentCrystal = 3;
            crystalAnimation.index = 0;
            //flame color end
            fill.color = Color.green;
        }
        else if (currentStreak < 150){
            slider.maxValue = dailyChestLevels[4];
            streakCounterBarText.SetText(currentStreak + "/" + dailyChestLevels[4]);
            //flame colors
            flameAnimateTask.currentTorch = 4;
            flameAnimateBar.currentTorch = 4;
            flameAnimateShip.currentTorch = 4;
            flameAnimateUI.currentTorch = 4;
            flameAnimateTask.index = 0;
            flameAnimateBar.index = 0;
            flameAnimateShip.index = 0;
            flameAnimateUI.index = 0;
            boatChest.sprite = dailyChest.chests[4][0];
            progressChest.sprite = dailyChest.chests[4][0];
            crystalAnimation.currentCrystal = 4;
            crystalAnimation.index = 0;
            //flame color end
            fill.color = Color.red;
        }
        else if (currentStreak >= 150){
            slider.maxValue = dailyChestLevels[5];
            streakCounterBarText.SetText(currentStreak + "/" + dailyChestLevels[5]);
            //flame colors
            flameAnimateTask.currentTorch = 2;
            flameAnimateBar.currentTorch = 2;
            flameAnimateShip.currentTorch = 2;
            flameAnimateUI.currentTorch = 2;
            flameAnimateTask.index = 0;
            flameAnimateBar.index = 0;
            flameAnimateShip.index = 0;
            flameAnimateUI.index = 0;
            boatChest.sprite = dailyChest.chests[5][0];
            progressChest.sprite = dailyChest.chests[5][0];
            crystalAnimation.currentCrystal = 2;
            crystalAnimation.index = 0;
            //flame color end
            fill.color = Color.cyan;
        }
        slider.value = currentStreak;  
    }

    public void testingStreak(){
        currentStreak+= 1;
        streakText.text = "" + currentStreak;
        streakTextShip.text = "" + currentStreak;
        streakUIText.text = currentStreak.ToString();
        streakTaskText.text = currentStreak.ToString();
        InitializeStreakBar();
    }

    public void addStreak()
    {
        DateTime currentDate = DateTime.Now.Date;

        // Calculate the difference in days
        TimeSpan dateDifference = currentDate - lastDate;
        Debug.Log("Date Diff is: " + dateDifference);

        if (dateDifference.TotalDays == 1)
        {
            // Increment streak if it's the next day
            currentStreak++;
        }
        if (dateDifference.TotalDays > 1)
        {
            // Reset streak if more than a day has passed
            currentStreak = 1;
        }
        if (lastDate == DateTime.MinValue){
            currentStreak = 1;
        }
        streakText.text = "" + currentStreak;
        streakTextShip.text = "" + currentStreak;
        streakUIText.text = currentStreak.ToString();
        streakTaskText.text = currentStreak.ToString();
        lastDate = DateTime.Now.Date;

        islandScript.questCheck(5, currentStreak);
        saveStreak();
        InitializeStreakBar();
    }

    public void saveStreak(){
        streakSaveClass streakSave = new streakSaveClass{
            lastDateSave = lastDate.ToString(),
            streak = currentStreak,
        };
        string jsonStorage = JsonUtility.ToJson(streakSave);
        SaveSystem.SaveStreak(jsonStorage);
    }

    public void saveDailyChest(){
        chestOpen dailySave = new chestOpen{
            chestOpened = chestClaimed.Date.ToString(),
        };
        string jsonStorage = JsonUtility.ToJson(dailySave);
        SaveSystem.SaveDailyChest(jsonStorage);
    }

    public void loadStreak(){
        string saveString = SaveSystem.LoadStreak();
        if (saveString != null){
            streakSaveClass loadedData = JsonUtility.FromJson<streakSaveClass>(saveString);
            lastDate = DateTime.Parse(loadedData.lastDateSave);
            currentStreak = loadedData.streak;
        }
        else{
            currentStreak = 0;
            lastDate = DateTime.MinValue;
        }
    }

    public void loadDailyChest(){
        string saveString = SaveSystem.LoadDailyChest();
        if (saveString != null){
            chestOpen loadedData = JsonUtility.FromJson<chestOpen>(saveString);
            chestClaimed = DateTime.Parse(loadedData.chestOpened);
        }
        else{
            chestClaimed = DateTime.MinValue;
        }
    }

    public void claimDailyChest(){
        rewardPopUp.SetActive(true);
        chestPopUp.SetActive(false);
        backing2.SetActive(true);
        backing.SetActive(false);
        
        

        if (currentStreak < dailyChestLevels[0])
        {
            rewardIndex = random.Next(0, lvl1Rewards.Length);
            rewardSprite.sprite = lvl1Rewards[rewardIndex];
            mathAbstraction(5);
        }
        else if (currentStreak < dailyChestLevels[1])
        {
            rewardIndex = random.Next(0, lvl2Rewards.Length);
            rewardSprite.sprite = lvl2Rewards[rewardIndex];
            mathAbstraction(10);
        }
        else if (currentStreak < dailyChestLevels[2])
        {
            rewardIndex = random.Next(0, lvl3Rewards.Length);
            rewardSprite.sprite = lvl3Rewards[rewardIndex];
            mathAbstraction(15);
        }
        else if (currentStreak < dailyChestLevels[3])
        {
            rewardIndex = random.Next(0, lvl4Rewards.Length);
            rewardSprite.sprite = lvl4Rewards[rewardIndex];
            mathAbstraction(20);
        }
        else if (currentStreak < dailyChestLevels[4])
        {
            rewardIndex = random.Next(0, lvl5Rewards.Length);
            rewardSprite.sprite = lvl5Rewards[rewardIndex];
            mathAbstraction(25);
        }
        else if (currentStreak < dailyChestLevels[5])
        {
            rewardIndex = random.Next(0, lvl6Rewards.Length);
            rewardSprite.sprite = lvl6Rewards[rewardIndex];
            mathAbstraction(30);
        }
        else
        {
            rewardIndex = random.Next(0, lvl7Rewards.Length);
            rewardSprite.sprite = lvl7Rewards[rewardIndex];
            mathAbstraction(50);
        }
    }

    public void mathAbstraction(int gemQ){
        if (rewardIndex == 0)
        {
            //rewardQ = random.Next(1, 5);
            //rewardQuantity.text = "x" + rewardQ;
            rewardQuantity.text = "x" + gemQ;
        }
        else
        {
            //OTHER REWARDS TO GO HERE TODO
        }
    }

    public void claimReward(){
        Debug.Log("Claimed");
        rewardPopUp.SetActive(false);
        backing.SetActive(false);
        backing2.SetActive(false);
        if (currentStreak < dailyChestLevels[0])
        {
            getLevelOneReward();
        }
        else if (currentStreak < dailyChestLevels[1])
        {
            getLevelTwoReward();
        }
        else if (currentStreak < dailyChestLevels[2])
        {
            getLevelXReward(15);
        }
        else if (currentStreak < dailyChestLevels[3])
        {
            getLevelXReward(20);
        }
        else if (currentStreak < dailyChestLevels[4])
        {
            getLevelXReward(25);
        }
        else if (currentStreak < dailyChestLevels[5])
        {
            getLevelXReward(30);
        }
        else
        {
            Debug.Log("Getting Max Reward");
            getLevelXReward(50);
        }
        waitingButton.SetActive(true);
        openChestButton.SetActive(false);
        chestClaimed = DateTime.Now.Date;
        saveDailyChest();
    }

    public void getLevelOneReward(){
        if (rewardIndex == 0)
        {
            Debug.Log("Adding Gems");
            gemStorage.addGems(5);
        }
        else if (rewardIndex == 1)
        {
            //baitBuy.FancyBaitTotal += rewardQ;
            //baitBuy.SaveBait();
            //OTHER REWARDS GO HERE
        }
    }

    public void getLevelTwoReward(){
        if (rewardIndex == 0)
        {
            gemStorage.addGems(10);
        }
        else if (rewardIndex == 1)
        {
            //baitBuy.ExtravagantBaitTotal += rewardQ;
            //baitBuy.SaveBait();
            //OTHER REWARDS GO HERE
        }
    }

    public void getLevelXReward(int gemQ){
        if (rewardIndex == 0)
        {
            gemStorage.addGems(gemQ);
        }
        else if (rewardIndex == 1)
        {
            //baitBuy.PristineBaitTotal += rewardQ;
            //baitBuy.SaveBait();
            //OTHER REWARDS GO HERE
        }
    }
}

public class streakSaveClass
{
    public string lastDateSave;
    public int streak = 0;
}

public class chestOpen
{
    public string chestOpened = DateTime.MinValue.ToString();
}
