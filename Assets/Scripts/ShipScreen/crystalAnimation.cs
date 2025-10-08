using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class crystalAnimation : MonoBehaviour
{
    public Sprite[] redCrystal;
    public Sprite[] yellowCrystal;
    public Sprite[] blueCrystal;
    public Sprite[] greenCrystal;
    public Sprite[] greyCrystals;
    private Sprite[][] crystals = new Sprite[6][];
    public Image image;

    [NonSerialized]
    public int currentCrystal = 0;
    [NonSerialized] public int index = 0;
    private float timer = 0;

    
    public void CrystalInit(){
        crystals[0] = redCrystal;
        crystals[1] = yellowCrystal;
        crystals[2] = blueCrystal;
        crystals[3] = greenCrystal;
        crystals[4] = greyCrystals;
        image.sprite = crystals[currentCrystal][index];
        index++;
        index %= crystals[currentCrystal].Length;
        timer = 0;
    }

    public void Update(){
        timer += Time.deltaTime;
        if (timer > .5){
            image.sprite = crystals[currentCrystal][index];
            index++;
            index %= crystals[currentCrystal].Length;
            timer = 0;
        }

    }

}