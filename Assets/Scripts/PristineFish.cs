using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PristineFish : MonoBehaviour
{
    public Sprite[] blueFishSprites;
    public Sprite[] purpleFishSprites;
    public Sprite[] lavaFishSprites;
    public Sprite[] allBlueFishSprites;
    public Sprite[][] pristineFishSprites = new Sprite[4][];
    [NonSerialized] public int spriteIndex = 0;
    [NonSerialized] public int oceanCaughtInIndex = 0;

    public void InitCall(){
        pristineFishSprites[0] = blueFishSprites;
        pristineFishSprites[1] = purpleFishSprites;
        pristineFishSprites[2] = lavaFishSprites;
        pristineFishSprites[3] = allBlueFishSprites;
    }
}
