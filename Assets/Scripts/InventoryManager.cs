using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class InventoryManager : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler
{
    public Transform[] inventorySlots;
    public TMP_Text[] itemQuantities;
    public InventoryKing inventoryKing;
    private Vector3 touchPosition;
    private int startSlot = 0;
    private float downTimer = 0;
    private bool touched = false;
    private bool dragging = false;
    public Camera cameraUI;
    private GameObject newInventorySlot;
    private GameObject original;
    private float timer = 0;
    private float cutoff = .15f;

    //RAYCAST VARS
    private GraphicRaycaster m_Raycaster;
    private PointerEventData m_PointerEventData;
    private RectTransform canvasRect;

    public void Update(){
        if (touched){
            if (Input.touchCount > 0 || Input.GetMouseButton(0)){
                downTimer += Time.deltaTime;
                if (downTimer > cutoff){
                    dragging = true;
                    inventoryKing.startDrag();
                }
            }
            else{
                downTimer = 0;
                dragging = false;
                inventoryKing.endDrag();
                touched = false;
                if (newInventorySlot != null && original.transform.childCount > 0){
                    Debug.Log("Square right before evaluation is " + inventoryKing.getSquare());
                    Debug.Log("The newInventorySlot.name being parsed is: " + newInventorySlot.name);
                    int parsed_new_slot;
                    int parsed_old_slot;
                    bool sell_slot_in_play = false;
                    if (newInventorySlot.name != "Sell" && newInventorySlot.name != "FishDonate" && newInventorySlot.name != "Seagull") {parsed_new_slot = int.Parse(newInventorySlot.name);}
                    else if (newInventorySlot.name == "FishDonate"){parsed_new_slot = inventorySlots.Length - 1;}
                    else{parsed_new_slot = 12; sell_slot_in_play = true;}
                    
                    if (original.name != "Sell" && original.name != "FishDonate" && original.name != "Seagull"){parsed_old_slot = int.Parse(original.name);}
                    else if (original.name == "FishDonate"){parsed_old_slot = inventorySlots.Length - 1;}
                    else{parsed_old_slot = 12; sell_slot_in_play = true;}
                    

                    //Checks to see if the fish didn't move squares at all
                    if (newInventorySlot == original){
                        Debug.Log("Back to OG");
                        transform.GetChild(0).transform.position = original.transform.position;
                    }

                    //checks to see if the user is trying to access inventory slots they can't
                    else if (!inventoryKing.getBattlePass() && inventoryKing.getSquare() && int.TryParse(newInventorySlot.name, out int hold) && parsed_new_slot > 3 && parsed_new_slot < 12){
                            Debug.Log("Trying to go into Battle Pass slot without BP!");
                            transform.GetChild(0).transform.SetParent(original.transform);
                            transform.GetChild(0).transform.position = original.transform.position;
                    }

                    //checks to see if the user wants to swap fish with another slot
                    else if(inventoryKing.getSwap() && inventoryKing.getSquare()){
                        // checks to see if we can stack the two fishies
                        if (int.Parse(itemQuantities[parsed_new_slot].text) < 9
                        && newInventorySlot.transform.GetChild(0).gameObject.GetComponent<Image>().sprite == original.transform.GetChild(0).gameObject.GetComponent<Image>().sprite)
                        {     
                            // if the square you're swapping to has less than 9 items, it might be stackable.         
                            // If the item is the same, stack the items instead of swapping. We will cap stacks of fish at 9
                            int new_total = int.Parse(itemQuantities[parsed_new_slot].text) + int.Parse(itemQuantities[parsed_old_slot].text);
                            if (new_total > 9){ // if it's stackable but the total exceeds 9, we have to send some fish back to the OG square
                                int old_slot_new_num = new_total - 9;
                                new_total = 9;
                                itemQuantities[parsed_new_slot].text = new_total.ToString();
                                itemQuantities[parsed_old_slot].text = old_slot_new_num.ToString();
                                newInventorySlot.transform.GetChild(0).transform.SetParent(original.transform);
                                transform.GetChild(0).transform.SetParent(newInventorySlot.transform);
                            }
                            else{ // if it's stackable but the total is under 9, we gotta delete the OG fish.
                                itemQuantities[parsed_new_slot].text = new_total.ToString();
                                itemQuantities[parsed_old_slot].text = "0";
                                itemQuantities[parsed_old_slot].gameObject.SetActive(false);
                                newInventorySlot.transform.GetChild(0).transform.SetParent(original.transform);
                                transform.GetChild(0).transform.SetParent(newInventorySlot.transform);
                                Destroy(original.transform.GetChild(0).gameObject);
                            }
                            

                        }
                        else{ // else we swap the fish since they're not stackable with each other
                            Debug.Log("Swap");
                            int newNum;
                            newNum = int.Parse(itemQuantities[parsed_new_slot].text);
                            itemQuantities[parsed_new_slot].text = itemQuantities[parsed_old_slot].text;
                            itemQuantities[parsed_old_slot].text = newNum.ToString();
                            newInventorySlot.transform.GetChild(0).transform.SetParent(original.transform);
                            transform.GetChild(0).transform.SetParent(newInventorySlot.transform);
                        }
                    }

                    //checks to see if the user dragged the fish into oblivion
                    else if (!inventoryKing.getSquare()){
                        Debug.Log("Out of Bounds");
                        transform.GetChild(0).transform.SetParent(original.transform);
                        transform.GetChild(0).transform.position = original.transform.position;
                    }

                    //sends the fish to the desired inventory slot if no other conditions are met!
                    else{
                        Debug.Log("Standard");
                        itemQuantities[parsed_new_slot].text = itemQuantities[parsed_old_slot].text;
                        itemQuantities[parsed_new_slot].gameObject.SetActive(true);
                        itemQuantities[parsed_old_slot].text = "0";
                        itemQuantities[parsed_old_slot].gameObject.SetActive(false);

                        transform.GetChild(0).transform.SetParent(newInventorySlot.transform);
                        
                    }
                    bool invenDonate = false;
                    bool freeDonate = false;

                    if ((parsed_new_slot == 12 && inventorySlots.Length - 1 == parsed_new_slot) || 
                    (parsed_old_slot == 12 && inventorySlots.Length - 1 == parsed_old_slot)){invenDonate = true;}

                    if ((parsed_new_slot == 28 && inventorySlots.Length - 1 == parsed_new_slot) || 
                    (parsed_old_slot == 28 && inventorySlots.Length - 1 == parsed_old_slot)){freeDonate = true;}

                    if ((parsed_new_slot >= 0 && parsed_new_slot < 12) || (parsed_old_slot >= 0 && parsed_old_slot < 12) || sell_slot_in_play || invenDonate) {inventoryKing.SaveInventory(); Debug.Log("Saving the Inventory Now!");}
                    if ((((parsed_new_slot > 11 && parsed_new_slot < 28) || (parsed_old_slot > 11 && parsed_old_slot < 28)) && !sell_slot_in_play && !invenDonate) || freeDonate) {Debug.Log("Saving the FreeChest Now!");}
                    if ((parsed_new_slot > 27 || parsed_old_slot > 27) && !freeDonate){Debug.Log("Saving the PaidChest Now!");}

                }
            }
            if (dragging){
                if (Input.touchCount > 0){
                    Touch touch = Input.GetTouch(0);
                    touchPosition = cameraUI.ScreenToWorldPoint(touch.position);
                    touchPosition = new Vector3 (touchPosition.x,touchPosition.y,1040);
                }
                else{
                    touchPosition = cameraUI.ScreenToWorldPoint(Input.mousePosition);
                    touchPosition = new Vector3 (touchPosition.x,touchPosition.y,1040);
                }
                
                if (inventorySlots[startSlot].childCount == 1){
                    inventorySlots[startSlot].GetChild(0).transform.position = touchPosition;
                    if (inventoryKing.getNewInventorySlot() != null){
                        if (inventoryKing.getNewInventorySlot().gameObject.transform.childCount != 0){
                            newInventorySlot = inventoryKing.getNewInventorySlot();
                            inventoryKing.setSwapTrue();
                        }
                        else{
                            newInventorySlot = inventoryKing.getNewInventorySlot();
                            inventoryKing.setSwapFalse();
                        }
                    }
                }
            }


        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        int.TryParse(gameObject.name, out startSlot);
        if (gameObject.name == "Sell")
        {
            startSlot = 12;
        }
        if (gameObject.name == "Seagull")
        {
            startSlot = 12;
        }
        if (gameObject.name == "FishDonate")
        {
            startSlot = inventorySlots.Length - 1;
        }
        touched = true;
        inventoryKing.inventorySlotTag(gameObject);
        original = gameObject;
        newInventorySlot = original;

        timer = Time.time;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        inventoryKing.inventorySlotTag(gameObject);
        //Debug.Log("Pointer is in " + gameObject.name);
        //Debug.Log(inventoryKing.getSquare());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (Time.time - timer <= cutoff)
        {
            //tap for if the user is sending items to the donate box
            if (inventoryKing.TheOne && gameObject.name != "FishDonate" && transform.childCount > 0)
            {
                GameObject ours = gameObject;
                int parsed_old_slot = int.Parse(gameObject.name);
                bool swap = false;
                GameObject newInventorySlotLocal = inventorySlots[12].gameObject;
                //checks to see if the user wants to swap fish with another slot

                if (transform.childCount == 1)
                {
                    if (newInventorySlotLocal.transform.childCount != 0)
                    {
                        swap = true;
                    }
                    else
                    {
                        swap = false;
                    }
                }

                if (swap)
                {
                    // checks to see if we can stack the two fishies
                    if (int.Parse(itemQuantities[12].text) < 9
                    && newInventorySlotLocal.transform.GetChild(0).gameObject.GetComponent<Image>().sprite == ours.transform.GetChild(0).gameObject.GetComponent<Image>().sprite)
                    {
                        // if the square you're swapping to has less than 9 items, it might be stackable.         
                        // If the item is the same, stack the items instead of swapping. We will cap stacks of fish at 9

                        int new_total = int.Parse(itemQuantities[12].text) + int.Parse(itemQuantities[parsed_old_slot].text);
                        if (new_total > 9)
                        { // if it's stackable but the total exceeds 9, we have to send some fish back to the OG square
                            //Debug.Log("AHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH");
                            int old_slot_new_num = new_total - 9;

                            Debug.Log("OLD SELL Q = " + itemQuantities[12].text);
                            itemQuantities[12].text = "9";
                            Debug.Log("NEW SELL Q = " + itemQuantities[12].text);

                            Debug.Log("OLD SLOT Q = " + itemQuantities[parsed_old_slot].text);
                            itemQuantities[parsed_old_slot].text = old_slot_new_num.ToString();
                            Debug.Log("NEW SLOT Q = " + itemQuantities[parsed_old_slot].text);
                            //newInventorySlot.transform.GetChild(0).transform.SetParent(ours.transform);
                            //transform.GetChild(0).transform.SetParent(newInventorySlot.transform);
                        }
                        else
                        { // if it's stackable but the total is under 9, we gotta delete the OG fish.
                            itemQuantities[12].text = new_total.ToString();
                            itemQuantities[parsed_old_slot].text = "0";
                            itemQuantities[parsed_old_slot].gameObject.SetActive(false);
                            //newInventorySlot.transform.GetChild(0).transform.SetParent(ours.transform);
                            //transform.GetChild(0).transform.SetParent(newInventorySlot.transform);
                            //Debug.Log("About to kill " + (ours.name));
                            Destroy(ours.transform.GetChild(0).gameObject);
                        }


                    }
                    else
                    { // else we swap the fish since they're not stackable with each other
                        Debug.Log("Swap");
                        int newNum;
                        newNum = int.Parse(itemQuantities[12].text);
                        itemQuantities[12].text = itemQuantities[parsed_old_slot].text;
                        itemQuantities[parsed_old_slot].text = newNum.ToString();
                        newInventorySlotLocal.transform.GetChild(0).transform.SetParent(ours.transform);
                        transform.GetChild(0).transform.SetParent(newInventorySlotLocal.transform);
                    }
                }

                //sends the fish to the desired inventory slot if no other conditions are met!
                else
                {
                    Debug.Log("Standard");
                    itemQuantities[12].text = itemQuantities[parsed_old_slot].text;
                    itemQuantities[12].gameObject.SetActive(true);
                    itemQuantities[parsed_old_slot].text = "0";
                    itemQuantities[parsed_old_slot].gameObject.SetActive(false);

                    transform.GetChild(0).transform.SetParent(newInventorySlotLocal.transform);

                }

                inventoryKing.SaveInventory();
            }


            //taps for fish box -> inventory
            if (inventoryKing.TheOne && gameObject.name == "FishDonate")
            {
                inventoryKing.leaveSellScreen();
                inventoryKing.SaveInventory();
                /*
                GameObject ours = gameObject;
                int parsed_old_slot = int.Parse(gameObject.name);
                bool swap = false;
                GameObject newInventorySlotLocal = inventorySlots[12].gameObject;
                //checks to see if the user wants to swap fish with another slot

                if (inventorySlots[startSlot].childCount == 1)
                {
                    if (newInventorySlotLocal.transform.childCount != 0)
                    {
                        swap = true;
                    }
                    else
                    {
                        swap = false;
                    }
                }

                if (swap)
                {
                    // checks to see if we can stack the two fishies
                    if (int.Parse(itemQuantities[12].text) < 9
                    && newInventorySlotLocal.transform.GetChild(0).gameObject.GetComponent<Image>().sprite == ours.transform.GetChild(0).gameObject.GetComponent<Image>().sprite)
                    {
                        // if the square you're swapping to has less than 9 items, it might be stackable.         
                        // If the item is the same, stack the items instead of swapping. We will cap stacks of fish at 9

                        int new_total = int.Parse(itemQuantities[12].text) + int.Parse(itemQuantities[parsed_old_slot].text);
                        if (new_total > 9)
                        { // if it's stackable but the total exceeds 9, we have to send some fish back to the OG square
                            //Debug.Log("AHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH");
                            int old_slot_new_num = new_total - 9;

                            Debug.Log("OLD SELL Q = " + itemQuantities[12].text);
                            itemQuantities[12].text = "9";
                            Debug.Log("NEW SELL Q = " + itemQuantities[12].text);

                            Debug.Log("OLD SLOT Q = " + itemQuantities[parsed_old_slot].text);
                            itemQuantities[parsed_old_slot].text = old_slot_new_num.ToString();
                            Debug.Log("NEW SLOT Q = " + itemQuantities[parsed_old_slot].text);
                            //newInventorySlot.transform.GetChild(0).transform.SetParent(ours.transform);
                            //transform.GetChild(0).transform.SetParent(newInventorySlot.transform);
                        }
                        else
                        { // if it's stackable but the total is under 9, we gotta delete the OG fish.
                            itemQuantities[12].text = new_total.ToString();
                            itemQuantities[parsed_old_slot].text = "0";
                            itemQuantities[parsed_old_slot].gameObject.SetActive(false);
                            //newInventorySlot.transform.GetChild(0).transform.SetParent(ours.transform);
                            //transform.GetChild(0).transform.SetParent(newInventorySlot.transform);
                            //Debug.Log("About to kill " + (ours.name));
                            Destroy(ours.transform.GetChild(0).gameObject);
                        }


                    }
                    else
                    { // else we swap the fish since they're not stackable with each other
                        Debug.Log("Swap");
                        int newNum;
                        newNum = int.Parse(itemQuantities[12].text);
                        itemQuantities[12].text = itemQuantities[parsed_old_slot].text;
                        itemQuantities[parsed_old_slot].text = newNum.ToString();
                        newInventorySlotLocal.transform.GetChild(0).transform.SetParent(ours.transform);
                        transform.GetChild(0).transform.SetParent(newInventorySlotLocal.transform);
                    }
                }

                //sends the fish to the desired inventory slot if no other conditions are met!
                else
                {
                    Debug.Log("Standard");
                    itemQuantities[12].text = itemQuantities[parsed_old_slot].text;
                    itemQuantities[12].gameObject.SetActive(true);
                    itemQuantities[parsed_old_slot].text = "0";
                    itemQuantities[parsed_old_slot].gameObject.SetActive(false);

                    transform.GetChild(0).transform.SetParent(newInventorySlotLocal.transform);

                }
            }*/
            }

            
            timer = 0;

        }
        // CODE TO MAKE NEW RAYCAST SYSTEM

        m_PointerEventData = eventData;
        //Set the Pointer Event Position to that of the game object
        //m_PointerEventData.position = this.transform.localPosition; DONT THINK I NEED THIS
 
        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();
 
        //Raycast using the Graphics Raycaster and mouse click position
        EventSystem.current.RaycastAll(m_PointerEventData, results);
        int x = 0;
        foreach (RaycastResult i in results){
            x++;
            break;
        }

        if (x > 0){
        Debug.Log(results[0].gameObject.name);
 
        if (results[0].gameObject.name.Contains("0") 
        || results[0].gameObject.name.Contains("1")
        || results[0].gameObject.name.Contains("2")
        || results[0].gameObject.name.Contains("3")
        || results[0].gameObject.name.Contains("4")
        || results[0].gameObject.name.Contains("5")
        || results[0].gameObject.name.Contains("6")
        || results[0].gameObject.name.Contains("7")
        || results[0].gameObject.name.Contains("8")
        || results[0].gameObject.name.Contains("9")
        || results[0].gameObject.name.Contains("10")
        || results[0].gameObject.name.Contains("11")
        || results[0].gameObject.name.Contains("Sell")
        || results[0].gameObject.name.Contains("12")
        || results[0].gameObject.name.Contains("13")
        || results[0].gameObject.name.Contains("14")
        || results[0].gameObject.name.Contains("15")
        || results[0].gameObject.name.Contains("FishDonate")
        || results[0].gameObject.name.Contains("Seagull")

        ){
            inventoryKing.enteredSquare();
        }
        else{
            inventoryKing.leftSquare();
        }
        }
        else{
            inventoryKing.leftSquare();
        }

        //END
    }
}
