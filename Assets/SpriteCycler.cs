using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Image = UnityEngine.UI.Image;   

public class SpriteCycler : MonoBehaviour
{
    private float timer = 0;
    private int index = 0;
    public int spriteOffset = 0;
    public TMP_Text quantity;
    public Sprite[] sprites;
    public Image display;
    public BaitBuy baitBuy;

    public void Init(){
        quantity.text = baitBuy.baitTotals[spriteOffset*3].ToString();
    }

    public void Update(){
       timer += Time.deltaTime;
       if (timer >= 3){
            index++;
            index %= sprites.Length;
            display.sprite = sprites[index];
            timer = 0;
            quantity.text = baitBuy.baitTotals[(spriteOffset*3) + index].ToString();
       } 
    }
}
