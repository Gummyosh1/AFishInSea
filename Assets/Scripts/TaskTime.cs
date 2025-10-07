using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TaskTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        getTime();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getTime(){
        DateTime theTime = DateTime.Now;
        string date = theTime.ToString("yyyy-MM-dd");
        string time = theTime.ToString("HH:mm:ss");
        string datetime = theTime.ToString("yyyy-MM-dd\\THH:mm:ss");

        Debug.Log(datetime);
    }
}
