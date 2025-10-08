using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
using Random = System.Random;

public class BaitMinigame : MonoBehaviour
{
    public NotEnoughScript NotEnough;
    public GameObject goAgainButton;
    public IslandScript islandScript;
    public GameObject baitGambleObj;
    public Button grayBackground;
    public Button ffButton;
    public GameObject popUp;
    public TMP_Text baitText;
    public GameObject homeBar;
    public GameObject grayBackground2;
    public BaitBuy baitBuy;
    public Image winningItem;
    public Image[] slots;
    public Image[] baits;
    public Sprite[] possibleBaitsNormal;
    public Sprite[] possibleBaitsFancy;
    public Sprite[] possibleBaitsExtravagant;
    public Sprite[] possibleBaitsPristine;
    [NonSerialized] public int[] baitOdds = {49, 74, 90, 99}; // ALL VALS ARE <=
    [NonSerialized] public int[] exactBaitOdds = {0, 1, 2};
    public Color green;
    public Color white;
    public Color red;
    private bool gambling = false;
    private int index = 0;
    private float timer = 0;
    private float cutoff = 0;
    private int cutoffCounter = 0;
    private int cutoffCounterCounter = 0;
    private Random random = new Random();
    private int ranVal = 0; 
    private int specificBaitVal = 0;
    private int firstCut = 16;
    private int secondCut = 8;
    private int thirdCut = 4;
    private int baitGained = 1;
    private bool waiting = false;
    public EquipmentScript equipmentScript;
    private int FFIndex = 0;
    private bool ff = false;


    public void init(){
        equipmentScript.baitInit();
    }


    //slots[0].color = new Color(91, 255, 61, 255); //GREEN
    //slots[0].color = new Color(255, 255, 255, 255); //WHITE

    public void gamble()
    {
        if (baitBuy.baitCoinTotal > 0)
        {
            slots[0].color = white;
            slots[1].color = white;
            slots[2].color = white;
            slots[3].color = white;

            ffButton.enabled = true;
            homeBar.SetActive(false);
            baitGambleObj.SetActive(true);
            baitBuy.baitCoinTotal--;
            giveReward();
            cutoff = 0.1f;
            cutoffCounter = 0;
            cutoffCounterCounter = 0;
            timer = 0;
            index = 0;
            slots[0].color = red;
            gambling = true;
        }
        else
        {
            if (!NotEnough.gameObject.activeSelf)
            {
                NotEnough.ActivateNotEnough();
            }
        }
    }
    
    public void gambleAgain(){
        if (baitBuy.baitCoinTotal > 0){
            popUp.SetActive(false);
            //slots[index].color = white;
            grayBackground.enabled = false;
            grayBackground2.SetActive(false);

            slots[0].color = white;
            slots[1].color = white;
            slots[2].color = white;
            slots[3].color = white;

            ffButton.enabled = true;
            homeBar.SetActive(false);
            baitGambleObj.SetActive(true);
            baitBuy.baitCoinTotal--;
            giveReward();
            cutoff = 0.1f;
            cutoffCounter = 0;
            cutoffCounterCounter = 0;
            timer = 0;
            index = 0;
            slots[0].color = red;
            gambling = true;
        }
    }

    public void fastForward()
    {
        ff = true;
        ffButton.enabled = false;
        if (baitBuy.baitCoinTotal <= 0)
        {
            goAgainButton.SetActive(false);
        }
        else
        {
            goAgainButton.SetActive(true);
        }
    }

    public void giveReward(){
        ranVal = random.Next(0,100);
        specificBaitVal = random.Next(0,3);

        calcCutoffs(ranVal);

        //RANDOMIZE ALL OF THEM THEN SET FOR THE ONE THAT WILL WIN
        //RANDOMIZE THEM All STAGE
        randomizeAllSlots();

        //SET THE ONE THAT WILL WIN
        setWinnerSprite();
        updateBaitTotals();
    }

