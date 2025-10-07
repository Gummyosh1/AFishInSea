using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CatchTimer : MonoBehaviour
{
    public Slider slider;
    public TMP_Text timeToCatchText;
    [NonSerialized]
    public float currentTime;


    public void InitializeTimeBar(float time){
        slider.maxValue = time;
        slider.value = time;
        timeToCatchText.SetText("" + time);
    }
}
