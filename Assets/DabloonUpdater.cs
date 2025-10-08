using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DabloonUpdater : MonoBehaviour
{
    public GemStorage gemStorage;
    public TMP_Text dabloonQuantity;

    public void loadDabloons(){
        gemStorage.LoadGems();
        dabloonQuantity.text = gemStorage.GemTotal.ToString();
    }
}
