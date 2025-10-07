using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Security.Claims;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class IslandManager : MonoBehaviour
{
    public QuestSlider questSlider;
    public Color greeeen;
    public Image[] backings1;
    public Image[] backings2;
    public Image[] backings3;

    public InventoryKing inventoryKingVillager;
    public GameObject inventoryVillagerOBJ;
    public TMP_Text[] questTextHolders;
    public Image[] questCompleters;
    public Sprite questCompletedSprite;
    public Sprite questNotCompletedSprite;

    public GameObject nextIslandButton;
    public Merchant merchant;
    public GameObject giveAllButton;

    //public GameObject[] textsBelowClaim;
    public GameObject[] stipendClaimWindow;
    public TMP_Text[] stipendClaimText;
    public GoldManager goldManager;
    public TMP_Text residentName;
    public Image residentScreenSprite;
    public HousingSprites housingSprites;
    public GameObject[] ResidentPopUps;
    public Image[] residentPopUpSpritesSolo;
    public Image[] residentPopUpSpritesDuo;
    public Image[] residentPopUpSpritesTrio;
    public GameProgress gameProgress;
    public GoldBar goldBar;
    public FishCollected fishCollected;
    public SellLocks sellLocks;
    public SellLocks sellLocks2;
    public GameObject popUpMenu;
    public GameObject navBar;
    public GameObject continueJourney;
    public IslandScript islandScript;
    public InventoryKing inventoryKing;
    public InventoryKing freeChestKing;
    public InventoryKing paidChestKing;
    public GameObject[] inventories;
    private bool listInit = false;
    private int counter = 0;


    [NonSerialized] public string[][][] Quests = new string[16][][];

    [NonSerialized] public int[][][] houseInfoStorage = new int[16][][];

    [NonSerialized] public int[][][][] questTracking = new int[16][][][];

    [NonSerialized] public int[][][][] questCaps = new int[16][][][];


    /// <REFERENCE GUIDE>
    /// -1 no villager
    /// 
    /// 0 Fish Donate
    /// 1 Mini Game High Scores
    /// 2 Buy Cosmetics
    /// 3 Complete # of Tasks
    /// 4 Roll certain # of bait rarity
    /// 5 Task streak
    /// 6 Gain a certain amnt of gold
    /// <REFERENCE GUIDE>
    [NonSerialized] public int[][][][] questTypeInfo = new int[16][][][];
    [NonSerialized] public int[][][][] questRarityInfo = new int[16][][][];

    [NonSerialized] public int[][][][] claimed = new int[16][][][];

    public void houseListInit(){

        if (!listInit)
        {

            for (int i = 0; i < 16; i++)
            {
                questCaps[i] = new int[3][][];
                questTracking[i] = new int[3][][];
                questTypeInfo[i] = new int[3][][];
                questRarityInfo[i] = new int[3][][];
            }

            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    questCaps[i][j] = new int[3][];
                    questTracking[i][j] = new int[3][];
                    questTypeInfo[i][j] = new int[3][];
                    questRarityInfo[i][j] = new int[3][];
                }
            }

            //TOTAL TRACKER

            //ISLAND 0
            houseInfoStorage[0] = new int[3][]; //40
            houseInfoStorage[0][0] = new int[] { 4, -1, -1 }; //0
            houseInfoStorage[0][1] = new int[] { 4, -1, -1 }; //0
            houseInfoStorage[0][2] = new int[] { -1, -1, -1 }; //NA

            //ISLAND 0 HOUSE 0
            questCaps[0][0][0] = new int[] { 1, 400, 3 };
            questCaps[0][0][1] = new int[] { -1, -1, -1 };
            questCaps[0][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 0 HOUSE 1
            questCaps[0][1][0] = new int[] { 1, 1000, 1 };
            questCaps[0][1][1] = new int[] { -1, -1, -1 };
            questCaps[0][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 0 HOUSE 2
            questCaps[0][2][0] = new int[] { -1, -1, -1 };
            questCaps[0][2][1] = new int[] { -1, -1, -1 };
            questCaps[0][2][2] = new int[] { -1, -1, -1 };

            //ISLAND 0 HOUSE 0
            questTracking[0][0][0] = new int[] { 1, 400, 3 };
            questTracking[0][0][1] = new int[] { -1, -1, -1 };
            questTracking[0][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 0 HOUSE 1
            questTracking[0][1][0] = new int[] { 1, 1000, 1 };
            questTracking[0][1][1] = new int[] { -1, -1, -1 };
            questTracking[0][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 0 HOUSE 2
            questTracking[0][2][0] = new int[] { -1, -1, -1 };
            questTracking[0][2][1] = new int[] { -1, -1, -1 };
            questTracking[0][2][2] = new int[] { -1, -1, -1 };

            //ISLAND 0 HOUSE 0
            questTypeInfo[0][0][0] = new int[] { 0, 1, 3 }; //DONATE, HS, #TASKS
            questTypeInfo[0][0][1] = new int[] { -1, -1, -1 };
            questTypeInfo[0][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 0 HOUSE 1
            questTypeInfo[0][1][0] = new int[] { 2, 6, 0 }; //COSMETIC, GOLD, DONATE
            questTypeInfo[0][1][1] = new int[] { -1, -1, -1 };
            questTypeInfo[0][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 0 HOUSE 2
            questTypeInfo[0][2][0] = new int[] { -1, -1, -1 };
            questTypeInfo[0][2][1] = new int[] { -1, -1, -1 };
            questTypeInfo[0][2][2] = new int[] { -1, -1, -1 };



            //ISLAND 0 HOUSE 0
            questRarityInfo[0][0][0] = new int[] { 0, 0, -1 }; //DONATE, HS, #TASKS
            questRarityInfo[0][0][1] = new int[] { -1, -1, -1 };
            questRarityInfo[0][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 0 HOUSE 1
            questRarityInfo[0][1][0] = new int[] { 0, -1, 1 }; //COSMETIC, GOLD, DONATE
            questRarityInfo[0][1][1] = new int[] { -1, -1, -1 };
            questRarityInfo[0][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 0 HOUSE 2
            questRarityInfo[0][2][0] = new int[] { -1, -1, -1 };
            questRarityInfo[0][2][1] = new int[] { -1, -1, -1 };
            questRarityInfo[0][2][2] = new int[] { -1, -1, -1 };

            //____________________________________________________________________________

            //ISLAND 1
            houseInfoStorage[1] = new int[3][]; //80
            houseInfoStorage[1][0] = new int[] { 30, -1, -1 }; //0
            houseInfoStorage[1][1] = new int[] { 15, 35, -1 }; //0,1
            houseInfoStorage[1][2] = new int[] { -1, -1, -1 }; //NA

            //ISLAND 1 HOUSE 0
            questCaps[1][0][0] = new int[] { 3, 1, 600 };
            questCaps[1][0][1] = new int[] { -1, -1, -1 };
            questCaps[1][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 1 HOUSE 1
            questCaps[1][1][0] = new int[] { 6, 1, 2000 };
            questCaps[1][1][1] = new int[] { 5, 2, 5 };
            questCaps[1][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 1 HOUSE 2
            questCaps[1][2][0] = new int[] { -1, -1, -1 };
            questCaps[1][2][1] = new int[] { -1, -1, -1 };
            questCaps[1][2][2] = new int[] { -1, -1, -1 };

            //ISLAND 1 HOUSE 0
            questTracking[1][0][0] = new int[] { 3, 1, 600 };
            questTracking[1][0][1] = new int[] { -1, -1, -1 };
            questTracking[1][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 1 HOUSE 1
            questTracking[1][1][0] = new int[] { 6, 1, 2000 };
            questTracking[1][1][1] = new int[] { 5, 2, 5 };
            questTracking[1][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 1 HOUSE 2
            questTracking[1][2][0] = new int[] { -1, -1, -1 };
            questTracking[1][2][1] = new int[] { -1, -1, -1 };
            questTracking[1][2][2] = new int[] { -1, -1, -1 };


            questTypeInfo[1][0][0] = new int[] { 0, 0, 1 };
            questTypeInfo[1][0][1] = new int[] { -1, -1, -1 };
            questTypeInfo[1][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 1 HOUSE 1
            questTypeInfo[1][1][0] = new int[] { 3, 2, 6 };
            questTypeInfo[1][1][1] = new int[] { 0, 5, 4 };
            questTypeInfo[1][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 1 HOUSE 2
            questTypeInfo[1][2][0] = new int[] { -1, -1, -1 };
            questTypeInfo[1][2][1] = new int[] { -1, -1, -1 };
            questTypeInfo[1][2][2] = new int[] { -1, -1, -1 };


            questRarityInfo[1][0][0] = new int[] { 1, 2, 0 };
            questRarityInfo[1][0][1] = new int[] { -1, -1, -1 };
            questRarityInfo[1][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 1 HOUSE 1
            questRarityInfo[1][1][0] = new int[] { -1, 0, -1 };
            questRarityInfo[1][1][1] = new int[] { 0, -1, 0 };
            questRarityInfo[1][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 1 HOUSE 2
            questRarityInfo[1][2][0] = new int[] { -1, -1, -1 };
            questRarityInfo[1][2][1] = new int[] { -1, -1, -1 };
            questRarityInfo[1][2][2] = new int[] { -1, -1, -1 };

            //____________________________________________________________________________

            //ISLAND 2
            houseInfoStorage[2] = new int[3][]; //120
            houseInfoStorage[2][0] = new int[] { 20, 72, -1 }; //0,1
            houseInfoStorage[2][1] = new int[] { 28, -1, -1 }; //0
            houseInfoStorage[2][2] = new int[] { -1, -1, -1 }; //NA

            //ISLAND 2 HOUSE 0
            questCaps[2][0][0] = new int[] { 6, 2500, 700 };
            questCaps[2][0][1] = new int[] { 1, 1, 1 };
            questCaps[2][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 2 HOUSE 1
            questCaps[2][1][0] = new int[] { 4, 12, 12 };
            questCaps[2][1][1] = new int[] { -1, -1, -1 };
            questCaps[2][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 2 HOUSE 2
            questCaps[2][2][0] = new int[] { -1, -1, -1 };
            questCaps[2][2][1] = new int[] { -1, -1, -1 };
            questCaps[2][2][2] = new int[] { -1, -1, -1 };


            //ISLAND 2 HOUSE 0
            questTracking[2][0][0] = new int[] { 6, 2500, 700 };
            questTracking[2][0][1] = new int[] { 1, 1, 1 };
            questTracking[2][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 2 HOUSE 1
            questTracking[2][1][0] = new int[] { 4, 12, 12 };
            questTracking[2][1][1] = new int[] { -1, -1, -1 };
            questTracking[2][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 2 HOUSE 2
            questTracking[2][2][0] = new int[] { -1, -1, -1 };
            questTracking[2][2][1] = new int[] { -1, -1, -1 };
            questTracking[2][2][2] = new int[] { -1, -1, -1 };



            //ISLAND 2 HOUSE 0
            questTypeInfo[2][0][0] = new int[] { 0, 6, 1 };
            questTypeInfo[2][0][1] = new int[] { 2, 2, 2 };
            questTypeInfo[2][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 2 HOUSE 1
            questTypeInfo[2][1][0] = new int[] { 5, 3, 4 };
            questTypeInfo[2][1][1] = new int[] { -1, -1, -1 };
            questTypeInfo[2][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 2 HOUSE 2
            questTypeInfo[2][2][0] = new int[] { -1, -1, -1 };
            questTypeInfo[2][2][1] = new int[] { -1, -1, -1 };
            questTypeInfo[2][2][2] = new int[] { -1, -1, -1 };



            //ISLAND 2 HOUSE 0
            questRarityInfo[2][0][0] = new int[] { 1, -1, 0 };
            questRarityInfo[2][0][1] = new int[] { 0, 1, 2 };
            questRarityInfo[2][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 2 HOUSE 1
            questRarityInfo[2][1][0] = new int[] { -1, -1, 0 };
            questRarityInfo[2][1][1] = new int[] { -1, -1, -1 };
            questRarityInfo[2][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 2 HOUSE 2
            questRarityInfo[2][2][0] = new int[] { -1, -1, -1 };
            questRarityInfo[2][2][1] = new int[] { -1, -1, -1 };
            questRarityInfo[2][2][2] = new int[] { -1, -1, -1 };

            //____________________________________________________________________________

            //ISLAND 3
            houseInfoStorage[3] = new int[3][]; //160
            houseInfoStorage[3][0] = new int[] { 100, -1, -1 }; //0
            houseInfoStorage[3][1] = new int[] { 60, -1, -1 }; //0
            houseInfoStorage[3][2] = new int[] { -1, -1, -1 }; //NA

            //ISLAND 3 HOUSE 0
            questCaps[3][0][0] = new int[] { 3000, 10, 800 };
            questCaps[3][0][1] = new int[] { -1, -1, -1 };
            questCaps[3][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 3 HOUSE 1
            questCaps[3][1][0] = new int[] { 10, 1, 20 };
            questCaps[3][1][1] = new int[] { -1, -1, -1 };
            questCaps[3][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 3 HOUSE 2
            questCaps[3][2][0] = new int[] { -1, -1, -1 };
            questCaps[3][2][1] = new int[] { -1, -1, -1 };
            questCaps[3][2][2] = new int[] { -1, -1, -1 };


            //ISLAND 3 HOUSE 0
            questTracking[3][0][0] = new int[] { 3000, 10, 800 };
            questTracking[3][0][1] = new int[] { -1, -1, -1 };
            questTracking[3][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 3 HOUSE 1
            questTracking[3][1][0] = new int[] { 10, 1, 20 };
            questTracking[3][1][1] = new int[] { -1, -1, -1 };
            questTracking[3][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 3 HOUSE 2
            questTracking[3][2][0] = new int[] { -1, -1, -1 };
            questTracking[3][2][1] = new int[] { -1, -1, -1 };
            questTracking[3][2][2] = new int[] { -1, -1, -1 };


            //ISLAND 3 HOUSE 0
            questTypeInfo[3][0][0] = new int[] { 6, 0, 1 };
            questTypeInfo[3][0][1] = new int[] { -1, -1, -1 };
            questTypeInfo[3][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 3 HOUSE 1
            questTypeInfo[3][1][0] = new int[] { 0, 2, 3 };
            questTypeInfo[3][1][1] = new int[] { -1, -1, -1 };
            questTypeInfo[3][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 3 HOUSE 2
            questTypeInfo[3][2][0] = new int[] { -1, -1, -1 };
            questTypeInfo[3][2][1] = new int[] { -1, -1, -1 };
            questTypeInfo[3][2][2] = new int[] { -1, -1, -1 };


            //ISLAND 3 HOUSE 0
            questRarityInfo[3][0][0] = new int[] { -1, 0, 0 };
            questRarityInfo[3][0][1] = new int[] { -1, -1, -1 };
            questRarityInfo[3][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 3 HOUSE 1
            questRarityInfo[3][1][0] = new int[] { 1, 0, -1 };
            questRarityInfo[3][1][1] = new int[] { -1, -1, -1 };
            questRarityInfo[3][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 3 HOUSE 2
            questRarityInfo[3][2][0] = new int[] { -1, -1, -1 };
            questRarityInfo[3][2][1] = new int[] { -1, -1, -1 };
            questRarityInfo[3][2][2] = new int[] { -1, -1, -1 };

            //____________________________________________________________________________

            //ISLAND 4
            houseInfoStorage[4] = new int[3][]; //200
            houseInfoStorage[4][0] = new int[] { 50, 60, -1 }; //0,1
            houseInfoStorage[4][1] = new int[] { 90, -1, -1 }; //0
            houseInfoStorage[4][2] = new int[] { -1, -1, -1 }; //NA

            //ISLAND 4 HOUSE 0
            questCaps[4][0][0] = new int[] { 10, 5, 200 };
            questCaps[4][0][1] = new int[] { 15, 10, 30 };
            questCaps[4][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 4 HOUSE 1
            questCaps[4][1][0] = new int[] { 1, 1, 1 };
            questCaps[4][1][1] = new int[] { -1, -1, -1 };
            questCaps[4][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 44 HOUSE 2
            questCaps[4][2][0] = new int[] { -1, -1, -1 };
            questCaps[4][2][1] = new int[] { -1, -1, -1 };
            questCaps[4][2][2] = new int[] { -1, -1, -1 };


            //ISLAND 4 HOUSE 0
            questTracking[4][0][0] = new int[] { 10, 5, 200 };
            questTracking[4][0][1] = new int[] { 15, 10, 30 };
            questTracking[4][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 4 HOUSE 1
            questTracking[4][1][0] = new int[] { 1, 1, 1 };
            questTracking[4][1][1] = new int[] { -1, -1, -1 };
            questTracking[4][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 4 HOUSE 2
            questTracking[4][2][0] = new int[] { -1, -1, -1 };
            questTracking[4][2][1] = new int[] { -1, -1, -1 };
            questTracking[4][2][2] = new int[] { -1, -1, -1 };


            questTypeInfo[4][0][0] = new int[] { 5, 0, 1 };
            questTypeInfo[4][0][1] = new int[] { 0, 0, 3 };
            questTypeInfo[4][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 4 HOUSE 1
            questTypeInfo[4][1][0] = new int[] { 2, 2, 2 };
            questTypeInfo[4][1][1] = new int[] { -1, -1, -1 };
            questTypeInfo[4][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 4 HOUSE 2
            questTypeInfo[4][2][0] = new int[] { -1, -1, -1 };
            questTypeInfo[4][2][1] = new int[] { -1, -1, -1 };
            questTypeInfo[4][2][2] = new int[] { -1, -1, -1 };


            questRarityInfo[4][0][0] = new int[] { -1, 2, 1 };
            questRarityInfo[4][0][1] = new int[] { 0, 1, -1 };
            questRarityInfo[4][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 4 HOUSE 1
            questRarityInfo[4][1][0] = new int[] { 0, 1, 2 };
            questRarityInfo[4][1][1] = new int[] { -1, -1, -1 };
            questRarityInfo[4][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 4 HOUSE 2
            questRarityInfo[4][2][0] = new int[] { -1, -1, -1 };
            questRarityInfo[4][2][1] = new int[] { -1, -1, -1 };
            questRarityInfo[4][2][2] = new int[] { -1, -1, -1 };

            //____________________________________________________________________________

            //ISLAND 5
            houseInfoStorage[5] = new int[3][]; //240
            houseInfoStorage[5][0] = new int[] { 100, -1, -1 }; //0
            houseInfoStorage[5][1] = new int[] { 60, 80, -1 }; //0,1
            houseInfoStorage[5][2] = new int[] { -1, -1, -1 }; //NA

            //ISLAND 5 HOUSE 0
            questCaps[5][0][0] = new int[] { 20, 400, 900 };
            questCaps[5][0][1] = new int[] { -1, -1, -1 };
            questCaps[5][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 5 HOUSE 1
            questCaps[5][1][0] = new int[] { 1, 1, 30 };
            questCaps[5][1][1] = new int[] { 14, 20, 3500 };
            questCaps[5][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 5 HOUSE 2
            questCaps[5][2][0] = new int[] { -1, -1, -1 };
            questCaps[5][2][1] = new int[] { -1, -1, -1 };
            questCaps[5][2][2] = new int[] { -1, -1, -1 };


            //ISLAND 5 HOUSE 0
            questTracking[5][0][0] = new int[] { 20, 400, 900 };
            questTracking[5][0][1] = new int[] { -1, -1, -1 };
            questTracking[5][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 5 HOUSE 1
            questTracking[5][1][0] = new int[] { 1, 1, 30 };
            questTracking[5][1][1] = new int[] { 14, 20, 3500 };
            questTracking[5][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 5 HOUSE 2
            questTracking[5][2][0] = new int[] { -1, -1, -1 };
            questTracking[5][2][1] = new int[] { -1, -1, -1 };
            questTracking[5][2][2] = new int[] { -1, -1, -1 };



            //ISLAND 5 HOUSE 0
            questTypeInfo[5][0][0] = new int[] { 0, 1, 1 };
            questTypeInfo[5][0][1] = new int[] { -1, -1, -1 };
            questTypeInfo[5][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 5 HOUSE 1
            questTypeInfo[5][1][0] = new int[] { 2, 2, 3 };
            questTypeInfo[5][1][1] = new int[] { 4, 5, 6 };
            questTypeInfo[5][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 5 HOUSE 2
            questTypeInfo[5][2][0] = new int[] { -1, -1, -1 };
            questTypeInfo[5][2][1] = new int[] { -1, -1, -1 };
            questTypeInfo[5][2][2] = new int[] { -1, -1, -1 };


            //ISLAND 5 HOUSE 0
            questRarityInfo[5][0][0] = new int[] { 0, 1, 0 };
            questRarityInfo[5][0][1] = new int[] { -1, -1, -1 };
            questRarityInfo[5][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 5 HOUSE 1
            questRarityInfo[5][1][0] = new int[] { 0, 1, -1 };
            questRarityInfo[5][1][1] = new int[] { 1, -1, -1 };
            questRarityInfo[5][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 5 HOUSE 2
            questRarityInfo[5][2][0] = new int[] { -1, -1, -1 };
            questRarityInfo[5][2][1] = new int[] { -1, -1, -1 };
            questRarityInfo[5][2][2] = new int[] { -1, -1, -1 };

            //____________________________________________________________________________

            //ISLAND 6
            houseInfoStorage[6] = new int[3][]; //280
            houseInfoStorage[6][0] = new int[] { 190, -1, -1 }; //0
            houseInfoStorage[6][1] = new int[] { 90, -1, -1 }; //0
            houseInfoStorage[6][2] = new int[] { -1, -1, -1 }; //NA

            //ISLAND 6 HOUSE 0
            questCaps[6][0][0] = new int[] { 40, 20, 1 };
            questCaps[6][0][1] = new int[] { -1, -1, -1 };
            questCaps[6][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 6 HOUSE 1
            questCaps[6][1][0] = new int[] { 599, 50, 5000 };
            questCaps[6][1][1] = new int[] { -1, -1, -1 };
            questCaps[6][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 6 HOUSE 2
            questCaps[6][2][0] = new int[] { -1, -1, -1 };
            questCaps[6][2][1] = new int[] { -1, -1, -1 };
            questCaps[6][2][2] = new int[] { -1, -1, -1 };


            //ISLAND 6 HOUSE 0
            questTracking[6][0][0] = new int[] { 40, 20, 1 };
            questTracking[6][0][1] = new int[] { -1, -1, -1 };
            questTracking[6][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 6 HOUSE 1
            questTracking[6][1][0] = new int[] { 599, 50, 5000 };
            questTracking[6][1][1] = new int[] { -1, -1, -1 };
            questTracking[6][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 6 HOUSE 2
            questTracking[6][2][0] = new int[] { -1, -1, -1 };
            questTracking[6][2][1] = new int[] { -1, -1, -1 };
            questTracking[6][2][2] = new int[] { -1, -1, -1 };



            //ISLAND 6 HOUSE 0
            questTypeInfo[6][0][0] = new int[] { 0, 0, 2 };
            questTypeInfo[6][0][1] = new int[] { -1, -1, -1 };
            questTypeInfo[6][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 6 HOUSE 1
            questTypeInfo[6][1][0] = new int[] { 1, 3, 6 };
            questTypeInfo[6][1][1] = new int[] { -1, -1, -1 };
            questTypeInfo[6][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 6 HOUSE 2
            questTypeInfo[6][2][0] = new int[] { -1, -1, -1 };
            questTypeInfo[6][2][1] = new int[] { -1, -1, -1 };
            questTypeInfo[6][2][2] = new int[] { -1, -1, -1 };


            //ISLAND 6 HOUSE 0
            questRarityInfo[6][0][0] = new int[] { 0, 3, 0 };
            questRarityInfo[6][0][1] = new int[] { -1, -1, -1 };
            questRarityInfo[6][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 6 HOUSE 1
            questRarityInfo[6][1][0] = new int[] { 1, -1, -1 };
            questRarityInfo[6][1][1] = new int[] { -1, -1, -1 };
            questRarityInfo[6][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 6 HOUSE 2
            questRarityInfo[6][2][0] = new int[] { -1, -1, -1 };
            questRarityInfo[6][2][1] = new int[] { -1, -1, -1 };
            questRarityInfo[6][2][2] = new int[] { -1, -1, -1 };

            //____________________________________________________________________________

            //ISLAND 7
            houseInfoStorage[7] = new int[3][]; //320
            houseInfoStorage[7][0] = new int[] { 87, 63, 60 }; //0,1,2
            houseInfoStorage[7][1] = new int[] { 110, -1, -1 }; //0
            houseInfoStorage[7][2] = new int[] { -1, -1, -1 }; //NA

            //ISLAND 7 HOUSE 0
            questCaps[7][0][0] = new int[] { 40, 30, 20 };
            questCaps[7][0][1] = new int[] { 1000, 1, 1 };
            questCaps[7][0][2] = new int[] { 30, 1, 50 };
            //ISLAND 7 HOUSE 1
            questCaps[7][1][0] = new int[] { 5000, 25, 15 };
            questCaps[7][1][1] = new int[] { -1, -1, -1 };
            questCaps[7][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 7 HOUSE 2
            questCaps[7][2][0] = new int[] { -1, -1, -1 };
            questCaps[7][2][1] = new int[] { -1, -1, -1 };
            questCaps[7][2][2] = new int[] { -1, -1, -1 };


            //ISLAND 7 HOUSE 0
            questTracking[7][0][0] = new int[] { 40, 30, 20 };
            questTracking[7][0][1] = new int[] { 1000, 1, 1 };
            questTracking[7][0][2] = new int[] { 30, 1, 50 };
            //ISLAND 7 HOUSE 1
            questTracking[7][1][0] = new int[] { 5000, 25, 15 };
            questTracking[7][1][1] = new int[] { -1, -1, -1 };
            questTracking[7][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 7 HOUSE 2
            questTracking[7][2][0] = new int[] { -1, -1, -1 };
            questTracking[7][2][1] = new int[] { -1, -1, -1 };
            questTracking[7][2][2] = new int[] { -1, -1, -1 };


            //ISLAND 7 HOUSE 0
            questTypeInfo[7][0][0] = new int[] { 0, 0, 0 };
            questTypeInfo[7][0][1] = new int[] { 1, 2, 2 };
            questTypeInfo[7][0][2] = new int[] { 0, 2, 3 };
            //ISLAND 7 HOUSE 1
            questTypeInfo[7][1][0] = new int[] { 6, 5, 4 };
            questTypeInfo[7][1][1] = new int[] { -1, -1, -1 };
            questTypeInfo[7][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 7 HOUSE 2
            questTypeInfo[7][2][0] = new int[] { -1, -1, -1 };
            questTypeInfo[7][2][1] = new int[] { -1, -1, -1 };
            questTypeInfo[7][2][2] = new int[] { -1, -1, -1 };


            //ISLAND 7 HOUSE 0
            questRarityInfo[7][0][0] = new int[] { 0, 1, 2 };
            questRarityInfo[7][0][1] = new int[] { 0, 0, 1 };
            questRarityInfo[7][0][2] = new int[] { 0, 2, -1 };
            //ISLAND 7 HOUSE 1
            questRarityInfo[7][1][0] = new int[] { -1, -1, 2 };
            questRarityInfo[7][1][1] = new int[] { -1, -1, -1 };
            questRarityInfo[7][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 7 HOUSE 2
            questRarityInfo[7][2][0] = new int[] { -1, -1, -1 };
            questRarityInfo[7][2][1] = new int[] { -1, -1, -1 };
            questRarityInfo[7][2][2] = new int[] { -1, -1, -1 };

            //____________________________________________________________________________

            //ISLAND 8
            houseInfoStorage[8] = new int[3][]; //360
            houseInfoStorage[8][0] = new int[] { 90, 120, -1 }; //0,1
            houseInfoStorage[8][1] = new int[] { 150, -1, -1 }; //0
            houseInfoStorage[8][2] = new int[] { -1, -1, -1 }; //NA

            //ISLAND 8 HOUSE 0
            questCaps[8][0][0] = new int[] { 15, 1, 30 };
            questCaps[8][0][1] = new int[] { 8000, 25, 20 };
            questCaps[8][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 8 HOUSE 1
            questCaps[8][1][0] = new int[] { 500, 1, 1 };
            questCaps[8][1][1] = new int[] { -1, -1, -1 };
            questCaps[8][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 8 HOUSE 2
            questCaps[8][2][0] = new int[] { -1, -1, -1 };
            questCaps[8][2][1] = new int[] { -1, -1, -1 };
            questCaps[8][2][2] = new int[] { -1, -1, -1 };


            //ISLAND 8 HOUSE 0
            questTracking[8][0][0] = new int[] { 15, 1, 30 };
            questTracking[8][0][1] = new int[] { 8000, 25, 20 };
            questTracking[8][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 8 HOUSE 1
            questTracking[8][1][0] = new int[] { 500, 1, 1 };
            questTracking[8][1][1] = new int[] { -1, -1, -1 };
            questTracking[8][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 8 HOUSE 2
            questTracking[8][2][0] = new int[] { -1, -1, -1 };
            questTracking[8][2][1] = new int[] { -1, -1, -1 };
            questTracking[8][2][2] = new int[] { -1, -1, -1 };


            //ISLAND 8 HOUSE 0
            questTypeInfo[8][0][0] = new int[] { 5, 2, 3 };
            questTypeInfo[8][0][1] = new int[] { 6, 0, 0 };
            questTypeInfo[8][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 8 HOUSE 1
            questTypeInfo[8][1][0] = new int[] { 1, 2, 2 };
            questTypeInfo[8][1][1] = new int[] { -1, -1, -1 };
            questTypeInfo[8][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 8 HOUSE 2
            questTypeInfo[8][2][0] = new int[] { -1, -1, -1 };
            questTypeInfo[8][2][1] = new int[] { -1, -1, -1 };
            questTypeInfo[8][2][2] = new int[] { -1, -1, -1 };


            //ISLAND 8 HOUSE 0
            questRarityInfo[8][0][0] = new int[] { -1, 0, -1 };
            questRarityInfo[8][0][1] = new int[] { -1, 0, 1 };
            questRarityInfo[8][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 8 HOUSE 1
            questRarityInfo[8][1][0] = new int[] { 2, 1, 2 };
            questRarityInfo[8][1][1] = new int[] { -1, -1, -1 };
            questRarityInfo[8][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 8 HOUSE 2
            questRarityInfo[8][2][0] = new int[] { -1, -1, -1 };
            questRarityInfo[8][2][1] = new int[] { -1, -1, -1 };
            questRarityInfo[8][2][2] = new int[] { -1, -1, -1 };

            //____________________________________________________________________________

            //ISLAND 9
            houseInfoStorage[9] = new int[3][]; //400
            houseInfoStorage[9][0] = new int[] { 180, 90, -1 }; //0,1
            houseInfoStorage[9][1] = new int[] { 130, -1, -1 }; //0
            houseInfoStorage[9][2] = new int[] { -1, -1, -1 }; //NA

            //ISLAND 9 HOUSE 0
            questCaps[9][0][0] = new int[] { 50, 700, 1 };
            questCaps[9][0][1] = new int[] { 40, 20, 25 };
            questCaps[9][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 9 HOUSE 1
            questCaps[9][1][0] = new int[] { 10000, 40, 35 };
            questCaps[9][1][1] = new int[] { -1, -1, -1 };
            questCaps[9][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 9 HOUSE 2
            questCaps[9][2][0] = new int[] { -1, -1, -1 };
            questCaps[9][2][1] = new int[] { -1, -1, -1 };
            questCaps[9][2][2] = new int[] { -1, -1, -1 };


            //ISLAND 9 HOUSE 0
            questTracking[9][0][0] = new int[] { 50, 700, 1 };
            questTracking[9][0][1] = new int[] { 40, 20, 25 };
            questTracking[9][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 9 HOUSE 1
            questTracking[9][1][0] = new int[] { 10000, 40, 35 };
            questTracking[9][1][1] = new int[] { -1, -1, -1 };
            questTracking[9][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 9 HOUSE 2
            questTracking[9][2][0] = new int[] { -1, -1, -1 };
            questTracking[9][2][1] = new int[] { -1, -1, -1 };
            questTracking[9][2][2] = new int[] { -1, -1, -1 };


            //ISLAND 9 HOUSE 0
            questTypeInfo[9][0][0] = new int[] { 0, 1, 2 };
            questTypeInfo[9][0][1] = new int[] { 3, 4, 5 };
            questTypeInfo[9][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 9 HOUSE 1
            questTypeInfo[9][1][0] = new int[] { 6, 0, 0 };
            questTypeInfo[9][1][1] = new int[] { -1, -1, -1 };
            questTypeInfo[9][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 9 HOUSE 2
            questTypeInfo[9][2][0] = new int[] { -1, -1, -1 };
            questTypeInfo[9][2][1] = new int[] { -1, -1, -1 };
            questTypeInfo[9][2][2] = new int[] { -1, -1, -1 };


            //ISLAND 9 HOUSE 0
            questRarityInfo[9][0][0] = new int[] { 0, 2, 0 };
            questRarityInfo[9][0][1] = new int[] { -1, 1, -1 };
            questRarityInfo[9][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 9 HOUSE 1
            questRarityInfo[9][1][0] = new int[] { -1, 1, 2 };
            questRarityInfo[9][1][1] = new int[] { -1, -1, -1 };
            questRarityInfo[9][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 9 HOUSE 2
            questRarityInfo[9][2][0] = new int[] { -1, -1, -1 };
            questRarityInfo[9][2][1] = new int[] { -1, -1, -1 };
            questRarityInfo[9][2][2] = new int[] { -1, -1, -1 };

            //____________________________________________________________________________

            //ISLAND 10
            houseInfoStorage[10] = new int[3][]; //440
            houseInfoStorage[10][0] = new int[] { 120, -1, -1 }; //0
            houseInfoStorage[10][1] = new int[] { 80, 60, -1 }; //0,1
            houseInfoStorage[10][2] = new int[] { 200, -1, -1 }; //0

            //ISLAND 10 HOUSE 0
            questCaps[10][0][0] = new int[] { 60, 40, 40 };
            questCaps[10][0][1] = new int[] { -1, -1, -1 };
            questCaps[10][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 10 HOUSE 1
            questCaps[10][1][0] = new int[] { 1000, 1050, 800 };
            questCaps[10][1][1] = new int[] { 50, 45, 20 };
            questCaps[10][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 10 HOUSE 2
            questCaps[10][2][0] = new int[] { 10, 1, 12000 };
            questCaps[10][2][1] = new int[] { -1, -1, -1 };
            questCaps[10][2][2] = new int[] { -1, -1, -1 };


            //ISLAND 10 HOUSE 0
            questTracking[10][0][0] = new int[] { 60, 40, 40 };
            questTracking[10][0][1] = new int[] { -1, -1, -1 };
            questTracking[10][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 10 HOUSE 1
            questTracking[10][1][0] = new int[] { 1000, 1050, 800 };
            questTracking[10][1][1] = new int[] { 50, 45, 20 };
            questTracking[10][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 10 HOUSE 2
            questTracking[10][2][0] = new int[] { 10, 1, 12000 };
            questTracking[10][2][1] = new int[] { -1, -1, -1 };
            questTracking[10][2][2] = new int[] { -1, -1, -1 };


            //ISLAND 10 HOUSE 0
            questTypeInfo[10][0][0] = new int[] { 0, 0, 0 };
            questTypeInfo[10][0][1] = new int[] { -1, -1, -1 };
            questTypeInfo[10][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 10 HOUSE 1
            questTypeInfo[10][1][0] = new int[] { 1, 1, 1 };
            questTypeInfo[10][1][1] = new int[] { 3, 5, 4 };
            questTypeInfo[10][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 10 HOUSE 2
            questTypeInfo[10][2][0] = new int[] { 0, 2, 6 };
            questTypeInfo[10][2][1] = new int[] { -1, -1, -1 };
            questTypeInfo[10][2][2] = new int[] { -1, -1, -1 };


            //ISLAND 10 HOUSE 0
            questRarityInfo[10][0][0] = new int[] { 0, 1, 2 };
            questRarityInfo[10][0][1] = new int[] { -1, -1, -1 };
            questRarityInfo[10][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 10 HOUSE 1
            questRarityInfo[10][1][0] = new int[] { 2, 0, 1 };
            questRarityInfo[10][1][1] = new int[] { -1, -1, 2 };
            questRarityInfo[10][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 10 HOUSE 2
            questRarityInfo[10][2][0] = new int[] { 3, 0, -1 };
            questRarityInfo[10][2][1] = new int[] { -1, -1, -1 };
            questRarityInfo[10][2][2] = new int[] { -1, -1, -1 };

            //____________________________________________________________________________

            //ISLAND 11
            houseInfoStorage[11] = new int[3][]; //480
            houseInfoStorage[11][0] = new int[] { 210, -1, -1 }; //0
            houseInfoStorage[11][1] = new int[] { 130, 140, -1 }; //0,1
            houseInfoStorage[11][2] = new int[] { -1, -1, -1 }; //NA

            //ISLAND 11 HOUSE 0
            questCaps[11][0][0] = new int[] { 1, 1, 1 };
            questCaps[11][0][1] = new int[] { -1, -1, -1 };
            questCaps[11][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 11 HOUSE 1
            questCaps[11][1][0] = new int[] { 80, 20, 1500 };
            questCaps[11][1][1] = new int[] { 70, 6, 15000 };
            questCaps[11][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 11 HOUSE 2
            questCaps[11][2][0] = new int[] { -1, -1, -1 };
            questCaps[11][2][1] = new int[] { -1, -1, -1 };
            questCaps[11][2][2] = new int[] { -1, -1, -1 };


            //ISLAND 11 HOUSE 0
            questTracking[11][0][0] = new int[] { 1, 1, 1 };
            questTracking[11][0][1] = new int[] { -1, -1, -1 };
            questTracking[11][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 11 HOUSE 1
            questTracking[11][1][0] = new int[] { 80, 20, 1500};
            questTracking[11][1][1] = new int[] { 70, 6, 15000 };
            questTracking[11][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 11 HOUSE 2
            questTracking[11][2][0] = new int[] { -1, -1, -1 };
            questTracking[11][2][1] = new int[] { -1, -1, -1 };
            questTracking[11][2][2] = new int[] { -1, -1, -1 };


            //ISLAND 11 HOUSE 0
            questTypeInfo[11][0][0] = new int[] { 2, 2, 2 };
            questTypeInfo[11][0][1] = new int[] { -1, -1, -1 };
            questTypeInfo[11][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 11 HOUSE 1
            questTypeInfo[11][1][0] = new int[] { 0, 0, 1 };
            questTypeInfo[11][1][1] = new int[] { 3, 4, 6 };
            questTypeInfo[11][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 11 HOUSE 2
            questTypeInfo[11][2][0] = new int[] { -1, -1, -1 };
            questTypeInfo[11][2][1] = new int[] { -1, -1, -1 };
            questTypeInfo[11][2][2] = new int[] { -1, -1, -1 };


            //ISLAND 11 HOUSE 0
            questRarityInfo[11][0][0] = new int[] { 0, 1, 2 };
            questRarityInfo[11][0][1] = new int[] { -1, -1, -1 };
            questRarityInfo[11][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 11 HOUSE 1
            questRarityInfo[11][1][0] = new int[] { 0, 3, 2 };
            questRarityInfo[11][1][1] = new int[] { -1, 3, -1 };
            questRarityInfo[11][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 11 HOUSE 2
            questRarityInfo[11][2][0] = new int[] { -1, -1, -1 };
            questRarityInfo[11][2][1] = new int[] { -1, -1, -1 };
            questRarityInfo[11][2][2] = new int[] { -1, -1, -1 };

            //____________________________________________________________________________

            //ISLAND 12
            houseInfoStorage[12] = new int[3][]; //520
            houseInfoStorage[12][0] = new int[] { 80, 90, 80 }; //0,1,2
            houseInfoStorage[12][1] = new int[] { 100, -1, -1 }; //0
            houseInfoStorage[12][2] = new int[] { 60, 110, -1 }; //0,1

            //ISLAND 12 HOUSE 0
            questCaps[12][0][0] = new int[] { 30, 20, 15 };
            questCaps[12][0][1] = new int[] { 15, 10, 10 };
            questCaps[12][0][2] = new int[] { 40, 15, 10 };
            //ISLAND 12 HOUSE 1
            questCaps[12][1][0] = new int[] { 300, 1, 1 };
            questCaps[12][1][1] = new int[] { -1, -1, -1 };
            questCaps[12][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 12 HOUSE 2
            questCaps[12][2][0] = new int[] { 70, 40, 70 };
            questCaps[12][2][1] = new int[] { 60, 50, 14000 };
            questCaps[12][2][2] = new int[] { -1, -1, -1 };


            //ISLAND 12 HOUSE 0
            questTracking[12][0][0] = new int[] { 30, 20, 15 };
            questTracking[12][0][1] = new int[] { 15, 10, 10 };
            questTracking[12][0][2] = new int[] { 40, 15, 10 };
            //ISLAND 12 HOUSE 1
            questTracking[12][1][0] = new int[] { 300, 1, 1 };
            questTracking[12][1][1] = new int[] { -1, -1, -1 };
            questTracking[12][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 12 HOUSE 2
            questTracking[12][2][0] = new int[] { 70, 40, 70 };
            questTracking[12][2][1] = new int[] { 60, 50, 14000 };
            questTracking[12][2][2] = new int[] { -1, -1, -1 };


            //ISLAND 12 HOUSE 0
            questTypeInfo[12][0][0] = new int[] { 0, 0, 0 };
            questTypeInfo[12][0][1] = new int[] { 0, 0, 0 };
            questTypeInfo[12][0][2] = new int[] { 0, 0, 0 };
            //ISLAND 12 HOUSE 1
            questTypeInfo[12][1][0] = new int[] { 1, 2, 2 };
            questTypeInfo[12][1][1] = new int[] { -1, -1, -1 };
            questTypeInfo[12][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 12 HOUSE 2
            questTypeInfo[12][2][0] = new int[] { 3, 4, 5 };
            questTypeInfo[12][2][1] = new int[] { 0, 0, 6 };
            questTypeInfo[12][2][2] = new int[] { -1, -1, -1 };



            //ISLAND 12 HOUSE 0
            questRarityInfo[12][0][0] = new int[] { 0, 1, 2 };
            questRarityInfo[12][0][1] = new int[] { 1, 2, 3 };
            questRarityInfo[12][0][2] = new int[] { 0, 2, 3 };
            //ISLAND 12 HOUSE 1
            questRarityInfo[12][1][0] = new int[] { 3, 0, 1 };
            questRarityInfo[12][1][1] = new int[] { -1, -1, -1 };
            questRarityInfo[12][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 12 HOUSE 2
            questRarityInfo[12][2][0] = new int[] { -1, 1, -1 };
            questRarityInfo[12][2][1] = new int[] { 0, 1, -1 };
            questRarityInfo[12][2][2] = new int[] { -1, -1, -1 };

            //____________________________________________________________________________

            //ISLAND 13
            houseInfoStorage[13] = new int[3][]; //560
            houseInfoStorage[13][0] = new int[] { 140, 80, -1 }; //0,1
            houseInfoStorage[13][1] = new int[] { 160, -1, -1 }; //0
            houseInfoStorage[13][2] = new int[] { 180, -1, -1 }; //0

            //ISLAND 13 HOUSE 0
            questCaps[13][0][0] = new int[] { 16000, 75, 25 };
            questCaps[13][0][1] = new int[] { 105, 80, 60 };
            questCaps[13][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 13 HOUSE 1
            questCaps[13][1][0] = new int[] { 80, 1, 30 };
            questCaps[13][1][1] = new int[] { -1, -1, -1 };
            questCaps[13][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 13 HOUSE 2
            questCaps[13][2][0] = new int[] { 800, 1100, 1800 };
            questCaps[13][2][1] = new int[] { -1, -1, -1 };
            questCaps[13][2][2] = new int[] { -1, -1, -1 };


            //ISLAND 13 HOUSE 0
            questTracking[13][0][0] = new int[] { 16000, 75, 25 };
            questTracking[13][0][1] = new int[] { 105, 80, 60 };
            questTracking[13][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 13 HOUSE 1
            questTracking[13][1][0] = new int[] { 80, 1, 30 };
            questTracking[13][1][1] = new int[] { -1, -1, -1 };
            questTracking[13][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 13 HOUSE 2
            questTracking[13][2][0] = new int[] { 800, 1100, 1800 };
            questTracking[13][2][1] = new int[] { -1, -1, -1 };
            questTracking[13][2][2] = new int[] { -1, -1, -1 };


            //ISLAND 13 HOUSE 0
            questTypeInfo[13][0][0] = new int[] { 6, 5, 4 };
            questTypeInfo[13][0][1] = new int[] { 0, 0, 0 };
            questTypeInfo[13][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 13 HOUSE 1
            questTypeInfo[13][1][0] = new int[] { 3, 2, 0 };
            questTypeInfo[13][1][1] = new int[] { -1, -1, -1 };
            questTypeInfo[13][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 13 HOUSE 2
            questTypeInfo[13][2][0] = new int[] { 1, 1, 1 };
            questTypeInfo[13][2][1] = new int[] { -1, -1, -1 };
            questTypeInfo[13][2][2] = new int[] { -1, -1, -1 };



            //ISLAND 13 HOUSE 0
            questRarityInfo[13][0][0] = new int[] { -1, -1, 2 };
            questRarityInfo[13][0][1] = new int[] { 0, 1, 2 };
            questRarityInfo[13][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 13 HOUSE 1
            questRarityInfo[13][1][0] = new int[] { -1, 0, 3 };
            questRarityInfo[13][1][1] = new int[] { -1, -1, -1 };
            questRarityInfo[13][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 13 HOUSE 2
            questRarityInfo[13][2][0] = new int[] { 3, 0, 2 };
            questRarityInfo[13][2][1] = new int[] { -1, -1, -1 };
            questRarityInfo[13][2][2] = new int[] { -1, -1, -1 };

            //____________________________________________________________________________

            //ISLAND 14
            houseInfoStorage[14] = new int[3][]; //600
            houseInfoStorage[14][0] = new int[] { 600, -1, -1 }; //0
            houseInfoStorage[14][1] = new int[] { -1, -1, -1 }; //NA
            houseInfoStorage[14][2] = new int[] { -1, -1, -1 }; //NA

            //ISLAND 14 HOUSE 0
            questCaps[14][0][0] = new int[] { 600, 2200, 100 };
            questCaps[14][0][1] = new int[] { -1, -1, -1 };
            questCaps[14][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 14 HOUSE 1
            questCaps[14][1][0] = new int[] { -1, -1, -1 };
            questCaps[14][1][1] = new int[] { -1, -1, -1 };
            questCaps[14][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 14 HOUSE 2
            questCaps[14][2][0] = new int[] { -1, -1, -1 };
            questCaps[14][2][1] = new int[] { -1, -1, -1 };
            questCaps[14][2][2] = new int[] { -1, -1, -1 };


            //ISLAND 14 HOUSE 0
            questTracking[14][0][0] = new int[] { 600, 2200, 100 };
            questTracking[14][0][1] = new int[] { -1, -1, -1 };
            questTracking[14][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 14 HOUSE 1
            questTracking[14][1][0] = new int[] { -1, -1, -1 };
            questTracking[14][1][1] = new int[] { -1, -1, -1 };
            questTracking[14][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 14 HOUSE 2
            questTracking[14][2][0] = new int[] { -1, -1, -1 };
            questTracking[14][2][1] = new int[] { -1, -1, -1 };
            questTracking[14][2][2] = new int[] { -1, -1, -1 };


            //ISLAND 14 HOUSE 0
            questTypeInfo[14][0][0] = new int[] { 0, 1, 3 };
            questTypeInfo[14][0][1] = new int[] { -1, -1, -1 };
            questTypeInfo[14][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 14 HOUSE 1
            questTypeInfo[14][1][0] = new int[] { -1, -1, -1 };
            questTypeInfo[14][1][1] = new int[] { -1, -1, -1 };
            questTypeInfo[14][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 14 HOUSE 2
            questTypeInfo[14][2][0] = new int[] { -1, -1, -1 };
            questTypeInfo[14][2][1] = new int[] { -1, -1, -1 };
            questTypeInfo[14][2][2] = new int[] { -1, -1, -1 };


            //ISLAND 14 HOUSE 0
            questRarityInfo[14][0][0] = new int[] { 0, 2, -1 };
            questRarityInfo[14][0][1] = new int[] { -1, -1, -1 };
            questRarityInfo[14][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 14 HOUSE 1
            questRarityInfo[14][1][0] = new int[] { -1, -1, -1 };
            questRarityInfo[14][1][1] = new int[] { -1, -1, -1 };
            questRarityInfo[14][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 14 HOUSE 2
            questRarityInfo[14][2][0] = new int[] { -1, -1, -1 };
            questRarityInfo[14][2][1] = new int[] { -1, -1, -1 };
            questRarityInfo[14][2][2] = new int[] { -1, -1, -1 };

            //____________________________________________________________________________

            //ISLAND 15
            houseInfoStorage[15] = new int[3][]; //640
            houseInfoStorage[15][0] = new int[] { 320, -1, -1 }; //0
            houseInfoStorage[15][1] = new int[] { 320, -1, -1 }; //0
            houseInfoStorage[15][2] = new int[] { -1, -1, -1 }; //NA

            //ISLAND 15 HOUSE 0
            questCaps[15][0][0] = new int[] { 100, 1, 1449 };
            questCaps[15][0][1] = new int[] { -1, -1, -1 };
            questCaps[15][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 15 HOUSE 1
            questCaps[15][1][0] = new int[] { 20000, 100, 100 };
            questCaps[15][1][1] = new int[] { -1, -1, -1 };
            questCaps[15][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 15 HOUSE 2
            questCaps[15][2][0] = new int[] { -1, -1, -1 };
            questCaps[15][2][1] = new int[] { -1, -1, -1 };
            questCaps[15][2][2] = new int[] { -1, -1, -1 };


            //ISLAND 15 HOUSE 0
            questTracking[15][0][0] = new int[] { 100, 1, 1449 };
            questTracking[15][0][1] = new int[] { -1, -1, -1 };
            questTracking[15][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 15 HOUSE 1
            questTracking[15][1][0] = new int[] { 20000, 100, 100 };
            questTracking[15][1][1] = new int[] { -1, -1, -1 };
            questTracking[15][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 15 HOUSE 2
            questTracking[15][2][0] = new int[] { -1, -1, -1 };
            questTracking[15][2][1] = new int[] { -1, -1, -1 };
            questTracking[15][2][2] = new int[] { -1, -1, -1 };


            //ISLAND 15 HOUSE 0
            questTypeInfo[15][0][0] = new int[] { 3, 2, 1 };
            questTypeInfo[15][0][1] = new int[] { -1, -1, -1 };
            questTypeInfo[15][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 15 HOUSE 1
            questTypeInfo[15][1][0] = new int[] { 6, 5, 4 };
            questTypeInfo[15][1][1] = new int[] { -1, -1, -1 };
            questTypeInfo[15][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 15 HOUSE 2
            questTypeInfo[15][2][0] = new int[] { -1, -1, -1 };
            questTypeInfo[15][2][1] = new int[] { -1, -1, -1 };
            questTypeInfo[15][2][2] = new int[] { -1, -1, -1 };


            //ISLAND 15 HOUSE 0
            questRarityInfo[15][0][0] = new int[] { -1, 0, 0 };
            questRarityInfo[15][0][1] = new int[] { -1, -1, -1 };
            questRarityInfo[15][0][2] = new int[] { -1, -1, -1 };
            //ISLAND 15 HOUSE 1
            questRarityInfo[15][1][0] = new int[] { -1, -1, 0 };
            questRarityInfo[15][1][1] = new int[] { -1, -1, -1 };
            questRarityInfo[15][1][2] = new int[] { -1, -1, -1 };
            //ISLAND 15 HOUSE 2
            questRarityInfo[15][2][0] = new int[] { -1, -1, -1 };
            questRarityInfo[15][2][1] = new int[] { -1, -1, -1 };
            questRarityInfo[15][2][2] = new int[] { -1, -1, -1 };



            //____________________________________________________________________________

            //TOTAL UNCHANGING

            Quests[0] = new string[3][]; //40
            Quests[0][0] = new string[] { "Give me 1 Normal fish,Score over 400 in Fishy Timing,Complete 3 Tasks", "0", "0" }; //0
            Quests[0][1] = new string[] { "Buy the Character from the Merchant,Gain 1000 Gold,Give me 1 Fancy fish", "0", "0" }; //0
            Quests[0][2] = new string[] { "0", "0", "0" }; //NA

            Quests[1] = new string[3][]; //80
            Quests[1][0] = new string[] { "Give me 3 Fancy fish,Give me 1 Extravagant fish,Score over 600 in Fishy Timing", "0", "0" }; //0
            Quests[1][1] = new string[] { "Complete 6 tasks,Buy the Cat from the Merchant,Gain 2000 Gold", "Give me 5 Normal fish,Get a streak of 2,Obtain 5 Normal bait", "0" }; //0,1
            Quests[1][2] = new string[] { "0", "0", "0" }; //NA

            Quests[2] = new string[3][]; //120
            Quests[2][0] = new string[] { "Give me 6 Fancy fish,Gain 2500 Gold,Score over 700 in Fishy Timing", "Buy the character from the Merchant,Buy the Sail from the Merchant,Buy the Chair from the Merchant", "0" }; //0,1
            Quests[2][1] = new string[] { "Get a task streak of 4 days,Complete 12 tasks,Obtain 12 Normal bait", "0", "0" }; //0
            Quests[2][2] = new string[] { "0", "0", "0" }; //NA

            Quests[3] = new string[3][]; //160
            Quests[3][0] = new string[] { "Gain 3000 Gold,Give me 10 Normal fish,Score over 800 in Fishy Timing", "0", "0" }; //0
            Quests[3][1] = new string[] { "Give me 10 Fancy fish,Buy the Penguin from the Merchant,Complete 20 tasks", "0", "0" }; //0
            Quests[3][2] = new string[] { "0", "0", "0" }; //NA

            Quests[4] = new string[3][]; //200
            Quests[4][0] = new string[] { "Get a streak of 10,Give me 5 Extravagant fish,Score over 200 in Whirlpool Walk", "Give me 15 Normal fish,Give me 10 Fancy fish,Complete 30 tasks", "0" }; //0,1
            Quests[4][1] = new string[] { "Buy the Rod of Sludge,Buy the Sails from the Merchant,Buy the Barrel from the Merchant", "0", "0" }; //0
            Quests[4][2] = new string[] { "0", "0", "0" }; //NA

            Quests[5] = new string[3][]; //240
            Quests[5][0] = new string[] { "Give me 20 Normal fish,Score over 400 in Whirlpool Walk,Score over 900 in Fishy Timing", "0", "0" }; //0
            Quests[5][1] = new string[] { "Buy the Cat from the Merchant,Buy the Sail from the Merchant,Complete 30 tasks", "Obtain 14 Fancy bait,Get a streak of 20,Gain 3500 Gold", "0" }; //0,1
            Quests[5][2] = new string[] { "0", "0", "0" }; //NA

            Quests[6] = new string[3][]; //280
            Quests[6][0] = new string[] { "Give me 40 Normal fish,Give me 20 Extravagant fish,Buy the Character from the Merchant", "0", "0" }; //0
            Quests[6][1] = new string[] { "Score over 599 in Whirlpool Walk,Complete 50 tasks,Gain 5000 Gold", "0", "0" }; //0
            Quests[6][2] = new string[] { "0", "0", "0" }; //NA

            Quests[7] = new string[3][]; //320
            Quests[7][0] = new string[] {"Give me 40 Normal fish,Give me 30 Fancy fish,Give me 20 Extravagant fish",
            "Score over 1000 in Fishy Timing,Buy the Fox from the Merchant,Buy the Sail from the Merchant","Give me 30 Normal fish,Buy the Bird from the Merchant,Complete 50 tasks"}; //0,1,2
            Quests[7][1] = new string[] { "Gain 5000 Gold,Get a streak of 25,Obtain 15 Extravagant bait", "0", "0" }; //0
            Quests[7][2] = new string[] { "0", "0", "0" }; //NA

            Quests[8] = new string[3][]; //360
            Quests[8][0] = new string[] { "Get a streak of 15,Buy the Rod from the Merchant,Complete 30 tasks", "Gain 8000 Gold,Give me 25 Normal fish,Give me 20 Fancy fish", "0" }; //0,1
            Quests[8][1] = new string[] { "Score over 500 in Memory Mania,Buy the Sail from the Merchant,Buy the Barrel from the Merchant", "0", "0" }; //0
            Quests[8][2] = new string[] { "0", "0", "0" }; //NA

            Quests[9] = new string[3][]; //400
            Quests[9][0] = new string[] { "Give me 50 Normal fish,Score over 700 in Memory Mania,Buy the Fox from the Merchant", "Complete 40 tasks,Obtain 20 Fancy bait,Get a streak of 25", "0" }; //0,1
            Quests[9][1] = new string[] { "Gain 10000 Gold,Give me 40 Fancy fish,Give me 35 Extravagant fish", "0", "0" }; //0
            Quests[9][2] = new string[] { "0", "0", "0" }; //NA

            Quests[10] = new string[3][]; //440
            Quests[10][0] = new string[] { "Give me 60 Normal fish,Give me 40 Fancy fish,Give me 40 Extravagant fish", "0", "0" }; //0
            Quests[10][1] = new string[] { "Score over 1000 in Memory Mania,Score over 1050 in Fishy Timing,Score over 800 in Whirlpool Walk", "Complete 50 tasks,Get a streak of 45,Obtain 20 Extravagant bait", "0" }; //0,1
            Quests[10][2] = new string[] { "Give me 10 Pristine fish,Buy the Character from the Merchant,Gain 12000 Gold", "0", "0" }; //0

            Quests[11] = new string[3][]; //480
            Quests[11][0] = new string[] { "Buy the Penguin from the Merchant,Buy the Sail from the Merchant,Buy the Bird from the Merchant", "0", "0" }; //0
            Quests[11][1] = new string[] { "Give me 80 Normal fish,Give me 20 Pristine fish,Score over 1500 in Memory Mania", "Complete 70 tasks,Obtain 6 Pristine bait,Gain 15000 Gold", "0" }; //0,1
            Quests[11][2] = new string[] { "0", "0", "0" }; //NA

            Quests[12] = new string[3][]; //520
            Quests[12][0] = new string[] {"Give me 30 Normal fish,Give me 20 Fancy fish,Give me 15 Extravagant fish",
                "Give me 15 Fancy fish,Give me 10 Extravagant fish,Give me 10 Pristine fish",
                "Give me 40 Normal fish,Give me 15 Extravagant fish,Give me 10 Pristine fish" }; //0,1,2
            Quests[12][1] = new string[] { "Score over 300 in Whack-A-Fish,Buy the Rod from the Merchant,Buy the Sail from the Merchant", "0", "0" }; //0
            Quests[12][2] = new string[] { "Complete 70 tasks,Obtain 40 Fancy bait,Get a streak of 70", "Give me 60 Normal fish,Give me 50 Fancy fish,Gain 14000 Gold", "0" }; //0,1

            Quests[13] = new string[3][]; //560
            Quests[13][0] = new string[] { "Gain 16000 Gold,Get a streak of 75,Obtain 25 Extravagant bait", "Give me 105 Normal fish,Give me 80 Fancy fish,Give me 60 Extravagant fish", "0" }; //0,1
            Quests[13][1] = new string[] { "Complete 80 tasks,Buy the Cat from the Merchant,Give me 30 Pristine fish", "0", "0" }; //0
            Quests[13][2] = new string[] { "Score over 800 in Whack-A-Fish,Score over 1100 in Fishy Timing,Score over 1800 in Memory Mania", "0", "0"}; //0

            Quests[14] = new string[3][]; //600
            Quests[14][0] = new string[] { "Give me 600 Normal fish,Score over 2200 in Memory Mania,Complete 100 tasks", "0", "0" }; //0
            Quests[14][1] = new string[] { "0", "0", "0" }; //NA
            Quests[14][2] = new string[] { "0", "0", "0" }; //NA

            Quests[15] = new string[3][]; //640
            Quests[15][0] = new string[] { "Complete 100 tasks,Buy the Fox from the Merchant,Score over 1449 in Fishy Timing", "0", "0" }; //0
            Quests[15][1] = new string[] { "Gain 20000 Gold,Get a streak of 100,Obtain 100 Normal bait", "0", "0" }; //0
            Quests[15][2] = new string[] { "0", "0", "0" }; //NA


            // CLAIMED STIPENDS / REWARDS LIST INIT
            for (int i = 0; i < 16; i++)
            {
                claimed[i] = new int[3][][];
                for (int j = 0; j < 3; j++)
                {
                    claimed[i][j] = new int[3][];
                    for (int k = 0; k < 3; k++)
                    {
                        claimed[i][j][k] = new int[3];
                    }
                }
            }

            loadCountData();

            loadClaimed();

            loadQuests();

            listInit = true;


            Debug.Log("NOW IT IS "+ questTypeInfo[0][0][0][0]);
        }
    }


    public void claimStipend(int numFishSold, string fishName, int fishRarity)
    {
        //int localFP = IslandFishTotals[curI][curH][curP];
        //int curFP = houseInfoStorage[curI][curH][curP];

        int holder = 0;

        switch (fishRarity)
        {
            case 0:
                holder = numFishSold;
                break;
            case 1:
                holder = numFishSold * 2;
                break;
            case 2:
                holder = numFishSold * 3;
                break;
            case 3:
                holder = numFishSold * 4;
                break;
            case 4:
                holder = numFishSold * 40;
                break;
        }

        goldManager.GainGold(holder * 40);

        merchant.playerGoldTotal.text = goldManager.getGold().ToString();

        stipendClaimWindow[0].SetActive(true);
        stipendClaimText[0].text = "You've sold\n" + numFishSold + " " + fishName + "\nfor " + (holder * 40) + " Gold Coins!";
            //claimed[curI][curH][curP][0] = 1;
            //saveClaimed();
            //claimButton.SetActive(false);
            //textsBelowClaim[0].SetActive(true);
            //textsBelowClaim[1].SetActive(true);

        /*
        //SECOND STIPEND
        if ((localFP - curFP) >= ((localFP / 3) * 2))
        {
            if (claimed[curI][curH][curP][1] == 0)
            {
                goldManager.GainGold((localFP / 3) * 40);

                stipendClaimWindow[1].SetActive(true);
                stipendClaimText[1].text = "Stipend 2 claimed! You've gained " + ((localFP / 3) * 40) + " Dabloons";
                claimed[curI][curH][curP][1] = 1;
                saveClaimed();
                claimButton.SetActive(false);
                textsBelowClaim[0].SetActive(true);
                textsBelowClaim[1].SetActive(true);
            }
        }


        //FULL PAYMENT
        if (curFP == 0)
        {
            if (claimed[curI][curH][curP][2] == 0)
            {
                goldManager.GainGold((localFP - ((localFP / 3) * 2)) * 40);

                stipendClaimWindow[2].SetActive(true);
                stipendClaimText[2].text = "Full payment claimed! You've gained " + ((localFP - ((localFP / 3) * 2)) * 40) + " Dabloons";
                claimed[curI][curH][curP][2] = 1;
                saveClaimed();
                claimButton.SetActive(false);
                textsBelowClaim[0].SetActive(true);
                textsBelowClaim[1].SetActive(true);
            }
        }
        */

        goldBar.goldInit();

        //EXCLAMATION POINT SYSTEM

        int a = merchant.costs[gameProgress.currentIslandIndex][0];
        int b = merchant.costs[gameProgress.currentIslandIndex][1];
        int c = merchant.costs[gameProgress.currentIslandIndex][2];

        inventoryKing.ClearThenLoadInventory();
        bool empty = inventoryKing.checkEmpty();

        //if ((goldManager.PlayerGold >= a && a != 0) || (goldManager.PlayerGold >= b && b != 0) || (goldManager.PlayerGold >= c && c != 0))
        //{
            //CHECKS TO SEE IF ENOUGH GOLD TO BUY ANYTHING, IF SO, SETS EXCLAMATION POINT TO ACTIVE OVER THE MERCHANT. IF NOT, SETS INACTIVE
            //if (a == 0 && b == 0 && c == 0)
            //{
              //  gameProgress.exclamationPoints[curI].transform.GetChild(gameProgress.exclamationPoints[curI].transform.childCount - 1).gameObject.SetActive(false);
            //}
            //else
            //{
              //  gameProgress.exclamationPoints[curI].transform.GetChild(gameProgress.exclamationPoints[curI].transform.childCount - 1).gameObject.SetActive(true);
            //}
        //}
        //else { gameProgress.exclamationPoints[curI].transform.GetChild(gameProgress.exclamationPoints[curI].transform.childCount - 1).gameObject.SetActive(false); }
        /*
        //FIRST HOUSE
        if ((houseInfoStorage[curI][0][0] != -1 && houseInfoStorage[curI][0][0] != 0) ||
            (houseInfoStorage[curI][0][1] != -1 && houseInfoStorage[curI][0][1] != 0) ||
            (houseInfoStorage[curI][0][2] != -1 && houseInfoStorage[curI][0][2] != 0))
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
        if ((houseInfoStorage[curI][1][0] != -1 && houseInfoStorage[curI][1][0] != 0) ||
            (houseInfoStorage[curI][1][1] != -1 && houseInfoStorage[curI][1][1] != 0) ||
            (houseInfoStorage[curI][1][2] != -1 && houseInfoStorage[curI][1][2] != 0))
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
        else if (houseInfoStorage[curI][1][0] == -1 && houseInfoStorage[curI][1][1] == -1 && houseInfoStorage[curI][1][2] == -1)
        {

        }
        else
        {
            gameProgress.exclamationPoints[curI].transform.GetChild(1).gameObject.SetActive(false);
        }


        //THIRD HOUSE
        if ((houseInfoStorage[curI][2][0] != -1 && houseInfoStorage[curI][2][0] != 0) ||
            (houseInfoStorage[curI][2][1] != -1 && houseInfoStorage[curI][2][1] != 0) ||
            (houseInfoStorage[curI][2][2] != -1 && houseInfoStorage[curI][2][2] != 0))
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
        else if (houseInfoStorage[curI][2][0] == -1 && houseInfoStorage[curI][2][1] == -1 && houseInfoStorage[curI][2][2] == -1)
        {

        }
        else
        {
            gameProgress.exclamationPoints[curI].transform.GetChild(2).gameObject.SetActive(false);
        }*/
    }
    

    public void claimAll(int numFishSold, string fishName, int fishRarity)
    {
        //int localFP = IslandFishTotals[curI][curH][curP];
        //int curFP = houseInfoStorage[curI][curH][curP];

        int holder = 0;

        switch (fishRarity)
        {
            case 0:
                holder = numFishSold;
                break;
            case 1:
                holder = numFishSold * 2;
                break;
            case 2:
                holder = numFishSold * 3;
                break;
            case 3:
                holder = numFishSold * 4;
                break;
            case 4:
                holder = numFishSold * 40;
                break;
        }

        goldManager.GainGold(holder * 40);
        merchant.playerGoldTotal.text = goldManager.getGold().ToString();
        goldBar.goldInit();
        //inventoryKing.ClearThenLoadInventory();
        //inventoryKing.checkEmpty();
    }

    public void HouseTap()
    {
        counter = 0;
        for (int i = 0; i < 3; i++)
        {
            if (houseInfoStorage[gameProgress.currentIslandIndex][islandScript.insideHouse][i] >= 0)
            {
                counter++;
            }
        }
        ResidentPopUps[counter - 1].SetActive(true);
        if (counter == 1)
        {
            for (int i = 0; i < counter; i++)
            {
                residentPopUpSpritesSolo[i].sprite = housingSprites.Islands[gameProgress.currentIslandIndex][islandScript.insideHouse][i];

                if (houseInfoStorage[gameProgress.currentIslandIndex][islandScript.insideHouse][i] == 0)
                {
                    backings1[i].color = greeeen;
                }
                else
                {
                    backings1[i].color = Color.white;
                }
            }
        }
        else if (counter == 2)
        {
            for (int i = 0; i < counter; i++)
            {
                residentPopUpSpritesDuo[i].sprite = housingSprites.Islands[gameProgress.currentIslandIndex][islandScript.insideHouse][i];

                residentPopUpSpritesDuo[i].sprite = housingSprites.Islands[gameProgress.currentIslandIndex][islandScript.insideHouse][i];
                if (houseInfoStorage[gameProgress.currentIslandIndex][islandScript.insideHouse][i] == 0)
                {
                    backings2[i].color = greeeen;
                }
                else
                {
                    backings2[i].color = Color.white;
                }
            }
        }
        else if (counter == 3)
        {
            for (int i = 0; i < counter; i++)
            {
                residentPopUpSpritesTrio[i].sprite = housingSprites.Islands[gameProgress.currentIslandIndex][islandScript.insideHouse][i];

                residentPopUpSpritesTrio[i].sprite = housingSprites.Islands[gameProgress.currentIslandIndex][islandScript.insideHouse][i];
                if (houseInfoStorage[gameProgress.currentIslandIndex][islandScript.insideHouse][i] == 0)
                {
                    backings3[i].color = greeeen;
                }
                else
                {
                    backings3[i].color = Color.white;
                }
            }
        }
    }

    public void HouseEnter()
    {
        string quests = Quests[gameProgress.currentIslandIndex][islandScript.insideHouse][islandScript.insidePerson];
        //int localNeeded = houseInfoStorage[gameProgress.currentIslandIndex][islandScript.insideHouse][islandScript.insidePerson];
        //thirds[0].text = (localFP / 3).ToString();
        //thirds[1].text = (localNeeded * 40).ToString();
        //thirds[2].text = (localFP - ((localFP / 3) * 2)).ToString();

        ResidentPopUps[counter - 1].SetActive(false);
        //giveAllButton.SetActive(false);

        //nextIslandButton.SetActive(false);

        residentScreenSprite.sprite = housingSprites.Islands[gameProgress.currentIslandIndex][islandScript.insideHouse][islandScript.insidePerson];
        residentName.text = housingSprites.Names[gameProgress.currentIslandIndex][islandScript.insideHouse][islandScript.insidePerson];
        goldBar.goldInitVillager();
        popUpMenu.SetActive(true);
        sellLocks2.toggleLockedSlots();
        navBar.SetActive(false);
        inventoryKingVillager.ClearThenLoadInventory();
        inventoryVillagerOBJ.SetActive(true);

        //islandScript.barInfo.text = (houseInfoStorage[gameProgress.currentIslandIndex][islandScript.insideHouse][islandScript.insidePerson] * 40).ToString();


        int curI = gameProgress.currentIslandIndex;
        int curH = islandScript.insideHouse;
        int curP = islandScript.insidePerson;
        string holder = Quests[curI][curH][curP];
        string[] personsQuests = holder.Split(',');
        int curFP = houseInfoStorage[curI][curH][curP];

        bool[] sliderInitBools = { false, false, false };

        for (int i = 0; i < 3; i++)
        {
            if (questTracking[curI][curH][curP][i] <= 0)
            {
                sliderInitBools[i] = true;
            }
            else
            {
                sliderInitBools[i] = false;
            }
        }
        questSlider.updateSlider(curI, curH, curP, sliderInitBools[0], sliderInitBools[1], sliderInitBools[2]);

        questTextHolders[0].text = personsQuests[0];
        questTextHolders[1].text = personsQuests[1];
        questTextHolders[2].text = personsQuests[2];

        /* COULD BE USEFUL FOR CHANGING COLOR FOR QUEST DONATIONS
        if (houseInfoStorage[curI][curH][curP] > 0)
        {
            islandScript.yellowBox.color = islandScript.yellow1;
        }
        else
        {
            islandScript.yellowBox.color = islandScript.red1;
        }
        */
    }


    public void houseExit()
    {
        popUpMenu.SetActive(false);
        navBar.SetActive(true);
        inventoryKingVillager.DonateBack();

        int a = merchant.costs[gameProgress.currentIslandIndex][0];
        int b = merchant.costs[gameProgress.currentIslandIndex][1];
        int c = merchant.costs[gameProgress.currentIslandIndex][2];
        //if (a == 0 && b == 0 && c == 0) { nextIslandButton.SetActive(true); }

        int curI = gameProgress.currentIslandIndex;

        inventoryKingVillager.ClearThenLoadInventory();
        bool empty = inventoryKingVillager.checkEmpty();

        /*if ((goldManager.PlayerGold >= a && a != 0) || (goldManager.PlayerGold >= b && b != 0) || (goldManager.PlayerGold >= c && c != 0))
        {
            //CHECKS TO SEE IF ENOUGH GOLD TO BUY ANYTHING, IF SO, SETS EXCLAMATION POINT TO ACTIVE OVER THE MERCHANT. IF NOT, SETS INACTIVE
            if (a == 0 && b == 0 && c == 0)
            {
                gameProgress.exclamationPoints[curI].transform.GetChild(gameProgress.exclamationPoints[curI].transform.childCount - 1).gameObject.SetActive(false);
            }
            else
            {
                gameProgress.exclamationPoints[curI].transform.GetChild(gameProgress.exclamationPoints[curI].transform.childCount - 1).gameObject.SetActive(true);
            }
        }
        else { gameProgress.exclamationPoints[curI].transform.GetChild(gameProgress.exclamationPoints[curI].transform.childCount - 1).gameObject.SetActive(false); }*/
        /*
        //FIRST HOUSE
        if ((houseInfoStorage[curI][0][0] != -1 && houseInfoStorage[curI][0][0] != 0) ||
            (houseInfoStorage[curI][0][1] != -1 && houseInfoStorage[curI][0][1] != 0) ||
            (houseInfoStorage[curI][0][2] != -1 && houseInfoStorage[curI][0][2] != 0))
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
        if ((houseInfoStorage[curI][1][0] != -1 && houseInfoStorage[curI][1][0] != 0) ||
            (houseInfoStorage[curI][1][1] != -1 && houseInfoStorage[curI][1][1] != 0) ||
            (houseInfoStorage[curI][1][2] != -1 && houseInfoStorage[curI][1][2] != 0))
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
        else if (houseInfoStorage[curI][1][0] == -1 && houseInfoStorage[curI][1][1] == -1 && houseInfoStorage[curI][1][2] == -1)
        {

        }
        else
        {
            gameProgress.exclamationPoints[curI].transform.GetChild(1).gameObject.SetActive(false);
        }


        //THIRD HOUSE
        if ((houseInfoStorage[curI][2][0] != -1 && houseInfoStorage[curI][2][0] != 0) ||
            (houseInfoStorage[curI][2][1] != -1 && houseInfoStorage[curI][2][1] != 0) ||
            (houseInfoStorage[curI][2][2] != -1 && houseInfoStorage[curI][2][2] != 0))
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
        else if (houseInfoStorage[curI][2][0] == -1 && houseInfoStorage[curI][2][1] == -1 && houseInfoStorage[curI][2][2] == -1)
        {

        }
        else
        {
            gameProgress.exclamationPoints[curI].transform.GetChild(2).gameObject.SetActive(false);
        }*/
    }

    public void updateQuest(int house, int person, int questIndex, int progress)
    {
        questTracking[gameProgress.currentIslandIndex][house][person][questIndex] -= progress;
        saveQuest();
    }
    
    public void updateQuestManual(int house, int person, int questIndex, int newNum)
    {
        questTracking[gameProgress.currentIslandIndex][house][person][questIndex] = questCaps[gameProgress.currentIslandIndex][house][person][questIndex] - newNum;
        saveQuest();
    }

    public void updateQuestStreak(int house, int person, int questIndex, int newNum)
    {
        if (newNum >= questTracking[gameProgress.currentIslandIndex][house][person][questIndex])
        {
            questTracking[gameProgress.currentIslandIndex][house][person][questIndex] = 0;
        }
        saveQuest();
    }

    public void loadCountData()
    {
        string saveString = SaveSystem.LoadFishCount1();
        if (saveString != null)
        {
            IslandFishStorage loadedData = JsonUtility.FromJson<IslandFishStorage>(saveString);
            houseInfoStorage[0][0] = loadedData.I1H1;
            houseInfoStorage[0][1] = loadedData.I1H2;
            houseInfoStorage[0][2] = loadedData.I1H3;

            houseInfoStorage[1][0] = loadedData.I2H1;
            houseInfoStorage[1][1] = loadedData.I2H2;
            houseInfoStorage[1][2] = loadedData.I2H3;

            houseInfoStorage[2][0] = loadedData.I3H1;
            houseInfoStorage[2][1] = loadedData.I3H2;
            houseInfoStorage[2][2] = loadedData.I3H3;

            houseInfoStorage[3][0] = loadedData.I4H1;
            houseInfoStorage[3][1] = loadedData.I4H2;
            houseInfoStorage[3][2] = loadedData.I4H3;

            houseInfoStorage[4][0] = loadedData.I5H1;
            houseInfoStorage[4][1] = loadedData.I5H2;
            houseInfoStorage[4][2] = loadedData.I5H3;

            houseInfoStorage[5][0] = loadedData.I6H1;
            houseInfoStorage[5][1] = loadedData.I6H2;
            houseInfoStorage[5][2] = loadedData.I6H3;

            houseInfoStorage[6][0] = loadedData.I7H1;
            houseInfoStorage[6][1] = loadedData.I7H2;
            houseInfoStorage[6][2] = loadedData.I7H3;

            houseInfoStorage[7][0] = loadedData.I8H1;
            houseInfoStorage[7][1] = loadedData.I8H2;
            houseInfoStorage[7][2] = loadedData.I8H3;

            houseInfoStorage[8][0] = loadedData.I9H1;
            houseInfoStorage[8][1] = loadedData.I9H2;
            houseInfoStorage[8][2] = loadedData.I9H3;

            houseInfoStorage[9][0] = loadedData.I10H1;
            houseInfoStorage[9][1] = loadedData.I10H2;
            houseInfoStorage[9][2] = loadedData.I10H3;

            houseInfoStorage[10][0] = loadedData.I11H1;
            houseInfoStorage[10][1] = loadedData.I11H2;
            houseInfoStorage[10][2] = loadedData.I11H3;

            houseInfoStorage[11][0] = loadedData.I12H1;
            houseInfoStorage[11][1] = loadedData.I12H2;
            houseInfoStorage[11][2] = loadedData.I12H3;

            houseInfoStorage[12][0] = loadedData.I13H1;
            houseInfoStorage[12][1] = loadedData.I13H2;
            houseInfoStorage[12][2] = loadedData.I13H3;

            houseInfoStorage[13][0] = loadedData.I14H1;
            houseInfoStorage[13][1] = loadedData.I14H2;
            houseInfoStorage[13][2] = loadedData.I14H3;

            houseInfoStorage[14][0] = loadedData.I15H1;
            houseInfoStorage[14][1] = loadedData.I15H2;
            houseInfoStorage[14][2] = loadedData.I15H3;

            houseInfoStorage[15][0] = loadedData.I16H1;
            houseInfoStorage[15][1] = loadedData.I16H2;
            houseInfoStorage[15][2] = loadedData.I16H3;
        }
        else
        {

        }
    }

    public void loadClaimed(){
        string saveString = SaveSystem.LoadClaimed();
        if (saveString != null){
            ClaimedClass loadedData = JsonUtility.FromJson<ClaimedClass>(saveString);
            claimed[0][0][0] = loadedData.I1H1P1;
            claimed[0][1][0] = loadedData.I1H2P1;
            
            claimed[1][0][0] = loadedData.I2H1P1;
            claimed[1][1][0] = loadedData.I2H2P1;
            claimed[1][1][1] = loadedData.I2H2P2;

            claimed[2][0][0] = loadedData.I3H1P1;
            claimed[2][0][1] = loadedData.I3H1P2;
            claimed[2][1][0] = loadedData.I3H2P1;

            claimed[3][0][0] = loadedData.I4H1P1;
            claimed[3][1][0] = loadedData.I4H2P1;

            claimed[4][0][0] = loadedData.I5H1P1;
            claimed[4][0][1] = loadedData.I5H1P2;
            claimed[4][1][0] = loadedData.I5H2P1;

            claimed[5][0][0] = loadedData.I6H1P1;
            claimed[5][1][0] = loadedData.I6H2P1;
            claimed[5][1][1] = loadedData.I6H2P2;

            claimed[6][0][0] = loadedData.I7H1P1;
            claimed[6][1][0] = loadedData.I7H2P1;

            claimed[7][0][0] = loadedData.I8H1P1;
            claimed[7][0][1] = loadedData.I8H1P2;
            claimed[7][0][2] = loadedData.I8H1P3;
            claimed[7][1][0] = loadedData.I8H2P1;

            claimed[8][0][0] = loadedData.I9H1P1;
            claimed[8][0][1] = loadedData.I9H1P2;
            claimed[8][1][0] = loadedData.I9H2P1;

            claimed[9][0][0] = loadedData.I10H1P1;
            claimed[9][0][1] = loadedData.I10H1P2;
            claimed[9][1][0] = loadedData.I10H2P1;

            claimed[10][0][0] = loadedData.I11H1P1;
            claimed[10][1][0] = loadedData.I11H2P1;
            claimed[10][1][1] = loadedData.I11H2P2;
            claimed[10][2][0] = loadedData.I11H3P1;

            claimed[11][0][0] = loadedData.I12H1P1;
            claimed[11][1][0] = loadedData.I12H2P1;
            claimed[11][1][1] = loadedData.I12H2P2;
            
            claimed[12][0][0] = loadedData.I13H1P1;
            claimed[12][0][1] = loadedData.I13H1P2;
            claimed[12][0][2] = loadedData.I13H1P3;
            claimed[12][1][0] = loadedData.I13H2P1;
            claimed[12][2][0] = loadedData.I13H3P1;
            claimed[12][2][1] = loadedData.I13H3P2;

            claimed[13][0][0] = loadedData.I14H1P1;
            claimed[13][0][1] = loadedData.I14H1P2;
            claimed[13][1][0] = loadedData.I14H2P1;
            claimed[13][2][0] = loadedData.I14H3P1;

            claimed[14][0][0] = loadedData.I15H1P1;

            claimed[15][0][0] = loadedData.I16H1P1;
            claimed[15][1][0] = loadedData.I16H2P1;
        }
    }
    

    public void loadQuests(){
        string saveString = SaveSystem.LoadQuests();
        if (saveString != null){
            ClaimedClass loadedData = JsonUtility.FromJson<ClaimedClass>(saveString);
            questTracking[0][0][0] = loadedData.I1H1P1;
            questTracking[0][1][0] = loadedData.I1H2P1;
            
            questTracking[1][0][0] = loadedData.I2H1P1;
            questTracking[1][1][0] = loadedData.I2H2P1;
            questTracking[1][1][1] = loadedData.I2H2P2;

            questTracking[2][0][0] = loadedData.I3H1P1;
            questTracking[2][0][1] = loadedData.I3H1P2;
            questTracking[2][1][0] = loadedData.I3H2P1;

            questTracking[3][0][0] = loadedData.I4H1P1;
            questTracking[3][1][0] = loadedData.I4H2P1;

            questTracking[4][0][0] = loadedData.I5H1P1;
            questTracking[4][0][1] = loadedData.I5H1P2;
            questTracking[4][1][0] = loadedData.I5H2P1;

            questTracking[5][0][0] = loadedData.I6H1P1;
            questTracking[5][1][0] = loadedData.I6H2P1;
            questTracking[5][1][1] = loadedData.I6H2P2;

            questTracking[6][0][0] = loadedData.I7H1P1;
            questTracking[6][1][0] = loadedData.I7H2P1;

            questTracking[7][0][0] = loadedData.I8H1P1;
            questTracking[7][0][1] = loadedData.I8H1P2;
            questTracking[7][0][2] = loadedData.I8H1P3;
            questTracking[7][1][0] = loadedData.I8H2P1;

            questTracking[8][0][0] = loadedData.I9H1P1;
            questTracking[8][0][1] = loadedData.I9H1P2;
            questTracking[8][1][0] = loadedData.I9H2P1;

            questTracking[9][0][0] = loadedData.I10H1P1;
            questTracking[9][0][1] = loadedData.I10H1P2;
            questTracking[9][1][0] = loadedData.I10H2P1;

            questTracking[10][0][0] = loadedData.I11H1P1;
            questTracking[10][1][0] = loadedData.I11H2P1;
            questTracking[10][1][1] = loadedData.I11H2P2;
            questTracking[10][2][0] = loadedData.I11H3P1;

            questTracking[11][0][0] = loadedData.I12H1P1;
            questTracking[11][1][0] = loadedData.I12H2P1;
            questTracking[11][1][1] = loadedData.I12H2P2;
            
            questTracking[12][0][0] = loadedData.I13H1P1;
            questTracking[12][0][1] = loadedData.I13H1P2;
            questTracking[12][0][2] = loadedData.I13H1P3;
            questTracking[12][1][0] = loadedData.I13H2P1;
            questTracking[12][2][0] = loadedData.I13H3P1;
            questTracking[12][2][1] = loadedData.I13H3P2;

            questTracking[13][0][0] = loadedData.I14H1P1;
            questTracking[13][0][1] = loadedData.I14H1P2;
            questTracking[13][1][0] = loadedData.I14H2P1;
            questTracking[13][2][0] = loadedData.I14H3P1;

            questTracking[14][0][0] = loadedData.I15H1P1;

            questTracking[15][0][0] = loadedData.I16H1P1;
            questTracking[15][1][0] = loadedData.I16H2P1;
        }
    }

    public void loadFishGiveth()
    {
        string saveString = SaveSystem.LoadFishGiven();
        if (saveString != null)
        {
            FishGivenClass loadedData = JsonUtility.FromJson<FishGivenClass>(saveString);
            islandScript.fishGivenList = loadedData.fishGiven;
        }
    }

    public void saveData(){
        IslandFishStorage islandFishStorage = new IslandFishStorage{
            I1H1 = houseInfoStorage[0][0],
            I1H2 = houseInfoStorage[0][1],
            I1H3 = houseInfoStorage[0][2],

            I2H1 = houseInfoStorage[1][0],
            I2H2 = houseInfoStorage[1][1],
            I2H3 = houseInfoStorage[1][2],

            I3H1 = houseInfoStorage[2][0],
            I3H2 = houseInfoStorage[2][1],
            I3H3 = houseInfoStorage[2][2],

            I4H1 = houseInfoStorage[3][0],
            I4H2 = houseInfoStorage[3][1],
            I4H3 = houseInfoStorage[3][2],

            I5H1 = houseInfoStorage[4][0],
            I5H2 = houseInfoStorage[4][1],
            I5H3 = houseInfoStorage[4][2],

            I6H1 = houseInfoStorage[5][0],
            I6H2 = houseInfoStorage[5][1],
            I6H3 = houseInfoStorage[5][2],

            I7H1 = houseInfoStorage[6][0],
            I7H2 = houseInfoStorage[6][1],
            I7H3 = houseInfoStorage[6][2],

            I8H1 = houseInfoStorage[7][0],
            I8H2 = houseInfoStorage[7][1],
            I8H3 = houseInfoStorage[7][2],

            I9H1 = houseInfoStorage[8][0],
            I9H2 = houseInfoStorage[8][1],
            I9H3 = houseInfoStorage[8][2],

            I10H1 = houseInfoStorage[9][0],
            I10H2 = houseInfoStorage[9][1],
            I10H3 = houseInfoStorage[9][2],

            I11H1 = houseInfoStorage[10][0],
            I11H2 = houseInfoStorage[10][1],
            I11H3 = houseInfoStorage[10][2],

            I12H1 = houseInfoStorage[11][0],
            I12H2 = houseInfoStorage[11][1],
            I12H3 = houseInfoStorage[11][2],

            I13H1 = houseInfoStorage[12][0],
            I13H2 = houseInfoStorage[12][1],
            I13H3 = houseInfoStorage[12][2],

            I14H1 = houseInfoStorage[13][0],
            I14H2 = houseInfoStorage[13][1],
            I14H3 = houseInfoStorage[13][2],

            I15H1 = houseInfoStorage[14][0],
            I15H2 = houseInfoStorage[14][1],
            I15H3 = houseInfoStorage[14][2],

            I16H1 = houseInfoStorage[15][0],
            I16H2 = houseInfoStorage[15][1],
            I16H3 = houseInfoStorage[15][2],

        };
        string jsonStorage = JsonUtility.ToJson(islandFishStorage);
        SaveSystem.SaveFishCount1(jsonStorage);
    }

    public void saveClaimed(){
        ClaimedClass claimedClass = new ClaimedClass{
            I1H1P1 = claimed[0][0][0],
            I1H2P1 = claimed[0][1][0],

            I2H1P1 = claimed[1][0][0],
            I2H2P1 = claimed[1][1][0],
            I2H2P2 = claimed[1][1][1],

            I3H1P1 = claimed[2][0][0],
            I3H1P2 = claimed[2][0][1],
            I3H2P1 = claimed[2][1][0],

            I4H1P1 = claimed[3][0][0],
            I4H2P1 = claimed[3][1][0],

            I5H1P1 = claimed[4][0][0],
            I5H1P2 = claimed[4][0][1],
            I5H2P1 = claimed[4][1][0],

            I6H1P1 = claimed[5][0][0],
            I6H2P1 = claimed[5][1][0],
            I6H2P2 = claimed[5][1][1],

            I7H1P1 = claimed[6][0][0],
            I7H2P1 = claimed[6][1][0],

            I8H1P1 = claimed[7][0][0],
            I8H1P2 = claimed[7][0][1],
            I8H1P3 = claimed[7][0][2],
            I8H2P1 = claimed[7][1][0],

            I9H1P1 = claimed[8][0][0],
            I9H1P2 = claimed[8][0][1],
            I9H2P1 = claimed[8][1][0],

            I10H1P1 = claimed[9][0][0],
            I10H1P2 = claimed[9][0][1],
            I10H2P1 = claimed[9][1][0],

            I11H1P1 = claimed[10][0][0],
            I11H2P1 = claimed[10][1][0],
            I11H2P2 = claimed[10][1][1],
            I11H3P1 = claimed[10][2][0],

            I12H1P1 = claimed[11][0][0],
            I12H2P1 = claimed[11][1][0],
            I12H2P2 = claimed[11][1][1],
            
            I13H1P1 = claimed[12][0][0],
            I13H1P2 = claimed[12][0][1],
            I13H1P3 = claimed[12][0][2],
            I13H2P1 = claimed[12][1][0],
            I13H3P1 = claimed[12][2][0],
            I13H3P2 = claimed[12][2][1],

            I14H1P1 = claimed[13][0][0],
            I14H1P2 = claimed[13][0][1],
            I14H2P1 = claimed[13][1][0],
            I14H3P1 = claimed[13][2][0],

            I15H1P1 = claimed[14][0][0],

            I16H1P1 = claimed[15][0][0],
            I16H2P1 = claimed[15][1][0],
        };
        string jsonStorage = JsonUtility.ToJson(claimedClass);
        SaveSystem.SaveClaimed(jsonStorage);
    }

    //TODO
    
    public void saveQuest() {
        ClaimedClass claimedClass = new ClaimedClass {
            I1H1P1 = questTracking[0][0][0],
            I1H2P1 = questTracking[0][1][0],

            I2H1P1 = questTracking[1][0][0],
            I2H2P1 = questTracking[1][1][0],
            I2H2P2 = questTracking[1][1][1],

            I3H1P1 = questTracking[2][0][0],
            I3H1P2 = questTracking[2][0][1],
            I3H2P1 = questTracking[2][1][0],

            I4H1P1 = questTracking[3][0][0],
            I4H2P1 = questTracking[3][1][0],

            I5H1P1 = questTracking[4][0][0],
            I5H1P2 = questTracking[4][0][1],
            I5H2P1 = questTracking[4][1][0],

            I6H1P1 = questTracking[5][0][0],
            I6H2P1 = questTracking[5][1][0],
            I6H2P2 = questTracking[5][1][1],

            I7H1P1 = questTracking[6][0][0],
            I7H2P1 = questTracking[6][1][0],

            I8H1P1 = questTracking[7][0][0],
            I8H1P2 = questTracking[7][0][1],
            I8H1P3 = questTracking[7][0][2],
            I8H2P1 = questTracking[7][1][0],

            I9H1P1 = questTracking[8][0][0],
            I9H1P2 = questTracking[8][0][1],
            I9H2P1 = questTracking[8][1][0],

            I10H1P1 = questTracking[9][0][0],
            I10H1P2 = questTracking[9][0][1],
            I10H2P1 = questTracking[9][1][0],

            I11H1P1 = questTracking[10][0][0],
            I11H2P1 = questTracking[10][1][0],
            I11H2P2 = questTracking[10][1][1],
            I11H3P1 = questTracking[10][2][0],

            I12H1P1 = questTracking[11][0][0],
            I12H2P1 = questTracking[11][1][0],
            I12H2P2 = questTracking[11][1][1],

            I13H1P1 = questTracking[12][0][0],
            I13H1P2 = questTracking[12][0][1],
            I13H1P3 = questTracking[12][0][2],
            I13H2P1 = questTracking[12][1][0],
            I13H3P1 = questTracking[12][2][0],
            I13H3P2 = questTracking[12][2][1],

            I14H1P1 = questTracking[13][0][0],
            I14H1P2 = questTracking[13][0][1],
            I14H2P1 = questTracking[13][1][0],
            I14H3P1 = questTracking[13][2][0],

            I15H1P1 = questTracking[14][0][0],

            I16H1P1 = questTracking[15][0][0],
            I16H2P1 = questTracking[15][1][0],
        };
        string jsonStorage = JsonUtility.ToJson(claimedClass);
        SaveSystem.SaveQuests(jsonStorage);
    }

    public void saveFishGiveth()
    {
        FishGivenClass fishGivenClass = new FishGivenClass
        {
            fishGiven = islandScript.fishGivenList,
        };
        string jsonStorage = JsonUtility.ToJson(fishGivenClass);
        SaveSystem.SaveFishGiven(jsonStorage);
    }

}

public class IslandFishStorage 
{

    //FP VAL STORAGE
    public int[] I1H1 = new int[3];
    public int[] I1H2 = new int[3];
    public int[] I1H3 = new int[3];
    public int[] I2H1 = new int[3];
    public int[] I2H2 = new int[3];
    public int[] I2H3 = new int[3];
    public int[] I3H1 = new int[3];
    public int[] I3H2 = new int[3];
    public int[] I3H3 = new int[3];
    public int[] I4H1 = new int[3];
    public int[] I4H2 = new int[3];
    public int[] I4H3 = new int[3];
    public int[] I5H1 = new int[3];
    public int[] I5H2 = new int[3];
    public int[] I5H3 = new int[3];
    public int[] I6H1 = new int[3];
    public int[] I6H2 = new int[3];
    public int[] I6H3 = new int[3];
    public int[] I7H1 = new int[3];
    public int[] I7H2 = new int[3];
    public int[] I7H3 = new int[3];
    public int[] I8H1 = new int[3];
    public int[] I8H2 = new int[3];
    public int[] I8H3 = new int[3];
    public int[] I9H1 = new int[3];
    public int[] I9H2 = new int[3];
    public int[] I9H3 = new int[3];
    public int[] I10H1 = new int[3];
    public int[] I10H2 = new int[3];
    public int[] I10H3 = new int[3];
    public int[] I11H1 = new int[3];
    public int[] I11H2 = new int[3];
    public int[] I11H3 = new int[3];
    public int[] I12H1 = new int[3];
    public int[] I12H2 = new int[3];
    public int[] I12H3 = new int[3];
    public int[] I13H1 = new int[3];
    public int[] I13H2 = new int[3];
    public int[] I13H3 = new int[3];
    public int[] I14H1 = new int[3];
    public int[] I14H2 = new int[3];
    public int[] I14H3 = new int[3];
    public int[] I15H1 = new int[3];
    public int[] I15H2 = new int[3];
    public int[] I15H3 = new int[3];
    public int[] I16H1 = new int[3];
    public int[] I16H2 = new int[3];
    public int[] I16H3 = new int[3];
    
}

public class ClaimedClass 
{
    public int[] I1H1P1 = new int[3];
    public int[] I1H2P1 = new int[3];
    public int[] I2H1P1 = new int[3];
    public int[] I2H2P1 = new int[3];
    public int[] I2H2P2 = new int[3];
    public int[] I3H1P1 = new int[3];
    public int[] I3H1P2 = new int[3];
    public int[] I3H2P1 = new int[3];
    public int[] I4H1P1 = new int[3];
    public int[] I4H2P1 = new int[3];
    public int[] I5H1P1 = new int[3];
    public int[] I5H1P2 = new int[3];
    public int[] I5H2P1 = new int[3];
    public int[] I6H1P1 = new int[3];
    public int[] I6H2P1 = new int[3];
    public int[] I6H2P2 = new int[3];
    public int[] I7H1P1 = new int[3];
    public int[] I7H2P1 = new int[3];
    public int[] I8H1P1 = new int[3];
    public int[] I8H1P2 = new int[3];
    public int[] I8H1P3 = new int[3];
    public int[] I8H2P1 = new int[3];
    public int[] I9H1P1 = new int[3];
    public int[] I9H1P2 = new int[3];
    public int[] I9H2P1 = new int[3];
    public int[] I10H1P1 = new int[3];
    public int[] I10H1P2 = new int[3];
    public int[] I10H2P1 = new int[3];
    public int[] I11H1P1 = new int[3];
    public int[] I11H2P1 = new int[3];
    public int[] I11H2P2 = new int[3];
    public int[] I11H3P1 = new int[3];
    public int[] I12H1P1 = new int[3];
    public int[] I12H2P1 = new int[3];
    public int[] I12H2P2 = new int[3];
    public int[] I13H1P1 = new int[3];
    public int[] I13H1P2 = new int[3];
    public int[] I13H1P3 = new int[3];
    public int[] I13H2P1 = new int[3];
    public int[] I13H3P1 = new int[3];
    public int[] I13H3P2 = new int[3];
    public int[] I14H1P1 = new int[3];
    public int[] I14H1P2 = new int[3];
    public int[] I14H2P1 = new int[3];
    public int[] I14H3P1 = new int[3];
    public int[] I15H1P1 = new int[3];
    public int[] I16H1P1 = new int[3];
    public int[] I16H2P1 = new int[3];
}

public class FishGivenClass
{
    public int[] fishGiven = new int[16];
}
