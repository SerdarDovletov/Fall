using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.SocialPlatforms;

public class IAPManager : MonoBehaviour, IStoreListener
{
    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider;
    
    public PlayerController pc;
    public GameController gc;
    
    public static string Coin400 = "coins400";
    public static string Coin1000 = "coins1000";
    public static string Coin5000 = "coins5000";
    public static string Coin10000 = "coins10000";
    public static string noAds = "noadsanddoublecoins";

    void Start()
    {
        if (m_StoreController == null) InitializePurchasing();
    }

    public void InitializePurchasing()
    {
        if (IsInitialized()) return;

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(Coin400, ProductType.Consumable);
        builder.AddProduct(Coin1000, ProductType.Consumable);
        builder.AddProduct(Coin5000, ProductType.Consumable);
        builder.AddProduct(Coin10000, ProductType.Consumable);
        builder.AddProduct(noAds, ProductType.NonConsumable);
        UnityPurchasing.Initialize(this, builder);
    }

    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }


    public void Buy400Coin()
    {
        if (GameController.instance.audioOn) GameController.instance.Select.Play();
        BuyProductID(Coin400);
    }
    public void Buy1000Coin()
    {
        if (GameController.instance.audioOn) GameController.instance.Select.Play();
        BuyProductID(Coin1000);
    }
    public void Buy5000Coin()
    {
        if (GameController.instance.audioOn) GameController.instance.Select.Play();
        BuyProductID(Coin5000);
    }
    public void Buy10000Coin()
    {
        if (GameController.instance.audioOn) GameController.instance.Select.Play();
        BuyProductID(Coin10000);
    }

    public void BuyNoAds()
    {
        if (GameController.instance.audioOn) GameController.instance.Select.Play();
        BuyProductID(noAds);
    }

    void BuyProductID(string productId)
    {
        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(productId);
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                m_StoreController.InitiatePurchase(product);
            }
            else Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
        }
        else Debug.Log("BuyProductID FAIL. Not initialized.");
    }
    public void RestorePurchases()
    {
        if (!IsInitialized())
        {
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            Debug.Log("RestorePurchases started ...");
            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            apple.RestoreTransactions((result) => {
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }
        else Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("OnInitialized: PASS");
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }


    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (String.Equals(args.purchasedProduct.definition.id, Coin400, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            gc.Purchased400Coins();
        }
        if (String.Equals(args.purchasedProduct.definition.id, Coin1000, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            gc.Purchased1000Coins();
        }
        if (String.Equals(args.purchasedProduct.definition.id, Coin5000, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            gc.Purchased5000Coins();
        }
        if (String.Equals(args.purchasedProduct.definition.id, Coin10000, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            gc.Purchased10000Coins();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, noAds, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            gc.PurchasedNoAdsAndDoubleCoins();
        }
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
}