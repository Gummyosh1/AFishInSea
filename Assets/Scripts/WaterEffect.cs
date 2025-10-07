using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class WaterEffect : MonoBehaviour
{
    public Sprite[] waterSplashes;
    private Image image;
    private bool moving = true;
    private float timer = 0;
    private int index = 0;
    private int cycle = 0;
    private bool gone = false;

    public void Start(){
        image = GetComponent<Image>();
    }

    public void Update(){
        if (moving){
            cycleSplashes();
        }
    }

    public void cycleSplashes(){
        timer += Time.deltaTime;
        if (timer > .25 && cycle < 3){
            cycle++;
            image.sprite = waterSplashes[index];
            index++;
            index %= waterSplashes.Length;
            timer = 0;
        }
        if (cycle == 3){
            if (timer > .25){
                if (!gone){
                    image.enabled = false;
                    gone = true;
                }
            }
            if (timer > 2){
                gone = false;
                cycle = 0;
                image.enabled = true;
            }
        }
    }
}
