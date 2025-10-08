using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    public Slider slider;
    public FightingProgress fightingProgress;
    public GoldManager goldManager;
    public BattleStage battleStage;
    public Gradient gradient;
    public Image fill;

    public FightingSave fightingSave;

    public TMP_Text enemyHealthText;

    public EnemyScript enemyScript;

    private int maxMonsterHealth = 500;

    public void Start(){
        maxMonsterHealth = fightingProgress.currentHealthList[fightingProgress.currentMonsterInLevelIndex];
        enemyScript.NewMonster();
        InitializeHealthBar();
    }

    public void InitializeHealthBar(){
        slider.maxValue = maxMonsterHealth;
        slider.value = fightingProgress.currentHealth;
        enemyHealthText.SetText(fightingProgress.currentHealth + "/" + maxMonsterHealth);

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void MaxHealth(){
        slider.maxValue = maxMonsterHealth;
        slider.value = maxMonsterHealth;
        fightingProgress.currentHealth = maxMonsterHealth;
        enemyHealthText.SetText(fightingProgress.currentHealth + "/" + maxMonsterHealth);

        fill.color = gradient.Evaluate(1f);
    }

    public void TakeDamage(){
        if (goldManager.PlayerGold >= 50){
        slider.value = fightingProgress.currentHealth - fightingProgress.currentAttackDamage;
        fightingProgress.currentHealth -= fightingProgress.currentAttackDamage;
        enemyHealthText.SetText(fightingProgress.currentHealth + "/" + maxMonsterHealth);

        fill.color = gradient.Evaluate(slider.normalizedValue);
        if (fightingProgress.currentHealth <= 0) {
            if (fightingProgress.currentMonsterInLevelIndex >= fightingProgress.Area1Order.Length - 1){
                fightingProgress.currentLevelIndex += 1;
                fightingProgress.currentMonsterInLevelIndex = -1;
                fightingProgress.setAreaHealthOrderList();
                battleStage.setStage(fightingProgress.currentLevelIndex);
            }
            fightingProgress.currentMonsterInLevelIndex += 1;
            enemyScript.NewMonster();
            maxMonsterHealth = fightingProgress.currentHealthList[fightingProgress.currentMonsterInLevelIndex];
            fightingProgress.currentMaxHealth = maxMonsterHealth;
            MaxHealth();
        }
        fightingSave.SaveFightingData();
        }
    }


    
}
