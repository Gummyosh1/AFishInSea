using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Video;
using Image = UnityEngine.UI.Image;

public class CharacterMover : MonoBehaviour
{
    public GameProgress gameProgress;

    public Image characterImage;
    public Sprite[] character1;
    public Sprite[] character2;
    public Sprite[] character3;
    public Sprite[] character4;
    public Sprite[] character5;
    public Sprite[] character6;
    public Sprite[] character7;
    public Sprite[] character8;

    public Transform[] shooters;
    public Transform characterTransform;
    private Transform endGoal;

    private int currentLocation = 0;
    private int index = 0;
    private int nextSpot;
    private int sit = 0;

    private float cutoff = 3;
    private float moveCutoff = .1f;
    private float timer = 0;
    private float moveTimer = 0;
    private float moveX = 0;
    private float moveY = 0;
    private float moveSpeed = .3f;
    private float sitTimer = 0;
    private float sitTimerTracker = 0;


    private bool move = false;
    private bool sitSetup = false;
    private bool delayed = false;




    [NonSerialized] public int equippedCharacter = 0; //TODO



    //0,1,2 DOWN
    //3,4,5 UP
    //6,7,8 RIGHT
    //9,10,11 LEFT
    //12 FACING LEFT SIT
    //13 FACING RIGHT SIT

    [NonSerialized] public Sprite[][] characterHolder = new Sprite[8][];


    public void Init()
    {
        characterHolder[0] = character1;
        characterHolder[1] = character2;
        characterHolder[2] = character3;
        characterHolder[3] = character4;
        characterHolder[4] = character5;
        characterHolder[5] = character6;
        characterHolder[6] = character7;
        characterHolder[7] = character8;

        characterImage.sprite = characterHolder[equippedCharacter][13];
    }

    public void setup1(int x)
    {
        //Debug.Log("THIS IS HAPPENING! in script " + gameObject.name);
        if (x != -1)
        {
            equippedCharacter = x;
            characterImage.sprite = characterHolder[equippedCharacter][0];
            transform.parent.gameObject.SetActive(true);
        }
        else
        {
            equippedCharacter = x;
            transform.parent.gameObject.SetActive(false);
        }
        
    }


    //O = BL
    //1 = BM
    //2 = BR
    //3 = TL
    //4 = TM
    //5 = TR
    public void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            timer += Time.deltaTime;

