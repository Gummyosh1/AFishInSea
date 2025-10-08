using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChairEquipper : MonoBehaviour
{
    public Sprite[] chairSprites;
    public EquipmentScript equipmentScript;
    public GameObject[] locks;
    public Button[] buttons;
    public Image EquipScreenImage;
    public GameObject equipPopUpOBJ;
    private int currentChair = 0;
    [NonSerialized] public int chairTotal = 4;

    public void popUpInit(){
        for (int i = 0; i < chairTotal; i++){
        if (equipmentScript.chairsOwned[i] == 1){
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
        currentChair = x;
        EquipScreenImage.sprite = chairSprites[x];
        equipPopUpOBJ.SetActive(true);
    }

    public void equipChair(){
        equipmentScript.equipChair(currentChair);
        Debug.Log("Current chair is "+ currentChair);
        equipPopUpOBJ.SetActive(false);
    }
}
