using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class BirdWobble : MonoBehaviour
{
    public Sprite[] birdWobble1;
    public Sprite[] birdWobble2;
    public Sprite[] birdWobble3;
    public Sprite[] birdWobble4;
    public Sprite[][] birdWobbleHolder = new Sprite[4][];
    public Image birdImage;
    public EquipmentScript equipmentScript;
    private int index = 0;
    private float timer = 0;
    private float cutoff = .5f;
    private bool[] delay = {false, false, false, false, false};
    private bool init = false;

    public void Start()
    {
        if (!init)
        {
            birdWobbleHolder[0] = birdWobble1;
            birdWobbleHolder[1] = birdWobble2;
            birdWobbleHolder[2] = birdWobble3;
            birdWobbleHolder[3] = birdWobble4;
        }
        init = true;
    }

    public void Update()
    {
        timer += Time.deltaTime;

        if (timer >= cutoff)
        {
            if (equipmentScript.equippedGoblet != -1)
            {
                bool pass = true;
                for (int i = 0; i < delay.Length; i++)
                {
                    if (!delay[i])
                    {
                        delay[i] = true;
                        timer = 0;
                        pass = false;
                        break;
                    }
                }
                if (pass)
                {
                    birdImage.sprite = birdWobbleHolder[equipmentScript.equippedGoblet][index];
                    index++;
                    if (index == birdWobbleHolder[equipmentScript.equippedGoblet].Length)
                    {
                        index = 0;
                        for (int i = 0; i < delay.Length; i++)
                        {
                            delay[i] = false;
                        }
                    }
                    timer = 0;
                }
            }
        }
    }
}
