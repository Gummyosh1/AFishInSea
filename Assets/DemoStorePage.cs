using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

public class DemoStorePage : MonoBehaviour, IDetailedStoreListener
{
    public GemStorage gemStorage;
    public BattlePass battlePass;
    private IStoreController StoreController;
    private IExtensionProvider ExtensionProvider;
    public delegate void PurchaseEvent(Product Model, Action OnComplete);
    public StoreIconProvider storeIconProvider;
    public event PurchaseEvent OnPurchase;

    private Action OnPurchaseCompleted;

    private int TFGBought = 0;
    public GameObject[] TFGButtons;
    private Product Model;

    public UIProduct[] uiProducts;

    private async void Awake()
    {
        InitializationOptions options = new InitializationOptions()
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            .SetEnvironmentName("test");
#else
            .SetEnvironmentName("production");
#endif
        await UnityServices.InitializeAsync(options);
        ResourceRequest operation = Resources.LoadAsync<TextAsset>("IAPProductCatalog");
        operation.completed += HandleIAPCatalogLoaded;
    }

    private void HandleIAPCatalogLoaded(AsyncOperation Operation)
    {
        ResourceRequest request = Operation as ResourceRequest;

        Debug.Log($"Loaded Asset: {request.asset}");
        ProductCatalog catalog = JsonUtility.FromJson<ProductCatalog>((request.asset as TextAsset).text);
        Debug.Log($"Loaded catalog with {catalog.allProducts.Count} items");

#if UNITY_ANDROID
        ConfigurationBuilder builder = ConfigurationBuilder.Instance(
            StandardPurchasingModule.Instance(AppStore.GooglePlay)
        );

#elif UNITY_IOS
        ConfigurationBuilder builder = ConfigurationBuilder.Instance(
            StandardPurchasingModule.Instance(AppStore.AppleAppStore)
        );

#else
        ConfigurationBuilder builder = ConfigurationBuilder.Instance(
            StandardPurchasingModule.Instance(AppStore.NotSpecified) 
        );
#endif

        foreach (ProductCatalogItem item in catalog.allProducts)
        {
            builder.AddProduct(item.id, item.type);
        }

        UnityPurchasing.Initialize(this, builder);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        StoreController = controller;
        ExtensionProvider = extensions;
        CreateUI();
        //StoreIconProvider.Initialize(StoreController.products);
        //StoreIconProvider.OnLoadComplete += HandleAllIconsLoaded;



    }

    private void HandlePurchase(Product Product, Action OnPurchaseCompleted)
    {
        StoreController.InitiatePurchase(Product);
        this.OnPurchaseCompleted = OnPurchaseCompleted;
    }


    //private void HandleAllIconsLoaded()
    //{
    //  StartCoroutine(CreateUI());
    //}

    private void CreateUI()
    {
        UIProduct uIProduct;

        Product Gems100Setup = StoreController.products.WithID("gems100");
        uIProduct = uiProducts[0];
        uIProduct.OnPurchase += HandlePurchase;
        uIProduct.Setup(Gems100Setup);

        Product TFGSetup = StoreController.products.WithID("fisherman");
        uIProduct = uiProducts[1];
        uIProduct.OnPurchase += HandlePurchase;
        uIProduct.Setup(TFGSetup);

        Product Gems600Setup = StoreController.products.WithID("gems600");
        uIProduct = uiProducts[2];
        uIProduct.OnPurchase += HandlePurchase;
        uIProduct.Setup(Gems600Setup);

        Product Gems1300Setup = StoreController.products.WithID("gems1300");
        uIProduct = uiProducts[3];
        uIProduct.OnPurchase += HandlePurchase;
        uIProduct.Setup(Gems1300Setup);

        Product Gems4000Setup = StoreController.products.WithID("gems4000");
        uIProduct = uiProducts[4];
        uIProduct.OnPurchase += HandlePurchase;
        uIProduct.Setup(Gems4000Setup);
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.LogError($"Error initializing IAP because of {error}." + "$\r\nShow a message to the player depending on the error.");
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log($"Failed to purchase {product.definition.id} because {failureReason}");
        OnPurchaseCompleted?.Invoke();
        OnPurchaseCompleted = null;
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        Debug.Log($"Successfully purchased {purchaseEvent.purchasedProduct.definition.id}");
        OnPurchaseCompleted?.Invoke();
        OnPurchaseCompleted = null;

        //TODO HERE IS WHERE YOU GIVE THE PLAYER THEIR ITEMS
        if (purchaseEvent.purchasedProduct.definition.id == "fisherman")
        {
            battlePass.buyPass();
            TFGBought = 1;
            saveIAP();
            TFGInit();
        }
        else if (purchaseEvent.purchasedProduct.definition.id == "gems100")
        {
            gemStorage.buy100();
        }
        else if (purchaseEvent.purchasedProduct.definition.id == "gems600")
        {
            gemStorage.buy600();
        }
        else if (purchaseEvent.purchasedProduct.definition.id == "gems1300")
        {
            gemStorage.buy1300();
        }
        else if (purchaseEvent.purchasedProduct.definition.id == "gems4000")
        {
            gemStorage.buy4000();
        }
        

        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {
        uiProducts[0].PurchaseButton.enabled = true;
        uiProducts[1].PurchaseButton.enabled = true;
        uiProducts[2].PurchaseButton.enabled = true;
        uiProducts[3].PurchaseButton.enabled = true;
        uiProducts[4].PurchaseButton.enabled = true;
    }

    public void TFGInit()
    {
        if (TFGBought == 1)
        {
            TFGButtons[TFGBought].SetActive(true);
            TFGButtons[0].SetActive(false);
        }
        else
        {
            TFGButtons[TFGBought].SetActive(false);
            TFGButtons[0].SetActive(true);
        }

    }


    public void saveIAP()
    {
        IAPSaveClass IAPClassInstance = new IAPSaveClass
        {
            FishingPassBought = TFGBought,
        };
        string jsonStorage = JsonUtility.ToJson(IAPClassInstance);
        SaveSystem.SaveIAP(jsonStorage);
    }

    public void loadIAP()
    {
        string saveString = SaveSystem.LoadIAP();
        if (saveString != null)
        {
            IAPSaveClass loadedData = JsonUtility.FromJson<IAPSaveClass>(saveString);
            TFGBought = loadedData.FishingPassBought;
            TFGInit();
        }
        else
        {
            TFGBought = 0;
        }
    }
    



    public void RestorePurchases()
    {
            var apple = ExtensionProvider.GetExtension<IAppleExtensions>();
            apple.RestoreTransactions((result, message) =>
            {
                if (result)
                {
                    Debug.Log("✅ RestorePurchases completed successfully.");
                }
                else
                {
                    Debug.LogWarning($"⚠️ RestorePurchases failed or no purchases to restore. Message: {message}");
                }
            });
    }


    
}

public class IAPSaveClass
{
    public int FishingPassBought = 0;
}
