using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipUpdater : MonoBehaviour
{
    public BaitBuy baitBuy;
    public GameObject coinPopUp;
    public PetWalk petWalk;
    public SailSwap sailSwap;

    public void Update(){
        if (baitBuy.baitCoinTotal > 0){
            coinPopUp.SetActive(true);
        }
        else{
            coinPopUp.SetActive(false);
        }
    }

    public void OnEnable(){
        petWalk.idling = true;

        if (sailSwap.on){
            sailSwap.hitboxOn.SetActive(true);
            sailSwap.hitboxOff.SetActive(false);
        }
        else{
            sailSwap.hitboxOn.SetActive(false);
            sailSwap.hitboxOff.SetActive(true);
        }
        sailSwap.onSprites = sailSwap.onLoader[sailSwap.sailingTracker.equippedSail];
        sailSwap.offSprites = sailSwap.offLoader[sailSwap.sailingTracker.equippedSail];
    }
}
