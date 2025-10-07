using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class Trader : MonoBehaviour
{
    public GameObject greetingWindow;
    public Sprite[] traderIdle1;
    public Sprite[] traderIdle2;
    public Sprite[] traderIdle3;
    public Sprite[] traderInteract;
    public Sprite[] traderInteractClose;
    public GameObject baitShop;
    public Image TraderImage;
    private float animationTimer = 0;
    private bool interactDone = false;
    private float idleAnimTimer = 0;
    private int currentFrame = 0;
    private bool Shopping = false;
    private bool animLock = false;
    private bool buy = false;
    private bool close = false;

    public void clickOff(){
        interactDone = false;
        Shopping = false;
        animLock = false;
        buy = false;
        currentFrame = 0;
        animationTimer = 0;
        idleAnimTimer = 0;
        TraderImage.sprite = traderIdle1[0];
    }

    public void Update(){
        if (buy){
            if (!interactDone){
                animLock = true;
                TraderBuyBackend();
            }
        }
        if (close){
            if (interactDone){
                animLock = true;
                TraderInteractCloseBackend();
            }
        }
        if (idleAnimTimer > 5 && !Shopping){
            TraderIdle2();
        }
        idleAnimTimer += Time.deltaTime;
    }

    private void TraderBuyBackend(){
        int len = traderInteract.Length;
        AnimateTraderMotion(traderInteract, len, .1f, true, true);
    }

    private void TraderInteractCloseBackend(){
        int len = traderInteractClose.Length;
        AnimateTraderMotion(traderInteractClose, len, .1f, true, false, false, true);

    }

    public void TraderBuy(){
        if (!animLock){
            buy = true;
            Shopping = true;
            currentFrame = 0;
            animationTimer = 0;
            idleAnimTimer = 0;
        }
    }

    public void TraderGreeting(){
        if (!animLock && !buy){
            greetingWindow.SetActive(true);
        }
    }

    public void traderClose(){
        if (!animLock && buy){
            close = true;
        }
    }

    public void TraderIdle1(){
        int len = traderIdle1.Length;
        AnimateTraderMotion(traderIdle1, len, .2f);
    }

    public void TraderIdle2(){
        int len = traderIdle2.Length;
        AnimateTraderMotion(traderIdle2, len, .15f);
    }

    public void TraderIdle3(){
        int len = traderIdle3.Length;
        AnimateTraderMotion(traderIdle3, len, .2f);
    }

    public void AnimateTraderMotion(Sprite[] sprites, int len, float frameSpeed, bool interactLocal = false, bool buyParam = false, bool sell = false, bool closingCurtain = false){
        animationTimer += Time.deltaTime;
        if (animationTimer >= frameSpeed){
            TraderImage.sprite = sprites[currentFrame];
            currentFrame += 1;
            animationTimer = 0;
        }
        if (currentFrame >= len){
            if (closingCurtain){
                Shopping = false;
                close = false;
                buy = false;
                baitShop.SetActive(false);
            }
            if (interactLocal){
                interactDone = !interactDone;
                animLock = false;
            }
            if (buyParam){
                greetingWindow.SetActive(false);
                baitShop.SetActive(true);
            }
            currentFrame = 0;
            animationTimer = 0;
            idleAnimTimer = 0;
        }
    }

}
