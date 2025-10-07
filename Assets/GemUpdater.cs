using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GemUpdater : MonoBehaviour
{
    public GemStorage gemStorage;
    public TMP_Text totalText;
    private float timer = 0;



    public void gemSaveInit()
    {
        totalText.text = gemStorage.GemTotal.ToString();
    }

    public void Update()
    {
        timer += Time.deltaTime;
        if (timer >= .2)
        {
            totalText.text = gemStorage.GemTotal.ToString();
            timer = 0;
        }
    }
}
