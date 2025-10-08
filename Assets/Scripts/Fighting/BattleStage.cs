using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStage : MonoBehaviour
{
    public GameObject[] StageList;

    public void setStage(int index){
        for (int i = 0; i < StageList.Length; i++){
            StageList[i].SetActive(false);
        }
        StageList[index].SetActive(true);
    }
}
