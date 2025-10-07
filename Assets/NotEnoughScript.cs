using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotEnoughScript : MonoBehaviour
{
    private float timer = 0;
    private float timer2 = 0;
    private float timeSpeed = .1f;
    private float decrementSpeed = .1f;
    private float[] startingHolders = { 0, 0, 0 };
    private float buffer = 1;
    public Image objectImage;
    public TMP_Text[] text;


    public void ActivateNotEnough()
    {
        gameObject.SetActive(true);
        if (startingHolders[0] == 0)
        {
            startingHolders[0] = objectImage.color.a;
            startingHolders[1] = text[0].color.a;
            startingHolders[2] = text[1].color.a;
        }
        //if ()
    }


    public void Update()
    {
        if (gameObject.activeSelf)
        {
            timer += Time.deltaTime;
            if (timer >= buffer)
            {
                timer2 += Time.deltaTime;
                if (timer2 >= timeSpeed)
                {
                    objectImage.color = new Color(1, 1, 1, objectImage.color.a - decrementSpeed);
                    text[0].color = new Color(text[0].color.r, text[0].color.g, text[0].color.b, text[0].color.a - decrementSpeed);
                    text[1].color = new Color(text[1].color.r, text[1].color.g, text[1].color.b, text[1].color.a - decrementSpeed);
                    timer2 = 0;
                    if (objectImage.color.a <= 0)
                    {
                        timer = 0;
                        timer2 = 0;
                        gameObject.SetActive(false);
                        objectImage.color = new Color(1, 1, 1, startingHolders[0]);
                        text[0].color = new Color(text[0].color.r, text[0].color.g, text[0].color.b, startingHolders[1]);
                        text[1].color = new Color(text[1].color.r, text[1].color.g, text[1].color.b, startingHolders[2]);
                        
                    }
                }
            }
        }
    }
}
