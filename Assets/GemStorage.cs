using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GemStorage : MonoBehaviour
{
    public PetWalk petWalk;
    [NonSerialized] public int GemTotal = 0;
    public SailingTracker sailingTracker;
    public GameProgress gameProgress;
    public TMP_Text homeGemQ;
    public TMP_Text storeGemQ;


    public void addGems(int GemAmount){
        GemTotal += GemAmount;
        homeGemQ.text = GemTotal.ToString();
        //THIS IS WHERE YOU'D UPDATE ANY RELEVANT TEXT HOLDERS FOR GEMS
        SaveGems();
    }

    public void buy100(){
        GemTotal += 100;
        homeGemQ.text = GemTotal.ToString();
        storeGemQ.text = GemTotal.ToString();
        //IAP CODE
        SaveGems();
    }

    public void buy600(){
        GemTotal += 600;
        homeGemQ.text = GemTotal.ToString();
        storeGemQ.text = GemTotal.ToString();
        //IAP CODE
        SaveGems();
    }

    public void buy1300(){
        GemTotal += 1300;
        homeGemQ.text = GemTotal.ToString();
        storeGemQ.text = GemTotal.ToString();
        //IAP CODE
        SaveGems();
    }

    public void buy4000(){
        GemTotal += 4000;
        homeGemQ.text = GemTotal.ToString();
        storeGemQ.text = GemTotal.ToString();
        //IAP CODE
        SaveGems();
    }

    public void buyCatOne(){
        GemTotal -= 1000;
        homeGemQ.text = GemTotal.ToString();
        storeGemQ.text = GemTotal.ToString();
        //IAP CODE
        SaveGems();
    }

    public void buyCatTwo(){
        GemTotal -= 1000;
        homeGemQ.text = GemTotal.ToString();
        storeGemQ.text = GemTotal.ToString();
        //IAP CODE
        SaveGems();
    }

    public void buyCatThree(){
        GemTotal -= 1000;
        homeGemQ.text = GemTotal.ToString();
        storeGemQ.text = GemTotal.ToString();
        //IAP CODE
        SaveGems();
    }
    
    public void buyFoxOne(){
        GemTotal -= 1000;
        homeGemQ.text = GemTotal.ToString();
        storeGemQ.text = GemTotal.ToString();
        //IAP CODE
        SaveGems();
    }

    public void buyFoxTwo(){
        GemTotal -= 1000;
        homeGemQ.text = GemTotal.ToString();
        storeGemQ.text = GemTotal.ToString();
        //IAP CODE
        SaveGems();
    }

    public void buyWithGems(int gemsNum)
    {
        GemTotal -= gemsNum;
        homeGemQ.text = GemTotal.ToString();
        storeGemQ.text = GemTotal.ToString();
        //IAP CODE
        SaveGems();
    }

    public void SaveGems()
    {
        GemHolder gemHolder = new GemHolder
        {
            gemTotal = GemTotal,
        };
        string jsonStorage = JsonUtility.ToJson(gemHolder);
        SaveSystem.SaveDabloons(jsonStorage);
    }

    public void LoadGems(){
        string saveString = SaveSystem.LoadDabloons();
        if (saveString != null){
            GemHolder loadedData = JsonUtility.FromJson<GemHolder>(saveString);
            GemTotal = loadedData.gemTotal;
        }
        else{
            GemTotal = 10;
        }
        homeGemQ.text = GemTotal.ToString();
    }
}

public class GemHolder
{
    public int gemTotal = 0;
}
