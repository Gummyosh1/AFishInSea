using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class WelcomePage : MonoBehaviour
{
    private int welcomed = 0;
    private int boatIntro = 0;
    private int fishingIntro = 0;
    private int islandIntro = 0;
    private int inventoryIntro = 0;
    private int speachIndex = 0;
    private int boatSpeachIndex = 0;
    private int fishingSpeachIndex = 0;
    private int islandSpeachIndex = 0;
    private int inventorySpeachIndex = 0;
    private int instance = -1; // 0 == boat, 1 == fishing, 2 == island
    private string[] welcomeTextList =
    {
        "Welcome aboard the Vessel of Progress sailor! When you complete tasks in your world, you receive rewards in this one!",
        "Our goal is to sail around the world, catch amazing fish, and fill the bellies of villages in need.",
        "You'll need to complete tasks to go fishing! For the first 3 tasks you complete every day, you'll get 2 bait coins.",
        "You can redeem these coins for bait, with rare bait yielding rare fish!",
        "Try it now! Complete your first task and come redeem your coins on the boat."
    };

    private string[] boatTextList =
    {
        "This is The Vessel of Progress. As we sail to new islands, local Rat Merchants will offer decorations for you to buy!",
        "For now, there are three important parts to the ship.",
        "Firstly, myself. If you tap on me, I'll allow you to redeem your bait coins for bait.",
        "What you get is random, with baits lower on the wheel being more rare than ones that are higher.",
        "Secondly, the Captain's Quarters. Here you can customize your ship and find information about every fish you've caught!",
        "To access it, tap on the door to the left of the main mast.",
        "Thirdly, your magical streak tracking chest. You can open this chest once a day for some amazing rewards.",
        "If you complete a task every single day, the rewards from this chest will increase over time.",
        "I hear only a select few have seen what comes out of the 100 day streak chest...",
        "On a final more cosmetic note, I hear that tapping on the sails makes our ship look pretty cool!"
    };

    private string[] fishingTextList =
    {
        "This is where your fishing adventure awaits, and the fun of completing tasks comes into play!",
        "Every time you receive a bait from me, you can play a mini game to reward yourself for your hard work.",
        "For now you'll only have access to Fishy Timing, but you'll be able to unlock more games and rods from the Rat Merchants.",
        "To equip your rod, bait, and pets once you've bought some from Rat Merchants, you'll need to go to the equipment menu.",
        "Your backpack stores the fish you've caught. Doing better in each game results in more fish!",
        "Finally, to cast your rod you'll just need to press the big button to start your game!"
    };

    private string[] islandTextList =
    {
        "Welcome to the first Island, the place we'll embark on our journey from!",
        "Every island will have two main components. Villagers and a Rat Merchant.",
        "Villgers are located within the houses, some islands having more than others.",
        "Our goal is to help the villagers. They will offer you quests, and it is our duty to complete them!",
        "I will not let us sail to the next island until we've completed every quest set for us by the villagers.",
        "Rat Merchants are different. Some towns love their Rat Merchant, and will ask you to buy supplies from him.",
        "Some towns hate theirs, and won't bother mentioning any goods he's selling. On these islands it's your choice to buy or not.",
        "You will always be able to sell your fish to the Rat Merchant to clear out your inventory space!",
        "Once you purchase a character to represent you, they'll show up on the Island! Tap on the campfire next to them to travel to the next Island."
    };

    private string[] inventoryTextList =
    {
        "This is the backpack you'll be throwing all your fish in after catching them!",
        "The background color of a fish displays it's rarity",
        "Grey is Normal, Green is Fancy, Purple is Extravagant, Teal is Pristine, and Black is one of the Elite Eight",
        "The rarer the fish, the more it will sell for at a Rat Merchant!"
    };

    public GameObject welcomeScreen;
    public GameObject captainHighlight;
    public GameObject quartersHighlight;
    public GameObject streakHighlight;
    public GameObject equipmentHighlight;
    public GameObject inventoryHighlight;
    public GameObject rodHighlight;
    public GameObject villagerHighlight;
    public GameObject merchantHighlight;
    public TMP_Text captainText;
    public CaptainVoicelines captainVoicelines;
    

    public void welcomeInit()
    {
        loadWelcome();
        if (welcomed == 0)
        {
            captainText.text = welcomeTextList[0];
            welcomeScreen.SetActive(true);
            //RUN WELCOME PROCESS
        }
    }

    public void boatInit()
    {
        loadWelcome();
        if (boatIntro == 0)
        {
            instance = 0;
            captainText.text = boatTextList[0];
            welcomeScreen.SetActive(true);
        }
    }

    public void fishInit()
    {
        loadWelcome();
        if (fishingIntro == 0)
        {
            instance = 1;
            captainText.text = fishingTextList[0];
            welcomeScreen.SetActive(true);
        }
    }

    public void islandInit()
    {
        loadWelcome();
        if (islandIntro == 0)
        {
            instance = 2;
            captainText.text = islandTextList[0];
            welcomeScreen.SetActive(true);
        }
    }
    
    public void inventoryInit()
    {
        loadWelcome();
        if (inventoryIntro == 0)
        {
            instance = 3;
            captainText.text = inventoryTextList[0];
            welcomeScreen.SetActive(true);
        }
    }

    public void ProgressWelcome()
    {
        if (welcomed == 0)
        {
            if (speachIndex < welcomeTextList.Length - 1)
            {
                speachIndex++;
                captainText.text = welcomeTextList[speachIndex];
            }
            else
            {
                finishWelcome();
            }
        }
        else if (boatIntro == 0 && instance == 0)
        {
            if (boatSpeachIndex < boatTextList.Length - 1)
            {
                boatSpeachIndex++;
                if (boatSpeachIndex == 2)
                {
                    captainHighlight.SetActive(true);
                }
                else if (boatSpeachIndex == 4)
                {
                    captainHighlight.SetActive(false);
                    quartersHighlight.SetActive(true);
                }
                else if (boatSpeachIndex == 6)
                {
                    quartersHighlight.SetActive(false);
                    streakHighlight.SetActive(true);
                }
                else if (boatSpeachIndex == 9)
                {
                    streakHighlight.SetActive(false);
                }
                captainText.text = boatTextList[boatSpeachIndex];
            }
            else
            {
                finishBoat();
            }
        }
        else if (fishingIntro == 0 && instance == 1)
        {
            if (fishingSpeachIndex < fishingTextList.Length - 1)
            {
                fishingSpeachIndex++;
                if (fishingSpeachIndex == 3)
                {
                    equipmentHighlight.SetActive(true);
                }
                else if (fishingSpeachIndex == 4)
                {
                    inventoryHighlight.SetActive(true);
                    equipmentHighlight.SetActive(false);
                }
                else if (fishingSpeachIndex == 5)
                {
                    inventoryHighlight.SetActive(false);
                    rodHighlight.SetActive(true);
                }
                captainText.text = fishingTextList[fishingSpeachIndex];
            }
            else
            {
                rodHighlight.SetActive(false);
                finishFishing();
            }
        }
        else if (islandIntro == 0 && instance == 2)
        {
            if (islandSpeachIndex < islandTextList.Length - 1)
            {
                islandSpeachIndex++;
                if (islandSpeachIndex == 2)
                {
                    villagerHighlight.SetActive(true);
                }
                else if (islandSpeachIndex == 5)
                {
                    villagerHighlight.SetActive(false);
                    merchantHighlight.SetActive(true);
                }
                else if (islandSpeachIndex == 8)
                {
                    merchantHighlight.SetActive(false);
                }
                captainText.text = islandTextList[islandSpeachIndex];
            }
            else
            {
                finishIsland();
            }
        }
        else if (inventoryIntro == 0 && instance == 3)
        {
            if (inventorySpeachIndex < inventoryTextList.Length - 1)
            {
                inventorySpeachIndex++;
                if (inventorySpeachIndex == 3)
                {

                }
                captainText.text = inventoryTextList[inventorySpeachIndex];
            }
            else
            {
                finishInventory();
            }
        }
        captainVoicelines.nextAudio();
    }

    public void finishWelcome()
    {
        welcomeScreen.SetActive(false);
        rodHighlight.SetActive(false);
        welcomed = 1;
        saveWelcome();
    }

    public void finishBoat()
    {
        welcomeScreen.SetActive(false);
        boatIntro = 1;
        saveWelcome();
    }

    public void finishFishing()
    {
        welcomeScreen.SetActive(false);
        fishingIntro = 1;
        saveWelcome();
    }

    public void finishIsland()
    {
        welcomeScreen.SetActive(false);
        islandIntro = 1;
        saveWelcome();
    }

    public void finishInventory()
    {
        welcomeScreen.SetActive(false);
        inventoryIntro = 1;
        saveWelcome();
    }


    public void saveWelcome()
    {
        Welcome welcomeClass = new Welcome
        {
            welcomedSave = welcomed,
            boatSave = boatIntro,
            fishingSave = fishingIntro,
            islandSave = islandIntro,
            inventorySave = inventoryIntro,
        };
        string jsonStorage = JsonUtility.ToJson(welcomeClass);
        SaveSystem.SaveWelcome(jsonStorage);
    }
    public void loadWelcome()
    {
        string saveString = SaveSystem.LoadWelcome();
        if (saveString != null)
        {
            Welcome loadedData = JsonUtility.FromJson<Welcome>(saveString);
            welcomed = loadedData.welcomedSave;
            boatIntro = loadedData.boatSave;
            fishingIntro = loadedData.fishingSave;
            islandIntro = loadedData.islandSave;
            inventoryIntro = loadedData.inventorySave;
        }
        else
        {
            welcomed = 0;
            boatIntro = 0;
            fishingIntro = 0;
            islandIntro = 0;
            inventoryIntro = 0;
        }
    }
}


public class Welcome
{
    public int welcomedSave = 0;
    public int boatSave = 0;
    public int fishingSave = 0;
    public int islandSave = 0;
    public int inventorySave = 0;
}
