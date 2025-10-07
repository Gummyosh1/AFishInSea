using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperOfDev : MonoBehaviour
{
    public GameObject[] things;

    public void flipper()
    {
        if (things[3].activeSelf)
        {
            things[3].SetActive(false);
        }
        else
        {
            things[0].SetActive(false);
            things[1].SetActive(false);
            things[2].SetActive(false);
            things[3].SetActive(true);
        }
    }
}
