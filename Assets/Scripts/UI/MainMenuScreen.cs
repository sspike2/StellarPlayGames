using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MainMenuScreen : UIScreenBase
{
    private int cheatButtonClick = 0;
    public override void Initialize()
    {
        base.Initialize();
    }

    public void Start()
    {

    }
    public void Play()
    {
        GameManager.Instance.PushState(GameStates.GameplayState);
    }

    public void Shop()
    {
        GameManager.Instance.PushState(GameStates.ShopState);
    }

    public void ShopCoinCheatButton()
    {
        cheatButtonClick++;
        if (cheatButtonClick > 5)
        {
            SaveManager.Instance.CheatCoinsAdd();
            cheatButtonClick = 0;
        }
    }




}
