using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeStorage : MonoBehaviour
{
    public string inputText;

    public GameObject reactionGroup;
    public TMP_Text reactionTextBox;

    public void GrabFromTimeField(string input){
        inputText = input;
    }
}
