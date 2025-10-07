using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;   

public class SeaAnimator : MonoBehaviour
{
    public Image spriteHolder;
    public Sprite[] sprites;
    private float timer = 0;
    private int index = 1;
    private float cutoff = 1;

    public void Update(){
        timer += Time.deltaTime;
        if (timer >= cutoff){
            spriteHolder.sprite = sprites[index];
            index++;
            index %= sprites.Length;
            timer = 0;
        }
    }
}
