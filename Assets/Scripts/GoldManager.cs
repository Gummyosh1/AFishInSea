using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public IslandScript islandScript;
    [NonSerialized]
    public int PlayerGold;

    public TMP_Text goldText;
    private string saveString;

    public void GainGold(int amount){
        PlayerGold += amount;

        islandScript.questCheck(6, amount);
        goldText.text = "" + PlayerGold;
        goldStorage goldStorage = new goldStorage{
            goldAmnt = PlayerGold,
        };
        string jsonStorage = JsonUtility.ToJson(goldStorage);
        SaveSystem.SaveResource(jsonStorage);
    }

    public int getGold(){
        return PlayerGold;
    }


    public void BuyItem(int cost){
        PlayerGold -= cost;
        goldText.text = "" + PlayerGold;
        goldStorage goldStorage = new goldStorage{
            goldAmnt = PlayerGold,
        };
        string jsonStorage = JsonUtility.ToJson(goldStorage);
        SaveSystem.SaveResource(jsonStorage);
    }

    public void loadGold(){
        saveString = SaveSystem.LoadResource();
        if (saveString != null){
            goldStorage loadedData = JsonUtility.FromJson<goldStorage>(saveString);
            PlayerGold = loadedData.goldAmnt;
            goldText.text = "" + PlayerGold;
        }
        else{
            PlayerGold = 0;
        }
    }


}

public class goldStorage{
    public int goldAmnt;
}
