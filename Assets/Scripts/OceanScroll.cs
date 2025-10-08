using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;
using Slider = UnityEngine.UI.Slider;
using Vector3 = UnityEngine.Vector3;

public class OceanScroll : MonoBehaviour
{
    public EquipmentScript equipmentScript;
    public GameObject miniShip;
    public GameObject baitCollection;
    [NonSerialized] public bool collectDelay = false;
    private bool iterationDone = false;
    public FishCollectionBook fishCollectionBook;
    public BaitQuantity baitQuantity;
    public InventoryKing inventoryKing;
    private bool oceanTapped = false;
    public GameObject inventoryIcon;
    public GameObject fishingDexIcon;
    public GameObject BaitIcon;
    public GameObject tooSlowPopUp;
    private float rarityWarningTimer = 0;
    private bool rarityPopStall = false;
    private float rarityPopStallTimer = 0;
    private float currentTime = 0;
    public Slider slider;
    public CatchTimer catchTimer;
    public float castTime = 2;
    private bool nearby = false;
    public Button myButton;
    public GameObject bobber;
    public GameObject wrongFishPopUp;
    public float maxArcHeight = 10.0f; // Maximum height of the arc
    public int numPoints = 50; // Number of points in the LineRenderer
    private Vector3 origin;
    public Image alertMarker;
    public Image badAlertMarker;
    private float elapsedTime = 0;
    public TMP_Text nearbyText;
    private float leftBorder;
    private float rightBorder;
    private float top;
    private float bottom;
    private bool catchTimerInit = false;
    private int fishCaughtIndex = 0;
    private float heightIncrementor = 20;
    private float disappearTimer = 0;
    private bool bobberSet = false;
    public Camera cameraUI;
    private double timeToCatch;
    public Button badButton;
    private float bobberTimer = 0;
    public TMP_Text timeToCatchText;
    private bool casting = false;
    private bool run = false;
    public  LineRenderer lineRenderer;
    private int pointTracker = 0;
    public GameObject waterBack;
    public SpriteRenderer fishCaught;
    private bool dontRun = false;

    //EAST BLUE FISH
    public Sprite[] EastBlueNormalFish;
    public Sprite[] EastBlueFancyFish;
    public Sprite[] EastBlueExtravagantFish;
    public Sprite[] EastBluePristineFish;
    public Sprite[] EastBlueMagicalFish;
    //EAST BLUE FISH END

    //GREEN FISH
    public Sprite[] GreenNormalFish;
    public Sprite[] GreenFancyFish;
    public Sprite[] GreenExtravagantFish;
    public Sprite[] GreenPristineFish;
    public Sprite[] GreenMagicalFish;
    //GREEN FISH END

    //PURPLE FISH
    public Sprite[] PurpleNormalFish;
    public Sprite[] PurpleFancyFish;
    public Sprite[] PurpleExtravagantFish;
    public Sprite[] PurplePristineFish;
    public Sprite[] PurpleMagicalFish;
    //PURPLE FISH END

    //LAVA FISH
    public Sprite[] LavaNormalFish;
    public Sprite[] LavaFancyFish;
    public Sprite[] LavaExtravagantFish;
    public Sprite[] LavaPristineFish;
    public Sprite[] LavaMagicalFish;
    //LAVA FISH END

    //ALLBLUE FISH
    public Sprite[] AllBlueNormalFish;
    public Sprite[] AllBlueFancyFish;
    public Sprite[] AllBlueExtravagantFish;
    public Sprite[] AllBluePristineFish;
    public Sprite[] AllBlueMagicalFish;
    //ALLBLUE FISH END

    private Sprite[][][] spriteLists = new Sprite[5][][];
    private Sprite[][] EastBlueHolder = new Sprite[5][];
    private Sprite[][] GreenHolder = new Sprite[5][];
    private Sprite[][] PurpleHolder = new Sprite[5][];
    private Sprite[][] LavaHolder = new Sprite[5][];
    private Sprite[][] AllBlueHolder = new Sprite[5][];

