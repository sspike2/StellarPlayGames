using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class ShopIcon : MonoBehaviour
{

    [SerializeField]
    private Image iconImg;

    [SerializeField]
    private TextMeshProUGUI costText;

    [SerializeField]
    private Button purchaseButton;

    ShopScreen shopScreen;

    private void Awake()
    {
        shopScreen = GetComponentInParent<ShopScreen>();
    }

    public void Purchase()
    {
        shopScreen.Purchase(this);
    }
   
    /// <summary>
    /// Setup button icons and images
    /// </summary>
    /// <param name="shopItemScriptableObj"></param>
    /// <param name="index"></param>
    public void SetupIcon(ShopItemScriptableObj shopItemScriptableObj, int index)
    {
        iconImg.sprite = shopItemScriptableObj.choiceIcon;
        if (SaveManager.Instance.ShopUnlocks[index])
        {
            foreach (var item in SaveManager.Instance.CurrentEquippedItems)
            {
                if (item.Value == index)
                {
                    costText.text = "Equipped";
                    purchaseButton.interactable = false;
                    return;
                }
            }

            costText.text = "Equip";
            purchaseButton.interactable = true;
        }
        else
        {
            costText.text = shopItemScriptableObj.cost.ToString();
            if (SaveManager.Instance.Coins > shopItemScriptableObj.cost)
            {
                purchaseButton.interactable = true;
            }
            else
            {
                purchaseButton.interactable = false;
            }
        }
    }
}
