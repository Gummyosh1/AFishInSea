using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;
using Image = UnityEngine.UI.Image;

public class MiniGameOne : MonoBehaviour
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
    public GameObject navBar;
    public Transform fishEdgeRight;
    public Transform fishEdgeLeft;
    public GameObject[] fish;
    public Image[] fishImages;
    public Sprite[] rewardSprites;
    public GameObject screenButton;
    public Transform hook;
    public GameObject miniGame1Holder;
    public GameObject rewardPopUp;
    public GameObject inventoryFullPopUp;
    public TMP_Text scoreText;
    public Image rewardImage;
    public TMP_Text rewardQuantityText;
    public Transform topStart;
    public Transform rightBound;
    public Transform leftBound;
    public Transform[] pristineLeft;
    public Transform[] pristineRight;
    public Transform[] extravagantRight;
    public Transform[] extravagantLeft;
    public Transform[] fancyRight;
    public Transform[] fancyLeft;
    public GameObject[] rarityPrefabs;
    private bool gameStart = false;
    private float timer = 0;
    private float timer2 = 0;
    private float opacity = 0;
    private float xHolder = 0;
    private float yHolder = 0;
    private float zHolder = 0;
    private bool opacitySetUp = false;
    private bool lineSetUp = false;
    private bool fish1 = false;
    private bool fish2 = false;
    private bool fish3 = false;
    private bool fish4 = false;
    private bool stopRod = false;
    private bool left = false;
    private bool moveUp1 = false;
    private bool moveUp2 = false;
    private bool moveUp3 = false;
    private float hookSpeed = 1f;
    private int[] rewardLevel = new int[4];
    private bool delay = false;
    private float delayTime = .5f;
    private float delayTimer = 0;
    private bool rewardPop = false;
    private int score = 0;
    private int fishEarnedQuantity = 0;
    private int fishCaughtIndex = 0;
    private float upHookSpeed = 2f;
    private int rarity = 0;
    //INDEX 0 IS THE HIGHEST
    private int[] NormalScores = {150, 100, 75, 50}; 
    private int[] FancyScores = {275, 175, 125, 75};
    private int[] ExtravagantScores = {425, 275, 200, 125};
    private int[] PristineScores = {600, 400, 300, 200};
    private int[] rewardCutoffs = {450, 700, 950, 1450};


    public void miniGamePlay()
    {
        //CHECKS IF WE HAVE 1 BAIT OF EQUIPPED TYPE
        bool full = inventoryKing.checkFull();
        if (baitBuy.baitTotals[(equipmentScript.equippedBaitRarity * 3) +
        equipmentScript.equippedBaitIndex] > 0 && !full)
        {

            miniGame1Holder.SetActive(true);

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
            fishPositioning();

            gameReset();
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

    public void fishPositioning(){
        for (int i = 0; i < 4; i++){
            float x_holda = fishEdgeRight.position.x - fishEdgeLeft.position.x;
            int x_dif = (int)MathF.Floor(x_holda);
            int x_location = UnityEngine.Random.Range(0,x_dif);
            fish[i].transform.position = new Vector3(fishEdgeLeft.position.x + x_location, fish[i].transform.position.y, fish[i].transform.position.z);
        }
    }

    public void gameReset(){
        fish[0].SetActive(false);
        fish[1].SetActive(false);
        fish[2].SetActive(false);
        fish[3].GetComponent<Image>().color = new Color (255,255,255,0);
        fish[3].SetActive(true);
        timer = 0;
        timer2 = 0;
        opacity = 0;
        score = 0;
        fishEarnedQuantity = 0;
        hook.position = topStart.position;
        xHolder = topStart.position.x;
        yHolder = topStart.position.y;
        zHolder = topStart.position.z;
        opacitySetUp = false;
        lineSetUp = false;
        fish1 = false;
        fish2 = false;
        fish3 = false;
        fish4 = false;
        stopRod = false;
        moveUp1 = false;
        moveUp2 = false;
        moveUp3 = false;
        delay = false;
        rewardLevel = new int[4];
        fishImages[3].sprite = rewardSprites[4];
        fishImages[2].sprite = rewardSprites[4];
        fishImages[1].sprite = rewardSprites[4];
        fishImages[0].sprite = rewardSprites[4];
    }

    public void rodStop(){
        screenButton.SetActive(false);
        stopRod = true;
    }

    public void calculateWinnings(int fishIndex){

    }

    public void rodMove(){
        timer += Time.deltaTime;
        if (timer > .02){
            if (left == false){
                if (hook.position.x <= rightBound.position.x){
                    xHolder += hookSpeed;
                    hook.position = new Vector3(xHolder, yHolder, zHolder);
                }
                else{
                    xHolder = rightBound.position.x;
                    hook.position = new Vector3(xHolder, yHolder, zHolder);
                    left = true;
                }
            }
            else{
                if (hook.position.x >= leftBound.position.x){
                    xHolder -= hookSpeed;
                    hook.position = new Vector3(xHolder, yHolder, zHolder);
                }
                else{
                    xHolder = leftBound.position.x;
                    hook.position = new Vector3(xHolder, yHolder, zHolder);
                    left = false;
                }
            }
            timer = 0;
        }
    }

    public void exitRewards(){
        rewardPopUp.SetActive(false);
        miniGame1Holder.SetActive(false);
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

        if (rarity == 0){
            fishCaughtIndex = UnityEngine.Random.Range(0,10);
        }
        else if (rarity == 1){
            fishCaughtIndex = UnityEngine.Random.Range(0,7);
        }
        else if (rarity == 2){
            fishCaughtIndex = UnityEngine.Random.Range(0,4);
        }
        else if (rarity == 3){
            fishCaughtIndex = UnityEngine.Random.Range(0,2);
        }
        else if (rarity == 4){
            fishCaughtIndex = UnityEngine.Random.Range(0,8);
        }
    }

    public void Update(){
        if (gameStart){
            timer += Time.deltaTime;
            timer2 += Time.deltaTime;  
            if (timer >= .1 && !opacitySetUp){
                if (opacity < 255){
                    fishImages[3].color = new Color (1f,1f,1f,opacity/255f);
                    Debug.Log(fishImages[3].color);
                }
                else{opacitySetUp = true;}
                
                if (opacity == 240){opacity += 20;}
                else{opacity += 20;}
                
                timer = 0;
            }
            if (timer2 >= .02 && !lineSetUp){
                if (hook.position.y <= fish[3].transform.position.y){
                    hook.position = new Vector3(xHolder, fish[3].transform.position.y, zHolder);
                    yHolder = fish[3].transform.position.y;
                    lineSetUp = true;
                }
                else{
                    yHolder -= upHookSpeed;
                    hook.position = new Vector3(xHolder, yHolder, zHolder);
                }

                timer2 = 0;
            }
            if (opacitySetUp && lineSetUp){
                gameStart = false;
                left = false;
                hookSpeed = 1f;
                timer = 0;
                timer2 = 0;
                screenButton.SetActive(true);
                
                fish1 = true;
            }
        }

        if (delay){
            delayTimer += Time.deltaTime;
            if (delayTimer > delayTime){
                delay = false;
                delayTimer = 0;
            }
        }

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
            if (timer >= .02){
                if (hook.position.y >= fish[2].transform.position.y){
                    hook.position = new Vector3(xHolder, fish[2].transform.position.y, zHolder);
                    yHolder = fish[2].transform.position.y;
                    moveUp1 = false;

                    screenButton.SetActive(true);
                    hookSpeed = 1.5f;
                    fish2 = true;
                }
                else{
                    yHolder += upHookSpeed;
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
            if (timer >= .02){
                if (hook.position.y >= fish[1].transform.position.y){
                    hook.position = new Vector3(xHolder, fish[1].transform.position.y, zHolder);
                    yHolder = fish[1].transform.position.y;
                    moveUp2 = false;

                    screenButton.SetActive(true);
                    hookSpeed = 2f;
                    fish3 = true;
                }
                else{
                    yHolder += upHookSpeed;
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
            if (timer >= .02){
                if (hook.position.y >= fish[0].transform.position.y){
                    hook.position = new Vector3(xHolder, fish[0].transform.position.y, zHolder);
                    yHolder = fish[0].transform.position.y;
                    moveUp3 = false;

                    screenButton.SetActive(true);
                    hookSpeed = 3f;
                    fish4 = true;
                }
                else{
                    yHolder += upHookSpeed;
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
            //scoreText.text = score.ToString();
            //rewardQuantityText.text = fishEarnedQuantity.ToString();


            //SETS CORRECT INFO TO THE FISH YOU'LL CATCH AND CATCHES THE FISH
            calcFish();
            if (fishEarnedQuantity != 0){
                inventoryKing.catchFish(rarity, fishCaughtIndex, fishEarnedQuantity);
                fishCollectionBook.registerFish(fishCaughtIndex, rarity, gameProgress.currentOceanIndex, fishEarnedQuantity);
            }

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

            islandScript.questCheck(1, score, 0);
            

            //SETS THE FISH CAUGHT TO THE CORRECT SPRITE
            rewardImage.sprite = fishSpriteHolder.spriteLists[gameProgress.currentOceanIndex][rarity][fishCaughtIndex];

            

            rewardPopUp.SetActive(true);
            rewardPop = false;
        }


    }
}
