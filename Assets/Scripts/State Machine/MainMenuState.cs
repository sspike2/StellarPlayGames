using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuState : BaseGameState
{
    public override void EnterState()
    {
        base.EnterState();
        UIManager.Instance.ChangeWindow(UIScreens.MainMenu);
    }

    public override void ResumeState()
    {
        base.ResumeState();
    }

    public override void SuspendState()
    {
        base.SuspendState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

}
