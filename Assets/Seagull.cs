using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using UnityEngine.UI;
using TMPro;

public class Seagull : MonoBehaviour
{
    /*public GemStorage gemStorage;
    public Sprite[] seagullSprites;
    public Image seagullHolder;
    public GameObject seagull;
    public GameObject seagullPopUpOBJ;
    public GameObject greyBack;
    public TabScript tabScript;
    public InventoryKing inventoryKing;
    public SeagullSellScript seagullSellScript;
    public TMP_Text sellSlotText;
    public TMP_Text seagullDescription;
    public Image seagulImageInPopUp;
    public GameObject seagullReward;
    public TMP_Text seagullRewardText;

    public Random random = new Random();
    private int rarityNum = 0;
    private int quantityNum = 0;
    private int rewardsNum = 0;
    private bool SeagullActive = false;
    private string holder;
    [NonSerialized] public int index = 0;
    [NonSerialized] public int[] rarity = {0,0,0,0,0,1,1,1,1,2,2,2,3,3,4};
    [NonSerialized] public int[] quantity = {1,2,3,4,5,1,2,3,4,1,2,3,1,2,1};
    [NonSerialized] public int[] rewards = {5, 10, 10, 10, 15, 15, 15, 15, 20, 20, 20, 25, 25, 30, 50};
    
    public void spawnSeagull(){
        seagull.SetActive(true);
        index = random.Next(0,15);
        seagullHolder.sprite = seagullSprites[index];
        rarityNum = rarity[index];
        quantityNum = quantity[index];
        rewardsNum = rewards[index];
        SeagullActive = true;
        SaveSeagull();
    }

    public void seagullPopUp(){
        seagullPopUpOBJ.SetActive(true);
        seagulImageInPopUp.sprite = seagullSprites[index];
        greyBack.SetActive(true);
        tabScript.navBar.SetActive(false);
        inventoryKing.ClearThenLoadInventory();
        switch(rarityNum){
            case 0:
                holder = "normal";
                break;
            case 1:
                holder = "fancy";
                break;
            case 2:
                holder = "extravagant";
                break;
            case 3:
                holder = "pristine";
                break;
            case 4:
                holder = "magical";
                break;
        }
        seagullDescription.text = "I require " + quantityNum + " " + holder + " fish";
    }

    public void completeSeagull(){
        gemStorage.addGems(rewardsNum);
        seagullPopUpOBJ.SetActive(false);
        seagull.SetActive(false);
        SeagullActive = false;
        seagullReward.SetActive(true);
        seagullRewardText.text = rewardsNum.ToString();
        inventoryKing.leaveSellScreenOG();
        SaveSeagull();
    }

    public void rewardPop(){
        greyBack.SetActive(false);
        seagullReward.SetActive(false);
    }

    public void fillBackToInven(){
        inventoryKing.leaveSellScreenOG();
    }

    public void giveToSeagull(){
        if (seagullSellScript.type != 5 && rarityNum <= seagullSellScript.type){
            quantityNum--;
            sellSlotText.text = (int.Parse(sellSlotText.text) - 1).ToString();
            seagullDescription.text = "I require " + quantityNum + " " + holder + " fish";
            if (int.Parse(sellSlotText.text) <= 0){
                Destroy(seagullSellScript.SellItem);
                sellSlotText.gameObject.SetActive(false);
            }
            if (quantityNum == 0){
                completeSeagull();
            }
            SaveSeagull();
        }
    }


    public void SaveSeagull(){
        SeagullClass seagullClass = new SeagullClass{
            rarity = rarityNum,
            quantity = quantityNum,
            rewards = rewardsNum,
            SeagullActive = SeagullActive,
            index = index,
        };
        string jsonStorage = JsonUtility.ToJson(seagullClass);
        SaveSystem.SaveSeagull(jsonStorage);
    }

    public void LoadSeagull(){
        string saveString = SaveSystem.LoadSeagull();
        if (saveString != null){
            SeagullClass loadedData = JsonUtility.FromJson<SeagullClass>(saveString);
            rarityNum = loadedData.rarity;
            quantityNum = loadedData.quantity;
            rewardsNum = loadedData.rewards;
            SeagullActive = loadedData.SeagullActive;
            index = loadedData.index;
            if (SeagullActive){
                seagull.SetActive(true);
                seagullHolder.sprite = seagullSprites[index];
            }
        }
    }

}

public class SeagullClass
{
    public int rarity = 0;
    public int quantity = 0;
    public int rewards = 0;
    public bool SeagullActive = false;
    public int index = 0;
}
*/
}