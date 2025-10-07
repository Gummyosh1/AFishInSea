using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
using Random = System.Random;

public class DiceRollMinigame : MonoBehaviour
{
    public GameObject rewardPopUp;
    public Image dice;
    [NonSerialized] public bool playing;
    private float timer = 0;
    private float cutoff = .2f;
    private int cutoffCounter = 0;
    public Sprite[] diceSprites;   
    [NonSerialized] public int index = 0;
    [NonSerialized] public Random random = new Random();
    [NonSerialized] public int baitGain = 0;
    public DailyTaskGift dailyTaskGift;
    public GameObject[] coins;
    private bool rewardPops = false;
    public GameObject rewardText;
    private int moveCounter = 0;
    private bool delay = false;
    private float delayTimer = 0;

    public void GameInit()
    {
        timer = 0;
        cutoff = .2f;
        cutoffCounter = 0;
    }

    public void resetGame(){
        timer = 0;
        playing = false;
        cutoff = .2f;
        cutoffCounter = 0;
        index = 0;
        baitGain = 0;
        rewardPops = false;
        dice.color = Color.white;
        moveCounter = 0;
        delayTimer = 0;
        delay = false;
        dice.transform.position += new Vector3(0, 45f, 0);
    }

    public void FixedUpdate(){
        if (playing){
            timer += Time.deltaTime;
            if (timer >= cutoff){
                int holder = index;
                index = random.Next(0,6);
                while (index == holder){
                    index = random.Next(0,6);
                }

                timer = 0;
                dice.sprite = diceSprites[index];
                cutoffCounter++;
                if (cutoffCounter == 10){
                    cutoff = .35f;
                }
                if (cutoffCounter == 17){
                    cutoff = .5f;
                }
                if (cutoffCounter == 20){
                    cutoff = 1f;
                }
                if (cutoffCounter == 22){
                    baitGain = index + 1;
                    dice.color = Color.green;
                    dailyTaskGift.finishDiceRoll();
                    rewardPops = true;
                    delay = true;
                    rewardText.SetActive(true);
                    for (int i = 0; i <= index; i++){
                        coins[i].SetActive(true);
                    }
                    for (int i = index + 1; i < 6; i++){
                        coins[i].SetActive(false);
                    }
                }
            }
        }

        if (rewardPops)
        {
            if (delay)
            {
                delayTimer += Time.deltaTime;
                if (delayTimer >= 1)
                {
                    delayTimer = 0;
                    delay = false;
                }
            }
            else
            {
                rewardPopUp.SetActive(true);
            }
                /*
            timer += Time.deltaTime;
            if (timer >= .01){
                if (moveCounter < 45){
                    dice.transform.position -= new Vector3(0, 1f, 0);
                    moveCounter++;
                    timer = 0;
                }
                if (moveCounter >= 40){
                    timer = 0;
                }
            }
            */

        }
    }
}
