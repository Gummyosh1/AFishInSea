using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Mover : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public ScrollRect scrollRect;
    private Vector3 transformers;
    private Camera cameraUI;
    public Transform topThird;
    public Transform bottomThird;
    public Transform[] tasks;
    public Transform taskParent;
    public HomeTaskStorage homeTaskStorage;

    private bool isPointerDown = false;
    private bool isDragging = false;
    private float pointerDownTimer = 0f;
    private float holdTime = .05f;
    private float scrollTimer = 0;
    private float scrollCutoff = .1f;
    

    private float scrollSpeed = .05f;
    private int originalIndex = 0;

    public void Start()
    {
        cameraUI = GameObject.Find("UICamera").GetComponent<Camera>();
        scrollRect = GameObject.Find("Scroll").GetComponent<ScrollRect>();
        topThird = GameObject.Find("TopThird").transform;
        bottomThird = GameObject.Find("BottomThird").transform;
        taskParent = GameObject.Find("Panel").GetComponent<Transform>();
        homeTaskStorage = GameObject.Find("Panel").GetComponent<HomeTaskStorage>();
    }

    public void OnEnable()
    {
        isPointerDown = true;
        pointerDownTimer = 0f;
        originalIndex = gameObject.transform.GetSiblingIndex();


        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Get the first touch

            transformers = touch.position;
        }
        else if (Input.GetMouseButton(0)) // Left click
        {
            transformers = Input.mousePosition;
        }
        //transformers = gameObject.transform.position;
    }


    void Update()
    {
        if (Input.touchCount == 0)
        {
            //doneWithIt(); //TODO COMMENT THIS OUT WHEN YOU WANT TO TEST ON PC
        }
        if (!Input.GetMouseButton(0)) // Left click
        {
            doneWithIt();
        }


        if (isPointerDown && !isDragging)
        {
            pointerDownTimer += Time.deltaTime;
            if (pointerDownTimer >= holdTime)
            {
                isDragging = true;
                if (scrollRect != null && scrollRect.enabled)
                {
                    scrollRect.enabled = false;
                }
                gameObject.transform.SetAsLastSibling();
            }
        }
        if (isDragging)
        {
            scrollTimer += Time.deltaTime;
            if (scrollTimer >= scrollCutoff)
            {
                if (gameObject.transform.position.y >= topThird.position.y)
                {
                    //Debug.Log("We are inside UP");
                    moveScrollUp();
                }
                else if (gameObject.transform.position.y <= bottomThird.position.y)
                {
                    //Debug.Log("We are inside DOWN");
                    moveScrollDown();
                }
                scrollTimer = 0;
            }
        }
        if (isDragging)
        {
            Vector3 mousePos;
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0); // Get the first touch

                mousePos = cameraUI.ScreenToWorldPoint(touch.position);
            }
            else if (Input.GetMouseButton(0)) // Left click
            {
                mousePos = cameraUI.ScreenToWorldPoint(Input.mousePosition);
            }
            else
            {
                mousePos = cameraUI.ScreenToWorldPoint(Input.mousePosition);
            }
            
            mousePos = new Vector3(mousePos.x, mousePos.y, 1040);
            gameObject.transform.position = mousePos;

            //RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out mousePos);
            //rectTransform.anchoredPosition = mousePos - offset;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
    public void doneWithIt()
    {
        int searchLength = taskParent.childCount;

        isPointerDown = false;
        pointerDownTimer = 0f;
        scrollTimer = 0;

        if (isDragging)
        {
            for (int i = 0; i < searchLength; i++)
            {
                //Debug.Log("Our y is " + gameObject.transform.position.y);
                //Debug.Log("Task " + i + " is at " + taskParent.GetChild(i).transform.position.y);
                if (gameObject.transform.position.y > taskParent.GetChild(i).transform.position.y)
                {
                    if (i == searchLength - 1)
                    {
                        i -= 1;
                    }
                    gameObject.transform.SetSiblingIndex(i);


                    //TEXT UPDATER!
                    string holder = homeTaskStorage.taskText[originalIndex];

                    if (originalIndex < i)
                    {
                        // Move down: shift everything up
                        for (int j = originalIndex; j < i; j++)
                        {
                            homeTaskStorage.taskText[j] = homeTaskStorage.taskText[j + 1];
                        }
                    }
                    else if (originalIndex > i)
                    {
                        // Move up: shift everything down
                        for (int j = originalIndex; j > i; j--)
                        {
                            homeTaskStorage.taskText[j] = homeTaskStorage.taskText[j - 1];
                        }
                    }

                    homeTaskStorage.taskText[i] = holder;
                    homeTaskStorage.SaveTaskData();

                    //Debug.Log("Set to sibling " + i);
                    //Debug.Log("____________________________________");
                    break;
                }
                Debug.Log("SearchLength = " + searchLength + " i is " + i);
                if (searchLength - 1 == i)
                {
                    //Debug.Log("RESETTING TO " + originalIndex);
                    //Debug.Log("____________________________________");
                    gameObject.transform.SetSiblingIndex(searchLength - 2);
                    string holder = homeTaskStorage.taskText[originalIndex];

                        // Move down: shift everything up
                    for (int j = originalIndex; j < i; j++)
                    {
                        homeTaskStorage.taskText[j] = homeTaskStorage.taskText[j + 1];
                    }

                    homeTaskStorage.taskText[i - 1] = holder;
                    homeTaskStorage.SaveTaskData();
                }
            }
            isDragging = false;
            scrollRect.enabled = true;
            originalIndex = 0;
            enabled = false;
            GetComponent<Image>().color = Color.white;
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        /*Debug.Log("THE POINTER IS UP! ________________________________");

        int searchLength = taskParent.childCount;

        isPointerDown = false;
        pointerDownTimer = 0f;
        scrollTimer = 0;

        if (isDragging)
        {
            for (int i = 0; i < searchLength; i++)
            {
                //Debug.Log("Our y is " + gameObject.transform.position.y);
                //Debug.Log("Task " + i + " is at " + taskParent.GetChild(i).transform.position.y);
                if (gameObject.transform.position.y > taskParent.GetChild(i).transform.position.y)
                {
                    if (i == searchLength - 1)
                    {
                        i -= 1;
                    }
                    gameObject.transform.SetSiblingIndex(i);


                    //TEXT UPDATER!
                    string holder = homeTaskStorage.taskText[originalIndex];

                    if (originalIndex < i)
                    {
                        // Move down: shift everything up
                        for (int j = originalIndex; j < i; j++)
                        {
                            homeTaskStorage.taskText[j] = homeTaskStorage.taskText[j + 1];
                        }
                    }
                    else if (originalIndex > i)
                    {
                        // Move up: shift everything down
                        for (int j = originalIndex; j > i; j--)
                        {
                            homeTaskStorage.taskText[j] = homeTaskStorage.taskText[j - 1];
                        }
                    }

                    homeTaskStorage.taskText[i] = holder;
                    homeTaskStorage.SaveTaskData();

                    //Debug.Log("Set to sibling " + i);
                    //Debug.Log("____________________________________");
                    break;
                }
                Debug.Log("SearchLength = " + searchLength + " i is " + i);
                if (searchLength - 1 == i)
                {
                    //Debug.Log("RESETTING TO " + originalIndex);
                    //Debug.Log("____________________________________");
                    gameObject.transform.SetSiblingIndex(searchLength - 2);
                }
            }
            isDragging = false;
            scrollRect.enabled = true;
            originalIndex = 0;
            enabled = false;
            GetComponent<Image>().color = Color.white;
        }*/

    }

    public void OnDrag(PointerEventData eventData)
    {
        /*if (isDragging)
        {
            Vector3 mousePos;
            mousePos = cameraUI.ScreenToWorldPoint(eventData.position);
            mousePos = new Vector3(mousePos.x, mousePos.y, 1040);
            gameObject.transform.position = mousePos;

            //RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out mousePos);
            //rectTransform.anchoredPosition = mousePos - offset;
        }*/
    }


    public void moveScrollUp()
    {
        if (scrollRect.verticalNormalizedPosition + scrollSpeed <= 1)
        {
            scrollRect.verticalNormalizedPosition += scrollSpeed;
        }
        else
        {
            scrollRect.verticalNormalizedPosition = 1;
        }
    }

    public void moveScrollDown()
    {
        if (scrollRect.verticalNormalizedPosition - scrollSpeed >= 0)
        {
            scrollRect.verticalNormalizedPosition -= scrollSpeed;
        }
        else
        {
            scrollRect.verticalNormalizedPosition = 0;
        }
    }
}

