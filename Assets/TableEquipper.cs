using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class TableEquipper : MonoBehaviour
{
    public Sprite[] tableSprites;
    public EquipmentScript equipmentScript;
    public GameObject[] locks;
    public Button[] buttons;
    public Image EquipScreenImage;
    public GameObject equipPopUpOBJ;
    private int currentTable = 0;
    [NonSerialized] public int tableTotal = 4;

    public void popUpInit(){
        for (int i = 0; i < tableTotal; i++){
        if (equipmentScript.tablesOwned[i] == 1){
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
        currentTable = x;
        EquipScreenImage.sprite = tableSprites[x];
        equipPopUpOBJ.SetActive(true);
    }

    public void equipTable(){
        equipmentScript.equipTable(currentTable);
        equipPopUpOBJ.SetActive(false);
    }
}
