using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class MasterLava : MonoBehaviour
{
    public Image[] LavaTileImages;
    public LavaTileAnimation[] lavaTiles;
    private float timer = 0;
    private int index = 0;
    private float cutoff = 1.5f;

    public void Update()
    {
        timer += Time.deltaTime;

        if (timer >= cutoff)
        {
            if (index == 0)
            {
                cutoff = 1.5f;
            }

            for (int i = 0; i < LavaTileImages.Length; i++)
            {
                LavaTileImages[i].sprite = lavaTiles[i].lavaTiles[index];
            }
            index++;
            index %= 4;
            
            if (index == 0)
            {
                cutoff = Random.Range(3f, 7f);

            }
            timer = 0;
        }
    }
}
