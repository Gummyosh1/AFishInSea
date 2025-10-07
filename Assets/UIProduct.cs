using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;
using Product = UnityEngine.Purchasing.Product;

public class UIProduct : MonoBehaviour
{
    public Button PurchaseButton;
    public delegate void PurchaseEvent(Product Model, Action OnComplete);
    public event PurchaseEvent OnPurchase;
    private Product Model;

    public void Setup(Product product)
    {
        Model = product;
    }

    public void Purchase()
    {
        PurchaseButton.enabled = false;
        Debug.Log("1");
        OnPurchase?.Invoke(Model, HandlePurchaseComplete);
        Debug.Log("2");
    }

    private void HandlePurchaseComplete()
    {
        PurchaseButton.enabled = true;
    }
}
