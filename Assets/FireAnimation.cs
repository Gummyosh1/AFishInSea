using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class FireAnimation : MonoBehaviour
{
    public Image campfire;
    public Sprite[] sprites;

    private float timer = 0;
    private float cutoff = .5f;

    private int index = 0;


    public void Update()
    {
        timer += Time.deltaTime;

        if (timer >= cutoff)
        {
            campfire.sprite = sprites[index];
            index++;
            index %= sprites.Length;
            timer = 0;
        }
    }
}
