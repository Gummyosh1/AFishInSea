using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class PrefabSetup : MonoBehaviour
{
    private NewTask newTask;
    private GameObject greyScreen;
    public GameObject upArrow;
    public GameObject downArrow;
    private UnityEngine.UI.Button myButton;
    private GameObject newTaskObject;
    private Transform Parent;
    private HomeTaskStorage homeTaskStorage;
    private TMP_Text childText;
    private TMP_InputField parentText;
    private CaptainMessages captainMessages;
    private GameProgress gameProgress;
    public Image arrow;
    public Color red;
    public Color green;
    

    private bool top = false;

    // Start is called before the first frame update
    public void Start()
    {
        newTask = GameObject.Find("NewTaskHolder").GetComponent<NewTask>();
        greyScreen = GameObject.Find("GreyOut");
        Parent = GameObject.Find("Panel").GetComponent<Transform>();
        captainMessages = GameObject.Find("GameHandles").GetComponent<CaptainMessages>();
        gameProgress = GameObject.Find("GameHandles").GetComponent<GameProgress>();

        homeTaskStorage = Parent.GetComponent<HomeTaskStorage>();
        parentText = transform.parent.GetChild(0).GetComponent<TMP_InputField>();
        myButton = GetComponent<UnityEngine.UI.Button>();
        myButton.onClick.AddListener(() => greyOff());
        myButton.onClick.AddListener(() => popOutKill());
    }

    private void greyOff()
    {
        newTask.greyScreen.SetActive(false);
    }

    private void popOutKill()
    {
        if (parentText.text != "")
        {
            if (!top)
            {
                newTaskObject = newTask.AddPrefabToLayoutGroupReturn();
                newTaskObject.transform.SetParent(Parent);

                newTaskObject.transform.SetAsLastSibling();
                int index = newTaskObject.transform.GetSiblingIndex() - 1;
                newTaskObject.transform.SetSiblingIndex(index);

                childText = newTaskObject.transform.GetChild(0).GetComponent<TMP_Text>();

                childText.text = parentText.text;
                if (parentText.text.Length > 100)
                {
                    childText.text = parentText.text.Substring(0, 100);
                }

                homeTaskStorage.taskText[index] = childText.text;

                homeTaskStorage.SaveTaskData();

                /*if (gameProgress.currentIslandIndex == 0){
                    captainMessages.taskAdded = true;
                    captainMessages.tutorialAdvancement();
                }*/
            }
            else
            {
                if (homeTaskStorage.taskText[homeTaskStorage.taskText.Length - 1] == "")
                {
                    newTaskObject = newTask.AddPrefabToLayoutGroupReturn();
                    newTaskObject.transform.SetParent(Parent);

                    newTaskObject.transform.SetAsFirstSibling();
                    int index = newTaskObject.transform.GetSiblingIndex();

                    childText = newTaskObject.transform.GetChild(0).GetComponent<TMP_Text>();

                    childText.text = parentText.text;
                    if (parentText.text.Length > 100)
                    {
                        childText.text = parentText.text.Substring(0, 100);
                    }

                    for (int i = homeTaskStorage.taskText.Length - 2; i >= 0; i--)
                    {
                        string holder = homeTaskStorage.taskText[i];
                        if (holder != "")
                        {
                            homeTaskStorage.taskText[i + 1] = holder;
                        }
                    }

                    homeTaskStorage.taskText[index] = childText.text;

                    homeTaskStorage.SaveTaskData();   
                }
            }
            
        }
        Destroy(transform.parent.gameObject);
    }


    public void addToTop()
    {
        if (top)
        {
            top = false;
            downArrow.SetActive(true);
            upArrow.SetActive(false);
        }
        else
        {
            top = true;
            arrow.color = green;
            upArrow.SetActive(true);
            downArrow.SetActive(false);
        }
    }


}
