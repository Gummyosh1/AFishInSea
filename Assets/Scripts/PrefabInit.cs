using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PrefabInit : MonoBehaviour
{
    private NewTask newTask;
    private TaskPopoutScript taskPopoutScript;

    private HomeTaskStorage homeTaskStorage;

    private GameObject textBoxObject;

    private GameObject[] tasks;

    private Transform Parent;

    private Button myButton;

    private TMP_Text texterStorage;

    private TMP_Text enteredText;

    public void Start()
    {
        newTask = GameObject.Find("NewTaskHolder").GetComponent<NewTask>();
        tasks = GameObject.FindGameObjectsWithTag("TaskHolder");
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(() => greyOff());
        myButton.onClick.AddListener(() => popOutKill());
        myButton.onClick.AddListener(() => SaveEditedText());
        textBoxObject = GameObject.Find("TextEntered");
        enteredText = textBoxObject.GetComponent<TMP_Text>();
        homeTaskStorage = GameObject.Find("Panel").GetComponent<HomeTaskStorage>();
    }

    private void greyOff(){
        newTask.greyScreen.SetActive(false);
    }

    private void popOutKill(){
        foreach (GameObject i in tasks)
        {
            taskPopoutScript = i.GetComponent<TaskPopoutScript>();
            if (taskPopoutScript.editor == true)
            {
                Debug.Log("WE ARE OPERATING ON " + taskPopoutScript.index);
                Parent = i.GetComponent<Transform>();
                Parent = Parent.parent.GetChild(0);
                texterStorage = Parent.GetComponent<TMP_Text>();

                texterStorage.text = enteredText.text;
                if (enteredText.text.Length > 100)
                {
                    texterStorage.text = enteredText.text.Substring(0, 100);
                }

                homeTaskStorage.taskText[taskPopoutScript.index] = enteredText.text;
                if (enteredText.text.Length > 100)
                {
                    homeTaskStorage.taskText[taskPopoutScript.index] = enteredText.text.Substring(0, 100);
                }

                taskPopoutScript.FinishEdit();
                SaveEditedText();
                return;
            }   
        }
    }

    public void trashTask()
    {
        foreach (GameObject i in tasks)
        {
            taskPopoutScript = i.GetComponent<TaskPopoutScript>();
            if (taskPopoutScript.editor == true)
            {
                Parent = i.GetComponent<Transform>();
                Parent = Parent.parent;

                homeTaskStorage.taskText[taskPopoutScript.index] = "";
                //0, 1, 2, 3, 4, 5, 6
                for (int j = taskPopoutScript.index; j < homeTaskStorage.taskText.Length - 1; j++)
                {
                    homeTaskStorage.taskText[j] = homeTaskStorage.taskText[j + 1];

                    if (j == homeTaskStorage.taskText.Length - 2)
                    {
                        homeTaskStorage.taskText[j + 1] = "";
                    }
                }

                Destroy(Parent.gameObject);
                taskPopoutScript.FinishEdit();
                SaveEditedText();
                greyOff();
                return;
            }   
        }
    }

    private void SaveEditedText()
    {
        homeTaskStorage.SaveTaskData();
    }

}
