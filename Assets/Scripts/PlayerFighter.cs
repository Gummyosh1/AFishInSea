using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class PlayerFighter : MonoBehaviour
{
    public GameObject[] Fighter;
    public FightingProgress fightingProgress;

    void Start()
    {
        swapFighter();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            fightingProgress.currentPlayerSkin++;
            if (fightingProgress.currentPlayerSkin > 3){
                fightingProgress.currentPlayerSkin = 0;
            }
            swapFighter();
            
        }
    }

    public void swapFighter(){
        for (int i = 0; i < Fighter.Length; i++){
            Fighter[i].SetActive(false);
        }
        Fighter[fightingProgress.currentPlayerSkin].SetActive(true);
    }
}
