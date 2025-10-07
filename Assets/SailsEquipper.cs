using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;
using Button = UnityEngine.UI.Button;

public class SailsEquipper : MonoBehaviour
{
    public SailSwap sailSwap;
    public SailingTracker sailingTracker;
    public GameObject[] locks;
    public Button[] buttons;
    public Image EquipScreenImage;
    public GameObject equipPopUpOBJ;
    private int currentSail = 0;

    [NonSerialized] public int sailTotal = 21;

    public void popUpInit(){
        for (int i = 0; i < sailTotal; i++){
        if (sailingTracker.sailsOwned[i] == 1){
            locks[i].SetActive(false);
            buttons[i].enabled = true;
        }
        else{
            locks[i].SetActive(true);
            buttons[i].enabled = false;
        }
        }
    }

    public void equipPopUp(int x){
        currentSail = x;
        EquipScreenImage.sprite = sailSwap.onLoader[x][2];
        equipPopUpOBJ.SetActive(true);
    }

    public void equipSail(){
        sailSwap.equipNewSail(currentSail);
        equipPopUpOBJ.SetActive(false);
    }
}
