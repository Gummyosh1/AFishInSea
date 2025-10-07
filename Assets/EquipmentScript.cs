using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class EquipmentScript : MonoBehaviour
{
    public RodDisplay rodDisplay;
    public SailingTracker sailingTracker;
    public SailSwap sailSwap;
    public Image[] cosmeticScreenImages;
    public GobletEquipper gobletEquipper;
    public ChairEquipper chairEquipper;
    public BarrelEquipper barrelEquipper;
    public TableEquipper tableEquipper;
    public Image tableImage;
    public Image chairImage;
    public Image gobletImage;
    public Image barrelImage;

    public TMP_Text trifectaText1;
    public TMP_Text trifectaText2;
    public TMP_Text trifectaText3;
    public TMP_Text equippedBaitQ;
    public TMP_Text equipmentPopUpQ;
    public GameObject baitTrifecta;
    public Image trifecta1;
    public Image trifecta2;
    public Image trifecta3; 
    
    public Image rodEquippedImage;
    public Image baitEquippedImage;
    public Image petEquippedImage;

    public GameObject EquipButton;
    public GameObject EquippedButton;
    public GameObject LockedButton;
    public GameObject EquipmentInfoScreen;
    public Image EquipmentInfoSprite;
    public TMP_Text itemName;
    public TMP_Text itemInfo;
    public TMP_Text itemObtainInfo;
    [NonSerialized] public int equipableStatus = 0; //0 can be equipped, 1 is equipped, 2 isn't unlocked

    public Sprite missing;
    public Sprite[] rods;
    public Sprite[] pets;

    [NonSerialized] public string[] rodNames = {"Old Angler's Rod","Rod of Sludge","Reel of Frozen Flames","The Golden Rod"};
    [NonSerialized] public string[] rodInfo = {"You've always had this rod... where did it come from anyway?","It seems a pile of goo is quite attatched to it!","This rod does not follow the laws of physics at all","A rod said to be 'The Chosen One' throughout the Angling community"};
    [NonSerialized] public string[] rodUnlockInfo = {"Unlocked by downloading the app","Carried by the Merchant on Island 5","Carried by the Merchant on Island 9","Carried by the Merchant on Island 13"};

    [NonSerialized] public string[] petsNames = {"Caramel","Platinum","Night","Panda","Ice","Flow","Molten","John"};
    [NonSerialized] public string[] petsInfo = {"A kind soul","A not so kind soul","Always happy to help!","Can be angry at night","Loves napping on the deck","Loves eating fish","Will do anything to get 500 gold coins","Hates crickets."};
    [NonSerialized] public string[] petsUnlockInfo = {"Unlocked at Island 2","Unlocked at Island 6","Unlocked at Island 14","Unlocked at Island 10","Unlocked at Island 16","Unlocked at Island 4","Unlocked at Island 12","Unlocked at Island 8"};

    [NonSerialized] public string[] baitNames = {"Worm","Ladybug","Shrimp","Firefly","Minnow","Sandwich",
    "Golden Shrimp","Lava Butterfly","Purple Butterfly","Dragonfly","Lava Dragonfly","Pickle"};
    [NonSerialized] public string[] baitInfo = {"Attracts fish of Normal rarity","Attracts fish of Fancy rarity",
    "Attracts fish of Extravagant","Attracts fish of Pristine rarity"};
    [NonSerialized] public string[] miniGameInfo = {"'A slimy critter who lives to burrow'","'A flying insect said to bring good luck to those it lands on'",
    "'Hunter of Sharks, Killer of Whales, Beast of the Blue, are just a few of this fellas titles'",
    "'You would not believe your eyes if ten million fireflies lit up the world as I fell asleep'",
    "'A fish designed to be eaten'","'Someone once parked their car on my sandwich. It was a sad day.'",
    "'Sir Shrimpillius Maximus is the shadow leader of the seas'",
    "'Its wings spit the fire of years of flight'","'The royalty of the skies, now captured to be feasted on by the creatures of the deep'",
    "'It is said that in ancient times this creature used to tame the dragons that flew the skies'",
    "'A variant of the Dragonfly native only to volcanoes and the molten lands'",
    "'A rare delicacy that was allegedly abundant in the Heian era'"};
    //USES BAITBUY FOR QUANTITY baitBuy.baitTotals[]

    [NonSerialized] public int[] rodsOwned = {1,0,0,0};
    [NonSerialized] public int[] chairsOwned = new int[10];
    [NonSerialized] public int[] tablesOwned = new int[10];
    [NonSerialized] public int[] barrelsOwned = new int[10];
    [NonSerialized] public int[] gobletsOwned = new int[10];

    [NonSerialized] public int equippedTable = -1;
    [NonSerialized] public int equippedChair = -1;
    [NonSerialized] public int equippedBarrel = -1;
    [NonSerialized] public int equippedGoblet = -1;


    [NonSerialized] public int equippedRod = 0;
    [NonSerialized] public int equippedBaitRarity = 0;
    [NonSerialized] public int equippedBaitIndex = 0;
    //USING petWalk.petEquipped FOR PETS

    private Sprite[][] baitLoader = new Sprite[4][];
    [NonSerialized] public Sprite[] baits;

    public PetWalk petWalk;
    public BaitMinigame baitMinigame;
    public BaitBuy baitBuy;
    private int currentPopUp = 0;
    private int currentIndex = 0;

    private bool inRod = false;
    private bool inBait = false;
    private bool inPet = false;

    public void baitInit(){
        baitLoader[0] = baitMinigame.possibleBaitsNormal;
        baitLoader[1] = baitMinigame.possibleBaitsFancy;
        baitLoader[2] = baitMinigame.possibleBaitsExtravagant;
        baitLoader[3] = baitMinigame.possibleBaitsPristine;
    }

    public void popUpInit(){
        baits = baitLoader[equippedBaitRarity];
        rodEquippedImage.sprite = rods[equippedRod];
        if (baitBuy.baitTotals[(equippedBaitRarity*3) + equippedBaitIndex] == 0){
            baitEquippedImage.sprite = missing;
        }
        else{
            baitEquippedImage.sprite = baits[equippedBaitIndex];
        }
        equippedBaitQ.text = baitBuy.baitTotals[(equippedBaitRarity*3) + equippedBaitIndex].ToString();
        petEquippedImage.sprite = pets[petWalk.petEquipped];
    }

    public void equipmentInit(){
        LoadRod();
    }

    public void autoEquip(){
        if (baitBuy.baitTotals[(equippedBaitRarity*3) + equippedBaitIndex] == 0){
            for (int i = 0; i < baitBuy.baitTotals.Length; i++){
                if (baitBuy.baitTotals[i] > 0){
                    Debug.Log("Found a fish at " + i+ " to equip!");
                    equippedBaitIndex = i % 3;
                    equippedBaitRarity = i / 3;
                    Debug.Log("Index = " + equippedBaitIndex);
                    Debug.Log("Rarity = " + equippedBaitRarity);
                    break;
                }
            }
        }
    }

    public void cosmeticScreenInit(){
        int holderTable = 0;
        int holderBarrel = 0;
        int holderSail = 0;
        int holderChair = 0;
        int holderGoblet = 0;
        if (equippedTable == -1){holderTable = 0;}
        else{holderTable = equippedTable;}

        if (equippedBarrel == -1){holderBarrel = 0;}
        else{holderBarrel = equippedBarrel;}

        if (sailingTracker.equippedSail == -1){holderSail = 0;}
        else{holderSail = sailingTracker.equippedSail;}

        if (equippedChair == -1){holderChair = 0;}
        else{holderChair = equippedChair;}

        if (equippedGoblet == -1){holderGoblet = 0;}
        else{holderGoblet = equippedGoblet;}

        cosmeticScreenImages[0].sprite = tableEquipper.tableSprites[holderTable];
        cosmeticScreenImages[1].sprite = barrelEquipper.barrelSprites[holderBarrel];
        cosmeticScreenImages[2].sprite = sailSwap.onLoader[holderSail][2];
        cosmeticScreenImages[3].sprite = chairEquipper.chairSprites[holderChair];
        cosmeticScreenImages[4].sprite = gobletEquipper.gobletSprites[holderGoblet];
    }

    public void equipButton(){
        if (inRod){
            equipRod();
        }
        else if (inBait){
            equipBait();
        }
        else if(inPet){
            equipPet();
        }
        rodDisplay.setupRod();
    }

    public void DEVRODS(){
        for (int i  = 0; i < rodsOwned.Length; i++){
            rodsOwned[i] = 1;
        }
    }   


    public void rodPopUp(int x){
        enterRod();
        currentPopUp = x;

        equipmentPopUpQ.gameObject.SetActive(false);
        EquipmentInfoScreen.SetActive(true);
        EquipmentInfoSprite.sprite = rods[x];
        itemName.text = rodNames[x];
        itemInfo.text = rodInfo[x];
        itemObtainInfo.text = rodUnlockInfo[x];

        if (x != equippedRod){
            if (rodsOwned[x] == 1){
                EquipButton.SetActive(true);
                EquippedButton.SetActive(false);
                LockedButton.SetActive(false);
            }
            else{
                LockedButton.SetActive(true);
                EquipButton.SetActive(false);
                EquippedButton.SetActive(false);
            }
        }
        else{
            EquippedButton.SetActive(true);
            LockedButton.SetActive(false);
            EquipButton.SetActive(false);
        }
    }

    public void equipRod(){
        equippedRod = currentPopUp;
        rodEquippedImage.sprite = rods[equippedRod];
        EquipmentInfoScreen.SetActive(false);
        SaveRod();
    }

    public void petPopUp(int x){
        enterPet();
        equipmentPopUpQ.gameObject.SetActive(false);
        currentPopUp = x;
        EquipmentInfoScreen.SetActive(true);
        EquipmentInfoSprite.sprite = pets[x];
        itemName.text = petsNames[x];
        itemInfo.text = petsInfo[x];
        itemObtainInfo.text = petsUnlockInfo[x];
        if (x != petWalk.petEquipped){
            if (petWalk.petsOwned[x] == 1){
                EquipButton.SetActive(true);
                EquippedButton.SetActive(false);
                LockedButton.SetActive(false);
            }
            else{
                LockedButton.SetActive(true);
                EquipButton.SetActive(false);
                EquippedButton.SetActive(false);
            }
        }
        else{
            EquippedButton.SetActive(true);
            LockedButton.SetActive(false);
            EquipButton.SetActive(false);
        }
    }

    public void equipPet(){
        petWalk.equipPet(currentPopUp);
        petEquippedImage.sprite = pets[petWalk.petEquipped];
        EquipmentInfoScreen.SetActive(false);
    }

    public void baitPopUpOne(int x){
        enterBait();
        currentPopUp = x;
        baits = baitLoader[x];
        baitTrifecta.SetActive(true);
        trifecta1.sprite = baits[0];
        trifecta2.sprite = baits[1];
        trifecta3.sprite = baits[2];
        trifectaText1.text = baitBuy.baitTotals[(currentPopUp*3) + 0].ToString();
        trifectaText2.text = baitBuy.baitTotals[(currentPopUp*3) + 1].ToString();
        trifectaText3.text = baitBuy.baitTotals[(currentPopUp*3) + 2].ToString();
    }

    public void baitPopUp(int x){
        currentIndex = x;

        equipmentPopUpQ.text = baitBuy.baitTotals[(currentPopUp * 3) + currentIndex].ToString();
        equipmentPopUpQ.gameObject.SetActive(true);
        EquipmentInfoScreen.SetActive(true);
        EquipmentInfoSprite.sprite = baits[x];
        int indexLocation = (currentPopUp*3) + currentIndex;
        itemName.text = baitNames[indexLocation];
        itemInfo.text = baitInfo[currentPopUp];
        itemObtainInfo.text = miniGameInfo[indexLocation];

        if (currentIndex != equippedBaitIndex || currentPopUp != equippedBaitRarity){
            if (baitBuy.baitTotals[indexLocation] > 0){
                EquipButton.SetActive(true);
                EquippedButton.SetActive(false);
                LockedButton.SetActive(false);
            }
            else{
                LockedButton.SetActive(true);
                EquipButton.SetActive(false);
                EquippedButton.SetActive(false);
            }
        }
        else{ 
            EquippedButton.SetActive(true);
            LockedButton.SetActive(false);
            EquipButton.SetActive(false);
        }
    }

    public void equipBait(){
        equippedBaitRarity = currentPopUp;
        equippedBaitIndex = currentIndex;
        baits = baitLoader[equippedBaitRarity];
        baitEquippedImage.sprite = baits[equippedBaitIndex];
        equippedBaitQ.text = baitBuy.baitTotals[(equippedBaitRarity*3) + equippedBaitIndex].ToString();
        baitTrifecta.SetActive(false);
        EquipmentInfoScreen.SetActive(false);
    }

    public void enterRod(){
        inRod = true;
        inBait = false;
        inPet = false;
    }

    public void enterBait(){
        inRod = false;
        inBait = true;
        inPet = false;
    }

    public void enterPet(){
        inRod = false;
        inBait = false;
        inPet = true;
    }

    public void equipTable(int x){
        equippedTable = x;
        saveDecorations();
        tableImage.sprite = tableEquipper.tableSprites[x];
    }

    public void equipBarrel(int x){
        equippedBarrel = x;
        saveDecorations();
        barrelImage.sprite = barrelEquipper.barrelSprites[x];
    }

    public void equipChair(int x){
        equippedChair = x;
        saveDecorations();
        chairImage.sprite = chairEquipper.chairSprites[x];
    }

    public void equipGoblet(int x){
        equippedGoblet = x;
        saveDecorations();
        gobletImage.sprite = gobletEquipper.gobletSprites[x];
    }

    public void saveDecorations(){
        DecorationSave decorationSaveStorage = new DecorationSave{
            chairsOwned = chairsOwned,
            tablesOwned = tablesOwned,
            barrelsOwned = barrelsOwned,
            gobletsOwned = gobletsOwned,
            equippedChair = equippedChair,
            equippedTable = equippedTable,
            equippedBarrel = equippedBarrel,
            equippedGoblet = equippedGoblet,
        };
        string jsonStorage = JsonUtility.ToJson(decorationSaveStorage);
        SaveSystem.SaveDecorations(jsonStorage);
    }

    public void loadDecorations(){
        string saveString = SaveSystem.LoadDecorations();
        if (saveString != null){
            DecorationSave loadedData = JsonUtility.FromJson<DecorationSave>(saveString);
            chairsOwned = loadedData.chairsOwned;
            tablesOwned = loadedData.tablesOwned;
            barrelsOwned = loadedData.barrelsOwned;
            gobletsOwned = loadedData.gobletsOwned;
            equippedChair = loadedData.equippedChair;
            equippedTable = loadedData.equippedTable;
            equippedBarrel = loadedData.equippedBarrel;
            equippedGoblet = loadedData.equippedGoblet;
        }
    }

    public void SaveRod(){
        RodSave rodSaveStorage = new RodSave{
            rodsOwned = rodsOwned,
            rodEquipped = equippedRod,
        };
        string jsonStorage = JsonUtility.ToJson(rodSaveStorage);
        SaveSystem.SaveRod(jsonStorage);
    }

    
    public void LoadRod(){
        string saveString = SaveSystem.LoadRod();
        if (saveString != null)
        {
            RodSave loadedData = JsonUtility.FromJson<RodSave>(saveString);
            rodsOwned = loadedData.rodsOwned;
            equippedRod = loadedData.rodEquipped;
            rodsOwned[0] = 1;
        }
    }
}


public class RodSave 
{
    public int[] rodsOwned = new int[8];
    public int rodEquipped = 0;
}

public class DecorationSave
{
    public int[] tablesOwned = new int[10];
    public int[] chairsOwned = new int[10];
    public int[] gobletsOwned = new int[10];
    public int[] barrelsOwned = new int[10];
    public int equippedTable = -1;
    public int equippedChair = -1;
    public int equippedBarrel = -1;
    public int equippedGoblet = -1;
}
