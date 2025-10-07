using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class EnemyScript : MonoBehaviour
{
    public GameObject[] monsterList;

    public FightingProgress fightingProgress;
    private int currentLevelIndex;
    private int MonsterInLevel;

    private Sprite[] CurrentAreaList;

    public Sprite[] Area1;
    public Sprite[] Area2;
    public Sprite[] Area3;
    public Sprite[] Area4;
    public Sprite[] Area5;
    public Sprite[] Area6;
    public Sprite[] Area7;
    public Sprite[] Area8;

    public void NewMonster(){
        setAreaList();

        MonsterInLevel = fightingProgress.currentMonsterInLevelIndex;

        for (int i = 0; i < monsterList.Length; i++){
            monsterList[i].SetActive(false);
        }
        monsterList[fightingProgress.currentMonsterTypeList[MonsterInLevel]].SetActive(true);

        monsterList[fightingProgress.currentMonsterTypeList[MonsterInLevel]].GetComponent<Image>().sprite = CurrentAreaList[fightingProgress.currentLevelList[MonsterInLevel]];
    }

    public void setAreaList(){
        currentLevelIndex = fightingProgress.currentLevelIndex;

        if (currentLevelIndex == 0){
            CurrentAreaList = Area1;
        }
        else if (currentLevelIndex == 1){
            CurrentAreaList = Area2;
        }
        else if (currentLevelIndex == 2){
            CurrentAreaList = Area3;
        }
        else if (currentLevelIndex == 3){
            CurrentAreaList = Area4;
        }
        else if (currentLevelIndex == 4){
            CurrentAreaList = Area5;
        }
        else if (currentLevelIndex == 5){
            CurrentAreaList = Area6;
        }
        else if (currentLevelIndex == 6){
            CurrentAreaList = Area7;
        }
        else if (currentLevelIndex == 7){
            CurrentAreaList = Area8;
        }
    }
}

