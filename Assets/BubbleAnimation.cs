using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class BubbleAnimation : MonoBehaviour
{
    public Image fishHolder;
    public Sprite[] pinkSprites;
    public Sprite[] yellowSprites;
    public Sprite[] orangeSprites;

    public GameObject[] hitBoxes;

    private bool go = false;
    private int colorIndex = -1;
    [NonSerialized] public bool failed = false;
    [NonSerialized] public bool succeeded = false;
    private float timer = 0;
    private int index = 0;
    private float cutoff = .1f;
    private bool delay = false;
    private float delayTime = 1f;
    private float delayTimer = 0;
    private bool hide = false;


    public void setup(bool start, int color, bool startup)
    {
        go = start;
        colorIndex = color;
        fishHolder.sprite = pinkSprites[0];
        index = 1;
        gameObject.SetActive(startup);
    }

    public void resetBubble()
    {
        go = false;
        colorIndex = -1;
        failed = false;
        succeeded = false;
        timer = 0;
        index = 0;
        cutoff = .1f;
        delay = false;
        delayTime = 1f;
        delayTimer = 0;
        hide = false;
    }

    public void Update()
    {
        if (go)
        {
            timer += Time.deltaTime;
            if (timer >= cutoff)
            {
                switch (colorIndex)
                {
                    case 0:
                        fishHolder.sprite = pinkSprites[index];
                        break;
                    case 1:
                        fishHolder.sprite = yellowSprites[index];
                        break;
                    case 2:
                        fishHolder.sprite = orangeSprites[index];
                        break;
                }

                for (int i = 0; i < hitBoxes.Length; i++)
                {
                    hitBoxes[i].SetActive(false);
                }
                hitBoxes[index].SetActive(true);

                index++;
                if (index == pinkSprites.Length)
                {
                    go = false;
                    delay = true;
                    index -= 2;
                    //failedFunc();
                }
                timer = 0;
            }
        }
        if (delay)
        {
            delayTimer += Time.deltaTime;

            if (delayTimer >= delayTime)
            {
                delayTimer = 0;
                delay = false;
                timer = 0;
                hide = true;
            }
        }
        if (hide)
        {
            timer += Time.deltaTime;
            if (timer >= cutoff)
            {
                switch (colorIndex)
                {
                    case 0:
                        fishHolder.sprite = pinkSprites[index];
                        break;
                    case 1:
                        fishHolder.sprite = yellowSprites[index];
                        break;
                    case 2:
                        fishHolder.sprite = orangeSprites[index];
                        break;
                }

                for (int i = 0; i < hitBoxes.Length; i++)
                {
                    hitBoxes[i].SetActive(false);
                }
                hitBoxes[index].SetActive(true);

                index--;
                if (index == 0)
                {
                    hide = false;
                    failed = true;
                    failedFunc();
                    //failedFunc();
                }
                timer = 0;
            }
        }
    }

    public void failedFunc()
    {
        if (failed)
        {
            gameObject.SetActive(false);
        }
    }
}