    public Sprite[] CatchNoti;
    private Vector3 touchPosition;
    Vector3[] empty;
    private int rarity;
    private bool secondCheck = false;
    private bool resetFishDelay = false;
    private bool thirdCheck = false;
    public BaitBuy baitBuy;
    public GameProgress gameProgress;
    public GameObject inventoryFullPopUp;

    


    //IMPORTANT VARIABLES
    [NonSerialized]
    public float baitCatchTime;
    [NonSerialized]
    public int[] Rarities = {50,30,15,4,1}; // Normal, Fancy, Extravagant, Pristine, Magical
    [NonSerialized] public int[] NormalRarity = {50,30,15,4,1};
    [NonSerialized] public int[] FancyRarity = {40,35,20,4,1};
    [NonSerialized] public int[] ExtravagantRarity = {30,40,19,10,1};
    [NonSerialized] public int[] PristineRarity = {1,4,50,30,15};
    [NonSerialized] public int[][] RarityStorage = new int[4][];
    [NonSerialized] public string[] RarityNames = {"Normal", "Fancy", "Extravagant", "Pristine", "Magical"};

    public void Start(){
        var dist = (transform.position - cameraUI.transform.position).z;
        leftBorder = cameraUI.ViewportToWorldPoint(new Vector3(0, 0, dist)).x + 10;
        rightBorder = cameraUI.ViewportToWorldPoint(new Vector3(1, 0, dist)).x - 10;
        bottom = cameraUI.ViewportToWorldPoint(new Vector3(0, 0, dist)).y + 40;
        top = cameraUI.ViewportToWorldPoint(new Vector3(0, 1, dist)).y - 40;
        waterBack.SetActive(true);
        origin = lineRenderer.gameObject.transform.position;
        lineRenderer.positionCount = numPoints;
        empty = new Vector3[numPoints];
        for (int i = 0; i < numPoints; i ++){
            empty[i] = Vector3.zero;
        }
        gameProgress.loadCurrentIsland();

        spriteLists[0] = EastBlueHolder;
        spriteLists[1] = GreenHolder;
        spriteLists[2] = PurpleHolder;
        spriteLists[3] = LavaHolder;
        spriteLists[4] = AllBlueHolder;

        spriteLists[0][0] = EastBlueNormalFish;
        spriteLists[0][1] = EastBlueFancyFish;
        spriteLists[0][2] = EastBlueExtravagantFish;
        spriteLists[0][3] = EastBluePristineFish;
        spriteLists[0][4] = EastBlueMagicalFish;

        spriteLists[1][0] = GreenNormalFish;
        spriteLists[1][1] = GreenFancyFish;
        spriteLists[1][2] = GreenExtravagantFish;
        spriteLists[1][3] = GreenPristineFish;
        spriteLists[1][4] = GreenMagicalFish;

        spriteLists[2][0] = PurpleNormalFish;
        spriteLists[2][1] = PurpleFancyFish;
        spriteLists[2][2] = PurpleExtravagantFish;
        spriteLists[2][3] = PurplePristineFish;
        spriteLists[2][4] = PurpleMagicalFish;

        spriteLists[3][0] = LavaNormalFish;
        spriteLists[3][1] = LavaFancyFish;
        spriteLists[3][2] = LavaExtravagantFish;
        spriteLists[3][3] = LavaPristineFish;
        spriteLists[3][4] = LavaMagicalFish;

        spriteLists[4][0] = AllBlueNormalFish;
        spriteLists[4][1] = AllBlueFancyFish;
        spriteLists[4][2] = AllBlueExtravagantFish;
        spriteLists[4][3] = AllBluePristineFish;
        spriteLists[4][4] = AllBlueMagicalFish;

        RarityStorage[0] = NormalRarity;
        RarityStorage[1] = FancyRarity;
        RarityStorage[2] = ExtravagantRarity;
        RarityStorage[3] = PristineRarity;
    }

    void OnEnable(){
        waterBack.SetActive(true);
        }