    public void FixedUpdate(){
        if (gambling){
            timer += Time.deltaTime;
            if (timer >= cutoff && !ff){
                slots[index].color = white;
                index++;
                index %= 4;
                slots[index].color = red;
                timer = 0;
                cutoffCounter++;
                if (cutoffCounterCounter == 0){
                    if (cutoffCounter >= firstCut){
                        cutoff += 0.2f;
                        cutoffCounterCounter += 1;
                        cutoffCounter = 0;
                    }
                }
                if (cutoffCounterCounter == 1){
                    if (cutoffCounter >= secondCut){
                        cutoff += 0.3f;
                        cutoffCounterCounter += 1;
                        cutoffCounter = 0;
                    }
                }
                if (cutoffCounterCounter == 2){
                    if (cutoffCounter >= thirdCut){
                        cutoff += 0.5f;
                        cutoffCounterCounter += 1;
                        cutoffCounter = 0;
                    }
                }
                if (cutoffCounterCounter == 3){
                    gambling = false;
                    slots[index].color = green;
                    waiting = true;
                }
            }
            else if (ff == true){
                timer = .75f;
                cutoffCounter = 0;
                cutoffCounterCounter = 0;
                gambling = false;
                Debug.Log("FF Index is " + FFIndex);
                slots[FFIndex].color = green;
                slots[(FFIndex + 1) % 4].color = white;
                slots[(FFIndex + 2) % 4].color = white;
                slots[(FFIndex + 3) % 4].color = white;
                waiting = true;
                ff = false;
            }
        }
        else if (waiting){
            timer += Time.deltaTime;
            if (timer >= .75){
                popUp.SetActive(true);
                //slots[index].color = white;
                grayBackground.enabled = true;
                grayBackground2.SetActive(true);
                waiting = false;
                timer = 0;
            }
        }
    }

    public void exitMinigame(){
        popUp.SetActive(false);
        baitGambleObj.SetActive(false);
        grayBackground.enabled = false;
        grayBackground2.SetActive(false);
        homeBar.SetActive(true);
    }


    public void calcCutoffs(int ranVal){
        if (ranVal <= baitOdds[0]){
            firstCut = 16;
            secondCut = 8;
            thirdCut = 4;
        }
        else if (ranVal <= baitOdds[1]){
            firstCut = 14;
            secondCut = 10;
            thirdCut = 5;
        }
        else if (ranVal <= baitOdds[2]){
            firstCut = 18;
            secondCut = 7;
            thirdCut = 5;
        }
        else if (ranVal <= baitOdds[3]){
            firstCut = 12;
            secondCut = 12;
            thirdCut = 7;
        }
    }


    public void randomizeAllSlots(){
        int ran = random.Next(0,3);
        if (ran == 0){
            baits[0].sprite = possibleBaitsNormal[0];
        }
        if (ran == 1){
            baits[0].sprite = possibleBaitsNormal[1];
        }
        if (ran == 2){
            baits[0].sprite = possibleBaitsNormal[2];
        }

        ran = random.Next(0,3);
        if (ran == 0){
                baits[1].sprite = possibleBaitsFancy[0];
            }
            if (ran == 1){
                baits[1].sprite = possibleBaitsFancy[1];
            }
            if (ran == 2){
                baits[1].sprite = possibleBaitsFancy[2];
            }

        ran = random.Next(0,3);
        if (ran == 0){
                baits[2].sprite = possibleBaitsExtravagant[0];
            }
            if (ran == 1){
                baits[2].sprite = possibleBaitsExtravagant[1];
            }
            if (ran == 2){
                baits[2].sprite = possibleBaitsExtravagant[2];
            }

        ran = random.Next(0,3);
        if (ran == 0){
                baits[3].sprite = possibleBaitsPristine[0];
            }
            if (ran == 1){
                baits[3].sprite = possibleBaitsPristine[1];
            }
            if (ran == 2){
                baits[3].sprite = possibleBaitsPristine[2];
            }
    }



    public void setWinnerSprite(){
        if (ranVal <= baitOdds[0]){
            FFIndex = 0;
            if (specificBaitVal == 0){
                baits[0].sprite = possibleBaitsNormal[0];
                winningItem.sprite = possibleBaitsNormal[0];
            }
            if (specificBaitVal == 1){
                baits[0].sprite = possibleBaitsNormal[1];
                winningItem.sprite = possibleBaitsNormal[1];
            }
            if (specificBaitVal == 2){
                baits[0].sprite = possibleBaitsNormal[2];
                winningItem.sprite = possibleBaitsNormal[2];
            }
        }
        else if (ranVal <= baitOdds[1]){
            FFIndex = 1;
            if (specificBaitVal == 0){
                baits[1].sprite = possibleBaitsFancy[0];
                winningItem.sprite = possibleBaitsFancy[0];
            }
            if (specificBaitVal == 1){
                baits[1].sprite = possibleBaitsFancy[1];
                winningItem.sprite = possibleBaitsFancy[1];
            }
            if (specificBaitVal == 2){
                baits[1].sprite = possibleBaitsFancy[2];
                winningItem.sprite = possibleBaitsFancy[2];
            }
        }
        else if (ranVal <= baitOdds[2]){
            FFIndex = 2;
            if (specificBaitVal == 0){
                baits[2].sprite = possibleBaitsExtravagant[0];
                winningItem.sprite = possibleBaitsExtravagant[0];
            }
            if (specificBaitVal == 1){
                baits[2].sprite = possibleBaitsExtravagant[1];
                winningItem.sprite = possibleBaitsExtravagant[1];
            }
            if (specificBaitVal == 2){
                baits[2].sprite = possibleBaitsExtravagant[2];
                winningItem.sprite = possibleBaitsExtravagant[2];
            }
        }
        else if (ranVal <= baitOdds[3]){
            FFIndex = 3;
            if (specificBaitVal == 0){
                baits[3].sprite = possibleBaitsPristine[0];
                winningItem.sprite = possibleBaitsPristine[0];
            }
            if (specificBaitVal == 1){
                baits[3].sprite = possibleBaitsPristine[1];
                winningItem.sprite = possibleBaitsPristine[1];
            }
            if (specificBaitVal == 2){
                baits[3].sprite = possibleBaitsPristine[2];
                winningItem.sprite = possibleBaitsPristine[2];
            }
        }
    }

