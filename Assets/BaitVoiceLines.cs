using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitVoiceLines : MonoBehaviour
{
    [NonSerialized] public string[] baitPhrases;

    public void Start(){
        init();
    }
    public void init(){
        baitPhrases[0] = "Here's some bait for the hard work you're putting in!";
    }
}
