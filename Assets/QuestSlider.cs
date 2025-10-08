using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
using TMPro;

public class QuestSlider : MonoBehaviour
{
    public IslandManager islandManager;
    public Slider[] slider;
    public Image[] fill;
    public TMP_Text[] questProgress;
    public Color green;
    public Color yellow;

    public void updateSlider(int island, int house, int person, bool firstDone, bool secondDone, bool thirdDone)
    {
        islandManager.loadQuests();
        int cap0 = islandManager.questCaps[island][house][person][0];
        int cap1 = islandManager.questCaps[island][house][person][1];
        int cap2 = islandManager.questCaps[island][house][person][2];

        int cur0 = cap0 - islandManager.questTracking[island][house][person][0];
        int cur1 = cap1 - islandManager.questTracking[island][house][person][1];
        int cur2 = cap2 - islandManager.questTracking[island][house][person][2];

        slider[0].maxValue = cap0;
        slider[1].maxValue = cap1;
        slider[2].maxValue = cap2;

        slider[0].value = cur0;
        slider[1].value = cur1;
        slider[2].value = cur2;

        questProgress[0].SetText(cur0 + "/" + cap0);
        questProgress[1].SetText(cur1 + "/" + cap1);
        questProgress[2].SetText(cur2 + "/" + cap2);


        if (firstDone)
        {
            fill[0].color = green;
        }
        else
        {
            fill[0].color = yellow;
        }

        if (secondDone)
        {
            fill[1].color = green;
        }
        else
        {
            fill[1].color = yellow;
        }
        
        if (thirdDone)
        {
            fill[2].color = green;
        }
        else
        {
            fill[2].color = yellow;
        }

    }
}
