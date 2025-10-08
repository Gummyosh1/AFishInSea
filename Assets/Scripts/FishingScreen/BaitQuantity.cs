using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaitQuantity : MonoBehaviour
{
    public BaitBuy baitBuy;

    public TMP_Text NormalBait;
    public TMP_Text FancyBait;
    public TMP_Text ExtravagantBait;
    public TMP_Text PristineBait;

    public GameObject NormalIcon;
    public GameObject FancyIcon;
    public GameObject ExtravagantIcon;
    public GameObject PristineIcon;
    public GameObject NoBait;

    public GameObject menu;
    public GameObject background;
    public Image fishCollectionIcon;
    public Sprite[] baits;
    public Image baitEquip;
    public TMP_Text baitEquipText;
    [NonSerialized]
    public int selectedBait = 6; // 0: Worm, 1: Ladybug, 2: Firefly, 3: Minnow, 4: Butterfly, 5: Dragonfly, 6: None

    /*public void BaitInit(){
        incrementNormal();
        incrementFancy();
        incrementExtravagant();
        incrementPristine();
    }

    public void incrementNormal(){
        NormalBait.text = "" + baitBuy.NormalBaitTotal;
    }

    public void incrementFancy(){
        FancyBait.text = "" + baitBuy.FancyBaitTotal;
    }

    public void incrementExtravagant(){
        ExtravagantBait.text = "" + baitBuy.ExtravagantBaitTotal;
    }

    public void incrementPristine(){
        PristineBait.text = "" + baitBuy.PristineBaitTotal;
    }*/

    public void popUpMenu(){
        menu.SetActive(!menu.activeSelf);
        background.SetActive(!background.activeSelf);
    }

    /*public void NormalOn(){
        if (baitBuy.NormalBaitTotal > 0){
            NormalIcon.SetActive(true);
            FancyIcon.SetActive(false);
            ExtravagantIcon.SetActive(false);
            PristineIcon.SetActive(false);
            NoBait.SetActive(false);
            baitEquip.color = Color.white;
            baitEquip.sprite = baits[0];
            baitEquipText.text = "" + baitBuy.NormalBaitTotal;
            selectedBait = 0;
        }
        else{
            NoBait.SetActive(true);
            NormalIcon.SetActive(false);
            FancyIcon.SetActive(false);
            ExtravagantIcon.SetActive(false);
            PristineIcon.SetActive(false);
            baitEquip.color = Color.black;
            baitEquip.sprite = baits[0];
            baitEquipText.text = "0";
            selectedBait = 4;
        }
    }

    public void FancyOn(){
        if (baitBuy.FancyBaitTotal > 0){
            NormalIcon.SetActive(false);
            FancyIcon.SetActive(true);
            ExtravagantIcon.SetActive(false);
            PristineIcon.SetActive(false);
            NoBait.SetActive(false);
            baitEquip.color = Color.white;
            baitEquip.sprite = baits[1];
            baitEquipText.text = "" + baitBuy.FancyBaitTotal;
            selectedBait = 1;
        }
        else{
            NoBait.SetActive(true);
            NormalIcon.SetActive(false);
            FancyIcon.SetActive(false);
            ExtravagantIcon.SetActive(false);
            PristineIcon.SetActive(false);
            baitEquip.color = Color.black;
            baitEquip.sprite = baits[0];
            baitEquipText.text = "0";
            selectedBait = 4;
        }
    }

    public void ExtravagantOn(){
        if (baitBuy.ExtravagantBaitTotal > 0){
            NormalIcon.SetActive(false);
            FancyIcon.SetActive(false);
            ExtravagantIcon.SetActive(true);
            PristineIcon.SetActive(false);
            NoBait.SetActive(false);
            baitEquip.color = Color.white;
            baitEquip.sprite = baits[2];
            baitEquipText.text = "" + baitBuy.ExtravagantBaitTotal;
            selectedBait = 2;
        }
        else{
            NoBait.SetActive(true);
            NormalIcon.SetActive(false);
            FancyIcon.SetActive(false);
            ExtravagantIcon.SetActive(false);
            PristineIcon.SetActive(false);
            baitEquip.color = Color.black;
            baitEquip.sprite = baits[0];
            baitEquipText.text = "0";
            selectedBait = 4;
        }
    }

    public void PristineOn(){
        if (baitBuy.PristineBaitTotal > 0){
            NormalIcon.SetActive(false);
            FancyIcon.SetActive(false);
            ExtravagantIcon.SetActive(false);
            PristineIcon.SetActive(true);
            NoBait.SetActive(false);
            baitEquip.color = Color.white;
            baitEquip.sprite = baits[3];
            baitEquipText.text = "" + baitBuy.PristineBaitTotal;
            selectedBait = 3;
        }
        else{
            NoBait.SetActive(true);
            NormalIcon.SetActive(false);
            FancyIcon.SetActive(false);
            ExtravagantIcon.SetActive(false);
            PristineIcon.SetActive(false);
            baitEquip.color = Color.black;
            baitEquip.sprite = baits[0];
            baitEquipText.text = "0";
            selectedBait = 4;
        }
    }*/
}
