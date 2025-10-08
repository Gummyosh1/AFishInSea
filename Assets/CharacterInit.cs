using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class CharacterInit : MonoBehaviour
{
    public GemStorage gemStorage;
    public TabScript tabScript;
    public GameObject charPopUp;
    public Button[] characterButtons;
    public GameObject[] locks;
    public GameProgress gameProgress;
    public Image characterImage;
    public CharacterMover characterMover;
    public Sprite[] characterSprites;
    public GameObject[] shopCharactersNOTPURCHASED;
    public GameObject[] shopCharactersPURCHASED;
    public Button[] shopButtons;
    private int skinsCost = 500;
    private int currentCharacter = 0;
    public GameObject popUpWindow;
    public GameObject notEnoughGems;
    public Sprite[] shopSprites;
    public Image shopPopUpImage;

    [NonSerialized] public int[] charactersOwned = new int[8];


    public void characterShopInit()
    {
        for (int i = 0; i < shopCharactersNOTPURCHASED.Length; i++)
        {
            if (charactersOwned[i + 5] == 1)
            {
                shopCharactersPURCHASED[i].SetActive(true);
                shopCharactersNOTPURCHASED[i].SetActive(false);
                shopButtons[i].enabled = false;
            }
            else
            {
                shopCharactersNOTPURCHASED[i].SetActive(true);
                shopCharactersPURCHASED[i].SetActive(false);
                shopButtons[i].enabled = true;
            }
        }
    }

    public void popUp()
    {
        //charactersOwned[0] = 1;
        lockInit();
        charPopUp.SetActive(true);

    }

    public void CharacterInitFunc()
    {
        LoadCharacters();
        if (gameProgress.equippedCharacter != -1)
        {
            characterImage.sprite = characterSprites[gameProgress.equippedCharacter];
            characterImage.color = new Color(1, 1, 1, 1);
        }
        else
        {
            characterImage.color = new Color(0, 0, 0, 1);
        }
    }

    public void buyCharacter(int index)
    {
        charactersOwned[index] = 1;
        gameProgress.equippedCharacter = index;
        tabScript.IslandActivate();
        characterShopInit();

        SaveCharacters();
    }

    public void shopBuyCharacter()
    {
        if (gemStorage.GemTotal >= skinsCost)
        {
            gemStorage.buyWithGems(skinsCost);
            charactersOwned[currentCharacter] = 1;
            gameProgress.equippedCharacter = currentCharacter;
            popUpWindow.SetActive(false);
            characterShopInit();
            SaveCharacters();
        }
    }

    public void buyPopUp(int characterIndex)
    {
        if (gemStorage.GemTotal >= skinsCost)
        {
            popUpWindow.SetActive(true);
            currentCharacter = characterIndex;
            shopPopUpImage.sprite = shopSprites[characterIndex - 5];
        }
        else
        {
            notEnoughGems.SetActive(true);
        }
        
    }

    public void selectCharacter(int characterSelected)
    {
        gameProgress.equippedCharacter = characterSelected;
        charPopUp.SetActive(false);
        characterImage.sprite = characterMover.characterHolder[characterSelected][0];
        characterImage.color = new Color(1, 1, 1, 1);
        SaveCharacters();
    }

    public void lockInit()
    {
        for (int i = 0; i < charactersOwned.Length; i++)
        {
            if (charactersOwned[i] == 1)
            {
                locks[i].SetActive(false);
                characterButtons[i].enabled = true;
            }
            else
            {
                locks[i].SetActive(true);
                characterButtons[i].enabled = false;
            }
        }
    }


    public void LoadCharacters()
    {
        string saveString = SaveSystem.LoadCharacters();
        if (saveString != null)
        {
            CharacterStorage loadedData = JsonUtility.FromJson<CharacterStorage>(saveString);
            for (int i = 0; i < charactersOwned.Length; i++)
            {
                charactersOwned[i] = loadedData.characters[i];
            }
            gameProgress.equippedCharacter = loadedData.equippedCharacter;
        }
        else
        {
            //charactersOwned[0] = 1;
            gameProgress.equippedCharacter = -1;
        }
    }

    public void SaveCharacters()
    {
        CharacterStorage characterHolder = new CharacterStorage
        {
            characters = charactersOwned,
            equippedCharacter = gameProgress.equippedCharacter
        };
        string jsonStorage = JsonUtility.ToJson(characterHolder);
        SaveSystem.SaveCharacters(jsonStorage);
    }

}

public class CharacterStorage
{
    public int[] characters = new int[8];
    public int equippedCharacter = -1;
}
