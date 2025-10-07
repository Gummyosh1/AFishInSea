using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;
using Image = UnityEngine.UI.Image;
using System.Data;
using System.Linq.Expressions;
using TMPro;
using Vector2 = UnityEngine.Vector2;

public class MiniGameTwo : MonoBehaviour
{
    public Sprite onWhirlpool;
    public Sprite offWhirlpool;

    public GameObject UpDownOBJ;
    public GameObject LeftRightOBJ;
    public GameObject miniGame2Holder;
    public GameObject equipmentSelector;
    public GameObject inventorySelector;
    public GameObject navBar;
    public GameObject inventoryFullPopUp;
    public GameObject rewardPopUp;

    public GraphicRaycaster raycaster;

    public Camera cameraUI;

    public Transform playerFishTransform;
    public Transform topRightCornerShooter;
    public Transform topLeftCornerShooter;
    public Transform bottomRightCornerShooter;
    public Transform bottomLeftCornerShooter;
    public Transform locationHolder;

    public Image rewardImage;
    private Image WhirlpoolOne;
    private Image WhirlpoolTwo;
    private Image WhirlpoolThree;
    private Image WhirlpoolFour;
    private Image HookOne;
    private Image HookTwo;
    private Image HookThree;
    private Image HookFour;

    public TMP_Text scoreText;
    public TMP_Text rewardQuantityText;


    public IslandScript islandScript;
    public TimerScript timerScript;
    public InventoryKing inventoryKing;
    public BaitBuy baitBuy;
    public EquipmentScript equipmentScript;
    public RodDisplay rodDisplay;
    public UpSensor upSensor;
    public DownSensor downSensor;
    public LeftSensor leftSensor;
    public RightSensor rightSensor;
    public FishCollectionBook fishCollectionBook;
    public GameProgress gameProgress;
    public FishSpriteHolder fishSpriteHolder;

    private int rewardsCollected = 0;
    private int score = 0;
    private int fishEarnedQuantity = 0;
    private int rarity = 0;
    private int fishCaughtIndex = 0;
    private int setupTracker = 0;

    private float upTimer = 0;
    private float downTimer = 0;
    private float leftTimer = 0;
    private float rightTimer = 0;
    //private float gameTimer = 0;
    private float movementDistance = .5f;
    private float moveSpeed = .01f;
    private float delayTimer = 0;
    //private float testTimer = 0;
    private float delayTime = 1f;
    private float introTimer = 0;

    private bool playing = true;
    private bool delay = false;
    private bool done = false;
    private bool swirl = false;
    private bool intro = false;
    private bool introInit = false;

    public GameObject[] maps;
    public GameObject[] whirpools1;
    public Image[] FadeIn;
    public Image[] whirlpool1Images;
    public GameObject[] rewards1;
    public Image[] rewards1Hitboxs;
    public Image[] Paths;

    private int[] whirlpoolsActive = new int[4];
    private int[] rewardsActive = new int[4];

    [NonSerialized] public bool movingUp = false;
    [NonSerialized] public bool movingDown = false;
    [NonSerialized] public bool movingLeft = false;
    [NonSerialized] public bool movingRight = false;
    private bool[] doneWhirls = {false, false, false, false, false, false, false, false};
    private bool gameDone = false;

    
    

