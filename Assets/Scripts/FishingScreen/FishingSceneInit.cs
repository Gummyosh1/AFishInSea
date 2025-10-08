using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingSceneInit : MonoBehaviour
{
    public InventoryKing inventoryKing;
    public BaitQuantity baitQuantity;

    public void Start(){
        /*baitQuantity.NormalOn();
        if (baitQuantity.selectedBait == 4){baitQuantity.FancyOn();}
        if (baitQuantity.selectedBait == 4){baitQuantity.ExtravagantOn();}
        if (baitQuantity.selectedBait == 4){baitQuantity.PristineOn();}*/
    }

    public void OnEnable(){
        inventoryKing.ClearThenLoadInventory();
        
        /*baitQuantity.NormalOn();
        if (baitQuantity.selectedBait == 4){baitQuantity.FancyOn();}
        if (baitQuantity.selectedBait == 4){baitQuantity.ExtravagantOn();}
        if (baitQuantity.selectedBait == 4){baitQuantity.PristineOn();}*/
    }
}
