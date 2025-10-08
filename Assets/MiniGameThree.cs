using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class MiniGameThree : MonoBehaviour
{
    public IslandScript islandScript;
    public RodDisplay rodDisplay;
    public BaitBuy baitBuy;
    public EquipmentScript equipmentScript;
    public InventoryKing inventoryKing;
    public FishCollectionBook fishCollectionBook;
    public FishSpriteHolder fishSpriteHolder;
    public GameProgress gameProgress;
    public GameObject equipmentSelector;
    public GameObject inventorySelector;
    public GameObject circlesHolder;
    public GameObject navBar;
    public MiniGame3Animation[] miniGame3Animations;
    public Sprite[] circleSprites; // 0 EMPTY, 1 SELECTED, 2 CORRECT, 3 WRONG
    public Image[] circleImages;
    public GameObject miniGame3Holder;
    public GameObject rewardPopUp;
    public GameObject inventoryFullPopUp;
    public TMP_Text scoreText;
    public Image rewardImage;
    public TMP_Text rewardQuantityText;
    private bool gameStart = false;

    //PART 1
    private bool round1 = false;
    private bool round1Init = false;
    private bool initCall = false;
    private bool initCall2 = false;
    private bool delayInit = false;
    private int round1SetupIndex = 0;
    private float round1TileDelay = .2f;

    //PART 2
    private bool round2 = false;
    private bool round2Init = false;
    private bool initCallPart2 = false;
    private bool initCall2Part2 = false;
    private bool delayInitPart2 = false;
    private int round2SetupIndex = 3;
    private float round2TileDelay = .2f;
    private bool initInTheInit = false;
    private bool initInTheInitInit = false;
    //private bool wrong = false;

    //PART 3
    private bool round3 = false;
    private bool round3Init = false;
    private bool initCallPart3 = false;
    private bool initCall2Part3 = false;
    private bool delayInitPart3 = false;
    private int round3SetupIndex = 6;
    private float round3TileDelay = .2f;
    private bool initInTheInitPart3 = false;
    private bool initInTheInitInitPart3 = false;
    private bool bypass = false;
    private bool bypass2 = false;
    private bool finalFlip = false;
    private bool finalFlip2 = false;
    private bool killua = false;
    private bool luffy = false;
    private bool wonGame = false;



    private bool part1Done = false;
    private bool part2Done = false;
    private bool endGameBool = false;

    [NonSerialized] public int[] guessedTiles = new int[9];
    [NonSerialized] public int guessIndex = 0;

    private float timer = 0;
    //private float timer2 = 0;
    private int setupIndex = 0;
    private int[] rewardLevel = new int[4];
    private int[] rewardCutoffs = {150, 500, 1000, 2200};
    [NonSerialized] public int[] correctTiles = new int[9];
    private bool delay = false;
    private float delayTime = 1f;
    private float delayTimer = 0;
    private bool rewardPop = false;
    private int score = 0;
    private int fishEarnedQuantity = 0;
    private int fishCaughtIndex = 0;
    private int rarity = 0;

    //INDEX 0 IS THE HIGHEST


    public void miniGamePlay()
    {
        //CHECKS IF WE HAVE 1 BAIT OF EQUIPPED TYPE
        bool full = inventoryKing.checkFull();
        if (baitBuy.baitTotals[(equipmentScript.equippedBaitRarity * 3) +
        equipmentScript.equippedBaitIndex] > 0 && !full)
        {

            miniGame3Holder.SetActive(true);

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

            correctTileInit();
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


    public void gameReset(){
        for (int i = 0; i < miniGame3Animations.Length; i++)
        {
            miniGame3Animations[i].gameObject.SetActive(true);
            miniGame3Animations[i].gameObject.GetComponent<Button>().enabled = false;
            miniGame3Animations[i].setDefaultImage();
            miniGame3Animations[i].YBFinish = false;
            miniGame3Animations[i].BYFinish = false;
            miniGame3Animations[i].YRFinish = false;
            miniGame3Animations[i].GYFinish = false;
        }
        resetCircles();
        round1 = false;
        round1Init = false;
        initCall = false;
        initCall2 = false;
        delayInit = false;
        round1SetupIndex = 0;
        round1TileDelay = .2f;
        circlesHolder.SetActive(true);

        round2 = false;
        round2Init = false;
        initCallPart2 = false;
        initCall2Part2 = false;
        delayInitPart2 = false;
        round2SetupIndex = 3;
        round2TileDelay = .2f;
        initInTheInit = false;
        initInTheInitInit = false;
        //wrong = false;

        round3 = false;
        round3Init = false;
        initCallPart3 = false;
        initCall2Part3 = false;
        delayInitPart3 = false;
        round3SetupIndex = 6;
        round3TileDelay = .2f;
        initInTheInitPart3 = false;
        initInTheInitInitPart3 = false;


        part1Done = false;
        part2Done = false;
        endGameBool = false;
        bypass = false;
        bypass2 = false;
        finalFlip = false;
        finalFlip2 = false;
        killua = false;
        luffy = false;
        wonGame = false;


        correctTiles = new int[9];
        round1Init = false;
        initInTheInit = false;
        initInTheInitInit = false;
        guessedTiles = new int[9];
        part1Done = false;
        round1 = false;
        timer = 0;
        //timer2 = 0;
        score = 0;
        setupIndex = 0;
        guessIndex = 0;
        delayTime = 1f;
        fishEarnedQuantity = 0;
        round1SetupIndex = 0;
        delay = false;
        rewardLevel = new int[4];
    }
    
    public void exitRewards(){
        rewardPopUp.SetActive(false);
        miniGame3Holder.SetActive(false);
        equipmentScript.autoEquip();
        rodDisplay.setupRod();
        rodDisplay.rodHolderOn();

        equipmentSelector.SetActive(true);
        inventorySelector.SetActive(true);
        navBar.SetActive(true);
    }

    public void calcFish(){
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

    private void turnButtons(bool a)
    {
        for (int i = 0; i < 9; i++)
        {
            miniGame3Animations[i].gameObject.GetComponent<Button>().enabled = a;
        }
    }
    
    public void geussTile(int tile)
    {
        //Debug.Log("GUESSING TILE");
        guessedTiles[guessIndex] = tile;
        //puts the tile that you guessed at the index of how many guesses you've taken so far
        if (tile == correctTiles[guessIndex])
        {
            circleImages[guessIndex].sprite = circleSprites[2];
            if (guessIndex != circleImages.Length - 1) {circleImages[guessIndex + 1].sprite = circleSprites[1];}

            miniGame3Animations[tile].YBFinish = false;
            if (guessIndex < 8)
            {
                miniGame3Animations[tile].yellowToBlueFlip = true;
            }
            else
            {
                miniGame3Animations[tile].yellowToBlueFlip = true;
                delay = true;
                delayTime = 1.5f;
            }
            addScore(); //INCREMENTS OUR SCORE BASED ON THE GUESS INDEX

            guessIndex++;
            if (guessIndex > 2 && !part1Done)
            {
                //Debug.Log("SUCCESFULLY REACHED 3 ON GUESS INDEX");
                guessIndex = 0;
                turnButtons(false);
                round2 = true;
                delay = true;
                delayTime = 1f;
                part1Done = true;
            }

            if (guessIndex > 5 && !part2Done)
            {
                //Debug.Log("SUCCESFULLY REACHED 6 ON GUESS INDEX");
                guessIndex = 0;
                turnButtons(false);
                round3 = true;
                delay = true;
                delayTime = 1f;
                part2Done = true;
            }

            if (guessIndex > 8)
            {
                wonGame = true;
                endGame();
            }
        }
        else
        {
            miniGame3Animations[tile].YRFinish = false;
            miniGame3Animations[tile].yellowToRedFlip = true;
            circleImages[guessIndex].sprite = circleSprites[3];
            //wrong = true;
            //ENDGAME
            endGame();
        }

    }

    private void correctTileInit()
    {
        List<int> numbers = new List<int>();
        for (int i = 0; i < 9; i++)
            numbers.Add(i);

        // Shuffle
        for (int i = 0; i < numbers.Count; i++)
        {
            int randIndex = UnityEngine.Random.Range(i, numbers.Count);
            int temp = numbers[i];
            numbers[i] = numbers[randIndex];
            numbers[randIndex] = temp;
        }

        for (int i = 0; i < 9; i++)
            correctTiles[i] = numbers[i];
    }


    private void addScore()
    {
        switch (guessIndex)
        {
            case 0:
                score += 50;
                break;
            case 1:
                score += 100;
                break;
            case 2:
                score += 150;
                break;
            case 3:
                score += 200;
                break;
            case 4:
                score += 250;
                break;
            case 5:
                score += 300;
                break;
            case 6:
                score += 350;
                break;
            case 7:
                score += 400;
                break;
            case 8:
                score += 500;
                break;
        }
    }

    private void resetCircles()
    {
        for (int i = 0; i < circleImages.Length; i++)
        {
            circleImages[i].sprite = circleSprites[0];
            circleImages[0].sprite = circleSprites[1];
        }
    }

    public void endGame()
    {
        endGameBool = true;
    }

    public void Update()
    {
        if (gameStart)
        {
            timer += Time.deltaTime;
            //timer2 += Time.deltaTime;
            if (timer >= .1)
            {
                miniGame3Animations[setupIndex].setup = true;
                setupIndex++;
                timer = 0;
                if (setupIndex == 9)
                {
                    gameStart = false;
                    delay = true;
                    delayTime = .25f;
                    round1 = true;
                }
            }
        }



        if (delay)
        {
            delayTimer += Time.deltaTime;
            if (delayTimer > delayTime)
            {
                //Debug.Log("Finished delaying!");
                delay = false;
                delayTimer = 0;
            }
        }

        if (round1 && !delay)
        {
            if (!round1Init)
            {
                round1Init = true;
                timer = round1TileDelay;
            }

            timer += Time.deltaTime;
            if (timer >= round1TileDelay)
            {
                if (!initCall) {miniGame3Animations[correctTiles[round1SetupIndex]].yellowToBlueFlip = true; initCall = true; }
                if (miniGame3Animations[correctTiles[round1SetupIndex]].YBFinish)
                {
                    timer = 0;
                    if (!delayInit) { delay = true; delayInit = true; }
                    delayTime = .25f;
                    //Debug.Log("About to return and delay value is " + delay + " and delayInit is " + delayInit);
                    if (delay) { return; }
                    if (!initCall2) miniGame3Animations[correctTiles[round1SetupIndex]].blueToYellowFlip = true; initCall2 = true;
                    if (miniGame3Animations[correctTiles[round1SetupIndex]].BYFinish)
                    {
                        miniGame3Animations[correctTiles[round1SetupIndex]].YBFinish = false;
                        miniGame3Animations[correctTiles[round1SetupIndex]].BYFinish = false;
                        round1SetupIndex++;
                        if (round1SetupIndex < 3){circleImages[round1SetupIndex].sprite = circleSprites[1]; circleImages[round1SetupIndex - 1].sprite = circleSprites[0];} //NEW
                        //Debug.Log("Round1Setup index is incrementing! It is now " + round1SetupIndex);
                        initCall = false;
                        initCall2 = false;
                        delayInit = false;
                        timer = round1TileDelay;
                        if (round1SetupIndex >= 3)
                        {
                            round1SetupIndex = 0;
                            round1 = false;
                            timer = 0;
                            resetCircles();
                            for (int i = 0; i < miniGame3Animations.Length; i++)
                            {
                                miniGame3Animations[i].gameObject.GetComponent<Button>().enabled = true;
                            }
                        }
                    }
                }
            }
        }


        if (round2 && !delay)
        {
            if (!round2Init)
            {
                if (!initInTheInit)
                {
                    //Debug.Log("IN THE ROUND 2 INIT");
                    miniGame3Animations[correctTiles[0]].setGreen();
                    miniGame3Animations[correctTiles[1]].setGreen();
                    miniGame3Animations[correctTiles[2]].setGreen();
                    initInTheInit = true;
                    delay = true;
                    delayTime = 1f;
                    return;
                }
                if (!initInTheInitInit)
                {
                    //Debug.Log("IN THE ROUND 2 INIT IN THE INIT");
                    //miniGame3Animations[correctTiles[0]].greenToYellowFlip = true;
                    //miniGame3Animations[correctTiles[1]].greenToYellowFlip = true;
                    //miniGame3Animations[correctTiles[2]].greenToYellowFlip = true;
                    initInTheInitInit = true;
                    bypass = true;
                }
                if (bypass/*miniGame3Animations[correctTiles[0]].GYFinish == true*/)
                {
                    //Debug.Log("Finished the green to yellow flip!");
                    round2Init = true;
                    initInTheInit = false;
                    initInTheInitInit = false;
                    timer = round2TileDelay;
                    miniGame3Animations[correctTiles[0]].GYFinish = false;
                    miniGame3Animations[correctTiles[1]].GYFinish = false;
                    miniGame3Animations[correctTiles[2]].GYFinish = false;

                    circleImages[3].sprite = circleSprites[1]; //NEW
                }
            }
            else {
            timer += Time.deltaTime;

            if (timer >= round2TileDelay)
            {
                if (!initCallPart2 && !killua) {miniGame3Animations[correctTiles[round2SetupIndex]].yellowToBlueFlip = true; initCallPart2 = true; }
                if (!killua && miniGame3Animations[correctTiles[round2SetupIndex]].YBFinish)
                {
                    timer = 0;
                    if (!delayInitPart2) { delay = true; delayInitPart2 = true; }
                    delayTime = .25f;
                    //Debug.Log("About to return and delay value is " + delay + " and delayInit is " + delayInitGame2);
                    if (delay) { return; }
                    if (!initCall2Part2) {miniGame3Animations[correctTiles[round2SetupIndex]].blueToYellowFlip = true; initCall2Part2 = true;}
                    if (miniGame3Animations[correctTiles[round2SetupIndex]].BYFinish)
                    {
                        miniGame3Animations[correctTiles[round2SetupIndex]].YBFinish = false;
                        miniGame3Animations[correctTiles[round2SetupIndex]].BYFinish = false;
                        round2SetupIndex++;
                        if (round2SetupIndex < 6){circleImages[round2SetupIndex].sprite = circleSprites[1]; circleImages[round2SetupIndex - 1].sprite = circleSprites[0];} //NEW
                        //Debug.Log("Round2Setup index is incrementing! It is now " + round2SetupIndex);
                        initCallPart2 = false;
                        initCall2Part2 = false;
                        delayInitPart2 = false;
                        timer = round2TileDelay;
                        if (round2SetupIndex >= 6)
                        {
                            finalFlip2 = true;
                        }
                    }
                }
            }
            if (finalFlip2){
                miniGame3Animations[correctTiles[0]].greenToYellowFlip = true;
                miniGame3Animations[correctTiles[1]].greenToYellowFlip = true;
                miniGame3Animations[correctTiles[2]].greenToYellowFlip = true;
                finalFlip2 = false;
                killua = true;
            }
            if (miniGame3Animations[correctTiles[0]].GYFinish == true){
                round2SetupIndex = 3;
                round2 = false;
                timer = 0;
                resetCircles();
                for (int i = 0; i < miniGame3Animations.Length; i++)
                {
                    miniGame3Animations[i].gameObject.GetComponent<Button>().enabled = true;
                }
            }
            }
        } 

        if (round3 && !delay)
        {
            if (!round3Init)
            {
                if (!initInTheInitPart3)
                {
                    //Debug.Log("IN THE ROUND 3 INIT");
                    miniGame3Animations[correctTiles[0]].setGreen();
                    miniGame3Animations[correctTiles[1]].setGreen();
                    miniGame3Animations[correctTiles[2]].setGreen();
                    miniGame3Animations[correctTiles[3]].setGreen();
                    miniGame3Animations[correctTiles[4]].setGreen();
                    miniGame3Animations[correctTiles[5]].setGreen();
                    initInTheInitPart3 = true;
                    delay = true;
                    delayTime = 1f;
                    return;
                }
                if (!initInTheInitInitPart3)
                {
                    //Debug.Log("IN THE ROUND 3 INIT IN THE INIT");
                    //miniGame3Animations[correctTiles[0]].greenToYellowFlip = true;
                    //miniGame3Animations[correctTiles[1]].greenToYellowFlip = true;
                    //miniGame3Animations[correctTiles[2]].greenToYellowFlip = true;
                    //miniGame3Animations[correctTiles[3]].greenToYellowFlip = true;
                    //miniGame3Animations[correctTiles[4]].greenToYellowFlip = true;
                    //miniGame3Animations[correctTiles[5]].greenToYellowFlip = true;
                    initInTheInitInitPart3 = true;
                    bypass2 = true;
                }
                if (bypass2/*miniGame3Animations[correctTiles[0]].GYFinish == true*/)
                {
                    //Debug.Log("Finished the green to yellow flip!");
                    round3Init = true;
                    initInTheInitPart3 = false;
                    initInTheInitInitPart3 = false;
                    timer = round3TileDelay;
                    miniGame3Animations[correctTiles[0]].GYFinish = false;
                    miniGame3Animations[correctTiles[1]].GYFinish = false;
                    miniGame3Animations[correctTiles[2]].GYFinish = false;
                    miniGame3Animations[correctTiles[3]].GYFinish = false;
                    miniGame3Animations[correctTiles[4]].GYFinish = false;
                    miniGame3Animations[correctTiles[5]].GYFinish = false;

                    circleImages[6].sprite = circleSprites[1];
                }
            }
            else {
            timer += Time.deltaTime;

            if (timer >= round3TileDelay)
            {
                if (!initCallPart3 && !luffy) { miniGame3Animations[correctTiles[round3SetupIndex]].yellowToBlueFlip = true; initCallPart3 = true; }
                if (!luffy && miniGame3Animations[correctTiles[round3SetupIndex]].YBFinish)
                {
                    timer = 0;
                    if (!delayInitPart3) { delay = true; delayInitPart3 = true; }
                    delayTime = .25f;
                    //Debug.Log("About to return and delay value is " + delay + " and delayInit is " + delayInitGame2);
                    if (delay) { return; }
                    if (!initCall2Part3) miniGame3Animations[correctTiles[round3SetupIndex]].blueToYellowFlip = true; initCall2Part3 = true;
                    if (miniGame3Animations[correctTiles[round3SetupIndex]].BYFinish)
                    {
                        miniGame3Animations[correctTiles[round3SetupIndex]].YBFinish = false;
                        miniGame3Animations[correctTiles[round3SetupIndex]].BYFinish = false;
                        round3SetupIndex++;
                        if (round3SetupIndex < 9){circleImages[round3SetupIndex].sprite = circleSprites[1]; circleImages[round3SetupIndex - 1].sprite = circleSprites[0];} //NEW
                        //Debug.Log("Round2Setup index is incrementing! It is now " + round3SetupIndex);
                            initCallPart3 = false;
                        initCall2Part3 = false;
                        delayInitPart3 = false;
                        timer = round3TileDelay;
                        if (round3SetupIndex >= 9)
                        {
                            finalFlip = true;
                        }
                    }
                }
            }
            if (finalFlip){
                miniGame3Animations[correctTiles[0]].greenToYellowFlip = true;
                miniGame3Animations[correctTiles[1]].greenToYellowFlip = true;
                miniGame3Animations[correctTiles[2]].greenToYellowFlip = true;
                miniGame3Animations[correctTiles[3]].greenToYellowFlip = true;
                miniGame3Animations[correctTiles[4]].greenToYellowFlip = true;
                miniGame3Animations[correctTiles[5]].greenToYellowFlip = true;
                finalFlip = false;
                luffy = true;
            }
            if (miniGame3Animations[correctTiles[0]].GYFinish == true){
                round3SetupIndex = 6;
                round3 = false;
                timer = 0;
                resetCircles();
                for (int i = 0; i < miniGame3Animations.Length; i++)
                {
                    miniGame3Animations[i].gameObject.GetComponent<Button>().enabled = true;
                }
            }
            }
        }

        if (endGameBool && !delay)
        {

            if (score >= rewardCutoffs[0]) { fishEarnedQuantity = 1; }

            if (score >= rewardCutoffs[1]) { fishEarnedQuantity = 2; }

            if (score >= rewardCutoffs[2]) { fishEarnedQuantity = 3; }

            if (score >= rewardCutoffs[3]) { fishEarnedQuantity = 5; }

            delayTime = 1f;
            delay = true; //HOLDS ON THE TAPPED SPOT FOR 1 SECOND
            for (int i = 0; i < miniGame3Animations.Length; i++)
            {
                miniGame3Animations[i].gameObject.GetComponent<Button>().enabled = false;
            }
            timer = 0;
            rewardPop = true;
            endGameBool = false;

            if (wonGame)
            {
                miniGame3Animations[0].setGreen();
                miniGame3Animations[1].setGreen();
                miniGame3Animations[2].setGreen();
                miniGame3Animations[3].setGreen();
                miniGame3Animations[4].setGreen();
                miniGame3Animations[5].setGreen();
                miniGame3Animations[6].setGreen();
                miniGame3Animations[7].setGreen();
                miniGame3Animations[8].setGreen();
            }
        }

        if (rewardPop && !delay) {
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

            islandScript.questCheck(1, score, 2);
            

            rewardPopUp.SetActive(true);
            rewardPop = false;
        }
    }
        /*
        if (fish1){
            if (!stopRod){
                rodMove();
            }
            else{
                //POINT CALCULATION SPOT
                float x = hook.position.x;
                if (x <= pristineRight[3].position.x && x >= pristineLeft[3].position.x){
                    rewardLevel[3] = 3;
                    fishImages[3].sprite = rewardSprites[3];
                    rewardLevel[3] = PristineScores[3];
                }
                else if (x <= extravagantRight[3].position.x && x >= extravagantLeft[3].position.x){
                    rewardLevel[3] = 2;
                    fishImages[3].sprite = rewardSprites[2];
                    rewardLevel[3] = ExtravagantScores[3];
                }
                else if (x <= fancyRight[3].position.x && x >= fancyLeft[3].position.x){
                    rewardLevel[3] = 1;
                    fishImages[3].sprite = rewardSprites[1];
                    rewardLevel[3] = FancyScores[3];
                }
                else {
                    rewardLevel[3] = 0;
                    fishImages[3].sprite = rewardSprites[0];
                    rewardLevel[3] = NormalScores[3];
                }
                Debug.Log(rewardLevel[3]);
                delay = true; //HOLDS ON THE TAPPED SPOT FOR A SECOND
                timer = 0;
                fish1 = false;
                stopRod = false;
                fish[2].SetActive(true);
                moveUp1 = true;
            }
        }
        if (moveUp1 && !delay){
            timer += Time.deltaTime;
            if (timer > .02){
                if (hook.position.y >= fish[2].transform.position.y){
                    hook.position = new Vector3(xHolder, fish[2].transform.position.y, zHolder);
                    yHolder = fish[2].transform.position.y;
                    moveUp1 = false;

                    screenButton.SetActive(true);
                    hookSpeed = 1.5f;
                    fish2 = true;
                }
                else{
                    yHolder += 1f;
                    hook.position = new Vector3(xHolder, yHolder, zHolder);
                }
                timer = 0;
            }
        }
        if (fish2){
            if (!stopRod){
                rodMove();
            }
            else{
                float x = hook.position.x;
                if (x <= pristineRight[2].position.x && x >= pristineLeft[2].position.x){
                    rewardLevel[2] = 3;
                    fishImages[2].sprite = rewardSprites[3];
                    rewardLevel[2] = PristineScores[2];
                }
                else if (x <= extravagantRight[2].position.x && x >= extravagantLeft[2].position.x){
                    rewardLevel[2] = 2;
                    fishImages[2].sprite = rewardSprites[2];
                    rewardLevel[2] = ExtravagantScores[2];
                }
                else if (x <= fancyRight[2].position.x && x >= fancyLeft[2].position.x){
                    rewardLevel[2] = 1;
                    fishImages[2].sprite = rewardSprites[1];
                    rewardLevel[2] = FancyScores[2];
                }
                else {
                    rewardLevel[2] = 0;
                    fishImages[2].sprite = rewardSprites[0];
                    rewardLevel[2] = NormalScores[2];
                }
                Debug.Log(rewardLevel[2]);
                delay = true; //HOLDS ON THE TAPPED SPOT FOR 1 SECOND
                screenButton.SetActive(false);
                timer = 0;
                fish2 = false;
                stopRod = false;
                fish[1].SetActive(true);
                moveUp2 = true;
            }
        }


        if (moveUp2 && !delay){
            timer += Time.deltaTime;
            if (timer > .02){
                if (hook.position.y >= fish[1].transform.position.y){
                    hook.position = new Vector3(xHolder, fish[1].transform.position.y, zHolder);
                    yHolder = fish[1].transform.position.y;
                    moveUp2 = false;

                    screenButton.SetActive(true);
                    hookSpeed = 2f;
                    fish3 = true;
                }
                else{
                    yHolder += 1f;
                    hook.position = new Vector3(xHolder, yHolder, zHolder);
                }
                timer = 0;
            }
        }
        if (fish3){
            if (!stopRod){
                rodMove();
            }
            else{
                float x = hook.position.x;
                if (x <= pristineRight[1].position.x && x >= pristineLeft[1].position.x){
                    rewardLevel[1] = 3;
                    fishImages[1].sprite = rewardSprites[3];
                    rewardLevel[1] = PristineScores[1];
                }
                else if (x <= extravagantRight[1].position.x && x >= extravagantLeft[1].position.x){
                    rewardLevel[1] = 2;
                    fishImages[1].sprite = rewardSprites[2];
                    rewardLevel[1] = ExtravagantScores[1];
                }
                else if (x <= fancyRight[1].position.x && x >= fancyLeft[1].position.x){
                    rewardLevel[1] = 1;
                    fishImages[1].sprite = rewardSprites[1];
                    rewardLevel[1] = FancyScores[1];
                }
                else {
                    rewardLevel[1] = 0;
                    fishImages[1].sprite = rewardSprites[0];
                    rewardLevel[1] = NormalScores[1];
                }
                Debug.Log(rewardLevel[1]);
                delay = true; //HOLDS ON THE TAPPED SPOT FOR 1 SECOND
                screenButton.SetActive(false);
                timer = 0;
                fish3 = false;
                stopRod = false;
                fish[0].SetActive(true);
                moveUp3 = true;
            }
        }


        if (moveUp3 && !delay){
            timer += Time.deltaTime;
            if (timer > .02){
                if (hook.position.y >= fish[0].transform.position.y){
                    hook.position = new Vector3(xHolder, fish[0].transform.position.y, zHolder);
                    yHolder = fish[0].transform.position.y;
                    moveUp3 = false;

                    screenButton.SetActive(true);
                    hookSpeed = 3f;
                    fish4 = true;
                }
                else{
                    yHolder += 1f;
                    hook.position = new Vector3(xHolder, yHolder, zHolder);
                }
                timer = 0;
            }
        }
        if (fish4){
            if (!stopRod){
                rodMove();
            }
            else{
                float x = hook.position.x;
                if (x <= pristineRight[0].position.x && x >= pristineLeft[0].position.x){
                    rewardLevel[0] = 3;
                    fishImages[0].sprite = rewardSprites[3];
                    rewardLevel[0] = PristineScores[0];
                }
                else if (x <= extravagantRight[0].position.x && x >= extravagantLeft[0].position.x){
                    rewardLevel[0] = 2;
                    fishImages[0].sprite = rewardSprites[2];
                    rewardLevel[0] = ExtravagantScores[0];
                }
                else if (x <= fancyRight[0].position.x && x >= fancyLeft[0].position.x){
                    rewardLevel[0] = 1;
                    fishImages[0].sprite = rewardSprites[1];
                    rewardLevel[0] = FancyScores[0];
                }
                else {
                    rewardLevel[0] = 0;
                    fishImages[0].sprite = rewardSprites[0];
                    rewardLevel[0] = NormalScores[0];
                }
                Debug.Log(rewardLevel[0]);

                for (int i = 0; i < 4; i++){
                    score += rewardLevel[i];
                }

                if (score >= rewardCutoffs[0]){
                    fishEarnedQuantity = 1;
                }
                if (score >= rewardCutoffs[1]){
                    fishEarnedQuantity = 2;
                }
                if (score >= rewardCutoffs[2]){
                    fishEarnedQuantity = 3;
                }
                if (score >= rewardCutoffs[3]){
                    fishEarnedQuantity = 9;
                }


                delay = true; //HOLDS ON THE TAPPED SPOT FOR 1 SECOND
                screenButton.SetActive(false);
                timer = 0;
                fish4 = false;
                stopRod = false;

                rewardPop = true;
            }
        }

        if (rewardPop && !delay){
            //SETS PLAYER SCORE TO THE TEXT IN POPUP
            scoreText.text = score.ToString();
            rewardQuantityText.text = fishEarnedQuantity.ToString();


            //SETS CORRECT INFO TO THE FISH YOU'LL CATCH AND CATCHES THE FISH
            calcFish();
            if (fishEarnedQuantity != 0){
                inventoryKing.catchFish(rarity, fishCaughtIndex, fishEarnedQuantity);
                fishCollectionBook.registerFish(fishCaughtIndex, rarity, gameProgress.currentOceanIndex);
            }
            

            //SETS THE FISH CAUGHT TO THE CORRECT SPRITE
            rewardImage.sprite = fishSpriteHolder.spriteLists[gameProgress.currentIslandIndex][rarity][fishCaughtIndex];

            

            rewardPopUp.SetActive(true);
            rewardPop = false;
        }
        */

    }