    void Update()
    {
        if (iterationDone){
            if (Input.touchCount > 0){
                dontRun = true;
                if (secondCheck){
                    resetFishDelay = true;
                    fishCaught.gameObject.SetActive(false);
                    wrongFishPopUp.SetActive(false);
                    tooSlowPopUp.SetActive(false);
                    slider.gameObject.SetActive(false);
                    BaitIcon.SetActive(true);
                    baitCollection.SetActive(true);
                    collectDelay = false;
                    miniShip.SetActive(true);
                    fishingDexIcon.SetActive(true);
                    inventoryIcon.SetActive(true);
                }
                if (thirdCheck){
                    iterationDone = false;
                    dontRun = false; 
                    secondCheck = false;
                    thirdCheck = false;
                    resetFishDelay = false;    
                }
            }
            else{
                secondCheck = true;
                if (resetFishDelay){
                    thirdCheck = true;
                } 
            }
        }
        if (Input.touchCount > 0){
            if (!casting && !dontRun && equipmentScript.equippedBaitRarity < 6){
                Cast();
            }
        }
        else{
            oceanTapped = false;
        }
        if (casting && !dontRun){
            miniGame(rarity);
        }
    }

    private void Cast(){ 
        Touch touch = Input.GetTouch(0);
        touchPosition = cameraUI.ScreenToWorldPoint(touch.position);
        bool full = inventoryKing.checkFull();
        if (touchPosition.y > bottom && touchPosition.y < top && oceanTapped){
            if (full){
                inventoryFullPopUp.SetActive(true);
            }
        }
        if (touchPosition.y > bottom && touchPosition.y < top && oceanTapped && !full){
            BaitIcon.SetActive(false);
            baitCollection.SetActive(false);
            collectDelay = true;
            miniShip.SetActive(false);
            fishingDexIcon.SetActive(false);
            inventoryIcon.SetActive(false);
            bobber.SetActive(true);
            casting = true;
            lineRenderer.gameObject.SetActive(true);
            lineRenderer.SetPosition(0, lineRenderer.gameObject.transform.position);

            Rarities = RarityStorage[baitQuantity.selectedBait];
            switch(baitQuantity.selectedBait){
                case 0:
                    baitBuy.WormTotal -= 1;
                    //VISUAL UPDATER HERE TODO
                    break;
                case 1:
                    baitBuy.LadyBugTotal -= 1;
                    //VISUAL UPDATER HERE TODO
                    break;
                case 2:
                    baitBuy.FireflyTotal -= 1;
                    //VISUAL UPDATER HERE TODO
                    break;
                case 3:
                    baitBuy.MinnowTotal -= 1;
                    //VISUAL UPDATER HERE TODO
                    break;
                case 4:
                    baitBuy.ButterflyTotal -= 1;
                    //VISUAL UPDATER HERE TODO
                    break;
                case 5:
                    baitBuy.DragonflyTotal -= 1;
                    //VISUAL UPDATER HERE TODO
                    break;
            }
            baitBuy.SaveBait();

            rarity = UnityEngine.Random.Range(1,101);
            if (rarity < Rarities[0]){ // Normal fish catch
                rarity = 0;
                fishCaughtIndex = UnityEngine.Random.Range(0,8);
                currentTime = 5;
            }
            else if (rarity >= Rarities[0] && rarity < Rarities[0] + Rarities[1]){ // Fancy fish catch
                rarity = 1;
                fishCaughtIndex = UnityEngine.Random.Range(0,6);
                currentTime = 2;
            }
            else if (rarity >= Rarities[0] + Rarities[1] && rarity < Rarities[0] + Rarities[1] + Rarities[2]){ // Extravagant fish catch
                rarity = 2;
                fishCaughtIndex = UnityEngine.Random.Range(0,3);
                currentTime = 1;
            }
            else if (rarity >= Rarities[0] + Rarities[1] + Rarities[2] && rarity < Rarities[0] + Rarities[1] + Rarities[2] + Rarities[3]){ // Pristine fish catch
                rarity = 3;
                fishCaughtIndex = UnityEngine.Random.Range(0,2);
                currentTime = .75f;
            }
            Debug.Log("Rarity will be " + rarity);
            Debug.Log("Index of fish will be " + fishCaughtIndex);

            var rand = new System.Random();
            timeToCatch = rand.NextDouble() * 5;
            if (timeToCatch < 1){
                timeToCatch +=1;
            }
        }
    }

