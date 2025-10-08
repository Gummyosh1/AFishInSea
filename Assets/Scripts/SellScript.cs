using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SellScript : MonoBehaviour
{
    public Transform SellSlot;
    public TMP_Text sellSlotText;
    public GoldManager goldManager;
    public TMP_Text sellText;
    private GameObject SellItem;
    private int type = 0;
    private int sellAmount = 0;


    public void Update(){
        if (SellSlot.childCount > 0){
            SellItem = SellSlot.GetChild(0).gameObject;
            if (SellItem.name.IndexOf("Normal") > -1){
                type = 0;
                sellText.text = "Type: Normal\nPrice: 25 Gold";
            }
            else if (SellItem.name.IndexOf("Fancy") > -1){
                type = 1;
                sellText.text = "Type: Fancy\nPrice: 50 Gold";
            }
            else if (SellItem.name.IndexOf("Extravagant") > -1){
                type = 2;
                sellText.text = "Type: Extravagant\nPrice: 100 Gold";
            }
            else if (SellItem.name.IndexOf("Pristine") > -1){
                sellText.text = "Type: Pristine\nPrice: 150 Gold";
                type = 3;
            }
            else if (SellItem.name.IndexOf("Magical") > -1){
                sellText.text = "Type: Magical\nPrice: 200 Gold";
                type = 4;
            }

        }
        else{
            sellText.text = "What've you fished up for me today?";
            type = 5;
        }
    }

    public void sellItem(){
        sellSlotText.text = (int.Parse(sellSlotText.text) - 1).ToString();
        switch(type){
            case 0:
                sellAmount = 25;
                break;
            case 1:
                sellAmount = 50;
                break;
            case 2:
                sellAmount = 100;
                break;
            case 3:
                sellAmount = 150;
                break;
            case 4:
                sellAmount = 200;
                break;
            case 5:
                sellAmount = 0;
                break;
        }
        goldManager.GainGold(sellAmount);
        if (int.Parse(sellSlotText.text) <= 0){
            Destroy(SellItem);
            sellSlotText.gameObject.SetActive(false);
        }
    }

}
