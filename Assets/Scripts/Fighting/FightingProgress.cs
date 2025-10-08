using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingProgress : MonoBehaviour
{
    public BattleStage battleStage;
    public Sprite[] Area1Minions;
    public Sprite[] Area2Minions;
    public Sprite[] Area3Minions;
    public Sprite[] Area4Minions;
    public Sprite[] Area5Minions;
    public Sprite[] Area6Minions;
    public Sprite[] Area7Minions;
    public Sprite[] Area8Minions;
    public Sprite[] Bosses;
    public int[] Area1Order = {0,1,2,3,4,5,6,4,5,2,7};
    public int[] Area2Order = {0,1,2,3,4,3,2,0,1,4,5};
    public int[] Area3Order = {0,1,2,3,4,2,0,1,0,4,5};
    public int[] Area4Order = {0,1,2,3,4,5,2,3,0,2,6};
    public int[] Area5Order = {0,1,2,3,1,3,2,3,0,2,4};
    public int[] Area6Order = {0,1,2,3,4,5,3,2,0,4,6};
    public int[] Area7Order = {0,1,2,3,4,5,3,1,0,2,6};
    public int[] Area8Order = {0,1,2,1,0,0,2,1,1,0,3};

    public static int[] Area1HealthVals = {1,2,3,4,5,5,5,6,7,7,15}; //sword does 1 dmg
    public static int[] Area2HealthVals = {20,20,20,25,25,25,25,25,26,28,36}; //sword does 3 dmg
    public static int[] Area3HealthVals = {100,110,120,130,140,150,160,170,180,190,220}; //sword does 10 dmg
    public static int[] Area4HealthVals = {400,400,400,450,450,450,450,450,450,475,500}; //sword does 20 dmg
    public static int[] Area5HealthVals = {1,2,3,4,5,6,7,4,5,2,100};
    public static int[] Area6HealthVals = {1,2,3,4,5,6,7,4,5,2,100};
    public static int[] Area7HealthVals = {1,2,3,4,5,6,7,4,5,2,100};
    public static int[] Area8HealthVals = {1,2,3,4,5,6,7,4,5,2,100};

    public enum Enemy {
        Mage,
        Genie,
        Ghost,
        Lamia,
        Minotaur,
        Mushroom,
        Scorpion,
        Skeleton,
        SkeletonWarrior,
        Slime,
        Wasp,
        Worm,
        Zombie,
    }

    public static int[] Area1MonsterType = {(int)Enemy.Ghost,(int)Enemy.Lamia,(int)Enemy.Scorpion,(int)Enemy.Scorpion,(int)Enemy.Slime,(int)Enemy.Slime,(int)Enemy.Worm,(int)Enemy.Slime,(int)Enemy.Slime,(int)Enemy.Scorpion,(int)Enemy.Genie};
    public static int[] Area2MonsterType = {(int)Enemy.Lamia,(int)Enemy.Scorpion,(int)Enemy.Slime,(int)Enemy.Slime,(int)Enemy.Worm,(int)Enemy.Slime,(int)Enemy.Slime,(int)Enemy.Lamia,(int)Enemy.Scorpion,(int)Enemy.Worm,(int)Enemy.Mage}; //0,1,2,3,4,3,2,0,1,4,5
    public static int[] Area3MonsterType = {(int)Enemy.Mushroom,(int)Enemy.Slime,(int)Enemy.Slime,(int)Enemy.Wasp,(int)Enemy.Zombie,(int)Enemy.Slime,(int)Enemy.Mushroom,(int)Enemy.Slime,(int)Enemy.Mushroom,(int)Enemy.Zombie,(int)Enemy.SkeletonWarrior}; // 0,1,2,3,4,2,0,1,0,4,5
    public static int[] Area4MonsterType = {(int)Enemy.Ghost,(int)Enemy.Ghost,(int)Enemy.Mushroom,(int)Enemy.Slime,(int)Enemy.Slime,(int)Enemy.Zombie,(int)Enemy.Mushroom,(int)Enemy.Slime,(int)Enemy.Ghost,(int)Enemy.Mushroom,(int)Enemy.Genie}; // 0,1,2,3,4,5,2,3,0,2,6
    public static int[] Area5MonsterType = {(int)Enemy.Ghost,(int)Enemy.Slime,(int)Enemy.Slime,(int)Enemy.Zombie,(int)Enemy.Slime,(int)Enemy.Zombie,(int)Enemy.Slime,(int)Enemy.Zombie,(int)Enemy.Ghost,(int)Enemy.Slime, (int)Enemy.SkeletonWarrior}; // 0,1,2,3,1,3,2,3,0,2,4
    public static int[] Area6MonsterType = {(int)Enemy.Slime,(int)Enemy.Slime,(int)Enemy.Slime,(int)Enemy.Wasp,(int)Enemy.Worm,(int)Enemy.Zombie,(int)Enemy.Wasp,(int)Enemy.Slime,(int)Enemy.Slime,(int)Enemy.Worm,(int)Enemy.Mage}; // 0,1,2,3,4,5,3,2,0,4,6
    public static int[] Area7MonsterType = {(int)Enemy.Lamia,(int)Enemy.Mushroom,(int)Enemy.Slime,(int)Enemy.Slime,(int)Enemy.Slime,(int)Enemy.Wasp,(int)Enemy.Slime,(int)Enemy.Mushroom,(int)Enemy.Lamia,(int)Enemy.Slime,(int)Enemy.SkeletonWarrior}; // 0,1,2,3,4,5,3,1,0,2,6
    public static int[] Area8MonsterType = {(int)Enemy.Skeleton,(int)Enemy.Skeleton,(int)Enemy.Skeleton,(int)Enemy.Skeleton,(int)Enemy.Skeleton,(int)Enemy.Skeleton,(int)Enemy.Skeleton,(int)Enemy.Skeleton,(int)Enemy.Skeleton,(int)Enemy.Skeleton,(int)Enemy.Minotaur}; // 0,1,2,1,0,0,2,1,1,0,3

    [System.NonSerialized]
    public int[] currentLevelList; // what order the monsters appear in each level
    [System.NonSerialized]
    public int[] currentHealthList;
    [System.NonSerialized]
    public int[] currentMonsterTypeList;

    //storage variables!!!
    [System.NonSerialized]
    public int currentLevelIndex = 0;
    [System.NonSerialized]
    public int currentMonsterInLevelIndex = 0;
    [System.NonSerialized]
    public int currentHealth;
    [System.NonSerialized]
    public int currentMaxHealth;
    [System.NonSerialized]
    public int currentPlayerSkin = 0;
    [System.NonSerialized]
    public int currentAttackCost;
    [System.NonSerialized]
    public int currentAttackDamage;
    
    //end Storage

    void Awake()
    {
        battleStage.setStage(currentLevelIndex);
        setAreaHealthOrderList();
        //FirstOpenInitialize();
        
    }

    /*public void FirstOpenInitialize(){
        currentMaxHealth = currentHealthList[currentMonsterInLevelIndex];
        currentHealth = currentMaxHealth;
        currentPlayerSkin = 0;
        currentAttackCost = 50;
        currentAttackDamage = 2;
    }*/


    public void setAreaHealthOrderList(){
        if (currentLevelIndex == 0){
            currentLevelList = Area1Order;
            currentHealthList = Area1HealthVals;
            currentMonsterTypeList = Area1MonsterType;
        }
        else if (currentLevelIndex == 1){
            currentLevelList = Area2Order;
            currentHealthList = Area2HealthVals;
            currentMonsterTypeList = Area2MonsterType;
        }
        else if (currentLevelIndex == 2){
            currentLevelList = Area3Order;
            currentHealthList = Area3HealthVals;
            currentMonsterTypeList = Area3MonsterType;
        }
        else if (currentLevelIndex == 3){
            currentLevelList = Area4Order;
            currentHealthList = Area4HealthVals;
            currentMonsterTypeList = Area4MonsterType;
        }
        else if (currentLevelIndex == 4){
            currentLevelList = Area5Order;
            currentHealthList = Area5HealthVals;
            currentMonsterTypeList = Area5MonsterType;
        }
        else if (currentLevelIndex == 5){
            currentLevelList = Area6Order;
            currentHealthList = Area6HealthVals;
            currentMonsterTypeList = Area6MonsterType;
        }
        else if (currentLevelIndex == 6){
            currentLevelList = Area7Order;
            currentHealthList = Area7HealthVals;
            currentMonsterTypeList = Area7MonsterType;
        }
        else if (currentLevelIndex == 7){
            currentLevelList = Area8Order;
            currentHealthList = Area8HealthVals;
            currentMonsterTypeList = Area8MonsterType;
        }
    }
}

