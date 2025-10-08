using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IslandChecker : MonoBehaviour
{
    public GameProgress gameProgress;
    public TMP_Text islandText;
    private float timer = 0;
    
    public void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 2)
        {
            islandText.text = "ISLAND " + (gameProgress.currentIslandIndex + 1).ToString();
            timer = 0;
        }
        
    }
}
