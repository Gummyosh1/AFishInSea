using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
using TMPro;
using System;

public class FishCollected : MonoBehaviour
{
    public Slider slider;
    public IslandScript islandScript;
    public IslandManager islandManager;
    public GameProgress gameProgress;
    public Image progBarHolder;
    public Sprite emptyBar;
    public Sprite progressBar;
    public GameObject soldOut;

    public void InitializeProgressBar()
    {

        //SLIDER INIT
        int curIsland = gameProgress.currentIslandIndex;
        int curTotal = 3;
        //Debug.Log("curTotal = " + curTotal);
        int curAmnt = islandManager.houseInfoStorage[curIsland][islandScript.insideHouse][islandScript.insidePerson];
        slider.maxValue = curTotal;
        slider.value = curAmnt;

        //SPRITE INIT
        if (curAmnt != 0)
        {
            progBarHolder.sprite = progressBar;
        }
        else
        {
            progBarHolder.sprite = emptyBar;
        }

        //TEXT INIT

        //fpTilFullPay.text = "I got " + (curAmnt * 40).ToString() + " Gold left to buy fish";

        if (curAmnt == 0)
        {
            soldOut.SetActive(true);
            //barText.gameObject.SetActive(false);
        }
        else
        {
            soldOut.SetActive(false);
            //barText.gameObject.SetActive(true);
        }

        //6 situations
        //1 amnt < 1/3 total
        //2 amnt == 1/3 total
        //3 amnt < 2/3 total
        //4 amnt == 2/3 total
        //5 amnt > 2/3 total
        /*
        if(curAmnt == curTotal/3 || curAmnt == (curTotal/3)*2){
            fpTilStipend.text = "0 FP before stipend pay";
        }
        else if(curAmnt > (curTotal/3)*2){
            fpTilStipend.text = "No more stipends available";
        }
        else if (curAmnt < curTotal/3){
            fpTilStipend.text = ((curTotal/3) - curAmnt).ToString() + " FP before stipend pay";
        }
        else if (curAmnt < (curTotal/3)*2){
            fpTilStipend.text = (((curTotal/3)*2) - curAmnt).ToString() + " FP before stipend pay";
        }*/

    }

    public void UpdateProgressBar()
    {
        //SLIDER UPDATING
        int curIsland = gameProgress.currentIslandIndex;
        //int curTotal = 3;
        int curAmnt = islandManager.houseInfoStorage[curIsland][islandScript.insideHouse][islandScript.insidePerson];
        slider.value = curAmnt;

        //SPRITE UPDATING
        if (curAmnt != 0)
        {
            progBarHolder.sprite = progressBar;
        }
        else
        {
            //Debug.Log(progBarHolder);
            //Debug.Log(emptyBar);
            progBarHolder.sprite = emptyBar;
        }

        //TEXT UPDATING

        //fpTilFullPay.text = "I got " + (curAmnt * 40).ToString() + " Gold left to buy fish";
        if (curAmnt == 0)
        {
            soldOut.SetActive(true);
            //barText.gameObject.SetActive(false);
        }
        else
        {
            soldOut.SetActive(false);
            //barText.gameObject.SetActive(true);
        }
        

        /*
        if(curAmnt == curTotal/3 || curAmnt == (curTotal/3)*2){
            fpTilStipend.text = "0 FP before stipend pay";
        }
        else if(curAmnt > (curTotal/3)*2){
            fpTilStipend.text = "No more stipends available";
        }
        else if (curAmnt < curTotal/3){
            fpTilStipend.text = ((curTotal/3) - curAmnt).ToString() + " FP before stipend pay";
        }
        else if (curAmnt < (curTotal/3)*2){
            fpTilStipend.text = (((curTotal/3)*2) - curAmnt).ToString() + " FP before stipend pay";
        }*/
    }
}
