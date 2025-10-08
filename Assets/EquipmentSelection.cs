using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSelection : MonoBehaviour
{
    public GameObject equipmentMenu;
    public GameObject greyBacking;
    public GameObject homeBar;
    public EquipmentScript equipmentScript;
    public SpriteCycler spriteCycler1;
    public SpriteCycler spriteCycler2;
    public SpriteCycler spriteCycler3;
    public SpriteCycler spriteCycler4;
    

    public void turnOnMenu(){
        equipmentMenu.SetActive(true);
        greyBacking.SetActive(true);
        homeBar.SetActive(false);
        equipmentScript.popUpInit();
        spriteCycler1.Init();
        spriteCycler2.Init();
        spriteCycler3.Init();
        spriteCycler4.Init();
    }

    public void turnOffMenu(){
        equipmentMenu.SetActive(false);
        greyBacking.SetActive(false);
        homeBar.SetActive(true);
    }
}
