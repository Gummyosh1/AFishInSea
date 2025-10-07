using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class NewTask : MonoBehaviour
{
    public GameObject prefab; // Reference to the prefab to instantiate

    public GameObject taskSetupPrefab;

    public GameObject greyScreen;
    public Transform layoutGroup; // Reference to the Vertical Layout Group's transform

    public RectTransform NewTaskButton;

    public Transform homescreen;

    public HomeTaskStorage homeTaskStorage;

    Vector3 delta = new Vector3(0,82.5f,0);

    public void AddPrefabToLayoutGroup()
    {
        // Instantiate the prefab
        GameObject newElement = Instantiate(prefab);

        // Set the parent to the layout group and reset the local position and scale
        newElement.transform.SetParent(layoutGroup, false);

        // Optionally, adjust the new element (e.g., set text or other properties)
        // For example, if it's a button with text:
        // newElement.GetComponentInChildren<Text>().text = "New Button";

        // Note: Setting the second parameter of SetParent to false ensures the instantiated
        // element keeps its local position, rotation, and scale. 

        RectTransform rt = layoutGroup.GetComponent (typeof (RectTransform)) as RectTransform;
        rt.sizeDelta += new Vector2 (0, 165);

        rt.position -= delta;

        NewTaskButton.SetAsLastSibling();

    }

    public void SetupWindow(){
        Debug.Log("We are cooked");
        if (homeTaskStorage.taskText[24] == "")
        {
            Debug.Log("Its true");
            GameObject newEditor = Instantiate(taskSetupPrefab);

            newEditor.transform.SetParent(homescreen, false);

            Transform layering = newEditor.GetComponent<RectTransform>();

            layering.SetAsLastSibling();

            greyOn();
        }
    }

    public GameObject AddPrefabToLayoutGroupReturn()
    {
        // Instantiate the prefab
        GameObject newElement = Instantiate(prefab);

        // Set the parent to the layout group and reset the local position and scale
        newElement.transform.SetParent(layoutGroup, false);

        // Optionally, adjust the new element (e.g., set text or other properties)
        // For example, if it's a button with text:
        // newElement.GetComponentInChildren<Text>().text = "New Button";

        // Note: Setting the second parameter of SetParent to false ensures the instantiated
        // element keeps its local position, rotation, and scale. 

        RectTransform rt = layoutGroup.GetComponent (typeof (RectTransform)) as RectTransform;
        rt.sizeDelta += new Vector2 (0, 165);

        rt.position -= delta;

        NewTaskButton.SetAsLastSibling();

        return newElement;
    }
    private void greyOn(){
        greyScreen.SetActive(true);
    }

    private void greyOffTap(){
        greyScreen.SetActive(false);
        
    }

}
