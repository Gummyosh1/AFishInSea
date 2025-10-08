using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
using Random = System.Random;

public class PetWalk : MonoBehaviour
{
    public Button[] buttons;
    public GameObject[] locks;
    public GameObject shadow;
    public GameObject petSelect;
    public EquipmentScript equipmentScript;
    public GameObject PetObject;
    public Transform rightEdge;
    public Transform leftEdge;
    public Transform bottomPoint;
    private Vector3 AC;
    private float acScalar = 0;
    private Vector3 BC;
    private float bcScalar = 0;
    public Image PetImage;
    public Sprite[] Cat1LeftSprites;
    public Sprite[] Cat2LeftSprites;
    public Sprite[] Cat3LeftSprites;
    public Sprite[] Fox1LeftSprites;
    public Sprite[] Fox2LeftSprites;
    public Sprite[] PenguinLeftSprites;
    public Sprite[] LavaPenguinLeftSprites;
    public Sprite[] PurpleFoxLeftSprites;
    private Sprite[][] leftLoader = new Sprite[8][];
    private Sprite[] LeftSprites;

    public Sprite[] Cat1RightSprites;
    public Sprite[] Cat2RightSprites;
    public Sprite[] Cat3RightSprites;
    public Sprite[] Fox1RightSprites;
    public Sprite[] Fox2RightSprites;
    public Sprite[] PenguinRightSprites;
    public Sprite[] LavaPenguinRightSprites;
    public Sprite[] PurpleFoxRightSprites;
    private Sprite[][] rightLoader = new Sprite[8][];
    private Sprite[] RightSprites;

    public Sprite[] Cat1UpSprites;
    public Sprite[] Cat2UpSprites;
    public Sprite[] Cat3UpSprites;
    public Sprite[] Fox1UpSprites;
    public Sprite[] Fox2UpSprites;
    public Sprite[] PenguinUpSprites;
    public Sprite[] LavaPenguinUpSprites;
    public Sprite[] PurpleFoxUpSprites;
    private Sprite[][] upLoader = new Sprite[8][];
    private Sprite[] UpSprites;

    public Sprite[] Cat1DownSprites;
    public Sprite[] Cat2DownSprites;
    public Sprite[] Cat3DownSprites;
    public Sprite[] Fox1DownSprites;
    public Sprite[] Fox2DownSprites;
    public Sprite[] PenguinDownSprites;
    public Sprite[] LavaPenguinDownSprites;
    public Sprite[] PurpleFoxDownSprites;
    private Sprite[][] downLoader = new Sprite[8][];
    private Sprite[] DownSprites;


    public PetListScript petListScript;
    public GameObject Zs;

    private float timer = 0;
    private float resetter = 0;
    private int index = 0;
    [NonSerialized]
    public bool idling = true;


