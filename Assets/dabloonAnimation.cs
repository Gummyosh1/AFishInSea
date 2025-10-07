using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class dabloonAnimation : MonoBehaviour
{
    public Sprite[] dabloons;
    public UnityEngine.UI.Image dabloonImage;
    private int index = 0;
    private float timer = 1;

    public void rotateCoin(){
        dabloonImage.sprite = dabloons[index];
        index++;
        index %= dabloons.Length;
    }

    public void Update(){
        timer += Time.deltaTime;
        if (timer > .25){
            rotateCoin();
            timer = 0;
        }
    }
}
