using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class BaitBuy : MonoBehaviour
{
    [NonSerialized]
    public int WormTotal = 0; // NormalBaitTotal
    [NonSerialized]
    public int LadyBugTotal = 0; // FancyBaitTotal
    [NonSerialized]
    public int FireflyTotal = 0; // ExtravagantBaitTotal | Epic Bait
    [NonSerialized]
    public int MinnowTotal = 0; // PristineBaitTotal | Really Epic Bait
    [NonSerialized] 
    public int ButterflyTotal = 0;
    [NonSerialized] 
    public int DragonflyTotal = 0;
    [NonSerialized]
    public int PickleTotal = 0;
    [NonSerialized]
    public int SandwichTotal = 0;
    [NonSerialized]
    public int ShrimpTotal = 0;
    [NonSerialized]
    public int ShrimpTwoTotal = 0;
    [NonSerialized]
    public int ButterflyTwoTotal = 0;
    [NonSerialized]
    public int DragonflyTwoTotal = 0;

    [NonSerialized] public int[] baitTotals = new int[12];

    [NonSerialized] public int baitCoinTotal = 0;
    public GoldManager goldManager;

    public void DEVBAITCOINS()
    {
        baitCoinTotal += 100;
    }

    public void addBait()
    {
        WormTotal = baitTotals[0];
        LadyBugTotal = baitTotals[1];
        ShrimpTotal = baitTotals[2];
        FireflyTotal = baitTotals[3];
        MinnowTotal = baitTotals[4];
        SandwichTotal = baitTotals[5];
        ShrimpTwoTotal = baitTotals[6];
        ButterflyTotal = baitTotals[7];
        ButterflyTwoTotal = baitTotals[8];
        DragonflyTotal = baitTotals[9];
        DragonflyTwoTotal = baitTotals[10];
        PickleTotal = baitTotals[11];
        SaveBait();
    }

    public void baitCountInit(){
        baitTotals[0] = WormTotal;
        baitTotals[1] = LadyBugTotal;
        baitTotals[2] = ShrimpTotal;
        baitTotals[3] = FireflyTotal;
        baitTotals[4] = MinnowTotal;
        baitTotals[5] = SandwichTotal;
        baitTotals[6] = ShrimpTwoTotal;
        baitTotals[7] = ButterflyTotal;
        baitTotals[8] = ButterflyTwoTotal;
        baitTotals[9] = DragonflyTotal;
        baitTotals[10] = DragonflyTwoTotal;
        baitTotals[11] = PickleTotal;
    }

    public void SaveBait(){
        BaitStorageClass baitStorage = new BaitStorageClass{
            wormBait = baitTotals[0],
            ladybugBait = baitTotals[1],
            shrimpBait = baitTotals[2],
            fireflyBait = baitTotals[3],
            minnowBait = baitTotals[4],
            sandwichBait = baitTotals[5],
            shrimpTwoBait = baitTotals[6],
            butterflyBait = baitTotals[7],
            butterflyTwoBait = baitTotals[8],  
            dragonflyBait = baitTotals[9],
            dragonflyTwoBait = baitTotals[10],
            pickleBait = baitTotals[11],
            
            baitCoinTotal = baitCoinTotal,
        };
        string jsonStorage = JsonUtility.ToJson(baitStorage);
        SaveSystem.SaveFishing(jsonStorage);
    }

    public void LoadBait(){
        string saveString = SaveSystem.LoadFishing();
        if (saveString != null){
            BaitStorageClass loadedData = JsonUtility.FromJson<BaitStorageClass>(saveString);
            WormTotal = loadedData.wormBait;
            LadyBugTotal = loadedData.ladybugBait;
            FireflyTotal = loadedData.fireflyBait;
            MinnowTotal = loadedData.minnowBait;
            ButterflyTotal = loadedData.butterflyBait;
            DragonflyTotal = loadedData.dragonflyBait;
            PickleTotal = loadedData.pickleBait;
            SandwichTotal = loadedData.sandwichBait;
            ShrimpTwoTotal = loadedData.shrimpTwoBait;
            ShrimpTotal = loadedData.shrimpBait;
            ButterflyTwoTotal = loadedData.butterflyTwoBait;
            DragonflyTwoTotal = loadedData.dragonflyTwoBait;
            baitCoinTotal = loadedData.baitCoinTotal;
            baitCountInit();
        }
        else{
            WormTotal = 0;
            LadyBugTotal = 0;
            FireflyTotal = 0;
            MinnowTotal = 0;
            ButterflyTotal = 0;
            DragonflyTotal = 0;
            PickleTotal = 0;
            SandwichTotal = 0;
            ShrimpTwoTotal = 0;
            ShrimpTotal = 0;
            ButterflyTwoTotal = 0;
            DragonflyTwoTotal = 0;
            baitCoinTotal = 0;
            baitCountInit();
        }
    }
}

public class BaitStorageClass {
    public int wormBait = 0;
    public int ladybugBait = 0;
    public int fireflyBait = 0;
    public int minnowBait = 0;
    public int butterflyBait = 0;
    public int dragonflyBait = 0;
    public int pickleBait = 0;
    public int sandwichBait = 0;
    public int shrimpTwoBait = 0;
    public int shrimpBait = 0;
    public int butterflyTwoBait = 0;
    public int dragonflyTwoBait = 0;
    public int baitCoinTotal = 0;
}
