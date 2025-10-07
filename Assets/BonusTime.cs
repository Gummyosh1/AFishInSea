using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BonusTime : MonoBehaviour
{
    public DailyTaskGift dailyTaskGift;
    public SailingTracker sailingTracker;
    public BattlePass battlePass;
    public GameProgress gameProgress;
    public StreakTracker streakTracker;
    public GameObject bonusAdderOBJ;
    public GemStorage gemStorage;
    public GameObject notEnoughCoins;
    public GameObject baitGainedOBJ;

    public void addBonusBaitCoins(){
        if (gemStorage.GemTotal >= 10)
        {
            sailingTracker.loadTasksCompletedToday();
            //IF WE HAVEN'T DONE 6 OR MORE CHALLENGES TODAY, WE GET TO PROGRESS!
            sailingTracker.tasksCompletedToday++;
            //sailingTracker.gainEnergy(battlePass.timeGain);
            //gameProgress.sailDistance();
            streakTracker.addStreak();
            streakTracker.InitializeStreakBar();
            bonusAdderOBJ.SetActive(false);
            gemStorage.buyWithGems(10);
            dailyTaskGift.baitAdd();
            coinsGainedPopUp();
        }
        else
        {
            notEnoughCoins.SetActive(true);
        }
    }

    public void coinsGainedPopUp(){
        baitGainedOBJ.SetActive(true);
    }
    public void tooManyTasksPopUp(){
        bonusAdderOBJ.SetActive(true);
    }
}
