using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Image = UnityEngine.UI.Image;

public class OceanAnimation : MonoBehaviour
{
    public GameProgress gameProgress;
    public Image[] tiles;
    public Sprite[] oceanSprites;
    public GameObject[] oceanAnimations;


    private int index = 1;
    private float timer = 0;
    private float cutoff = 1f;


    public void updateOcean(int ocean)
    {
        for (int n = 0; n < 4; n++)
        {
            oceanAnimations[n].SetActive(false);
        }
        oceanAnimations[ocean].SetActive(true);
    }

    public void Update()
    {
        if (gameObject.activeSelf)
        {
            timer += Time.deltaTime;

            if (timer >= cutoff)
            {
                for (int i = 0; i < tiles.Length; i++)
                {
                    tiles[i].sprite = oceanSprites[index];
                }
                timer = 0;
                index++;
                index %= oceanSprites.Length;
            }
        }
        
    }
}
