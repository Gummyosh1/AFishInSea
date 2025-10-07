using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePass : MonoBehaviour
{
    [NonSerialized] public float timeGain = 1;
    [NonSerialized] public bool passPurchased = false;

    public void buyPass(){
        passPurchased = true;
        SavePass();
    }

    public void SavePass(){
        BattlePassClass BPStorage = new BattlePassClass{
            passPurchased = passPurchased,
        };
        string jsonStorage = JsonUtility.ToJson(BPStorage);
        SaveSystem.SaveBP(jsonStorage);
    }

    public void LoadPass(){
        string saveString = SaveSystem.LoadBP();
        if (saveString != null){
            BattlePassClass loadedData = JsonUtility.FromJson<BattlePassClass>(saveString);
            passPurchased = loadedData.passPurchased;
        }
        else{
            passPurchased = false;
        }
    }
}

public class BattlePassClass{
    public bool passPurchased = false;
}
