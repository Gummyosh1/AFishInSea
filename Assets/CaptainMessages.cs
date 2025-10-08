using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CaptainMessages : MonoBehaviour
{
    [NonSerialized] public string tutorial0 = "Welcome to the ship matey! I'm glad ya joined us!";
    [NonSerialized] public string tutorial1 = "Me name is Cobal Longhorn, Captain of The Fisherman Pirates";
    [NonSerialized] public string tutorial2 = "After the Great Wave, the world got split into a bunch o' islands.";
    [NonSerialized] public string tutorial3 = "You'd think life could never get better for us plunderin' folk!";
    [NonSerialized] public string tutorial4 = "But alas, the entire world has been ravaged by the sea.";
    [NonSerialized] public string tutorial5 = "Everyone is barely surviving, meaning there's no gold to be plundered!";
    [NonSerialized] public string tutorial6 = "We must catch fish and deliver them to people in need!";
    [NonSerialized] public string tutorial7 = "If this world doesn't have loot for the taken, we got nothin' to do!";
    [NonSerialized] public string tutorial8 = "Let's get these scallywags back on their feet so we can keep plunderin'";
    [NonSerialized] public string tutorial9 = "Lemme show ya the ropes of sailing!";
    [NonSerialized] public string tutorial10 = "Here's your Task Page!";
    [NonSerialized] public string tutorial11 = "Whenever you've got a task to do in your world, write it down here!";
    [NonSerialized] public string tutorial12 = "Add in an example task now!";
    [NonSerialized] public string tutorial13 = "Completin' yer tasks will help us sail onwards to the next island.";
    [NonSerialized] public string tutorial14 = "When you complete yer tasks, we get to sail for an hour!";
    [NonSerialized] public string tutorial15 = "Stay disciplined matey!!";
    [NonSerialized] public string tutorial16 = "Only press done here when the task is done in your world!";
    [NonSerialized] public string tutorial17 = "For the sake of showin ya, complete the task now!";
    [NonSerialized] public string tutorial18 = "Good job! Let's go check out our sailin' on the ship!";
    [NonSerialized] public string tutorial19 = "";
    [NonSerialized] public string tutorial20 = "";
    [NonSerialized] public string tutorial21 = "";
    [NonSerialized] public string tutorial22 = "";
    [NonSerialized] public string tutorial23 = "";
    [NonSerialized] public string tutorial24 = "";
    [NonSerialized] public string tutorial25 = "";
    [NonSerialized] public string tutorial26 = "";
    [NonSerialized] public string tutorial27 = "";
    [NonSerialized] public string tutorial28 = "";
    [NonSerialized] public string tutorial29 = "";
    [NonSerialized] public string tutorial30 = "";

    [NonSerialized] public string[] tutorialMessages = new string[31];
    public TMP_Text tutorialText;
    [NonSerialized] public int tutorialIndex = 0;
    public GameObject shadowBackgrounds;
    public GameObject textBox;
    public GameObject coverHolder;
    public Image tasksCover;
    public Image tasksCoverBlocker;
    public Image fishingCover;
    public Image menuCover;
    public GameObject fullScreenTap;
    public GameObject taskAddTap;
    public GameObject tutorialHolder;
    public Transform panel;
    [NonSerialized] public bool taskAdded = false;
    [NonSerialized] public bool taskCompleted = false;
    public GameProgress gameProgress;
    public GameObject shipTravelFromTaskBlocker;


    public void tutorialMessagesInit(){
        tutorialMessages[0] = tutorial0;
        tutorialMessages[1] = tutorial1;
        tutorialMessages[2] = tutorial2;
        tutorialMessages[3] = tutorial3;
        tutorialMessages[4] = tutorial4;
        tutorialMessages[5] = tutorial5;
        tutorialMessages[6] = tutorial6;
        tutorialMessages[7] = tutorial7;
        tutorialMessages[8] = tutorial8;
        tutorialMessages[9] = tutorial9;
        tutorialMessages[10] = tutorial10;
        tutorialMessages[11] = tutorial11;
        tutorialMessages[12] = tutorial12;
        tutorialMessages[13] = tutorial13;
        tutorialMessages[14] = tutorial14;
        tutorialMessages[15] = tutorial15;
        tutorialMessages[16] = tutorial16;
        tutorialMessages[17] = tutorial17;
        tutorialMessages[18] = tutorial18;
        tutorialMessages[19] = tutorial19;
        tutorialMessages[20] = tutorial20;
        tutorialMessages[21] = tutorial21;
        tutorialMessages[22] = tutorial22;
        tutorialMessages[23] = tutorial23;
        tutorialMessages[24] = tutorial24;
        tutorialMessages[25] = tutorial25;
        tutorialMessages[26] = tutorial26;
        tutorialMessages[27] = tutorial27;
        tutorialMessages[28] = tutorial28;
        tutorialMessages[29] = tutorial29;
        tutorialMessages[30] = tutorial30;
        tutorialText.text = tutorialMessages[0];
    }

    public void tutorialAdvancement(){
        if (tutorialIndex < 8){
            tutorialIndex++;
            tutorialText.text = tutorialMessages[tutorialIndex];
        }
        else if (tutorialIndex == 8){
            tutorialIndex++;
            tutorialText.text = tutorialMessages[tutorialIndex];
            Color customColor = new Color(0.9960784f, 1f, 0f, 0.1019608f);
            tasksCover.color = customColor;
            tasksCover.raycastTarget = true;
            fullScreenTap.SetActive(false);
        }
        else if (tutorialIndex == 9){
            //UNDO OF 8
            Color customColor = new Color(0, 0, 0, 0.5882353f);
            tasksCover.color = customColor;
            tasksCover.raycastTarget = false;
            fullScreenTap.SetActive(true);
            //END
            
            tutorialIndex++;
            tutorialText.text = tutorialMessages[tutorialIndex];
        }
        else if (tutorialIndex == 10){
            tutorialIndex++;
            tutorialText.text = tutorialMessages[tutorialIndex];
        }
        else if (tutorialIndex == 11 && !taskCompleted){
            shadowBackgrounds.SetActive(false);
            coverHolder.SetActive(false);
            taskAddTap.SetActive(true); //COMMENT OUT THIS LINE IF YOU NEED TO RESET THE TASKS MANUALLY
            fullScreenTap.SetActive(false);
            tutorialIndex++;
            tutorialText.text = tutorialMessages[tutorialIndex];
        }
        else if (tutorialIndex == 12 && taskAdded && !taskCompleted){
            tutorialIndex++;
            tutorialText.text = tutorialMessages[tutorialIndex];
            fullScreenTap.SetActive(true);
            shadowBackgrounds.SetActive(true);
            coverHolder.SetActive(true);
        }
        else if(tutorialIndex < 16 && !taskCompleted){
            tutorialIndex++;
            tutorialText.text = tutorialMessages[tutorialIndex];
        }
        else if(tutorialIndex == 16){
            tutorialIndex++;
            tutorialText.text = tutorialMessages[tutorialIndex];
            fullScreenTap.SetActive(false);
            shadowBackgrounds.SetActive(false);
            coverHolder.SetActive(false);
        }
        else if(tutorialIndex == 17 && taskCompleted){
            Debug.Log("we out here");
            tutorialIndex++;
            tutorialText.text = tutorialMessages[tutorialIndex];
            taskAddTap.SetActive(false);
            shipTravelFromTaskBlocker.SetActive(true);
        }
    }

    public void TutorialInit(){
        tutorialHolder.SetActive(true);
    }
}
