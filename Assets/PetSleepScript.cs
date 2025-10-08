using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PetSleepScript : MonoBehaviour
{
    public GameObject[] Zs;
    private float timer = 0;
    private int index = 0;

    public void Update(){
        timer += Time.deltaTime;
        if (timer >= 1){
            if (index < 3){
                Zs[index].SetActive(true);
                index++;
            }
            else{
                Zs[0].SetActive(false);
                Zs[1].SetActive(false);
                Zs[2].SetActive(false);
                index = 0;
            }

            timer = 0;
        }
    }
}
