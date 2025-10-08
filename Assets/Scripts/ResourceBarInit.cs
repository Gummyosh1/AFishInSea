using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBarInit : MonoBehaviour
{
    public GameObject ResourceBar;
    public bool amongus = false;

    public void OnEnable(){
        ResourceBar.SetActive(true);
    }

    public void Update(){
        if (!amongus){
            ResourceBar.SetActive(true);
            amongus = true;
        }
    }
}
