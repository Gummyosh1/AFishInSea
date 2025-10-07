using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    public GameObject Inventory;
    public GameObject InventoryLocks;
    public InventoryKing inventoryKing;
    public GameObject background;
    public GameObject InventoryQuantities;
    public BattlePass battlePass;
    public void InventoryPopUp(){
        Inventory.SetActive(!Inventory.activeSelf);
        background.SetActive(!background.activeSelf);
        InventoryQuantities.SetActive(!InventoryQuantities.activeSelf);
        if (inventoryKing.battlePass.passPurchased){
            InventoryLocks.SetActive(false);
        }
        else if (Inventory.activeSelf){
            InventoryLocks.SetActive(true);
        }
        else{
            InventoryLocks.SetActive(false);
        }
    }
}
