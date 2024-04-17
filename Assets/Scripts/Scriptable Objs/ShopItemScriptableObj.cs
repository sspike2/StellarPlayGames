using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItem", menuName = "Scriptable Obj/Shop Item", order = 1)]
public class ShopItemScriptableObj : ScriptableObject
{
    public Sprite choiceIcon;
    public int cost;
}
