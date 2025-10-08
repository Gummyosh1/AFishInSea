using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MiniGameFour : MonoBehaviour
{
    public BubbleAnimation[] bubbleAnimations;

    public InventoryKing inventoryKing;
    public BaitBuy baitBuy;
    public EquipmentScript equipmentScript;
    public RodDisplay rodDisplay;
    public FishCollectionBook fishCollectionBook;
    public GameProgress gameProgress;
    public FishSpriteHolder fishSpriteHolder;
    public IslandScript islandScript;
    public FishNames fishNames;


    public GameObject miniGame4Holder;
    public GameObject equipmentSelector;
    public GameObject inventorySelector;
    public GameObject navBar;
    public GameObject inventoryFullPopUp;
    public GameObject rewardPopUp;
    public Button shadow;

    public Slider slider;
    public Image fill;
    public TMP_Text gameProgressText;
    public Color yellow;
    public Color green;

    public TMP_Text scoreText;
    public TMP_Text rewardQuantityText;
    public Image rewardImage;

    private bool gameStart = false;
    private bool gameEndChecker = false;
    private bool gameOver = false;

    private int fishCaughtIndex;
    private int rarity;
    private int bubbleGameCount = 12;
    private int index = 0;
    private int currentScore = 0;
    private int score = 0;
    private int fishEarnedQuantity = 0;
    private bool endDelay = false;
    private float endTimer = 0;

    private float timer = 0;
    private float cutoff = .75f;

    private int[] bubblesToPop = new int[12]; //MUST MATCH BUBBLEGAMECOUNT
    private int[] rewardCutoffs = {300, 700, 900, 1200};


    public void miniGamePlay()
    {
        //CHECKS IF WE HAVE 1 BAIT OF EQUIPPED TYPE
        bool full = inventoryKing.checkFull();
        if (baitBuy.baitTotals[(equipmentScript.equippedBaitRarity * 3) +
        equipmentScript.equippedBaitIndex] > 0 && !full)
        {

            miniGame4Holder.SetActive(true);

            //TURNS CASTING DISPLAY, EQUIPMENT SELECTOR AND INVENTORY SELECTOR OFF
            rodDisplay.rodHolderOff();
            equipmentSelector.SetActive(false);
            inventorySelector.SetActive(false);
            navBar.SetActive(false);

            //DECREMENTS BAIT TOAL & SAVES IT
            baitBuy.baitTotals[(equipmentScript.equippedBaitRarity * 3) +
            equipmentScript.equippedBaitIndex] -= 1;
            baitBuy.SaveBait();

            //SETS FISH LOCATIONS RANDOMLY
            //fishPositioning();
            gameReset();

            bubbleInit();
            gameStart = true;
        }
        else if (full)
        {
            inventoryFullPopUp.SetActive(true);
        }
        else
        {
            rodDisplay.notEnoughScript.ActivateNotEnough();
        }
    }

    public void gameReset()
    {
        for (int i = 0; i < bubbleAnimations.Length; i++)
        {
            bubbleAnimations[i].gameObject.SetActive(false);
            bubbleAnimations[i].resetBubble();
        }
        currentScore = 0;
        gameStart = false;
        gameEndChecker = false;
        gameOver = false;
        fishCaughtIndex = 0;
        rarity = 0;
        bubbleGameCount = 12;
        index = 0;
        currentScore = 0;
        score = 0;
        fishEarnedQuantity = 0;
        timer = 0;
        cutoff = .75f;
        bubblesToPop = new int[12];
        endDelay = false;
        endTimer = 0;

        updateSlider();
    }

    public void exitRewards()
    {
        rewardPopUp.SetActive(false);
        miniGame4Holder.SetActive(false);
        equipmentScript.autoEquip();
        rodDisplay.setupRod();
        rodDisplay.rodHolderOn();

        equipmentSelector.SetActive(true);
        inventorySelector.SetActive(true);
        navBar.SetActive(true);
    }

    public void calcFish()
    {
        fishCaughtIndex = 0;
        rarity = equipmentScript.equippedBaitRarity;
        int gambit = UnityEngine.Random.Range(0, 1004);
        if (gambit == 444)
        {
            rarity = 4;
        }

        if (rarity == 0)
        {
            fishCaughtIndex = UnityEngine.Random.Range(0, 10);
        }
        else if (rarity == 1)
        {
            fishCaughtIndex = UnityEngine.Random.Range(0, 7);
        }
        else if (rarity == 2)
        {
            fishCaughtIndex = UnityEngine.Random.Range(0, 4);
        }
        else if (rarity == 3)
        {
            fishCaughtIndex = UnityEngine.Random.Range(0, 2);
        }
        else if (rarity == 4)
        {
            fishCaughtIndex = UnityEngine.Random.Range(0, 8);
        }
    }

    public void bubbleInit()
    {
        //RESETS BUBBLE LIST
        for (int i = 0; i < bubblesToPop.Length; i++)
        {
            bubblesToPop[i] = 0;
        }

        bubblesToPop = GenerateUniqueRandomArray(bubbleGameCount, 0, 31);

        /*for (int i = 0; i < bubblesToPop.Length; i++)
        {
            Debug.Log(bubblesToPop[i]);
        }

        Debug.Log("____________");*/

    }


    private int[] GenerateUniqueRandomArray(int length, int min, int max) //MADE BY GPT
    {
        // Create a list of all possible values
        int rangeSize = max - min + 1;
        int[] allValues = new int[rangeSize];
        for (int i = 0; i < rangeSize; i++)
        {
            allValues[i] = min + i;
        }

        // Shuffle the array using Fisher-Yates algorithm
        System.Random rng = new System.Random();
        for (int i = rangeSize - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            int temp = allValues[i];
            allValues[i] = allValues[j];
            allValues[j] = temp;
        }

        // Create the result array with the first 'length' elements
        int[] result = new int[length];
        for (int i = 0; i < length; i++)
        {
            result[i] = allValues[i];
        }

        return result;
    }

    /*public void startBubble(int index)
    {
        int colorI = Random.Range(0, 3);
        bubbleAnimations[index].setup(true, colorI,true);
    }*/

    public void stopBubble(int index)
    {
        bubbleAnimations[index].setup(false, -1, false);
        bubbleAnimations[index].succeeded = true;
        currentScore += 1;
        score += 100;

        updateSlider();
    }

    public void updateSlider()
    {
        int cap0 = bubbleGameCount;
        int cur0 = currentScore;
        slider.maxValue = cap0;
        slider.value = cur0;

        gameProgressText.SetText(cur0 + "/" + cap0);

        if (cur0 != cap0)
        {
            fill.color = yellow;
        }
        else
        {
            fill.color = green;
        }

        if (gameOver)
        {
            fill.color = green;
        }

    }


    public void FixedUpdate()
    {
        if (gameStart)
        {
            timer += Time.deltaTime;

            if (timer >= cutoff)
            {
                Debug.Log("BUBBLESTOPOP[INDEX] = " + bubblesToPop[index]);
                int indexCalled = bubblesToPop[index];
                int colorI = Random.Range(0, 3);
                bubbleAnimations[indexCalled].setup(true, colorI, true);

                index++;
                timer = 0;
                cutoff -= .05f;

                if (index >= bubbleGameCount)
                {
                    gameStart = false;
                    gameEndChecker = true;
                }
            }
        }





        if (gameEndChecker)
        {
            gameOver = true;

            for (int i = 0; i < 12; i++)
            {
                int indexCalled = bubblesToPop[i];
                if (bubbleAnimations[indexCalled].gameObject.activeSelf)
                {
                    gameOver = false;
                    break;
                }
            }

            if (gameOver)
            {
                if (score >= rewardCutoffs[0]) { fishEarnedQuantity = 1; }

                if (score >= rewardCutoffs[1]) { fishEarnedQuantity = 2; }

                if (score >= rewardCutoffs[2]) { fishEarnedQuantity = 3; }

                if (score >= rewardCutoffs[3]) { fishEarnedQuantity = 6; }


                Debug.Log("GAME IS OVER GAME IS OVER GAME IS OVER!");
                updateSlider();

                calcFish();

                //SETS PLAYER SCORE TO THE TEXT IN POPUP
                if (rarity != 4)
                {
                    scoreText.text = score.ToString();
                    rewardQuantityText.text = fishEarnedQuantity.ToString() + " " +
                    rodDisplay.fishNames.nameLists[gameProgress.currentOceanIndex][rarity][fishCaughtIndex];
                }
                else
                {
                    scoreText.text = score.ToString() + " " +
                    rodDisplay.fishNames.nameLists[4][0][fishCaughtIndex];;
                    rewardQuantityText.text = "1";
                }

                //SETS CORRECT INFO TO THE FISH YOU'LL CATCH AND CATCHES THE FISH
                if (fishEarnedQuantity != 0)
                {
                    if (rarity != 4)
                    {
                        inventoryKing.catchFish(rarity, fishCaughtIndex, fishEarnedQuantity);
                        fishCollectionBook.registerFish(fishCaughtIndex, rarity, gameProgress.currentOceanIndex, fishEarnedQuantity);
                    }
                    else
                    {
                        inventoryKing.catchFish(4, fishCaughtIndex, 1);
                        fishCollectionBook.registerFish(fishCaughtIndex, rarity, 4, 1);
                    }

                }

                //SETS THE FISH CAUGHT TO THE CORRECT SPRITE
                if (rarity != 4)
                {
                    rewardImage.sprite = fishSpriteHolder.spriteLists[gameProgress.currentOceanIndex][rarity][fishCaughtIndex];
                }
                else
                {
                    rewardImage.sprite = fishSpriteHolder.spriteLists[4][0][fishCaughtIndex];
                }

                islandScript.questCheck(1, score, 3);

                shadow.enabled = false;
                rewardPopUp.SetActive(true);
                endDelay = true;
                gameOver = false;
                gameEndChecker = false;
            }
        }

        if (endDelay)
        {
            endTimer += Time.deltaTime;
            if (endTimer >= .5)
            {
                shadow.enabled = true;
                endTimer = 0;
                endDelay = false;
            }
        }
    }
    
    
}
