using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GobletEquipper : MonoBehaviour
{
    public Sprite[] gobletSprites;
    public EquipmentScript equipmentScript;
    public GameObject[] locks;
    public Button[] buttons;
    public Image EquipScreenImage;
    public GameObject equipPopUpOBJ;
    private int currentGoblet = 0;
    [NonSerialized] public int gobletTotal = 4;

    public void popUpInit(){
        for (int i = 0; i < gobletTotal; i++){
        if (equipmentScript.gobletsOwned[i] == 1){
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
        currentGoblet = x;
        EquipScreenImage.sprite = gobletSprites[x];
        equipPopUpOBJ.SetActive(true);
    }

    public void equipGoblet(){
        equipmentScript.equipGoblet(currentGoblet);
        equipPopUpOBJ.SetActive(false);
    }
}
