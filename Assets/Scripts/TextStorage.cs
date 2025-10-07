using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextStorage : MonoBehaviour
{
    // Start is called before the first frame update
    public string inputText;

    public GameObject reactionGroup;
    public TMP_Text reactionTextBox;

    public void GrabFromInputField(string input){
        inputText = input;
    }

}
