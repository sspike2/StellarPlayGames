using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopScreen : UIScreenBase
{



    [SerializeField]
    private ShopIcon[] shopIcons;


    [SerializeField]
    private TextMeshProUGUI coinsText;



    public override void Initialize()
    {
        base.Initialize();
        SetupShop();

    }

    private void SetupShop()
    {
        for (int i = 0; i < shopIcons.Length; i++)
        {
            shopIcons[i].SetupIcon(GameManager.Instance.ShopItems[i], i);
        }
        UpdateCoinsText();
    }

    public void UpdateCoinsText()
    {
        coinsText.text = $"Coins: {SaveManager.Instance.Coins}";
    }
    /// <summary>
    /// Purchase new skin
    /// </summary>
    /// <param name="shopIcon"></param>
    public void Purchase(ShopIcon shopIcon)
    {

        int index = Array.IndexOf(shopIcons, shopIcon);

        ShopItemScriptableObj currentShopItem = GameManager.Instance.ShopItems[index];

        if (!SaveManager.Instance.ShopUnlocks[index])
        {
            SaveManager.Instance.ItemPurchased(index, currentShopItem.cost);
        }
        /// Change the current equipped item to selected one
        SaveManager.Instance.CurrentEquippedItems[currentShopItem.choice] = index;

        SaveManager.Instance.SaveShop();

        // Reset the shop
        SetupShop();
    }

    public void CloseShopButton()
    {
        GameManager.Instance.PopState();
    }
}
