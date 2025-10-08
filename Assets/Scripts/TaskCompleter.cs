using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class TaskCompleter : MonoBehaviour
{

    public HomeTaskStorage homeTaskStorage;
    public SailingTracker sailingTracker;
    public StreakTracker streakTracker;
    public GameProgress gameProgress;
    public BattlePass battlePass;
    public CaptainMessages captainMessages;
    public DailyTaskGift dailyTaskGift;
    public Color movementColor;



    public ScrollRect scrollRect;     // The ScrollRect component
    public RectTransform content;     // The Content of the ScrollRect
    public RectTransform viewport;    // The viewport RectTransform
    private Mover mover;
    public CustomButton myCustomButton;
    private float enableTimer = 0;
    private bool enabler = false;
    private float moveCutoff = .3f;


    public void Start()
    {
        homeTaskStorage = GameObject.Find("Panel").GetComponent<HomeTaskStorage>();
        scrollRect = GameObject.Find("Scroll").GetComponent<ScrollRect>();
        content = (RectTransform)homeTaskStorage.gameObject.transform;
        viewport = (RectTransform)scrollRect.gameObject.transform;
        sailingTracker = GameObject.Find("ShipHomeScreen").GetComponent<SailingTracker>();
        streakTracker = GameObject.Find("GameHandles").GetComponent<StreakTracker>();
        gameProgress = GameObject.Find("GameHandles").GetComponent<GameProgress>();
        battlePass = GameObject.Find("GameHandles").GetComponent<BattlePass>();
        captainMessages = GameObject.Find("GameHandles").GetComponent<CaptainMessages>();
        dailyTaskGift = GameObject.Find("DailyGift").GetComponent<DailyTaskGift>();
        mover = gameObject.GetComponent<Mover>();
        myCustomButton = GetComponent<CustomButton>();
    }


    public void Update()
    {
        if (myCustomButton.IsButtonPressed())
        {
            //Debug.Log("I AM HERE");
            enableTimer += Time.deltaTime;
            if (enableTimer >= moveCutoff)
            {
                mover.enabled = true;
                enabler = true;
                enableTimer = 0;
            }
        }

        if (!myCustomButton.IsButtonPressed() && enabler == false)
        {
            enableTimer = 0;

            mover.enabled = false;
        }
    }

    public void moveOn()
    {
        mover.enabled = true;
        this.GetComponent<Image>().color = movementColor;
    }

    public void moveOff()
    {
        mover.enabled = false;
    }


    public void moveUp()
    {
        int index = transform.GetSiblingIndex();
        if (index != 0)
        {
            string holder = homeTaskStorage.taskText[transform.GetSiblingIndex()];
            homeTaskStorage.taskText[transform.GetSiblingIndex()] = homeTaskStorage.taskText[transform.GetSiblingIndex() - 1];
            homeTaskStorage.taskText[transform.GetSiblingIndex() - 1] = holder;
            homeTaskStorage.SaveTaskData();

            transform.SetSiblingIndex(index - 1);

            Center();

            if (index - 1 == 0)
            {
                scrollRect.verticalNormalizedPosition = 1f;
            }
        }
    }

    public void moveDown()
    {
        int index = transform.GetSiblingIndex();
        int endIndexForTasks = transform.parent.childCount - 2;
        if (index != endIndexForTasks)
        {
            string holder = homeTaskStorage.taskText[transform.GetSiblingIndex()];
            homeTaskStorage.taskText[transform.GetSiblingIndex()] = homeTaskStorage.taskText[transform.GetSiblingIndex() + 1];
            homeTaskStorage.taskText[transform.GetSiblingIndex() + 1] = holder;
            homeTaskStorage.SaveTaskData();

            transform.SetSiblingIndex(index + 1);

            Center();
        }
    }

    public void killTask()
    {
        sailingTracker.loadTasksCompletedToday();
        //IF WE HAVEN'T DONE 6 OR MORE CHALLENGES TODAY, WE GET TO PROGRESS!
        Debug.Log("Check 1 is " + (sailingTracker.tasksCompletedToday < 3));
        Debug.Log("Check 2 is " + (sailingTracker.lastTaskCompleteDate.Date == DateTime.Now.Date));
        if (sailingTracker.tasksCompletedToday < 3 && sailingTracker.lastTaskCompleteDate.Date == DateTime.Now.Date)
        {

            sailingTracker.tasksCompletedToday++;
            dailyTaskGift.baitAdd(); //ADDS THE BAIT COINS TO STORAGE
            streakTracker.addStreak(); // ADDS THE STREAK IF APPLICABLE
            streakTracker.InitializeStreakBar(); //SETS UP THE STREAK BAR THING

            gameProgress.coinsGainedPopUp(); //UI FOR COIN GAIN


            //MANAGES THE LIST AND DELETES COMPLETED TASK
            int index = transform.GetSiblingIndex();
            for (int i = index; i < homeTaskStorage.taskText.Length - 2; i++)
            {
                homeTaskStorage.taskText[i] = homeTaskStorage.taskText[i + 1];
            }
            homeTaskStorage.taskText[homeTaskStorage.taskText.Length - 1] = "";
            homeTaskStorage.SaveTaskData();
            Destroy(gameObject);

            //THIS SECTION IS FOR THE DICE ROLLER
            if (sailingTracker.tasksCompletedToday == 1)
            {
                //seagull.spawnSeagull();
                dailyTaskGift.ProgressOne();
            }
            if (sailingTracker.tasksCompletedToday == 2)
            {
                //seagull.spawnSeagull();
                dailyTaskGift.ProgressTwo();
            }
            if (sailingTracker.tasksCompletedToday == 3)
            {
                //seagull.spawnSeagull();
                dailyTaskGift.ProgressThree();
            }
        }
        else if (sailingTracker.lastTaskCompleteDate.Date != DateTime.Now.Date)
        {
            Debug.Log("Check 3 is true");
            sailingTracker.tasksCompletedToday = 1;

            dailyTaskGift.baitAdd();
            streakTracker.addStreak();
            streakTracker.InitializeStreakBar();

            gameProgress.coinsGainedPopUp(); //UI FOR COIN GAIN

            //LIST MANAGEMENT
            int index = transform.GetSiblingIndex();
            Debug.Log("Sibling index is " + index);
            for (int i = index; i < homeTaskStorage.taskText.Length - 2; i++)
            {
                homeTaskStorage.taskText[i] = homeTaskStorage.taskText[i + 1];
                Debug.Log(homeTaskStorage.taskText[i]);
                Debug.Log("Val at index of 0 is " + homeTaskStorage.taskText[0]);
                Debug.Log("Val at index of 1 is " + homeTaskStorage.taskText[1]);
                Debug.Log("Val at index of 2 is " + homeTaskStorage.taskText[2]);
                Debug.Log("Val at index of 3 is " + homeTaskStorage.taskText[3]);
            }
            homeTaskStorage.taskText[homeTaskStorage.taskText.Length - 1] = "";
            homeTaskStorage.SaveTaskData();
            Destroy(gameObject);

            //DICE ROLLER
            dailyTaskGift.ProgressOne();
            sailingTracker.claimed = false;
        }
        //IF WE'VE ALREADY DONE 6, WE DON'T GET ANY PROGRESSION, BUT THE TASK IS STILL DELETED!
        else
        {
            gameProgress.tooManyTasksPopUp(); //UI FOR COIN GAIN
            streakTracker.addStreak();

            int index = transform.GetSiblingIndex();
            for (int i = index; i < homeTaskStorage.taskText.Length - 2; i++)
            {
                homeTaskStorage.taskText[i] = homeTaskStorage.taskText[i + 1];
            }
            homeTaskStorage.taskText[homeTaskStorage.taskText.Length - 1] = "";
            homeTaskStorage.SaveTaskData();
            //taskCapPopUp.SetActive(true);
            Destroy(gameObject);
        }

        if (gameProgress.currentIslandIndex == 0)
        {
            captainMessages.taskCompleted = true;
            captainMessages.tutorialAdvancement();
        }


        gameProgress.questTaskCompleterBuffer();
        sailingTracker.saveTasksCompletedToday();
        sailingTracker.lastTaskCompleteDate = DateTime.Now.Date;
    }


    public void Center()
    {
        Canvas.ForceUpdateCanvases();

        // Get world corners of the content
        Vector3[] contentCorners = new Vector3[4];
        content.GetWorldCorners(contentCorners);
        float topY = contentCorners[1].y;    // Top-left corner
        float bottomY = contentCorners[0].y; // Bottom-left corner

        float contentHeight = topY - bottomY;
        float itemY = gameObject.GetComponent<RectTransform>().position.y;

        // Distance of the item from the bottom of the content
        float itemOffset = itemY - bottomY;

        // Get normalized scroll position
        float normalizedPos = itemOffset / contentHeight;

        scrollRect.verticalNormalizedPosition = Mathf.Clamp01(normalizedPos);
    }


}

