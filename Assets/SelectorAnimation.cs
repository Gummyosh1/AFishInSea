using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class SelectorAnimation : MonoBehaviour
{
    private float timer = 1;
    private Image image;
    public Sprite[] selectionSprites;
    private int index = 0;

    void Start(){
        image = gameObject.GetComponent<Image>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > .2){
            image.sprite = selectionSprites[index];
            index++;
            index %= selectionSprites.Length;
            timer = 0;
            
        }
    }
}