    public void MapInit()
    {
        int mapIndex = UnityEngine.Random.Range(0, maps.Length);
        foreach (GameObject item in maps)
        {
            item.SetActive(false);
        }
        maps[mapIndex].SetActive(true);

        //WHIRPOOL ACTIVATION
        whirlpoolsActive[0] = UnityEngine.Random.Range(0, 8);

        whirlpoolsActive[1] = UnityEngine.Random.Range(0, 8);
        while (whirlpoolsActive[1] == whirlpoolsActive[0])
        {
            whirlpoolsActive[1] = UnityEngine.Random.Range(0, 8);
        }

        whirlpoolsActive[2] = UnityEngine.Random.Range(0, 8);
        while (whirlpoolsActive[2] == whirlpoolsActive[0] || whirlpoolsActive[2] == whirlpoolsActive[1])
        {
            whirlpoolsActive[2] = UnityEngine.Random.Range(0, 8);
        }

        whirlpoolsActive[3] = UnityEngine.Random.Range(0, 8);
        while (whirlpoolsActive[3] == whirlpoolsActive[0] || whirlpoolsActive[3] == whirlpoolsActive[1] || whirlpoolsActive[3] == whirlpoolsActive[2])
        {
            whirlpoolsActive[3] = UnityEngine.Random.Range(0, 8);
        }


        //REWARD ACTIVATION 
        rewardsActive[0] = UnityEngine.Random.Range(0, 8);

        rewardsActive[1] = UnityEngine.Random.Range(0, 8);
        while (rewardsActive[1] == rewardsActive[0])
        {
            rewardsActive[1] = UnityEngine.Random.Range(0, 8);
        }

        rewardsActive[2] = UnityEngine.Random.Range(0, 8);
        while (rewardsActive[2] == rewardsActive[0] || rewardsActive[2] == rewardsActive[1])
        {
            rewardsActive[2] = UnityEngine.Random.Range(0, 8);
        }

        rewardsActive[3] = UnityEngine.Random.Range(0, 8);
        while (rewardsActive[3] == rewardsActive[0] || rewardsActive[3] == rewardsActive[1] || rewardsActive[3] == rewardsActive[2])
        {
            rewardsActive[3] = UnityEngine.Random.Range(0, 8);
        }

        switch (mapIndex)
        {
            case 0:
                whirpools1[0].SetActive(false);
                whirpools1[1].SetActive(false);
                whirpools1[2].SetActive(false);
                whirpools1[3].SetActive(false);
                whirpools1[4].SetActive(false);
                whirpools1[5].SetActive(false);
                whirpools1[6].SetActive(false);
                whirpools1[7].SetActive(false);

                whirpools1[whirlpoolsActive[0]].SetActive(true);
                whirpools1[whirlpoolsActive[1]].SetActive(true);
                whirpools1[whirlpoolsActive[2]].SetActive(true);
                whirpools1[whirlpoolsActive[3]].SetActive(true);

                whirpools1[whirlpoolsActive[0]].transform.GetChild(0).GetComponent<Image>().raycastTarget = true;
                whirpools1[whirlpoolsActive[1]].transform.GetChild(0).GetComponent<Image>().raycastTarget = true;
                whirpools1[whirlpoolsActive[2]].transform.GetChild(0).GetComponent<Image>().raycastTarget = true;
                whirpools1[whirlpoolsActive[3]].transform.GetChild(0).GetComponent<Image>().raycastTarget = true;

                WhirlpoolOne = whirpools1[whirlpoolsActive[0]].GetComponent<Image>();
                WhirlpoolTwo = whirpools1[whirlpoolsActive[1]].GetComponent<Image>();
                WhirlpoolThree = whirpools1[whirlpoolsActive[2]].GetComponent<Image>();
                WhirlpoolFour = whirpools1[whirlpoolsActive[3]].GetComponent<Image>();

                rewards1[0].SetActive(false);
                rewards1[1].SetActive(false);
                rewards1[2].SetActive(false);
                rewards1[3].SetActive(false);
                rewards1[4].SetActive(false);
                rewards1[5].SetActive(false);
                rewards1[6].SetActive(false);
                rewards1[7].SetActive(false);

                rewards1[rewardsActive[0]].SetActive(true);
                rewards1[rewardsActive[1]].SetActive(true);
                rewards1[rewardsActive[2]].SetActive(true);
                rewards1[rewardsActive[3]].SetActive(true);

                rewards1Hitboxs[rewardsActive[0]].raycastTarget = true;
                rewards1Hitboxs[rewardsActive[1]].raycastTarget = true;
                rewards1Hitboxs[rewardsActive[2]].raycastTarget = true;
                rewards1Hitboxs[rewardsActive[3]].raycastTarget = true;

                HookOne = rewards1[rewardsActive[0]].GetComponent<Image>();
                HookTwo = rewards1[rewardsActive[1]].GetComponent<Image>();
                HookThree = rewards1[rewardsActive[2]].GetComponent<Image>();
                HookFour = rewards1[rewardsActive[3]].GetComponent<Image>();

                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
        }
    }

