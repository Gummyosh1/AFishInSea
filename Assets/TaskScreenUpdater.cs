using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskScreenUpdater : MonoBehaviour
{
    public BaitBuy baitBuy;
    public TMP_Text baitCoinTotalText;

    public void Update(){
        baitCoinTotalText.text = baitBuy.baitCoinTotal.ToString();
    }
}