    private void miniGame(int rarity){
        if (!bobberSet){
            DrawParabolicArc();
        }
        else if (!rarityPopStall){
            rarityPopStallTimer += Time.deltaTime;
            if (rarityPopStallTimer > 1){
                rarityPopStall = true;
            }
        }
        else if (!nearby){
            if (rarity != 2){
                nearbyText.text = "A " + RarityNames[rarity] + " Fish is nearby...";
            }
            else{
                nearbyText.text = "An " + RarityNames[rarity] + " Fish is nearby...";
            }
            nearbyText.gameObject.SetActive(true);
            rarityWarningTimer += Time.deltaTime;
            if (rarityWarningTimer > 3){
                nearby = true;
            }
        }
        else{
        nearbyText.gameObject.SetActive(false);
        if (!catchTimerInit){
            catchTimer.InitializeTimeBar(currentTime);
            catchTimerInit = true;
        }
        slider.gameObject.SetActive(true);
        elapsedTime += Time.deltaTime;

        if (elapsedTime > timeToCatch && !run && casting){
            
            //Good button
            float randY = UnityEngine.Random.Range(-65, top);
            float randX = UnityEngine.Random.Range(leftBorder, rightBorder);
            myButton.gameObject.transform.position = new Vector3(randX, randY, 1040);
            myButton.gameObject.SetActive(true);
            //alertMarker.gameObject.SetActive(true);

            //Bad Button
            while(true){
                randY = UnityEngine.Random.Range(-65, top);
                randX = UnityEngine.Random.Range(leftBorder, rightBorder);
                if (randY - myButton.gameObject.transform.position.y <= 5 && randY - myButton.gameObject.transform.position.y >= -5){
                    randY = UnityEngine.Random.Range(-65, top);
                   }
                else if (randX - myButton.gameObject.transform.position.x <= 3 && randX - myButton.gameObject.transform.position.x >= -3){
                    randX = UnityEngine.Random.Range(leftBorder, rightBorder);
                }
                else{
                    break;
                }
            }
            badButton.gameObject.transform.position = new Vector3(randX, randY, 1040);
            badButton.gameObject.SetActive(true);
            //badAlertMarker.gameObject.SetActive(true);

            Handheld.Vibrate();
            run = true;
        }
        else if (run){

            // Timer bar at top
            if (currentTime > 0){
                slider.value = currentTime;
                currentTime -= Time.deltaTime;
                float holder = Mathf.Round(currentTime * 100f) * 0.01f;
                timeToCatchText.SetText(holder.ToString("0.00"));
            }
            if (currentTime < 0){
                slider.value = 0;
                timeToCatchText.SetText("" + 0.0);
            }
            //end Timer bar at top code

            disappearTimer += Time.deltaTime;

            if (rarity == 0){
                if (disappearTimer > 5){
                    TooSlow();
                    disappearTimer = 0;
                }
            }
            else if (rarity == 1){
                if (disappearTimer > 2){
                    TooSlow();
                    disappearTimer = 0;
                }
            } 
            else if (rarity == 2){
                if (disappearTimer > 1){
                    TooSlow();
                    disappearTimer = 0;
                }
            } 
            else if (rarity == 3){
                if (disappearTimer > .75){
                    TooSlow();
                    disappearTimer = 0;
                }
            } 
        }
            
    }
    }

