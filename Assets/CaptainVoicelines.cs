using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptainVoicelines : MonoBehaviour
{
    public GameObject[] AudioHolders;
    private int priorAudio = 0;


    public void Awake()
    {
        for (int i = 0; i < AudioHolders.Length; i++)
        {
            AudioHolders[i].SetActive(false);
        }
        //int voiceSound = Random.Range(0, AudioHolders.Length);
        //priorAudio = voiceSound;
        //AudioHolders[voiceSound].SetActive(true);
    }

    public void nextAudio()
    {
        bool active = false;
        for (int i = 0; i < AudioHolders.Length; i++)
        {
            if (AudioHolders[i].GetComponent<AudioSource>().isPlaying)
            {
                active = true;
            }
            AudioHolders[i].SetActive(false);
        }
        if (!active)
        {
            int voiceSound = Random.Range(0, AudioHolders.Length);
            while (voiceSound == priorAudio)
            {
                voiceSound = Random.Range(0, AudioHolders.Length);
            }
            priorAudio = voiceSound;
            AudioHolders[voiceSound].SetActive(true);
        }
    }
}
