using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class RarityUpdater : MonoBehaviour
{
    public Image[] slots;
    public InventoryKing inventoryKing;
    public Color normal;
    public Color fancy;
    public Color extravagant;
    public Color pristine;
    public Color magical;
    public Color none;

    public void Update()
    {
        inventoryKing.nonSetupLoad();
        for (int i = 0; i < 12; i++)
        {
            if (inventoryKing.dataStorage[i][0] == 1)
            {
                switch (inventoryKing.dataStorage[i][1])
                {
                    case 0:
                        slots[i].color = normal;
                        break;
                    case 1:
                        slots[i].color = fancy;
                        break;
                    case 2:
                        slots[i].color = extravagant;
                        break;
                    case 3:
                        slots[i].color = pristine;
                        break;
                    case 4:
                        slots[i].color = magical;
                        break;
                }
            }
            else
            {
                slots[i].color = none;
            }
        }
    }
}
