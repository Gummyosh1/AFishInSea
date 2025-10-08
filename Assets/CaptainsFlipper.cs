using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptainsFlipper : MonoBehaviour
{
    public GameObject backArrow;
    public void clicked()
    {
        backArrow.SetActive(false);
    }

    public void clickedBack()
    {
        backArrow.SetActive(true);
    }
}
