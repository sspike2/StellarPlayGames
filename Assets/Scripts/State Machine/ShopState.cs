using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopState : BaseGameState
{
    public override void EnterState()
    {
        base.EnterState();
        UIManager.Instance.ChangeWindow(UIScreens.Shop);
    }
}
