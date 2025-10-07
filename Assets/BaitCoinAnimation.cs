using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class BaitCoinAnimation : MonoBehaviour
{
    public Sprite[] frames;
    public Image baitCoinSprite;
    private float timer = 0;
    private float cutoff = 1f/12f;
    private bool animateOn = false;
    private int index = 0;

    public void Animate(){
        index = 0;
        baitCoinSprite.sprite = frames[index];
        timer = 0;
        animateOn = true;
    }

    public void Awake(){
        Animate();
    }

    public void stopAnimation(){
        animateOn = false;  
    }

    public void Update(){
        if (animateOn){
            timer += Time.deltaTime;
            if (timer >= cutoff){
                baitCoinSprite.sprite = frames[index];
                timer = 0;
                index++;
                if (index == 38){
                    animateOn = false;
                    index = 0;
                    baitCoinSprite.sprite = frames[0];
                    
                }
            }
        }
    }
}
