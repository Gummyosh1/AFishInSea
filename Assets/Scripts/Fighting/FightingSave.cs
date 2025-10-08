using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class FightingSave : MonoBehaviour
{

    private static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/";

    public FightingProgress fightingProgress;
    void Awake()
    {
        SaveSystem.Init();
        Load();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            SaveFightingData();
        }

    }

    public void SaveFightingData(){
            FightingStorage fightingStorage = new FightingStorage(){
        
            currentLevelIndex = fightingProgress.currentLevelIndex,
            currentMonsterInLevelIndex = fightingProgress.currentMonsterInLevelIndex,
            currentHealth = fightingProgress.currentHealth,
            currentMaxHealth = fightingProgress.currentMaxHealth,
            currentPlayerSkin = fightingProgress.currentPlayerSkin,
            currentAttackCost = fightingProgress.currentAttackCost,
            currentAttackDamage = fightingProgress.currentAttackDamage,
        };
        string jsonStorage = JsonUtility.ToJson(fightingStorage);
        SaveSystem.SaveFighting(jsonStorage);
    }

    public void Load(){

        string saveString = SaveSystem.LoadFighting();
        if (saveString != null){
            Debug.Log("Loaded the string " + saveString);
            FightingStorage loadedData = JsonUtility.FromJson<FightingStorage>(saveString);

            fightingProgress.currentLevelIndex = loadedData.currentLevelIndex;
            fightingProgress.currentMonsterInLevelIndex = loadedData.currentMonsterInLevelIndex;
            fightingProgress.currentHealth = loadedData.currentHealth;
            fightingProgress.currentMaxHealth = loadedData.currentMaxHealth;
            fightingProgress.currentPlayerSkin = loadedData.currentPlayerSkin;
            fightingProgress.currentAttackCost = loadedData.currentAttackCost;
            fightingProgress.currentAttackDamage = loadedData.currentAttackDamage;
        }
         else {
            fightingProgress.currentLevelIndex = 0;
            fightingProgress.currentMonsterInLevelIndex = 0;
            fightingProgress.currentHealth = 1;
            fightingProgress.currentMaxHealth = 1;
            fightingProgress.currentPlayerSkin = 0;
            fightingProgress.currentAttackCost = 50;
            fightingProgress.currentAttackDamage = 1;
        }
    }
}

public class FightingStorage {
    public int currentLevelIndex;
    public int currentMonsterInLevelIndex;
    public int currentHealth;
    public int currentMaxHealth;
    public int currentPlayerSkin;
    public int currentAttackCost;
    public int currentAttackDamage;
}
