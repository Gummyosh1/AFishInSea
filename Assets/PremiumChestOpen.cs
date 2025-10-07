using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PremiumChestOpen : MonoBehaviour
{
    public BattlePass battlePass;
    public GameObject premiumChestScreen;
    public InventoryKing inventoryKing;
    public GameObject premiumLock;
    public GameObject book;

    /*public void tapPremium(){
        if (battlePass.passPurchased){
            book.SetActive(false);
            premiumChestScreen.SetActive(true);
            //inventoryKing.ClearThenLoadPaidChest();
            inventoryKing.ClearThenLoadInventory();
        }
    }

    public void OnEnable(){
        if (battlePass.passPurchased){
            premiumLock.SetActive(false);
        }
    }*/
}
