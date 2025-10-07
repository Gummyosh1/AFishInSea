using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBarDown : MonoBehaviour
{
    public GameObject ResourceBar;

    public void OnEnable(){
        ResourceBar.SetActive(false);
    }
}
