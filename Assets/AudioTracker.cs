using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioTracker : MonoBehaviour
{
    [NonSerialized] public float volume = .3f;
    private int activeAudio = 0;
    public GameObject[] audioSourceObjects;
    public AudioSource[] audioSources;
    public Slider slider;
    public VolumeUpdater volumeUpdater;

    public void audioInit()
    {
        loadAudio();
        int randomInt = UnityEngine.Random.Range(0, 2);  // Returns a value from 0 to 9
        activeAudio = randomInt;
        audioSources[activeAudio].volume = volume;
        audioSourceObjects[activeAudio].SetActive(true);
        slider.value = volume;
        volumeUpdater.UpdatePercentageText(volume);
    }

    public void changeVolume()
    {
        float before = volume;
        volume = slider.value;
        if (volume != before)
        {
            audioSources[activeAudio].volume = volume;
            saveAudio();
        }
    }

    public void saveAudio()
    {
        audioClass audioSave = new audioClass
        {
            savedVolume = volume,
        };
        string jsonStorage = JsonUtility.ToJson(audioSave);
        SaveSystem.SaveAudio(jsonStorage);
    }

    public void loadAudio()
    {
        string saveString = SaveSystem.LoadAudio();
        if (saveString != null){
            audioClass loadedData = JsonUtility.FromJson<audioClass>(saveString);
            volume = loadedData.savedVolume;
        }
        else{
            volume = .3f;
        }
    }
}

public class audioClass
{
    public float savedVolume;
}
