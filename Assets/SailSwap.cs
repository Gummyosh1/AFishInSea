using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class SailSwap : MonoBehaviour
{
    public Sprite[] toggleOn0; 
    public Sprite[] toggleOn1;
    public Sprite[] toggleOn2;
    public Sprite[] toggleOn3;
    public Sprite[] toggleOn4;
    public Sprite[] toggleOn5;
    public Sprite[] toggleOn6;
    public Sprite[] toggleOn7;
    public Sprite[] toggleOn8;
    public Sprite[] toggleOn9;
    public Sprite[] toggleOn10;
    public Sprite[] toggleOn11;
    public Sprite[] toggleOn12;
    public Sprite[] toggleOn13;
    public Sprite[] toggleOn14;
    public Sprite[] toggleOn15;
    public Sprite[] toggleOn16; //YELLOW
    public Sprite[] toggleOn17; //LIGHT RAINBOW
    public Sprite[] toggleOn18; //FULL RAINBOW
    public Sprite[] toggleOn19; //SHINY SILVER
    public Sprite[] toggleOn20; //SHINY GOLD
    public Sprite[] toggleOn21; //DAMAGED

    public Sprite[] toggleOff0;
    public Sprite[] toggleOff1;
    public Sprite[] toggleOff2;
    public Sprite[] toggleOff3;
    public Sprite[] toggleOff4;
    public Sprite[] toggleOff5;
    public Sprite[] toggleOff6;
    public Sprite[] toggleOff7;
    public Sprite[] toggleOff8;
    public Sprite[] toggleOff9;
    public Sprite[] toggleOff10;
    public Sprite[] toggleOff11;
    public Sprite[] toggleOff12;
    public Sprite[] toggleOff13;
    public Sprite[] toggleOff14;
    public Sprite[] toggleOff15;
    public Sprite[] toggleOff16; //YELLOW
    public Sprite[] toggleOff17; //LIGHT RAINBOW
    public Sprite[] toggleOff18; //FULL RAINBOW
    public Sprite[] toggleOff19; //SHINY SILVER
    public Sprite[] toggleOff20; //SHINY GOLD
    public Sprite[] toggleOff21; //DAMAGE

    public Sprite[][] onLoader = new Sprite[22][];
    public Sprite[][] offLoader = new Sprite[22][];

    [NonSerialized] public Sprite[] onSprites;
    [NonSerialized] public Sprite[] offSprites;

    [NonSerialized] public int equippedSail = 0;
    [NonSerialized] public bool on = false;
    public Image sail;
    private bool coiling = false;
    private float timer = 0;
    private float cutoff = .5f;
    private int index = 0;
    public GameObject hitboxOn;
    public GameObject hitboxOff;    

    public SailingTracker sailingTracker;


    public void Init(){
        onLoader[0] = toggleOn0;
        onLoader[1] = toggleOn1;
        onLoader[2] = toggleOn2;
        onLoader[3] = toggleOn3;
        onLoader[4] = toggleOn4;
        onLoader[5] = toggleOn5;
        onLoader[6] = toggleOn6;
        onLoader[7] = toggleOn7;
        onLoader[8] = toggleOn8;
        onLoader[9] = toggleOn9;
        onLoader[10] = toggleOn10;
        onLoader[11] = toggleOn11;
        onLoader[12] = toggleOn12;
        onLoader[13] = toggleOn13;
        onLoader[14] = toggleOn14;
        onLoader[15] = toggleOn15;
        onLoader[16] = toggleOn16;
        onLoader[17] = toggleOn17;
        onLoader[18] = toggleOn18;
        onLoader[19] = toggleOn19;
        onLoader[20] = toggleOn20;
        onLoader[21] = toggleOn21;

        offLoader[0] = toggleOff0;
        offLoader[1] = toggleOff1;
        offLoader[2] = toggleOff2;
        offLoader[3] = toggleOff3;
        offLoader[4] = toggleOff4;
        offLoader[5] = toggleOff5;
        offLoader[6] = toggleOff6;
        offLoader[7] = toggleOff7;
        offLoader[8] = toggleOff8;
        offLoader[9] = toggleOff9;
        offLoader[10] = toggleOff10;
        offLoader[11] = toggleOff11;
        offLoader[12] = toggleOff12;
        offLoader[13] = toggleOff13;
        offLoader[14] = toggleOff14;
        offLoader[15] = toggleOff15;
        offLoader[16] = toggleOff16;
        offLoader[17] = toggleOff17;
        offLoader[18] = toggleOff18;
        offLoader[19] = toggleOff19;
        offLoader[20] = toggleOff20;
        offLoader[21] = toggleOff21;

        onSprites = onLoader[sailingTracker.equippedSail];
        offSprites = offLoader[sailingTracker.equippedSail];
        sail.sprite = offSprites[2];
    }

    public void OnEnable(){
        if (on){
            hitboxOn.SetActive(true);
            hitboxOff.SetActive(false);
        }
        else{
            hitboxOn.SetActive(false);
            hitboxOff.SetActive(true);
        }
        onSprites = onLoader[sailingTracker.equippedSail];
        offSprites = offLoader[sailingTracker.equippedSail];
    }

    public void equipNewSail(int x){
        if (sailingTracker.sailsOwned[x] == 1){
            sailingTracker.equippedSail = x;
            onSprites = onLoader[x];
            offSprites = offLoader[x];
            if (on)
            {
                sail.sprite = onSprites[2];
            }
            else
            {
                sail.sprite = offSprites[2];
            }
            sailingTracker.saveSailing();
        }
    }

    public void toggleSail(){
        if (!coiling){
            if (on){
                hitboxOn.SetActive(false);
                hitboxOff.SetActive(true);
            }
            else{
                hitboxOn.SetActive(true);
                hitboxOff.SetActive(false);
            }
            on = !on;
            index = 0;
            timer = 0;

            coiling = true;
        }
    }

    public void FixedUpdate(){
        if (coiling){
            timer += Time.deltaTime;
            if (timer >= cutoff){
                timer = 0;
                if (!on){
                    sail.sprite = offSprites[index];
                }
                if (on){
                    sail.sprite = onSprites[index];
                }
                index++;
                if (index == 3){
                    coiling = false;
                }
            }
        }
    }
}
