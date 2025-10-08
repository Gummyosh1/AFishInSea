using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class FlameAnimate : MonoBehaviour
{
    public Sprite[] redTorches;
    public Sprite[] yellowTorches;
    public Sprite[] blueTorches;
    public Sprite[] greenTorches;
    public Sprite[] greyTorches;
    private Sprite[][] torches = new Sprite[5][];
    public Image image;

    [NonSerialized]
    public int currentTorch = 0;
    [NonSerialized] public int index = 0;
    private float timer = 0;
    private bool inited = false;

    public void initList()
    {
        torches[0] = redTorches;
        torches[1] = yellowTorches;
        torches[2] = blueTorches;
        torches[3] = greenTorches;
        torches[4] = greyTorches;
        inited = true;
    }

    public void tabOver()
    {
        timer = 1;
    }

    public void Update()
    {
        timer += Time.deltaTime;
        if (timer > .25)
        {
            image.sprite = torches[currentTorch][index];
            index++;
            index %= torches[currentTorch].Length;
            timer = 0;
        }

    }

}