            if (timer >= cutoff && !move)
            {
                equippedCharacter = gameProgress.equippedCharacter;
                nextLocationHelper();
                move = true;
                cutoff = UnityEngine.Random.Range(2, 6);
            }
            if (move && (sitTimer == 0))
            {
                moveTimer += Time.deltaTime;
                if (moveTimer >= moveCutoff)
                {
                    if (moveX > 0)
                    {
                        if (characterTransform.position.x < endGoal.position.x)
                        {
                            characterTransform.position = new Vector3(characterTransform.position.x + moveX, characterTransform.position.y + moveY, characterTransform.position.z);
                            index++;
                            index %= 3;
                            characterImage.sprite = characterHolder[equippedCharacter][index + 6];
                            moveTimer = 0;
                        }
                        else
                        {
                            characterTransform.position = endGoal.position;
                            characterImage.sprite = characterHolder[equippedCharacter][6];
                            index = 0;
                            move = false;
                            currentLocation = nextSpot;
                            moveTimer = 0;
                            timer = 0;
                        }

                    }
                    else if (moveX < 0)
                    {
                        if (characterTransform.position.x > endGoal.position.x)
                        {
                            characterTransform.position = new Vector3(characterTransform.position.x + moveX, characterTransform.position.y + moveY, characterTransform.position.z);
                            index++;
                            index %= 3;
                            characterImage.sprite = characterHolder[equippedCharacter][index + 9];
                            moveTimer = 0;
                        }
                        else
                        {
                            characterTransform.position = endGoal.position;
                            characterImage.sprite = characterHolder[equippedCharacter][9];
                            index = 0;
                            move = false;
                            currentLocation = nextSpot;
                            moveTimer = 0;
                            timer = 0;
                        }
                    }
                    else if (moveY > 0)
                    {
                        if (characterTransform.position.y < endGoal.position.y)
                        {
                            characterTransform.position = new Vector3(characterTransform.position.x + moveX, characterTransform.position.y + moveY, characterTransform.position.z);
                            index++;
                            index %= 3;
                            characterImage.sprite = characterHolder[equippedCharacter][index + 3];
                            moveTimer = 0;
                        }
                        else
                        {
                            characterTransform.position = endGoal.position;
                            characterImage.sprite = characterHolder[equippedCharacter][3];
                            index = 0;
                            move = false;
                            currentLocation = nextSpot;
                            moveTimer = 0;
                            timer = 0;
                        }
                    }
                    else if (moveY < 0)
                    {
                        if (characterTransform.position.y > endGoal.position.y)
                        {
                            characterTransform.position = new Vector3(characterTransform.position.x + moveX, characterTransform.position.y + moveY, characterTransform.position.z);
                            index++;
                            index %= 3;
                            characterImage.sprite = characterHolder[equippedCharacter][index];
                            moveTimer = 0;
                        }
                        else
                        {
                            characterTransform.position = endGoal.position;
                            characterImage.sprite = characterHolder[equippedCharacter][0];
                            index = 0;
                            move = false;
                            currentLocation = nextSpot;
                            moveTimer = 0;
                            timer = 0;
                        }
                    }
                }
            }
            else if (sitTimer > 0)
            {
                sitTimerTracker += Time.deltaTime;
                if (!sitSetup)
                {
                    if (sit == 0)
                    {
                        Debug.Log("Equipped character is " + equippedCharacter);
                        characterImage.sprite = characterHolder[equippedCharacter][13];
                    }
                    else if (sit == 1)
                    {
                        characterImage.sprite = characterHolder[equippedCharacter][12];
                    }

                    sitSetup = true;
                }

                if (sitTimerTracker >= sitTimer)
                {
                    if (!delayed)
                    {
                        delayed = true;
                        sitTimer += 1;
                        characterImage.sprite = characterHolder[equippedCharacter][0];
                    }
                    else if (delayed)
                    {
                        delayed = false;
                        sitTimer = 0;
                        sitTimerTracker = 0;
                        sitSetup = false;
                        sit = 0;
                        sitSetup = false;
                    }
                }

            }
        }
        }




    private int nextLocationHelper()
    {
        switch (currentLocation)
        {
            case 0:
                nextSpot = UnityEngine.Random.Range(0, 2);
                if (nextSpot == 0)
                {
                    nextSpot = 1;
                    moveX = moveSpeed;
                    moveY = 0;
                    endGoal = shooters[nextSpot];
                }
                else
                {
                    nextSpot = 3;
                    moveY = moveSpeed;
                    moveX = 0;
                    endGoal = shooters[nextSpot];
                }

                sit = UnityEngine.Random.Range(0, 2);
                if (sit == 1)
                {
                    sit = 0;
                    sitTimer = UnityEngine.Random.Range(8, 15);
                }
                else
                {
                    sitTimer = 0;
                }

                return nextSpot;

            case 1:
                nextSpot = UnityEngine.Random.Range(0, 3);
                if (nextSpot == 0)
                {
                    nextSpot = 0;
                    moveX = -moveSpeed;
                    moveY = 0;
                    endGoal = shooters[nextSpot];
                }
                else if (nextSpot == 1)
                {
                    nextSpot = 2;
                    moveX = moveSpeed;
                    moveY = 0;
                    endGoal = shooters[nextSpot];
                }
                else if (nextSpot == 2)
                {
                    nextSpot = 4;
                    moveY = moveSpeed;
                    moveX = 0;
                    endGoal = shooters[nextSpot];
                }

                sitTimer = 0;
                return nextSpot;

            case 2:
                nextSpot = UnityEngine.Random.Range(0, 2);
                if (nextSpot == 0)
                {
                    nextSpot = 1;
                    moveX = -moveSpeed;
                    moveY = 0;
                    endGoal = shooters[nextSpot];
                }
                else
                {
                    nextSpot = 5;
                    moveY = moveSpeed;
                    moveX = 0;
                    endGoal = shooters[nextSpot];
                }

                sit = UnityEngine.Random.Range(0, 2);
                Debug.Log("SIT IS " + sit);
                if (sit == 1)
                {
                    sit = 1;
                    sitTimer = UnityEngine.Random.Range(8, 15);
                }
                else
                {
                    sitTimer = 0;
                }

                return nextSpot;

            case 3:
                nextSpot = UnityEngine.Random.Range(0, 2);
                if (nextSpot == 0)
                {
                    nextSpot = 0;
                    moveY = -moveSpeed;
                    moveX = 0;
                    endGoal = shooters[nextSpot];
                }
                else
                {
                    nextSpot = 4;
                    moveX = moveSpeed;
                    moveY = 0;
                    endGoal = shooters[nextSpot];
                }
                sitTimer = 0;
                return nextSpot;

            case 4:
                nextSpot = UnityEngine.Random.Range(0, 3);
                if (nextSpot == 0)
                {
                    nextSpot = 3;
                    moveX = -moveSpeed;
                    moveY = 0;
                    endGoal = shooters[nextSpot];
                }
                else if (nextSpot == 1)
                {
                    nextSpot = 5;
                    moveX = moveSpeed;
                    moveY = 0;
                    endGoal = shooters[nextSpot];
                }
                else if (nextSpot == 2)
                {
                    nextSpot = 1;
                    moveY = -moveSpeed;
                    moveX = 0;
                    endGoal = shooters[nextSpot];
                }
                sitTimer = 0;
                return nextSpot;

            case 5:
                nextSpot = UnityEngine.Random.Range(0, 2);
                if (nextSpot == 0)
                {
                    nextSpot = 4;
                    moveX = -moveSpeed;
                    moveY = 0;
                    endGoal = shooters[nextSpot];
                }
                else
                {
                    nextSpot = 2;
                    moveY = -moveSpeed;
                    moveX = 0;
                    endGoal = shooters[nextSpot];
                }
                sitTimer = 0;
                return nextSpot;
        }
        return -1;
    }
    
}
