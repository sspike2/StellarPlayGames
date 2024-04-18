using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : BaseGameState
{
    public override void EnterState()
    {
        base.EnterState();
        SaveManager.Instance.UpdateCoins();
        UIManager.Instance.ChangeWindow(UIScreens.GameOverMenu);
    }
}
