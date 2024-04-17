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
    public void SetupIcon(ShopItemScriptableObj shopItemScriptableObj, int index)
    {
        iconImg.sprite = shopItemScriptableObj.choiceIcon;
        if (SaveManager.Instance.ShopUnlocks[index])
        {
            if (SaveManager.Instance.CurrentEquippedItems.Contains(index))
            {
                costText.text = "Equipped";
                purchaseButton.enabled = false;
                return;
            }
            costText.text = "Equip";
            purchaseButton.enabled = true;
        }
        else
        {
            costText.text = shopItemScriptableObj.cost.ToString();
            if (SaveManager.Instance.Coins > shopItemScriptableObj.cost)
            {
                purchaseButton.enabled = true;
            }
            else
            {
                purchaseButton.enabled = false;
            }
        }
    }
}
