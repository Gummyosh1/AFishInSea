using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopInit : MonoBehaviour
{
    public TMP_Text storeDabloonQ;
    public GemStorage gemStorage;

    public void OnEnable(){
        storeDabloonQ.text = gemStorage.GemTotal.ToString();
    }
}
