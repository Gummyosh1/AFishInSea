using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellLocks : MonoBehaviour
{
    public GameObject InventoryLocks;
    public InventoryKing inventoryKing;
    public BattlePass battlePass;

    public void toggleLockedSlots(){
        if (battlePass.passPurchased){
            InventoryLocks.SetActive(false);
        }
        else{
            InventoryLocks.SetActive(true);
        }
    }
}
