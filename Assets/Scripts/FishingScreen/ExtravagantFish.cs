using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ExtravagantFish : MonoBehaviour
{
    public Sprite[] blueFishSprites;
    public Sprite[] purpleFishSprites;
    public Sprite[] lavaFishSprites;
    public Sprite[] allBlueFishSprites;
    public Sprite[][] extravagantFishSprites = new Sprite[4][];
    [NonSerialized] public int spriteIndex = 0;
    [NonSerialized] public int oceanCaughtInIndex = 0;

    public void InitCall(){
        extravagantFishSprites[0] = blueFishSprites;
        extravagantFishSprites[1] = purpleFishSprites;
        extravagantFishSprites[2] = lavaFishSprites;
        extravagantFishSprites[3] = allBlueFishSprites;
    }
}
