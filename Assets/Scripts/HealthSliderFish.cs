using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthSliderFish : MonoBehaviour
{
    public Slider slider;

    public Gradient gradient;
    public Image fill;

    public TMP_Text fishDistanceText;

    private int maxFishDistance = 150;
    private int currentFishDistance;

    public int LureDistance = 5;

    public void Start(){
        currentFishDistance = maxFishDistance;
        MaxDistance();
    }

    public void MaxDistance(){
        slider.maxValue = maxFishDistance;
        slider.value = maxFishDistance;
        fishDistanceText.SetText(currentFishDistance + "/" + maxFishDistance + "m");

        fill.color = gradient.Evaluate(1f);
    }

    public void LureCloser(){
        slider.value = currentFishDistance - LureDistance;
        currentFishDistance -= LureDistance;
        fishDistanceText.SetText(currentFishDistance + "/" + maxFishDistance + "m");

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    
}
