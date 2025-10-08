using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TabScript : MonoBehaviour
{
    public GemUpdater gemUpdater;
    public TMP_Text islandText;
    public AudioTracker audioTracker;
    public FlameAnimate[] flameAnimate;
    public WelcomePage welcomePage;
    public GameObject notEnuffPopUp;
    public GameObject[] merchantTabs;
    public InventoryButton inventoryButton;
    public GameObject homeBar;
    public DemoStorePage demoStorePage;
    public CharacterInit characterInit;
    public DailyTaskGift dailyTaskGift;
    public OceanAnimation oceanAnimation;
    public CharacterMover[] characterMover;
    public StreakTracker streakTracker;
    public FishSpriteHolder fishSpriteHolder;
    public RodDisplay rodDisplay;
    public FishCollectionBook fishCollectionBook;
    public GameObject petHolder;
    public GobletEquipper gobletEquipper;
    public ChairEquipper chairEquipper;
    public BarrelEquipper barrelEquipper;
    public TableEquipper tableEquipper;
    public GameObject nextIslandButton;
    public Merchant merchant;
    public EquipmentScript equipmentScript;
    public SailSwap sailSwap;
    public TMP_Text gemQuantity;
    public TMP_Text coinQuantity;
    public GoldManager goldManager;
    public GameObject MerchantScreen;
    public GameObject Ocean;
    public GameObject baitSelection;
    public InventoryKing inventoryKing;
    public InventoryKing inventoryKing2;
    public GameObject Tasks;
    public PetWalk petWalk;
    public GameObject Fishing;
    public GameObject[] baitInits;
    public GameObject Ship;
    public GameObject GoldPass;
    public GameObject CaptainsQuarters;
    public GameObject QuartersBackground;
    public GameObject IslandScreen;
    public GameObject navBar;
    //public GameObject returnToShip;
    public GameObject ocean;
    public GameObject menuHolder;
    public GameObject menuContents;
    public GameObject shopScreen;

    public GameObject ResourceBar;
    public Trader trader;
    public GameProgress gameProgress;
    public GameObject islandReachedPopUp;
    public SaveSystem saveSystem;
    public IslandManager islandManager;
    public IslandScript islandScript;
    public CaptainMessages captainMessages;
    public GemStorage gemStorage;
    public Seagull seagull;
    public BattlePass battlePass;
    public SailingTracker sailingTracker;
    public BaitMinigame baitMinigame;

    public GameObject[] captainTabOut;


    public void Start()
    {
        SaveSystem.Init();
        merchant.costsInit();
        sailingTracker.loadSailing();
        equipmentScript.equipmentInit();
        equipmentScript.loadDecorations();
        inventoryKing.LoadInventory();
        battlePass.LoadPass();
        fishCollectionBook.CollectionInit();
        gameProgress.loadCurrentIsland();
        //islandManager.loadFishGiveth();
        islandManager.houseListInit();
        //islandScript.loadHouseData();
        captainMessages.tutorialMessagesInit();
        gemStorage.LoadGems();
        petWalk.Init();
        goldManager.loadGold();
        sailingTracker.loadTasksCompletedToday();
        sailingTracker.INITIATED = true;
        sailSwap.Init();
        baitMinigame.init();
        fishSpriteHolder.Init();
        characterInit.CharacterInitFunc();
        demoStorePage.loadIAP();
        audioTracker.audioInit();
        
        for (int i = 0; i < characterMover.Length; i++)
        {
            characterMover[i].Init();
        }
        islandText.text = "ISLAND " + (gameProgress.currentIslandIndex + 1);
        gemUpdater.gemSaveInit();



        Fishing.SetActive(true);
        GoldPass.SetActive(true);
        Tasks.SetActive(true);
        Ship.SetActive(false);
        IslandScreen.SetActive(false);
        shopScreen.SetActive(false);
        Fishing.SetActive(false);
        GoldPass.SetActive(false);
        Tasks.SetActive(true);
        gameProgress.Ocean.SetActive(true);
        oceanAnimation.updateOcean(gameProgress.currentOceanIndex);
        QuartersBackground.SetActive(false);
        CaptainsQuarters.SetActive(false);
        ResourceBar.SetActive(false);
        trader.clickOff();

        welcomePage.welcomeInit();
    }

    public void HomeClick()
    {
        if (Tasks.activeSelf == false)
        {
            Tasks.SetActive(true);
        }
        Ship.SetActive(false);
        Fishing.SetActive(false);
        GoldPass.SetActive(false);
        ResourceBar.SetActive(false);
        QuartersBackground.SetActive(false);
        CaptainsQuarters.SetActive(false);
        shopScreen.SetActive(false);
        IslandScreen.SetActive(false);
        gameProgress.Ocean.SetActive(true);
        oceanAnimation.updateOcean(gameProgress.currentOceanIndex);
        gameProgress.islandBackgrounds[gameProgress.currentIslandIndex].SetActive(false);
        gameProgress.islandButtons[gameProgress.currentIslandIndex].SetActive(false);
        dailyTaskGift.characterInit();
        CaptainTabOut();
    }

    public void FishingClick()
    {
        welcomePage.fishInit();
        if (Fishing.activeSelf == false)
        {
            Fishing.SetActive(true);
        }
        equipmentScript.autoEquip();
        rodDisplay.setupRod();
        inventoryKing.leaveSellScreen();
        Ship.SetActive(false);
        ResourceBar.SetActive(false);
        MerchantScreen.SetActive(false);
        //Ocean.SetActive(true);
        baitSelection.SetActive(false);
        Tasks.SetActive(false);
        GoldPass.SetActive(false);
        trader.clickOff();
        shopScreen.SetActive(false);
        IslandScreen.SetActive(false);
        CaptainsQuarters.SetActive(false);
        gameProgress.Ocean.SetActive(true);
        oceanAnimation.updateOcean(gameProgress.currentOceanIndex);
        gameProgress.islandBackgrounds[gameProgress.currentIslandIndex].SetActive(false);
        gameProgress.islandButtons[gameProgress.currentIslandIndex].SetActive(false);
        //returnToShip.SetActive(true);
        CaptainTabOut();
    }

    public void QuartersClick()
    {
        if (CaptainsQuarters.activeSelf == false)
        {
            CaptainsQuarters.SetActive(true);
        }
        Ship.SetActive(false);
        Tasks.SetActive(false);
        GoldPass.SetActive(false);
        IslandScreen.SetActive(false);
        Fishing.SetActive(false);
        shopScreen.SetActive(false);
        gameProgress.Ocean.SetActive(true);
        oceanAnimation.updateOcean(gameProgress.currentOceanIndex);
        gameProgress.islandBackgrounds[gameProgress.currentIslandIndex].SetActive(false);
        gameProgress.islandButtons[gameProgress.currentIslandIndex].SetActive(false);
    }

    public void ShopClick()
    {
        shopScreen.SetActive(true);
        inventoryKing.leaveSellScreen();
        characterInit.characterShopInit();
        sailingTracker.sailsShopInit();
        Fishing.SetActive(false);
        Ship.SetActive(false);
        Tasks.SetActive(false);
        GoldPass.SetActive(false);
        trader.clickOff();
        navBar.SetActive(true);
        IslandScreen.SetActive(false);
        gameProgress.Ocean.SetActive(true);
        oceanAnimation.updateOcean(gameProgress.currentOceanIndex);
        gameProgress.islandBackgrounds[gameProgress.currentIslandIndex].SetActive(false);
        gameProgress.islandButtons[gameProgress.currentIslandIndex].SetActive(false);
        CaptainTabOut();
        //returnToShip.SetActive(true);
    }

    public void ShipClick()
    {
        welcomePage.boatInit();
        flameAnimate[0].tabOver();
        flameAnimate[1].tabOver();
        flameAnimate[2].tabOver();
        int curI = gameProgress.currentIslandIndex;
        if (Ship.activeSelf == false)
        {
            Ship.SetActive(true);
        }
        Tasks.SetActive(false);
        Fishing.SetActive(false);
        GoldPass.SetActive(false);
        trader.clickOff();
        navBar.SetActive(true);
        ResourceBar.SetActive(false);
        shopScreen.SetActive(false);
        QuartersBackground.SetActive(false);
        CaptainsQuarters.SetActive(false);
        gameProgress.Ocean.SetActive(true);
        oceanAnimation.updateOcean(gameProgress.currentOceanIndex);
        gameProgress.islandBackgrounds[curI].SetActive(false);
        gameProgress.islandButtons[curI].SetActive(false);
        IslandScreen.SetActive(false);
        gemQuantity.text = gemStorage.GemTotal.ToString();
        coinQuantity.text = goldManager.PlayerGold.ToString();
        sailingTracker.popUpReset();
        CaptainTabOut();

        //TABLE INIT
        int equippedTable = equipmentScript.equippedTable;
        if (equippedTable != -1)
        {
            equipmentScript.tableImage.sprite = tableEquipper.tableSprites[equippedTable];
            equipmentScript.tableImage.color = new Color(255, 255, 255, 255);
        }
        else { equipmentScript.tableImage.color = new Color(255, 255, 255, 0); }

        //BARREL INIT
        int equippedBarrel = equipmentScript.equippedBarrel;
        if (equippedBarrel != -1)
        {
            equipmentScript.barrelImage.sprite = barrelEquipper.barrelSprites[equippedBarrel];
            equipmentScript.barrelImage.color = new Color(255, 255, 255, 255);
        }
        else { equipmentScript.barrelImage.color = new Color(255, 255, 255, 0); }

        //Chair INIT
        int equippedChair = equipmentScript.equippedChair;
        if (equippedChair != -1)
        {
            equipmentScript.chairImage.sprite = chairEquipper.chairSprites[equippedChair];
            equipmentScript.chairImage.color = new Color(255, 255, 255, 255);
        }
        else { equipmentScript.chairImage.color = new Color(255, 255, 255, 0); }

        //Goblet INIT
        int equippedGoblet = equipmentScript.equippedGoblet;
        if (equippedGoblet != -1)
        {
            equipmentScript.gobletImage.sprite = gobletEquipper.gobletSprites[equippedGoblet];
            equipmentScript.gobletImage.color = new Color(255, 255, 255, 255);
        }
        else { equipmentScript.gobletImage.color = new Color(255, 255, 255, 0); }

        //PET INIT
        if (petWalk.petsOwned[0] == 0)
        {
            petHolder.SetActive(false);
        }
        else
        {
            petHolder.SetActive(true);
            petWalk.equipPet(petWalk.petEquipped);
        }
    }

    public void IslandActivate()
    {
        welcomePage.islandInit();
        int curO = gameProgress.currentOceanIndex;
        int curI = gameProgress.currentIslandIndex;
        IslandScreen.SetActive(true);
        int a = merchant.costs[gameProgress.currentIslandIndex][0];
        int b = merchant.costs[gameProgress.currentIslandIndex][1];
        int c = merchant.costs[gameProgress.currentIslandIndex][2];
        //if (a == 0 && b == 0 && c == 0) { nextIslandButton.SetActive(true); }
        islandScript.questCheck(5, streakTracker.currentStreak);
        helperExclamation(curI);
        Tasks.SetActive(false);
        Ship.SetActive(false);
        Fishing.SetActive(false);
        GoldPass.SetActive(false);
        trader.clickOff();
        navBar.SetActive(true);
        shopScreen.SetActive(false);
        gameProgress.Ocean.SetActive(true);
        oceanAnimation.updateOcean(gameProgress.currentOceanIndex);
        gameProgress.islandBackgrounds[curI].SetActive(true);
        gameProgress.islandButtons[curI].SetActive(true);
        //gameProgress.exclamationPoints[curI].SetActive(true);
        islandScript.boatSetup();
        islandScript.boatExit();
        islandScript.sellAllLockInit();
        ResourceBar.SetActive(false);
        if (gameProgress.currentIslandIndex != 15)
        {
            characterMover[gameProgress.currentIslandIndex].setup1(gameProgress.equippedCharacter);
        }

        QuartersBackground.SetActive(false);
        CaptainsQuarters.SetActive(false);
        CaptainTabOut();
    }

    public void menuClick()
    {
        if (menuContents.activeSelf == false)
        {
            menuContents.SetActive(true);
            menuHolder.transform.GetChild(1).gameObject.SetActive(false);
            menuHolder.transform.GetChild(3).gameObject.SetActive(false);
            menuHolder.transform.GetChild(2).gameObject.SetActive(true);
        }
        else
        {
            menuContents.SetActive(false);
            menuHolder.transform.GetChild(1).gameObject.SetActive(true);
            menuHolder.transform.GetChild(3).gameObject.SetActive(true);
            menuHolder.transform.GetChild(2).gameObject.SetActive(false);
        }
    }

    public void GoldPassClick()
    {
        if (GoldPass.activeSelf == false)
        {
            GoldPass.SetActive(true);
        }
    }

    public void toTFGScreen()
    {
        ShopClick();
        homeBar.SetActive(true);
        merchant.merchantExit();
        merchantTabs[0].SetActive(true);
        merchantTabs[1].SetActive(false);
        merchantTabs[2].SetActive(false);
    }

    public void toDabloonsScreen()
    {
        ShopClick();
        homeBar.SetActive(true);
        notEnuffPopUp.SetActive(false);
        merchantTabs[0].SetActive(false);
        merchantTabs[1].SetActive(false);
        merchantTabs[2].SetActive(true);
    }

    public void toTFGScreenFromInventory()
    {
        ShopClick();
        inventoryButton.InventoryPopUp();
        homeBar.SetActive(true);
        merchantTabs[0].SetActive(true);
        merchantTabs[1].SetActive(false);
        merchantTabs[2].SetActive(false);
    }

    public void toTFGScreenFromVillager()
    {
        ShopClick();
        homeBar.SetActive(true);
        islandManager.houseExit();
        merchantTabs[0].SetActive(true);
        merchantTabs[1].SetActive(false);
        merchantTabs[2].SetActive(false);
    }

    public void ResourceOff()
    {
        ResourceBar.SetActive(false);
    }

    public void ResourceOn()
    {
        ResourceBar.SetActive(true);
    }

    public void CaptainTabOut()
    {
        for (int i = 0; i < captainTabOut.Length; i++)
        {
            captainTabOut[i].SetActive(false);
        }
    }

    private void helperExclamation(int curI)
    {
        int a = merchant.costs[gameProgress.currentIslandIndex][0];
        int b = merchant.costs[gameProgress.currentIslandIndex][1];
        int c = merchant.costs[gameProgress.currentIslandIndex][2];

        inventoryKing2.ClearThenLoadInventory();
        bool empty = inventoryKing2.checkEmpty();

        /*if ((goldManager.PlayerGold >= a && a != 0) || (goldManager.PlayerGold >= b && b != 0) || (goldManager.PlayerGold >= c && c != 0))
        {
            if (a == 0 && b == 0 && c == 0)
            {
                gameProgress.exclamationPoints[curI].transform.GetChild(gameProgress.exclamationPoints[curI].transform.childCount - 1).gameObject.SetActive(false);
            }
            else
            {
                gameProgress.exclamationPoints[curI].transform.GetChild(gameProgress.exclamationPoints[curI].transform.childCount - 1).gameObject.SetActive(true);
            }
            //CHECKS TO SEE IF ENOUGH GOLD TO BUY ANYTHING, IF SO, SETS EXCLAMATION POINT TO ACTIVE OVER THE MERCHANT. IF NOT, SETS INACTIVE  
        }
        //else { gameProgress.exclamationPoints[curI].transform.GetChild(gameProgress.exclamationPoints[curI].transform.childCount - 1).gameObject.SetActive(false); }*/
        /*
        //FIRST HOUSE
        if ((islandManager.houseInfoStorage[curI][0][0] != -1 && islandManager.houseInfoStorage[curI][0][0] != 0) ||
            (islandManager.houseInfoStorage[curI][0][1] != -1 && islandManager.houseInfoStorage[curI][0][1] != 0) ||
            (islandManager.houseInfoStorage[curI][0][2] != -1 && islandManager.houseInfoStorage[curI][0][2] != 0))
        {
            if (!empty)
            {
                gameProgress.exclamationPoints[curI].transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                gameProgress.exclamationPoints[curI].transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        else
        {
            gameProgress.exclamationPoints[curI].transform.GetChild(0).gameObject.SetActive(false);
        }

        //SECOND HOUSE
        if ((islandManager.houseInfoStorage[curI][1][0] != -1 && islandManager.houseInfoStorage[curI][1][0] != 0) ||
            (islandManager.houseInfoStorage[curI][1][1] != -1 && islandManager.houseInfoStorage[curI][1][1] != 0) ||
            (islandManager.houseInfoStorage[curI][1][2] != -1 && islandManager.houseInfoStorage[curI][1][2] != 0))
        {
            if (!empty)
            {
                gameProgress.exclamationPoints[curI].transform.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                gameProgress.exclamationPoints[curI].transform.GetChild(1).gameObject.SetActive(false);
            }
        }
        else if (islandManager.houseInfoStorage[curI][1][0] == -1 && islandManager.houseInfoStorage[curI][1][1] == -1 && islandManager.houseInfoStorage[curI][1][2] == -1)
        {

        }
        else
        {
            gameProgress.exclamationPoints[curI].transform.GetChild(1).gameObject.SetActive(false);
        }


        //THIRD HOUSE
        if ((islandManager.houseInfoStorage[curI][2][0] != -1 && islandManager.houseInfoStorage[curI][2][0] != 0) ||
            (islandManager.houseInfoStorage[curI][2][1] != -1 && islandManager.houseInfoStorage[curI][2][1] != 0) ||
            (islandManager.houseInfoStorage[curI][2][2] != -1 && islandManager.houseInfoStorage[curI][2][2] != 0))
        {
            if (!empty)
            {
                gameProgress.exclamationPoints[curI].transform.GetChild(2).gameObject.SetActive(true);
            }
            else
            {
                gameProgress.exclamationPoints[curI].transform.GetChild(2).gameObject.SetActive(false);
            }
        }
        else if (islandManager.houseInfoStorage[curI][2][0] == -1 && islandManager.houseInfoStorage[curI][2][1] == -1 && islandManager.houseInfoStorage[curI][2][2] == -1)
        {

        }
        else
        {
            gameProgress.exclamationPoints[curI].transform.GetChild(2).gameObject.SetActive(false);
        }*/
    }
}
