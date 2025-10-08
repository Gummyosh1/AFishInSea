using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FancyFish : MonoBehaviour
{
    public Sprite[] blueFishSprites;
    public Sprite[] purpleFishSprites;
    public Sprite[] lavaFishSprites;
    public Sprite[] allBlueFishSprites;
    public Sprite[][] fancyFishSprites = new Sprite[4][];
    [NonSerialized] public int spriteIndex = 0;
    [NonSerialized] public int oceanCaughtInIndex = 0;

    public void InitCall(){
        fancyFishSprites[0] = blueFishSprites;
        fancyFishSprites[1] = purpleFishSprites;
        fancyFishSprites[2] = lavaFishSprites;
        fancyFishSprites[3] = allBlueFishSprites;
    }
}
