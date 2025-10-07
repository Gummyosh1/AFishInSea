using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class MiniGame3Animation : MonoBehaviour
{
    public MiniGameThree miniGameThree;
    public Sprite[] coinFrames;
    public Sprite[] coinFramesYR;
    public Sprite[] coinFramesGY;
    public Image coinFrame;
    private float timer = 0;
    private float cutoff = 0.035f;

    private int indexSetup = 17;
    private int indexFullFlip = 0;
    private int indexYellowToBlue = 0;
    private int indexBlueToYellow = 11;
    private int indexYellowToRed = 0;
    private int indexGreenToYellow = 0;

    private int endDexSetup = 23;
    private int endDexFullFlip = 23;
    private int endDexYellowToBlue = 11;
    private int endDexBlueToYellow = 23;
    private int endDexYellowToRed = 11;
    private int endDexGreenToYellow = 11;
    
    [NonSerialized] public bool setup = false;
    [NonSerialized] public bool fullFlip = false;
    [NonSerialized] public bool yellowToBlueFlip = false;
    [NonSerialized] public bool YBFinish = false;
    [NonSerialized] public bool blueToYellowFlip = false;
    [NonSerialized] public bool BYFinish = false;
    [NonSerialized] public bool yellowToRedFlip = false;
    [NonSerialized] public bool YRFinish = false;
    [NonSerialized] public bool greenToYellowFlip = false;
    [NonSerialized] public bool GYFinish = false;


    public void Update()
    {
        if (fullFlip) _flip();
        if (yellowToBlueFlip) YBFinish = _yellowToBlue();
        if (blueToYellowFlip) BYFinish = _blueToYellow();
        if (setup) _setup();
        if (yellowToRedFlip) YRFinish = _yellowToRed();
        if (greenToYellowFlip) GYFinish = _greenToYellow();
    }

    private void _flip(){
        timer += Time.deltaTime;
        if (timer >= cutoff){
            indexFullFlip++;
            if (indexFullFlip >= endDexFullFlip){
                fullFlip = false;
                indexFullFlip = 0;
                timer = 0;
                return;
            } 
            coinFrame.sprite = coinFrames[indexFullFlip];
            timer = 0;
        }
    }

    public void setGreen()
    {
        coinFrame.sprite = coinFramesGY[0];
    }

    private bool _yellowToBlue()
    {
        //NEEDS SET TO BE PUT TO FALSE OUTSIDE OF THIS FUNCTION CALL
        timer += Time.deltaTime;
        if (timer >= cutoff)
        {
            indexYellowToBlue++;
            if (indexYellowToBlue >= endDexYellowToBlue)
            {
                yellowToBlueFlip = false;
                indexYellowToBlue = 0;
                timer = 0;
                return true;
            }
            coinFrame.sprite = coinFrames[indexYellowToBlue];
            timer = 0;
        }
        return false;
    }

    private bool _yellowToRed(){
        //NEEDS SET TO BE PUT TO FALSE OUTSIDE OF THIS FUNCTION CALL
        timer += Time.deltaTime;
        if (timer >= cutoff){
            indexYellowToRed++;
            if (indexYellowToRed >= endDexYellowToRed){
                yellowToRedFlip = false;
                indexYellowToRed = 0;
                timer = 0;
                return true;
            } 
            coinFrame.sprite = coinFramesYR[indexYellowToRed];
            timer = 0;
        }
        return false;
    }
    
    private bool _greenToYellow(){
        timer += Time.deltaTime;
        if (timer >= cutoff){
            indexGreenToYellow++;
            if (indexGreenToYellow >= endDexGreenToYellow){
                greenToYellowFlip = false;
                indexGreenToYellow = 0;
                timer = 0;
                return true;
            } 
            coinFrame.sprite = coinFramesGY[indexGreenToYellow];
            timer = 0;
        }
        return false;
    }



    private void _setup()
    {
        timer += Time.deltaTime;
        if (timer >= cutoff)
        {
            indexSetup++;
            if (indexSetup >= endDexSetup)
            {
                setup = false;
                indexSetup = 17;
                timer = 0;
                return;
            }
            coinFrame.sprite = coinFrames[indexSetup];
            timer = 0;
        }
    }

    private bool _blueToYellow(){
        //NEEDS SET TO BE PUT TO FALSE OUTSIDE OF THIS FUNCTION CALL
        timer += Time.deltaTime;
        if (timer >= cutoff){
            indexBlueToYellow++;
            if (indexBlueToYellow >= endDexBlueToYellow){
                blueToYellowFlip = false;
                indexBlueToYellow = 11;
                timer = 0;
                return true;
            } 
            coinFrame.sprite = coinFrames[indexBlueToYellow];
            timer = 0;
        }
        return false;
    }

    

    public void setDefaultImage(){
        coinFrame.sprite = coinFrames[17];
    }

}
