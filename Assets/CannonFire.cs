using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class CannonFire : MonoBehaviour
{
    public Sprite[] sprites;
    private float timer = .5f;
    public Image cannon;
    private int index = 0;

    public void Update(){
        timer += Time.deltaTime;
        if (timer > .1){
            index++;
            index %= sprites.Length;
            cannon.sprite = sprites[index];
            timer = 0;
        }
    }
}
