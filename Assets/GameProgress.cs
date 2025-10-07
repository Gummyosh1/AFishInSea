using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgress : MonoBehaviour
{
    [NonSerialized]
    public int currentIslandIndex = 0;
    [NonSerialized]
    public int currentOceanIndex = 0;
    [NonSerialized]
    public int equippedCharacter = 0;
    public GameObject islandScene;
    public GameObject[] islandBackgrounds;
    public GameObject[] islandButtons;
    //public GameObject[] exclamationPoints;
    public GameObject Ocean;
    public OceanAnimation oceanAnimation;
    public TabScript tabScript;
    public IslandManager islandManager;
    public IslandScript islandScript;
    public BattlePass battlePass;
    public GameObject islandReachedPopUp;
    public GameObject shipHomeScreen;
    public BonusTime bonusTime;


    /*
    KILLED
    [NonSerialized] public double[] beforeFishTravelTimes = {0,6, 12, 24, 24, 30, 31, 32, 40, 50, 100, 150, 150, 200, 250, 250, 300, 541};
    [NonSerialized] public double[] originalTravelTimes = {0,7, 21, 47, 65, 72, 96, 118, 170, 160, 248, 392, 457, 506, 529, 851, 956, 1041};
    [NonSerialized] public double[] totalTravelTimes = {0,7, 21, 47, 65, 72, 96, 118, 170, 160, 248, 392, 457, 506, 529, 851, 956, 1041};
    KILLED
    */

    //[NonSerialized] public float[] travelTimes = {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
    //KILLED

    public void characterSwapTest()
    {
        equippedCharacter++;
    }

    public void coinsGainedPopUp()
    {
        bonusTime.coinsGainedPopUp();
    }

    public void tooManyTasksPopUp(){
        bonusTime.tooManyTasksPopUp();
    }


    public void reachIsland(){
        if (shipHomeScreen.activeSelf){
            tabScript.navBar.SetActive(false);
            islandReachedPopUp.SetActive(true);
        }
        //tabScript.IslandActivate();
    }

    public void confirmIsland(){
        tabScript.IslandActivate();
        saveCurrentIsland();
    }

    public void questTaskCompleterBuffer()
    {
        islandScript.questCheck(3);
    }

    /*
    KILLED
    public void sailDistance(){
        travelTimes[currentIslandIndex] += battlePass.timeGain;
        saveTravelTime();
    }
    KILLED
    */


    /*
    KILLED
    public void checkIslandStatus(){
        if (travelTimes[currentIslandIndex] >= totalTravelTimes[currentIslandIndex]){
            reachIsland();
        }
    }
    KILLED
    */

    public void completeIsland()
    {
        islandBackgrounds[currentIslandIndex].SetActive(false);
        islandButtons[currentIslandIndex].SetActive(false);
        //exclamationPoints[currentIslandIndex].SetActive(false);
        islandReachedPopUp.SetActive(false);
        currentIslandIndex++;
        if (currentIslandIndex == 4)
        {
            //change to Purple Sea
            currentOceanIndex = 1;
        }
        else if (currentIslandIndex == 8)
        {
            //change to Lava Ocean
            currentOceanIndex = 2;
        }
        else if (currentIslandIndex == 12)
        {
            //change to All Blue
            currentOceanIndex = 3;
        }
        Ocean.SetActive(true);
        oceanAnimation.updateOcean(currentOceanIndex);
        //islandManager.saveData();
        tabScript.IslandActivate();
        saveCurrentIsland();
    }

    public void saveCurrentIsland(){
        CurrentIslandClass currentIslandStorage = new CurrentIslandClass{
            currentIslandStored = currentIslandIndex,
            currentOceanIndex = currentOceanIndex,
        };
        string jsonStorage = JsonUtility.ToJson(currentIslandStorage);
        SaveSystem.SaveIsland(jsonStorage);
    }

    /*
    KILLED
    public void saveTravelTime(){
        TravelTimeClass islandFishStorage = new TravelTimeClass{
            travelTimeList = travelTimes,
        };
        string jsonStorage = JsonUtility.ToJson(islandFishStorage);
        SaveSystem.SaveTravelTime(jsonStorage);
    }
    KILLED
    */
    

    public void loadCurrentIsland(){
        string saveString = SaveSystem.LoadIsland();
        if (saveString != null){
            CurrentIslandClass loadedData = JsonUtility.FromJson<CurrentIslandClass>(saveString);
            currentIslandIndex = loadedData.currentIslandStored;
            currentOceanIndex = loadedData.currentOceanIndex;
        }
        else{
            currentIslandIndex = 0;
        }
    }

    /*
    KILLED
    public void loadTravelTime(){
        string saveString = SaveSystem.LoadTravelTime();
        if (saveString != null){
            TravelTimeClass loadedData = JsonUtility.FromJson<TravelTimeClass>(saveString);
            travelTimes = loadedData.travelTimeList;
        }
    }
    KILLED
    */

    /*
    KILLED
    public void saveTotalTravelTimes(){
        TotalTravelClass totalTravelClass = new TotalTravelClass{
            TotalTravelTimes = totalTravelTimes,
        };
        string jsonStorage = JsonUtility.ToJson(totalTravelClass);
        SaveSystem.SaveTotalTravel(jsonStorage);
    }

    public void loadTotalTravelTimes(){
        string saveString = SaveSystem.LoadTotalTravel();
        if (saveString != null){
            TotalTravelClass loadedData = JsonUtility.FromJson<TotalTravelClass>(saveString);
            totalTravelTimes = loadedData.TotalTravelTimes;
        }
    }
    */

}

/*
KILLED
public class TravelTimeClass
{
    public float[] travelTimeList = {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
}
KILLED
*/

public class CurrentIslandClass
{
    public int currentIslandStored = 0;
    public int currentOceanIndex = 0;
}

/*
KILLED
public class TotalTravelClass
{
    public double[] TotalTravelTimes = new double[18];
}
KILLED
*/