    public void updateBaitTotals(){
        Debug.Log(ranVal);
        Debug.Log(specificBaitVal);
        Debug.Log("______________");
        if (ranVal <= baitOdds[0])
        {
            if (specificBaitVal == 0)
            {
                baitBuy.baitTotals[0] += baitGained; //WORM
                baitText.text = "NORMAL\nWorm moment";
            }
            if (specificBaitVal == 1)
            {
                baitBuy.baitTotals[1] += baitGained; //LADYBUG
                baitText.text = "NORMAL\nLucky catch!";
            }
            if (specificBaitVal == 2)
            {
                baitBuy.baitTotals[2] += baitGained; //SHRIMP
                baitText.text = "NORMAL\nOne of the longest living creatures";
            }

            islandScript.questCheck(4, 0, 1);//0 IS RARITY, 1 IS QUANTITY
        }
        else if (ranVal <= baitOdds[1])
        {
            if (specificBaitVal == 0)
            {
                baitBuy.baitTotals[3] += baitGained; //FIREFLY
                baitText.text = "FANCY\nWhat a pull! Maybe i could use it as a lantern";
            }
            if (specificBaitVal == 1)
            {
                baitBuy.baitTotals[4] += baitGained; //MINNOW
                baitText.text = "FANCY\nA harmless fish who roams the seas";
            }
            if (specificBaitVal == 2)
            {
                baitBuy.baitTotals[5] += baitGained; //SANDWICH
                baitText.text = "FANCY\nA delicious treat for anyone on land, or even in the sea!";
            }

            islandScript.questCheck(4, 1, 1);//0 IS RARITY, 1 IS QUANTITY
        }
        else if (ranVal <= baitOdds[2])
        {
            if (specificBaitVal == 0)
            {
                baitBuy.baitTotals[6] += baitGained; //SHRIMPTWO
                baitText.text = "EXTRAVAGANT\nSir Shrimpillius Maximus, ruler of the seventh domain";
            }
            if (specificBaitVal == 1)
            {
                baitBuy.baitTotals[7] += baitGained; //BUTTERFLY
                baitText.text = "EXTRAVAGANT\nA beautiful creature. Fish have always wondered how it tastes...";
            }
            if (specificBaitVal == 2)
            {
                baitBuy.baitTotals[8] += baitGained; //BUTTERFLY TWO
                baitText.text = "EXTRAVAGANT\nA beautiful creature. Fish have always wondered how it tastes...";
            }

            islandScript.questCheck(4, 2, 1);//0 IS RARITY, 1 IS QUANTITY
        }
        else if (ranVal <= baitOdds[3])
        {
            if (specificBaitVal == 0)
            {
                baitBuy.baitTotals[9] += baitGained; //DRAGONFLY
                baitText.text = "PRISTINE\nFloats like a butterfly, yet stings like a bee";
            }
            if (specificBaitVal == 1)
            {
                baitBuy.baitTotals[10] += baitGained; //DRAGONFLY TWO
                baitText.text = "PRISTINE\nFloats like a butterfly, yet stings like a bee";
            }
            if (specificBaitVal == 2)
            {
                baitBuy.baitTotals[11] += baitGained; //PICKLE
                baitText.text = "PRISTINE\nIt's a pickle";
            }

            islandScript.questCheck(4, 3, 1);//0 IS RARITY, 1 IS QUANTITY
        }
        baitBuy.addBait();
    }
}
