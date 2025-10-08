using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishNames : MonoBehaviour
{
    [NonSerialized] public string[] EastBlueNormalFish =
    {
        "Gubble",
        "Charcoal Karp",
        "Tigerfish",
        "Sungazer",
        "Petra",
        "Armored Spot",
        "Slip",
        "Evil Slider",
        "Capsule Fish",
        "Starfish"
    };
    [NonSerialized] public string[] EastBlueFancyFish = 
    {
        "Acanthodii",
        "Sawhead",
        "Merthful",
        "Glorpy",
        "Cirratus",
        "Baby Sunfish",
        "Stylish Fella"
    };
    [NonSerialized] public string[] EastBlueExtravagantFish = 
    {
        "Alewife",
        "Shmimp",
        "Shardbearer",
        "Scaled Demon"
    };
    [NonSerialized] public string[] EastBluePristineFish = 
    {
        "Killingham",
        "Fool's Diamondfish"
    };
    //EAST BLUE FISH END

    //PURPLE FISH
    [NonSerialized] public string[] PurpleNormalFish =
    {
        "Vengeful Tolapia",
        "Yellow Bloater",
        "Shielder",
        "Prince Eel",
        "Gravely",
        "Magenta Pyke",
        "Vexed Puffin",
        "Yellow Starfish",
        "Crusty",
        "Golden Oct"
    };
    [NonSerialized] public string[] PurpleFancyFish = 
    {
        "Blightfin",
        "Longforn",
        "Chainmail Guppy",
        "Loven",
        "Electric Slider",
        "Stawberryfin",
        "Graphite Perch"
    };
    [NonSerialized] public string[] PurpleExtravagantFish = 
    {
        "Redeye Ghost",
        "Regal Seahorse",
        "Friendly Oct",
        "Rave Jelly"
    };
    [NonSerialized] public string[] PurplePristineFish = 
    {
        "Midnight Perch",
        "Magma Jelly"
    };
    //PURPLE FISH END

    //LAVA FISH
    [NonSerialized] public string[] LavaNormalFish = 
    {
        "Redhair",
        "Molten Gobbler",
        "Bluetrimmed Redfish",
        "Mischievous Bloater",
        "Mushroom Guppy",
        "Gilded Haddock",
        "Stout Red",
        "Flame Anglerfish",
        "Big Sardine",
        "Crab"
    };
    [NonSerialized] public string[] LavaFancyFish = 
    {
        "Buttertrout",
        "Overbite",
        "Horned Armorfish",
        "Chili Bass",
        "Lobster",
        "Golden Lobster",
        "Amber Jelly"
    };
    [NonSerialized] public string[] LavaExtravagantFish = 
    {
        "Baby Ghost",
        "Wise Lonma",
        "Bloodfish",
        "Pink Puffin"
    };
    [NonSerialized] public string[] LavaPristineFish = 
    {
        "Soul Stealer",
        "Mountainkiller"
    };
    //LAVA FISH END

    //ALLBLUE FISH
    [NonSerialized] public string[] AllBlueNormalFish = 
    {
        "Emerald Floater",
        "Linas",
        "Genetic Clownfish",
        "Sudsit Bluegill",
        "Choral Bass",
        "Brightied",
        "Mossy Fella",
        "Angry Fella",
        "Bluefetti",
        "Bluehorse"
    };
    [NonSerialized] public string[] AllBlueFancyFish = 
    {
        "Underscale",
        "Tricerafish",
        "Zombiefish",
        "Grish",
        "Marsulta",
        "Blobster",
        "Blue Oct"
    };
    [NonSerialized] public string[] AllBlueExtravagantFish = 
    {
        "Gummyfish",
        "Allent",
        "Daggerfang",
        "Sunkissed Gompler"
    };
    [NonSerialized] public string[] AllBluePristineFish =
    {
        "Maritime",
        "Snapdragon"
    };
    //ALLBLUE FISH END

    [NonSerialized] public string[] EliteEightSprites =
    {
        "Taiyaki",
        "Bowled Menace",
        "Diamondfish",
        "Ghost Betafish",
        "Cracker Fish",
        "Immortal Glass Eel",
        "Notefish",
        "Golden Ruler",
    };

    public string[][][] nameLists = new string[5][][];
    private string[][] EastBlueHolder = new string[5][];
    private string[][] PurpleHolder = new string[5][];
    private string[][] LavaHolder = new string[5][];
    private string[][] AllBlueHolder = new string[5][];
    private string[][] EliteEightHolder = new string[5][];
    

    public void Start()
    {
        EastBlueHolder[0] = EastBlueNormalFish;
        EastBlueHolder[1] = EastBlueFancyFish;
        EastBlueHolder[2] = EastBlueExtravagantFish;
        EastBlueHolder[3] = EastBluePristineFish;

        PurpleHolder[0] = PurpleNormalFish;
        PurpleHolder[1] = PurpleFancyFish;
        PurpleHolder[2] = PurpleExtravagantFish;
        PurpleHolder[3] = PurplePristineFish;

        LavaHolder[0] = LavaNormalFish;
        LavaHolder[1] = LavaFancyFish;
        LavaHolder[2] = LavaExtravagantFish;
        LavaHolder[3] = LavaPristineFish;

        AllBlueHolder[0] = AllBlueNormalFish;
        AllBlueHolder[1] = AllBlueFancyFish;
        AllBlueHolder[2] = AllBlueExtravagantFish;
        AllBlueHolder[3] = AllBluePristineFish;

        EliteEightHolder[0] = EliteEightSprites;
        EliteEightHolder[1] = null;
        EliteEightHolder[2] = null;
        EliteEightHolder[3] = null;

        nameLists[0] = EastBlueHolder;
        nameLists[1] = PurpleHolder;
        nameLists[2] = LavaHolder;
        nameLists[3] = AllBlueHolder;
        nameLists[4] = EliteEightHolder;
    }
}
