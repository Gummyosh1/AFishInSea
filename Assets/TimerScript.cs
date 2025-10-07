using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class TimerScript : MonoBehaviour
{
    public Sprite[] clocks;
    public Image clock;
    [NonSerialized] public int index = 0;
    [NonSerialized] public float timer = 0;
    private float cutoff = 1.5f;
    [NonSerialized] public bool done = false;

    // Update is called once per frame

    public void resetImage()
    {
        clock.sprite = clocks[0];
    }
    void FixedUpdate()
    {
        if (!done)
        {
            timer += Time.deltaTime;
            if (timer >= cutoff)
            {
                clock.sprite = clocks[index];
                index++;
                timer = 0;
                if (index >= clocks.Length)
                {
                    done = true;
                }
            }
        }
    }
}
