using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabLock : MonoBehaviour
{
    public GameObject premiumLock;
    public GameObject premiumTab;
    public BattlePass battlePass;

    public void OnEnable(){
        if (battlePass.passPurchased){
            premiumLock.SetActive(false);
            premiumTab.GetComponent<Image>().raycastTarget = true;
        }
        else{
            premiumLock.SetActive(true);
            premiumTab.GetComponent<Image>().raycastTarget = false;
        }
    }
}
