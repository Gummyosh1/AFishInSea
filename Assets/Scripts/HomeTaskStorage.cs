using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HomeTaskStorage : MonoBehaviour
{
    [NonSerialized] public TMP_Text textHolder;

    [NonSerialized] public string[] taskText = new string[25];

    [NonSerialized] public string jsonStorage;

    private NewTask newTask;


    public void Awake()
    {
        for (int i = 0; i < taskText.Length; i++)
        {
            taskText[i] = "";
        }
        SaveSystem.Init();
        newTask = GetComponent<NewTask>();
        Load();
    }

    public void Update(){
        /*if (Input.GetKeyDown(KeyCode.Space)){
            SaveTaskData();
        }

        if (Input.GetKeyDown(KeyCode.Alpha0)){
            Load();
        }*/
    }

    public int getTaskIndex(){
        int index = 0;
        

        return index;
    }

    public void SaveTaskData(){
        /*for (int i = 0; i < transform.childCount - 1; i++){
            textHolder = transform.GetChild(i).GetChild(0).GetComponent<TMP_Text>();
            taskText[i] = textHolder.text;
        }*/
        Debug.Log(taskText);
        ListClass listClass = new ListClass
        {
            textList = taskText
        };
        jsonStorage = JsonUtility.ToJson(listClass);  
        Debug.Log(jsonStorage);
        SaveSystem.SaveHome(jsonStorage);
    }


    public void Load(){

        string saveString = SaveSystem.LoadHome();
        if (saveString != null){
            ListClass loadedData = JsonUtility.FromJson<ListClass>(saveString);

            for (int i = 0; i < 25; i++){
                taskText[i] = loadedData.textList[i];
                if (taskText[i] != ""){
                    GameObject obj = newTask.AddPrefabToLayoutGroupReturn();
                    obj.GetComponent<Transform>().GetChild(0).GetComponent<TMP_Text>().text = taskText[i];
                }
            }
        }
    }
    
}

public class ListClass {
    public string[] textList = new string[25];
}