    public void miniGamePlay()
    {
        //CHECKS IF WE HAVE 1 BAIT OF EQUIPPED TYPE
        bool full = inventoryKing.checkFull();
        if (baitBuy.baitTotals[(equipmentScript.equippedBaitRarity * 3) +
        equipmentScript.equippedBaitIndex] > 0 && !full)
        {

            MapInit();
            intro = true;
            introSetupOpacity();
            miniGame2Holder.SetActive(true);

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
            playing = true;
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
        for (int i = 0; i < doneWhirls.Length; i++)
        {
            doneWhirls[i] = false;
        }
        rewardsCollected = 0;
        resetTimers();
        done = false;
        gameDone = false;
        timerScript.index = 0;
        timerScript.timer = 0;
        timerScript.done = false;
        timerScript.resetImage();
        //gameTimer = 0;
        upSensor.direction = 0;
        downSensor.direction = 1;
        leftSensor.direction = 2;
        rightSensor.direction = 3;
        playerFishTransform.position = locationHolder.position;
        score = 0;
        fishEarnedQuantity = 0;
        setupTracker = 0;
        introTimer = 0;
        intro = false;
        introInit = false;
    }

    public void introSetupOpacity()
    {
        foreach (Image a in FadeIn)
        {
            a.color = new Color(a.color.r, a.color.g, a.color.b, 0);
            WhirlpoolOne.color = new Color(WhirlpoolOne.color.r, WhirlpoolOne.color.g, WhirlpoolOne.color.b, 0);
            WhirlpoolTwo.color = new Color(WhirlpoolTwo.color.r, WhirlpoolTwo.color.g, WhirlpoolTwo.color.b, 0);
            WhirlpoolThree.color = new Color(WhirlpoolThree.color.r, WhirlpoolThree.color.g, WhirlpoolThree.color.b, 0);
            WhirlpoolFour.color = new Color(WhirlpoolFour.color.r, WhirlpoolFour.color.g, WhirlpoolFour.color.b, 0);
            HookOne.color = new Color(HookOne.color.r, HookOne.color.g, HookOne.color.b, 0);
            HookTwo.color = new Color(HookTwo.color.r, HookTwo.color.g, HookTwo.color.b, 0);
            HookThree.color = new Color(HookThree.color.r, HookThree.color.g, HookThree.color.b, 0);
            HookFour.color = new Color(HookFour.color.r, HookFour.color.g, HookFour.color.b, 0);
        }
    }

    /*
    public bool RaycastFuture(float xOffset = 0, float yOffset = 0){
        Vector3 holder = new Vector3(playerFishTransform.position.x + xOffset, playerFishTransform.position.y + yOffset, playerFishTransform.position.z);
        Vector3 imageScreenPosition = RectTransformUtility.WorldToScreenPoint(cameraUI, holder);

        // Create PointerEventData at that position
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = imageScreenPosition
        };

        // Perform the raycast
        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerData, results);

        bool found = false;
        Debug.Log(results.Count);
        foreach (RaycastResult result in results)
        {
            if (result.gameObject != playerFishTransform.gameObject) // Ignore the image itself
            {
                Debug.Log(result.gameObject.name);
                if (result.gameObject.name.Contains("LeftRightPath") || result.gameObject.name.Contains("UpDownPath")){
                    found = true;
                }
            }
        }
        Debug.Log(found);
        return found;
    }*/

    //TODO
    //ADD GAME OBJECT PARAMETER TO RAYCAST FUTURE AND THEN HAVE EVERY TIME YOU CALL IT HAVE YOU CYCLE THRU ALL 4 OF THE
    //SHOOTERS SO THAT IT CHECKS THE CORNERS INSTEAD OF THE MIDDLE AND IF ANY RETURN FALSE THEN YOU DON'T LET IT MOVE


    public bool IsFishOverlapping(Image targetImage, Transform location, float paramX = 0, float paramY = 0)
    {
        Vector2 offsetPosition = new Vector2(location.position.x + paramX, location.position.y + paramY);
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(cameraUI, offsetPosition);
        return RectTransformUtility.RectangleContainsScreenPoint(targetImage.rectTransform, screenPoint, cameraUI);
    }


    public bool OnPath(float x, float y)
    {
        bool[] go = { false, false, false, false};
        for (int i = 0; i < Paths.Length; i++)
        {
            if (IsFishOverlapping(Paths[i], topLeftCornerShooter, x, y))
            {
                go[0] = true;
            }
            if (IsFishOverlapping(Paths[i], topRightCornerShooter, x, y))
            {
                go[1] = true;
            }
            if (IsFishOverlapping(Paths[i], bottomLeftCornerShooter, x, y))
            {
                go[2] = true;
            }
            if (IsFishOverlapping(Paths[i], bottomRightCornerShooter, x, y))
            {
                go[3] = true;
            }
        }
        foreach (bool a in go)
        {
            if (a == false)
            {
                return false;
            }
        }
        return true;
    }

    public void CheckFishCollision()
    {
        for (int i = 0; i < whirlpool1Images.Length; i++)
        {
            if (whirpools1[i].activeSelf && IsFishOverlapping(whirlpool1Images[i], playerFishTransform))
            {
                if (doneWhirls[i] == false)
                {
                    whirlpoolRandom();
                    whirpools1[i].SetActive(false);
                    Debug.Log("We just succesfully set something to false");
                    doneWhirls[i] = true;
                }
            }
        }

        for (int i = 0; i < rewards1Hitboxs.Length; i++)
        {
            /*if (rewards1[i].activeSelf &&
            (  IsFishOverlapping(rewards1Hitboxs[i], topRightCornerShooter)
            || IsFishOverlapping(rewards1Hitboxs[i], topLeftCornerShooter)
            || IsFishOverlapping(rewards1Hitboxs[i], bottomLeftCornerShooter)
            || IsFishOverlapping(rewards1Hitboxs[i], bottomRightCornerShooter)))*/

            if (rewards1[i].activeSelf && IsFishOverlapping(rewards1[i].GetComponent<Image>(), playerFishTransform))
            {
                Debug.Log("HITTING THE HOOK");
                collectReward();
                rewards1[i].SetActive(false);
            }
        }
    }



    /*public bool RaycastFuture(Transform location, float xOffset = 0, float yOffset = 0)
    {
        Vector3 holder = new Vector3(location.position.x + xOffset, location.position.y + yOffset, location.position.z);
        Vector3 imageScreenPosition = RectTransformUtility.WorldToScreenPoint(cameraUI, holder);
        testTimer += Time.deltaTime;
        // Create PointerEventData at that position
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = imageScreenPosition
        };

        // Perform the raycast
        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerData, results);

        bool found = false;
        foreach (RaycastResult result in results)
        {
            if (testTimer >= 1)
            {
                Debug.Log("RAYCASTED ITEM IS " + result.gameObject.name);
            }
            if (result.gameObject != location.gameObject) // Ignore the image itself
            {
                if (result.gameObject.name.Contains("LeftRightPath") || result.gameObject.name.Contains("UpDownPath"))
                {
                    found = true;
                }
                if (result.gameObject.name.Contains("WhirlPool"))
                {
                    whirlpoolRandom();
                    result.gameObject.GetComponent<Image>().raycastTarget = false;
                    result.gameObject.transform.parent.GetComponent<Image>().sprite = offWhirlpool;
                }
                if (result.gameObject.name.Contains("Hook Hitbox"))
                {
                    Debug.Log("HITTING THE HOOK");
                    collectReward();
                    result.gameObject.transform.parent.gameObject.SetActive(false);
                }
            }
        }
        if (testTimer >= 1)
        {
            testTimer = 0;
        }
        return found;
    }

    public bool RaycastAll(float paramX = 0, float paramY = 0){
        bool found = false;
        if (
        RaycastFuture(topLeftCornerShooter, paramX, paramY) &&
        RaycastFuture(bottomRightCornerShooter, paramX, paramY) &&
        RaycastFuture(bottomLeftCornerShooter, paramX, paramY) &&
        RaycastFuture(topRightCornerShooter, paramX, paramY)){
            found = true;
        }

        return found;
    }*/

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

    public void collectReward(){
        rewardsCollected++;
        if (rewardsCollected == 4)
        {
            timerScript.done = true;
            score = 1500;
            fishEarnedQuantity = 6;
        }
        if (rewardsCollected == 3)
        {
            score = 1000;
            fishEarnedQuantity = 3;
        }
        if (rewardsCollected == 2)
        {
            score = 600;
            fishEarnedQuantity = 2;
        }
        if (rewardsCollected == 1)
        {
            score = 250;
            fishEarnedQuantity = 1;
        }
        if (rewardsCollected == 4)
        {
            //SETS PLAYER SCORE TO THE TEXT IN POPUP
            scoreText.text = score.ToString();
            rewardQuantityText.text = fishEarnedQuantity.ToString();

            //SETS CORRECT INFO TO THE FISH YOU'LL CATCH AND CATCHES THE FISH
            calcFish();
            if (fishEarnedQuantity != 0)
            {
                inventoryKing.catchFish(rarity, fishCaughtIndex, fishEarnedQuantity);
                fishCollectionBook.registerFish(fishCaughtIndex, rarity, gameProgress.currentOceanIndex, fishEarnedQuantity);
            }

            //SETS THE FISH CAUGHT TO THE CORRECT SPRITE
            //rewardImage.sprite = onWhirlpool;
            rewardImage.sprite = fishSpriteHolder.spriteLists[gameProgress.currentOceanIndex][rarity][fishCaughtIndex];



            rewardPopUp.SetActive(true);

            islandScript.questCheck(1, score, 1);


            endGame();
        }
    }

    public void exitRewards(){
        rewardPopUp.SetActive(false);
        miniGame2Holder.SetActive(false);
        equipmentScript.autoEquip();
        rodDisplay.setupRod();
        rodDisplay.rodHolderOn();

        equipmentSelector.SetActive(true);
        inventorySelector.SetActive(true);
        navBar.SetActive(true);
    }

    public void whirlpoolRandom()
    {
        //blockMove = true;
        delay = true;
        delayTime = 1;
        swirl = true;

        int[] BenLassy = new int[4];

        int oldStore = upSensor.direction;

        BenLassy[0] = UnityEngine.Random.Range(0, 4);
        while (oldStore == BenLassy[0])
        {
            BenLassy[0] = UnityEngine.Random.Range(0, 4);
        }

        switch (BenLassy[0])
        {
            case 0:
                BenLassy[1] = 1;
                BenLassy[2] = UnityEngine.Random.Range(0, 2) + 2;
                if (BenLassy[2] == 2)
                {
                    BenLassy[3] = 3;
                }
                else
                {
                    BenLassy[3] = 2;
                }
                break;
            case 1:
                BenLassy[1] = 0;
                BenLassy[2] = UnityEngine.Random.Range(0, 2) + 2;
                if (BenLassy[2] == 2)
                {
                    BenLassy[3] = 3;
                }
                else
                {
                    BenLassy[3] = 2;
                }
                break;
            case 2:
                BenLassy[1] = 3;
                BenLassy[2] = UnityEngine.Random.Range(0, 2);
                if (BenLassy[2] == 0)
                {
                    BenLassy[3] = 1;
                }
                else
                {
                    BenLassy[3] = 0;
                }
                break;
            case 3:
                BenLassy[1] = 2;
                BenLassy[2] = UnityEngine.Random.Range(0, 2);
                if (BenLassy[2] == 0)
                {
                    BenLassy[3] = 1;
                }
                else
                {
                    BenLassy[3] = 0;
                }
                break;
        }

        upSensor.direction = BenLassy[0];
        downSensor.direction = BenLassy[1];
        leftSensor.direction = BenLassy[2];
        rightSensor.direction = BenLassy[3];

        upSensor.manualUp();
        downSensor.manualUp();
        leftSensor.manualUp();
        rightSensor.manualUp();
    }

    public void endGame()
    {
        rewardPopUp.SetActive(true);
        done = true;
    }

    public void resetTimers(){
        upTimer = 0;
        downTimer = 0;  
        leftTimer = 0;  
        rightTimer = 0;
    }

    public void moveUp(){
        if (playing){
            upTimer += Time.deltaTime;
            if (upTimer >= moveSpeed && OnPath(0, movementDistance))
            {
                playerFishTransform.position = new Vector3(playerFishTransform.position.x, playerFishTransform.position.y + movementDistance, playerFishTransform.position.z);
                upTimer = 0;
                CheckFishCollision();
            }
        }
    }

    public void moveDown(){
        if (playing){
            downTimer += Time.deltaTime;
            if (downTimer >= moveSpeed && OnPath(0, -movementDistance))
            {
                playerFishTransform.position = new Vector3(playerFishTransform.position.x, playerFishTransform.position.y - movementDistance, playerFishTransform.position.z);
                downTimer = 0;
                CheckFishCollision();
            }
        }
    }

    public void moveLeft(){
        if (playing){
            leftTimer += Time.deltaTime;
            if (leftTimer >= moveSpeed && OnPath(-movementDistance, 0))
            {
                playerFishTransform.position = new Vector3(playerFishTransform.position.x - movementDistance, playerFishTransform.position.y, playerFishTransform.position.z);
                leftTimer = 0;
                CheckFishCollision();
            }
        }
    }

    public void moveRight(){
        if (playing){
            rightTimer += Time.deltaTime;
            if (rightTimer >= moveSpeed && OnPath(movementDistance, 0))
            {
                playerFishTransform.position = new Vector3(playerFishTransform.position.x + movementDistance, playerFishTransform.position.y, playerFishTransform.position.z);
                rightTimer = 0;
                CheckFishCollision();
            }
        }
    }

    public void FixedUpdate()
    {
        if (!intro)
        {
            introTimer += Time.deltaTime;

            if (introTimer >= .1f)
            {
                foreach (Image a in FadeIn)
                {
                    a.color = new Color(a.color.r, a.color.g, a.color.b, a.color.a + .1f);
                }
                WhirlpoolOne.color = new Color(WhirlpoolOne.color.r, WhirlpoolOne.color.g, WhirlpoolOne.color.b, WhirlpoolOne.color.a + .1f);
                WhirlpoolTwo.color = new Color(WhirlpoolTwo.color.r, WhirlpoolTwo.color.g, WhirlpoolTwo.color.b, WhirlpoolTwo.color.a + .1f);
                WhirlpoolThree.color = new Color(WhirlpoolThree.color.r, WhirlpoolThree.color.g, WhirlpoolThree.color.b, WhirlpoolThree.color.a + .1f);
                WhirlpoolFour.color = new Color(WhirlpoolFour.color.r, WhirlpoolFour.color.g, WhirlpoolFour.color.b, WhirlpoolFour.color.a + .1f);
                HookOne.color = new Color(HookOne.color.r, HookOne.color.g, HookOne.color.b, HookOne.color.a + .1f);
                HookTwo.color = new Color(HookTwo.color.r, HookTwo.color.g, HookTwo.color.b, HookTwo.color.a + .1f);
                HookThree.color = new Color(HookThree.color.r, HookThree.color.g, HookThree.color.b, HookThree.color.a + .1f);
                HookFour.color = new Color(HookFour.color.r, HookFour.color.g, HookFour.color.b, HookFour.color.a + .1f);

                setupTracker++;
                introTimer = 0;
                if (setupTracker >= 10)
                {
                    intro = true;
                }
            }
        }

        else
        {
            if (timerScript.index > 16 && !gameDone)
            {
                //score = 0;
                //fishEarnedQuantity = 0;
                //SETS PLAYER SCORE TO THE TEXT IN POPUP
                //scoreText.text = score.ToString();
                //rewardQuantityText.text = fishEarnedQuantity.ToString();

                //SETS CORRECT INFO TO THE FISH YOU'LL CATCH AND CATCHES THE FISH
                calcFish();
                if (fishEarnedQuantity != 0)
                {
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

                //SETS THE FISH CAUGHT TO THE CORRECT SPRITE
                rewardImage.sprite = onWhirlpool;
                rewardImage.sprite = fishSpriteHolder.spriteLists[gameProgress.currentOceanIndex][rarity][fishCaughtIndex];

                rewardPopUp.SetActive(true);
                gameDone = true;

                endGame();
            }

            if (delay)
            {
                delayTimer += Time.deltaTime;
                if (delayTimer >= delayTime)
                {
                    delayTimer = 0;
                    delay = false;
                    swirl = false;
                }
            }

            if (delay && swirl)
            {
                playerFishTransform.Rotate(0f, 0f, 360 * Time.deltaTime);

            }

            if (playing && !delay && !done)
            {
                if (movingUp)
                {
                    moveUp();
                }
                else if (movingDown)
                {
                    moveDown();
                }
                else if (movingLeft)
                {
                    moveLeft();
                }
                else if (movingRight)
                {
                    moveRight();
                }
            }
        }
    }
}
