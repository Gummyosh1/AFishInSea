using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalFish : MonoBehaviour
{
    public Sprite[] blueFishSprites;
    public Sprite[] purpleFishSprites;
    public Sprite[] lavaFishSprites;
    public Sprite[] allBlueFishSprites;
    public Sprite[][] normalFishSprites = new Sprite[4][];
    [NonSerialized] public int spriteIndex = 0;
    [NonSerialized] public int oceanCaughtInIndex = 0;

    public void InitCall(){
        normalFishSprites[0] = blueFishSprites;
        normalFishSprites[1] = purpleFishSprites;
        normalFishSprites[2] = lavaFishSprites;
        normalFishSprites[3] = allBlueFishSprites;
    }
    
}
