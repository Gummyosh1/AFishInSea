using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Image = UnityEngine.UI.Image;

public class LeftSensor : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public MiniGameTwo miniGameTwo;
    public Image fishImage;
    public Sprite[] sprites;
    [NonSerialized] public int direction = 2;

    public void OnPointerDown(PointerEventData eventData)
    {
        //miniGameTwo.blockMove = false;
        if (direction == 0)
        {
            miniGameTwo.movingUp = true;
            fishImage.sprite = sprites[0];
        }
        else if (direction == 1)
        {
            miniGameTwo.movingDown = true;
            fishImage.sprite = sprites[1];
        }
        else if (direction == 2)
        {
            miniGameTwo.movingLeft = true;
            fishImage.sprite = sprites[2];
        }
        else if (direction == 3)
        {
            miniGameTwo.movingRight = true;
            fishImage.sprite = sprites[3];
        }
        //miniGameTwo.blockMove = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        miniGameTwo.movingUp = false;
        miniGameTwo.movingDown = false;
        miniGameTwo.movingLeft = false;
        miniGameTwo.movingRight = false;
    }
    
    public void manualUp()
    {
        miniGameTwo.movingUp = false;
        miniGameTwo.movingDown = false;
        miniGameTwo.movingLeft = false;
        miniGameTwo.movingRight = false;
    }
}
