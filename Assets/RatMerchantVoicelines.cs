using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatMerchantVoicelines : MonoBehaviour
{
    public GameObject[] audioHolders;
    // Start is called before the first frame update
    public void OnEnable()
    {
        int voiceSound = Random.Range(0, audioHolders.Length);
        for (int i = 0; i < audioHolders.Length; i++)
        {
            audioHolders[i].SetActive(false);
        }
        audioHolders[voiceSound].SetActive(true);
    }
}
