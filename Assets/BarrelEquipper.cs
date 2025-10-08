using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarrelEquipper : MonoBehaviour
{
    public Sprite[] barrelSprites;
    public EquipmentScript equipmentScript;
    public GameObject[] locks;
    public Button[] buttons;
    public Image EquipScreenImage;
    public GameObject equipPopUpOBJ;
    private int currentBarrel = 0;
    [NonSerialized] public int barrelTotal = 4;

    public void popUpInit(){
        for (int i = 0; i < barrelTotal; i++){
        if (equipmentScript.barrelsOwned[i] == 1){
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
        currentBarrel = x;
        EquipScreenImage.sprite = barrelSprites[x];
        equipPopUpOBJ.SetActive(true);
    }

    public void equipBarrel(){
        equipmentScript.equipBarrel(currentBarrel);
        equipPopUpOBJ.SetActive(false);
    }
}