    public Image selected_pet_image;
    public GameObject petPopUp;
    public GameObject backing;
    private int walkCount = 0; 
    private float breakTimer = 0; 
    private int waiting = 1;
    private int direction = 2;
    private Random random = new Random();
    [NonSerialized] public int[] petsOwned = {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
    [NonSerialized] public int petEquipped = 0;
    [NonSerialized] public int petTotal = 8;

    public void Init(){
        LoadPet();
        if (petEquipped == -1){
            petEquipped = 0;
            //PetObject.SetActive(false);
            //Zs.SetActive(false);
        }
        else{
            PetObject.SetActive(true);
            Zs.SetActive(true);
            PetImage.sprite = petListScript.petSleep[petEquipped];
        }

        AC = bottomPoint.position - leftEdge.position;
        //Debug.Log("bottom Point - left Edge = " + bottomPoint.position + " - " + leftEdge.position + " = " + AC);
        acScalar = AC.y / AC.x;
        BC = bottomPoint.position - rightEdge.position;
        //Debug.Log("bottom Point - right Edge = " + bottomPoint.position + " - " + rightEdge.position + " = " + BC);
        bcScalar = BC.y / BC.x;

        leftLoader[0] = Cat1LeftSprites;
        leftLoader[1] = Cat2LeftSprites;
        leftLoader[2] = Cat3LeftSprites;
        leftLoader[3] = Fox1LeftSprites;
        leftLoader[4] = Fox2LeftSprites;
        leftLoader[5] = PenguinLeftSprites;
        leftLoader[6] = LavaPenguinLeftSprites;
        leftLoader[7] = PurpleFoxLeftSprites;
        LeftSprites = leftLoader[petEquipped];

        rightLoader[0] = Cat1RightSprites;
        rightLoader[1] = Cat2RightSprites;
        rightLoader[2] = Cat3RightSprites;
        rightLoader[3] = Fox1RightSprites;
        rightLoader[4] = Fox2RightSprites;
        rightLoader[5] = PenguinRightSprites;
        rightLoader[6] = LavaPenguinRightSprites;
        rightLoader[7] = PurpleFoxRightSprites;
        RightSprites = rightLoader[petEquipped];

        upLoader[0] = Cat1UpSprites;
        upLoader[1] = Cat2UpSprites;
        upLoader[2] = Cat3UpSprites;
        upLoader[3] = Fox1UpSprites;
        upLoader[4] = Fox2UpSprites;
        upLoader[5] = PenguinUpSprites;
        upLoader[6] = LavaPenguinUpSprites;
        upLoader[7] = PurpleFoxUpSprites;
        UpSprites = upLoader[petEquipped];

        downLoader[0] = Cat1DownSprites;
        downLoader[1] = Cat2DownSprites;
        downLoader[2] = Cat3DownSprites;
        downLoader[3] = Fox1DownSprites;
        downLoader[4] = Fox2DownSprites;
        downLoader[5] = PenguinDownSprites;
        downLoader[6] = LavaPenguinDownSprites;
        downLoader[7] = PurpleFoxDownSprites;
        DownSprites = downLoader[petEquipped];
        
        selected_pet_image.sprite = LeftSprites[1];

        /*
        //DELETE THIS BEFORE GAME RELEASE LOL
        petsOwned[0] = 1;
        petsOwned[1] = 1;
        petsOwned[2] = 1;
        petsOwned[3] = 1;
        petsOwned[4] = 1;
        petsOwned[5] = 1;
        petsOwned[6] = 1;
        petsOwned[7] = 1;
        */
    }

    public void petPopUpFunc(){
        petPopHelper(0,0); // CAT 1
        petPopHelper(5,6); // PENGUIN
        petPopHelper(1,1); // CAT 2
        petPopHelper(7,5); // PURPLE FOX
        petPopHelper(3,3); // FOX 1
        petPopHelper(6,7); // RED PENGUIN
        petPopHelper(2,2); // CAT 3
        petPopHelper(4,4); // FOX 2
        petPopUp.SetActive(true);
        shadow.SetActive(true);
    }

    public void petPopHelper(int indexOfPet, int indexOfLock){
        if (petsOwned[indexOfPet] == 0){
            locks[indexOfLock].SetActive(true);
            buttons[indexOfLock].enabled = false;
        }
        else{
            locks[indexOfLock].SetActive(false);
            buttons[indexOfLock].enabled = true;
        }
    }

    public void walk(){
        idling = false;
        petPopUp.SetActive(false);
        backing.SetActive(false);
        waiting = 0;
    }

    public void FixedUpdate(){
        if (idling){
            PetImage.sprite = petListScript.petSleep[petEquipped];
            Zs.SetActive(true);
        }
        else{
            Zs.SetActive(false);
            breakTimer += Time.deltaTime;
            if (breakTimer >= waiting){
                switch (direction){ //0 north, 1 east, 2 south, 3 west
                    case 0:
                        upWalkAnimation();
                        break;
                    case 1:
                        rightWalkAnimation();
                        break;
                    case 2:
                        downWalkAnimation();
                        break;
                    case 3:
                        leftWalkAnimation();
                        break;
                }
            }
        }
        float correctXLeft = ((PetObject.transform.position.y - leftEdge.position.y) / acScalar) + leftEdge.position.x;
        float correctXRight = ((PetObject.transform.position.y - rightEdge.position.y) / bcScalar) + rightEdge.position.x;
        //Debug.Log("Correct X is " + correctX);
        //Debug.Log("Current X is " + PetObject.transform.position.x);
        if (PetObject.transform.position.x <= correctXLeft){
            PetObject.transform.position = new Vector3(correctXLeft, PetObject.transform.position.y, PetObject.transform.position.z);
        }
        if (PetObject.transform.position.x >= correctXRight){
            PetObject.transform.position = new Vector3(correctXRight, PetObject.transform.position.y, PetObject.transform.position.z);
        }
        if (PetObject.transform.position.y <= bottomPoint.position.y){
            PetObject.transform.position = bottomPoint.position;    
            }
        if (PetObject.transform.position.y >= leftEdge.position.y){
            PetObject.transform.position = new Vector3(PetObject.transform.position.x, leftEdge.position.y, PetObject.transform.position.z);
        }
        
    }

    public void leftWalkAnimation(){
        resetter += Time.deltaTime;
        if (resetter <= 1){
            timer += Time.deltaTime;
            if (timer >= .25){
                PetImage.sprite = LeftSprites[index];
                index++;
                index %= LeftSprites.Length;
                timer = 0;
                MoveLeft();
            }
        }
        else{
            resetter = 0;

            waiting = random.Next(1,11);
            direction = random.Next(0,4);
            walkCount++;
            if (walkCount > 1){
                breakTimer = 0;
                walkCount = 0;
            }

            PetImage.sprite = LeftSprites[1];
        }
    }

    public void rightWalkAnimation(){
        resetter += Time.deltaTime;
        if (resetter <= 1){
            timer += Time.deltaTime;
            if (timer >= .25){
                PetImage.sprite = RightSprites[index];
                index++;
                index %= RightSprites.Length;
                timer = 0;
                MoveRight();
            }
        }
        else{
            resetter = 0;

            waiting = random.Next(1,11);
            direction = random.Next(0,4);
            walkCount++;
            if (walkCount > 1){
                breakTimer = 0;
                walkCount = 0;
            }

            PetImage.sprite = RightSprites[1];
        }
    }

    public void downWalkAnimation(){
        resetter += Time.deltaTime;
        if (resetter <= 1){
            timer += Time.deltaTime;
            if (timer >= .25){
                PetImage.sprite = DownSprites[index];
                index++;
                index %= RightSprites.Length;
                timer = 0;
                MoveDown();
            }
        }
        else{
            resetter = 0;

            waiting = random.Next(1,11);
            walkCount++;
            direction = random.Next(0,2);
            if (direction == 0){direction = 1;}
            else {direction = 3;}
            if (walkCount > 1){
                breakTimer = 0;
                walkCount = 0;
            }

            PetImage.sprite = DownSprites[1];
        }
    }
    
    public void upWalkAnimation(){
        resetter += Time.deltaTime;
        if (resetter <= 1){
            timer += Time.deltaTime;
            if (timer >= .25){
                PetImage.sprite = UpSprites[index];
                index++;
                index %= RightSprites.Length;
                timer = 0;
                MoveUp();
            }
        }
        else{
            resetter = 0;

            waiting = random.Next(1,11);
            walkCount++;
            direction = random.Next(0,2);
            if (direction == 0){direction = 1;}
            else {direction = 3;}
            if (walkCount > 1){
                breakTimer = 0;
                walkCount = 0;
            }

            PetImage.sprite = UpSprites[1];
        }
    }
    

    public void MoveLeft(){
        PetObject.transform.position -= new Vector3(2,0,0);
        Debug.Log("Moving Left");
    }

    public void MoveRight(){
        PetObject.transform.position += new Vector3(2,0,0);
    }

    public void MoveDown(){
        PetObject.transform.position -= new Vector3(0,2,0);
    }

    public void MoveUp(){
        PetObject.transform.position += new Vector3(0,2,0);
    }

    public void buyCatOne(){
        petsOwned[0] = 1;
        SavePet();
    }

    public void buyCatTwo(){
        petsOwned[1] = 1;
        SavePet();
    }

    public void buyCatThree(){
        petsOwned[2] = 1;
        SavePet();
    }

    public void buyFoxOne(){
        petsOwned[3] = 1;
        SavePet();
    }

    public void buyFoxTwo(){
        petsOwned[4] = 1;
        SavePet();
    }

    public void buyPenguin(){
        petsOwned[5] = 1;
        SavePet();
    }

    public void buyLavaPenguin(){
        petsOwned[6] = 1;
        SavePet();
    }

    public void buyPurpleFox(){
        petsOwned[7] = 1;
        SavePet();
    }

    public void equipCatOne(){
        if (petsOwned[0] == 1){
            petEquipped = 0;
            LeftSprites = leftLoader[petEquipped];
            RightSprites = rightLoader[petEquipped];
            UpSprites = upLoader[petEquipped];
            DownSprites = downLoader[petEquipped];
            selected_pet_image.sprite = LeftSprites[1];
            SavePet();
            petPopUp.SetActive(false);
            backing.SetActive(false);
        }
    }

    public void equipCatTwo(){
        if (petsOwned[1] == 1){
            petEquipped = 1;
            LeftSprites = leftLoader[petEquipped];
            RightSprites = rightLoader[petEquipped];
            UpSprites = upLoader[petEquipped];
            DownSprites = downLoader[petEquipped];
            selected_pet_image.sprite = LeftSprites[1];
            SavePet();
            petPopUp.SetActive(false);
            backing.SetActive(false);
        }
    }

    public void equipCatThree(){
        if (petsOwned[2] == 1){
            petEquipped = 2;
            LeftSprites = leftLoader[petEquipped];
            RightSprites = rightLoader[petEquipped];
            UpSprites = upLoader[petEquipped];
            DownSprites = downLoader[petEquipped];
            selected_pet_image.sprite = LeftSprites[1];
            SavePet();
            petPopUp.SetActive(false);
            backing.SetActive(false);
        }
    }

    public void equipFoxOne(){
        if (petsOwned[3] == 1){
            petEquipped = 3;
            LeftSprites = leftLoader[petEquipped];
            RightSprites = rightLoader[petEquipped];
            UpSprites = upLoader[petEquipped];
            DownSprites = downLoader[petEquipped];
            selected_pet_image.sprite = LeftSprites[1];
            SavePet();
            petPopUp.SetActive(false);
            backing.SetActive(false);
        }
    }

    public void equipFoxTwo(){
        if (petsOwned[4] == 1){
            petEquipped = 4;
            LeftSprites = leftLoader[petEquipped];
            RightSprites = rightLoader[petEquipped];
            UpSprites = upLoader[petEquipped];
            DownSprites = downLoader[petEquipped];
            selected_pet_image.sprite = LeftSprites[1];
            SavePet();
            petPopUp.SetActive(false);
            backing.SetActive(false);
        }
    }

    public void equipPenguin(){
        if (petsOwned[5] == 1){
            petEquipped = 5;
            LeftSprites = leftLoader[petEquipped];
            RightSprites = rightLoader[petEquipped];
            UpSprites = upLoader[petEquipped];
            DownSprites = downLoader[petEquipped];
            selected_pet_image.sprite = LeftSprites[1];
            SavePet();
            petPopUp.SetActive(false);
            backing.SetActive(false);
        }
    }

    public void equipLavaPenguin(){
        if (petsOwned[6] == 1){
            petEquipped = 6;
            LeftSprites = leftLoader[petEquipped];
            RightSprites = rightLoader[petEquipped];
            UpSprites = upLoader[petEquipped];
            DownSprites = downLoader[petEquipped];
            selected_pet_image.sprite = LeftSprites[1];
            SavePet();
            petPopUp.SetActive(false);
            backing.SetActive(false);
        }
    }

    public void equipPurpleFox(){
        if (petsOwned[7] == 1){
            petEquipped = 7;
            LeftSprites = leftLoader[petEquipped];
            RightSprites = rightLoader[petEquipped];
            UpSprites = upLoader[petEquipped];
            DownSprites = downLoader[petEquipped];
            selected_pet_image.sprite = LeftSprites[1];
            SavePet();
            petPopUp.SetActive(false);
            backing.SetActive(false);
        }
    }

    public void equipPet(int x){
        if (petsOwned[x] == 1){
            petEquipped = x;
            LeftSprites = leftLoader[petEquipped];
            RightSprites = rightLoader[petEquipped];
            UpSprites = upLoader[petEquipped];
            DownSprites = downLoader[petEquipped];
            selected_pet_image.sprite = LeftSprites[1];
            SavePet();
            petPopUp.SetActive(false);
            backing.SetActive(false);
        }
    }

    public void SavePet(){
        PetHolderClass petHolder = new PetHolderClass{
            petOwned = petsOwned,
            petEquipped = petEquipped,
        };
        string jsonStorage = JsonUtility.ToJson(petHolder);
        SaveSystem.SavePet(jsonStorage);
    }

    public void LoadPet(){
        string saveString = SaveSystem.LoadPet();
        if (saveString != null){
            PetHolderClass loadedData = JsonUtility.FromJson<PetHolderClass>(saveString);
            petsOwned = loadedData.petOwned;
            petEquipped = loadedData.petEquipped;
        }
    }

}

public class PetHolderClass
{
    public int[] petOwned = {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
    public int petEquipped = -1;
}
