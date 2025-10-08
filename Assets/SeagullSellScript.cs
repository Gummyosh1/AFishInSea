using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class SeagullSellScript : MonoBehaviour
{
    public Transform SellSlot;
    public TMP_Text sellSlotText;
    public GoldManager goldManager;
    [NonSerialized] public GameObject SellItem;
    [NonSerialized] public int type = 0;

    public void Update(){
        if (SellSlot.childCount > 0){
            SellItem = SellSlot.GetChild(0).gameObject;
            if (SellItem.name.IndexOf("Normal") > -1){
                type = 0;
            }
            else if (SellItem.name.IndexOf("Fancy") > -1){
                type = 1;
            }
            else if (SellItem.name.IndexOf("Extravagant") > -1){
                type = 2;
            }
            else if (SellItem.name.IndexOf("Pristine") > -1){
                type = 3;
            }
            else if (SellItem.name.IndexOf("Magical") > -1){
                type = 4;
            }

        }
        else{
            type = 5;
        }
    }
}