    public void catchFish(){
        fishCaught.gameObject.SetActive(true);
        alertMarker.gameObject.SetActive(false);
        myButton.gameObject.SetActive(false);
        badButton.gameObject.SetActive(false);
        bobber.SetActive(false);
        fishCaught.sprite = spriteLists[gameProgress.currentOceanIndex][rarity][fishCaughtIndex];
        inventoryKing.catchFish(rarity, fishCaughtIndex, 1);
        //fishCollectionBook.registerFish(fishCaughtIndex, rarity, gameProgress.currentOceanIndex);
        casting = false;
        timeToCatch = 0;
        rarity = 0;
        elapsedTime = 0;
        run = false;
        iterationDone = true;
        bobberSet = false;
        catchTimerInit = false;
        rarityPopStall = false;
        rarityPopStallTimer = 0;
        rarityWarningTimer = 0;
        nearby = false;
        lineRenderer.gameObject.SetActive(false);
        lineRenderer.SetPositions(empty);
        disappearTimer = 0;
        bobber.transform.position = lineRenderer.transform.position;
    }

    public void WrongFish(){
        wrongFishPopUp.SetActive(true);
        myButton.gameObject.SetActive(false);
        badButton.gameObject.SetActive(false);
        bobber.SetActive(false);
        casting = false;
        timeToCatch = 0;
        rarity = 0;
        elapsedTime = 0;
        run = false;
        iterationDone = true;
        rarityWarningTimer = 0;
        nearby = false;
        bobberSet = false;
        rarityPopStall = false;
        rarityPopStallTimer = 0;
        disappearTimer = 0;
        catchTimerInit = false;
        lineRenderer.gameObject.SetActive(false);
        lineRenderer.SetPositions(empty);
        bobber.transform.position = lineRenderer.transform.position;
    }

    public void TooSlow(){
        tooSlowPopUp.SetActive(true);
        myButton.gameObject.SetActive(false);
        badButton.gameObject.SetActive(false);
        bobber.SetActive(false);
        casting = false;
        timeToCatch = 0;
        rarity = 0;
        elapsedTime = 0;
        run = false;
        iterationDone = true;
        rarityWarningTimer = 0;
        rarityPopStall = false;
        rarityPopStallTimer = 0;
        nearby = false;
        bobberSet = false;
        disappearTimer = 0;
        catchTimerInit = false;
        lineRenderer.gameObject.SetActive(false);
        lineRenderer.SetPositions(empty);
        
        bobber.transform.position = lineRenderer.transform.position;
    }

    public void DrawParabolicArc()
    {
        Vector3[] points = new Vector3[numPoints];
        float step = 1.0f / (numPoints - 1);
        float peakHeight = 20f;
        //peakHeight = Mathf.Abs(touchPosition.x - origin.x) / 2; // Peak of the arc
        //peakHeight = Mathf.Min(peakHeight, maxArcHeight);

        //Bobber start
        bobberTimer += Time.deltaTime;

        if (bobberTimer >= castTime/100){

            float t = pointTracker * step;
            Vector3 location = Vector3.Lerp(origin, touchPosition, t);
            location.y += Mathf.Sin(Mathf.PI * t) * peakHeight;
            Vector3 locationLock = new Vector3 (location.x, location.y, 1040);
            bobber.transform.position = locationLock;


            pointTracker += 1;
            bobberTimer = 0;
        }
        //bobber end

        if (pointTracker > numPoints/2){
            if (heightIncrementor < 21){
                heightIncrementor += 1;
            }
        }

        for (int i = 0; i < numPoints; i++)
        {
            float t = i * step; // Progress along the path [0, 1]
            Vector3 point = Vector3.Lerp(origin, bobber.transform.position, t); // Linear interpolation

            // Calculate the height of the arc at this point
            point.y += Mathf.Sin(Mathf.PI * t) * heightIncrementor;

            points[i] = point;
        }

        lineRenderer.SetPositions(points);


        //Cast end logic
        if (pointTracker >= numPoints){
            bobberSet = true;
            pointTracker = 0;
        }
        
    }

    public void OceanTap(){
        oceanTapped = true;
    }


}
