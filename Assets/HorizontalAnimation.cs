using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class HorizontalAnimation : MonoBehaviour
{
    private float timer = 0;

    private float cutoff = 1f;

    public Image bubbleTile;
    public Sprite[] sprites;
    private int index = 0;

    public void Update()
    {
        timer += Time.deltaTime;
        if (timer > cutoff){
            index++;
            index %= sprites.Length;
            bubbleTile.sprite = sprites[index];
            timer = 0;
        }
    }
}
