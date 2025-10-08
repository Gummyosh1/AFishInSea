using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VolumeUpdater : MonoBehaviour
{
    public Slider slider;         // Assign your slider in the Inspector
    public TMP_Text percentageText;
    public AudioTracker audioTracker;

    private float timer = 0;

    void Start()
    {
        // Set up listener for value changes
        slider.onValueChanged.AddListener(UpdatePercentageText);
    }

    public void UpdatePercentageText(float value)
    {
        float percentage = value * 100f;
        percentageText.text = Mathf.RoundToInt(percentage) + "%";
    }

    public void Update()
    {
        timer += Time.deltaTime;
        if (timer >= .3)
        {
            audioTracker.changeVolume();
        }
    }
}
