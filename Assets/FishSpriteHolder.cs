using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpriteHolder : MonoBehaviour
{
    public Sprite[] EastBlueNormalFish;
    public Sprite[] EastBlueFancyFish;
    public Sprite[] EastBlueExtravagantFish;
    public Sprite[] EastBluePristineFish;
    //EAST BLUE FISH END

    //PURPLE FISH
    public Sprite[] PurpleNormalFish;
    public Sprite[] PurpleFancyFish;
    public Sprite[] PurpleExtravagantFish;
    public Sprite[] PurplePristineFish;
    //PURPLE FISH END

    //LAVA FISH
    public Sprite[] LavaNormalFish;
    public Sprite[] LavaFancyFish;
    public Sprite[] LavaExtravagantFish;
    public Sprite[] LavaPristineFish;
    //LAVA FISH END

    //ALLBLUE FISH
    public Sprite[] AllBlueNormalFish;
    public Sprite[] AllBlueFancyFish;
    public Sprite[] AllBlueExtravagantFish;
    public Sprite[] AllBluePristineFish;
    //ALLBLUE FISH END

    public Sprite[] EliteEightSprites;

    public Sprite[][][] spriteLists = new Sprite[5][][];
    private Sprite[][] EastBlueHolder = new Sprite[5][];
    private Sprite[][] PurpleHolder = new Sprite[5][];
    private Sprite[][] LavaHolder = new Sprite[5][];
    private Sprite[][] AllBlueHolder = new Sprite[5][];
    private Sprite[][] EliteEightHolder = new Sprite[5][];
    

    public void Init()
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

        spriteLists[0] = EastBlueHolder;
        spriteLists[1] = PurpleHolder;
        spriteLists[2] = LavaHolder;
        spriteLists[3] = AllBlueHolder;
        spriteLists[4] = EliteEightHolder;
    }
}
