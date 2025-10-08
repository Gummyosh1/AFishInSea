using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldBar : MonoBehaviour
{
    public TMP_Text goldTotal;
    public TMP_Text goldTotalVillager;
    public GoldManager goldManager;


    public void goldInit()
    {
        goldTotal.text = goldManager.PlayerGold.ToString();
    }

    public void goldInitVillager()
    {
        goldTotalVillager.text = goldManager.PlayerGold.ToString();
    }
}
