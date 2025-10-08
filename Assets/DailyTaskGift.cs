using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class DailyTaskGift : MonoBehaviour
{
    public GameProgress gameProgress;
    public CharacterMover characterMover;
    public Image characterSprite;
    public BaitBuy baitBuy;
    public GameObject finished;
    public GameObject unfinished;
    public Image progressBarSprite;
    public TMP_Text progressText;
    public Sprite[] bars;
    public SailingTracker sailingTracker;
    public GameObject diceRoll;
    public Image diceSprite;
    public Sprite diceSpriteOrigin;
    public DiceRollMinigame diceRollMinigame;
    public Button greyBack;
    public GameObject homeBar;


    public void characterInit()
    {
        if (gameProgress.equippedCharacter != -1)
        {
            characterSprite.sprite = characterMover.characterHolder[gameProgress.equippedCharacter][0];
            characterSprite.color = new Color(1, 1, 1, 1);
        }
        
    }

    public void taskInit()
    {
        switch (sailingTracker.tasksCompletedToday)
        {
            case 0:
                ProgressZero();
                break;
            case 1:
                ProgressOne();
                break;
            case 2:
                ProgressTwo();
                break;
            case 3:
                if (!sailingTracker.claimed)
                {
                    ProgressThree();
                }
                else { Completed(); }
                break;
            case 4:
                if (!sailingTracker.claimed)
                {
                    ProgressThree();
                }
                else { Completed(); }
                break;
        }
    }

    public void ProgressZero(){
        unfinished.SetActive(true);
        finished.SetActive(false);
        progressBarSprite.sprite = bars[0];
        progressText.text = "0/3";
    }
    public void ProgressOne(){
        unfinished.SetActive(true);
        finished.SetActive(false);
        progressBarSprite.sprite = bars[1];
        progressText.text = "1/3";
    }

    public void ProgressTwo(){
        unfinished.SetActive(true);
        finished.SetActive(false);
        progressBarSprite.sprite = bars[2];
        progressText.text = "2/3";
    }

    public void ProgressThree(){
        unfinished.SetActive(false);
        finished.SetActive(true);
        progressBarSprite.sprite = bars[3];
        progressText.text = "3/3";
    }

    public void Completed(){
        unfinished.SetActive(true);
        finished.SetActive(false);
        progressBarSprite.sprite = bars[3];
        progressText.text = "3/3";
    }

    public void DiceRoll(){
        homeBar.SetActive(false);
        sailingTracker.claimed = true;
        sailingTracker.saveTasksCompletedToday();
        Completed();
        diceRoll.SetActive(true);
        diceSprite.sprite = diceSpriteOrigin;
        diceRollMinigame.GameInit();
        diceRollMinigame.playing = true;
        greyBack.enabled = false;
    }

    public void finishDiceRoll(){
        greyBack.enabled = true;
        diceRollMinigame.playing = false;
        baitBuy.baitCoinTotal += diceRollMinigame.baitGain;
        baitBuy.SaveBait();
    }

    public void baitAdd(){
        baitBuy.baitCoinTotal += 2;
        baitBuy.SaveBait();
    }
}
