using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyChest : MonoBehaviour
{
    public Sprite[] chest1;
    public Sprite[] chest2;
    public Sprite[] chest3;
    public Sprite[] chest4;
    public Sprite[] chest5;
    public Sprite[] chest6;
    public Sprite[] chest7;

    public Sprite[][] chests = new Sprite[7][];

    public void chestInit(){
        chests[0] = chest1;
        chests[1] = chest2;
        chests[2] = chest3;
        chests[3] = chest4;
        chests[4] = chest5;
        chests[5] = chest6;
        chests[6] = chest7;
    }
}
