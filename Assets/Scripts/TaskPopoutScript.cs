using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Animations;

public class TaskPopoutScript : MonoBehaviour
{
    public GameObject prefab;

    private GameObject newElement;

    private Transform homescreen;

    private Transform ourTransform;

    private TMP_Text startingText;

    private Button myButton;

    private RectTransform layering;

    private TMP_InputField changerText;

    public int index = 0;

    private NewTask greyScreen;
    
    [System.NonSerialized]
    public bool editor = false;

    public void Start(){
        ourTransform = GetComponent<Transform>();
        myButton = GetComponent<Button>();
        homescreen = GameObject.Find("TaskScreen").GetComponent<Transform>();
        myButton.onClick.AddListener(() => greyOn());
        greyScreen = GameObject.Find("NewTaskHolder").GetComponent<NewTask>();
    }

    public void EditTask(){
        newElement = Instantiate(prefab);

        newElement.transform.SetParent(homescreen, false);

        layering = newElement.GetComponent<RectTransform>();
    
        layering.SetAsLastSibling();

        startingText = ourTransform.parent.GetChild(0).GetComponent<TMP_Text>();
        Debug.Log("Starting text is: " + startingText.text);

        changerText = newElement.GetComponent<RectTransform>().GetChild(0).GetComponent<TMP_InputField>();

        changerText.text = startingText.text;

        editor = true;
        
        index = transform.parent.GetSiblingIndex();
    }

    public void FinishEdit(){
        editor = false;
        Destroy(newElement);
    }

    private void greyOn(){
        greyScreen.greyScreen.SetActive(true);
    }

    
}
