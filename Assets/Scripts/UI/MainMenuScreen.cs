using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MainMenuScreen : UIScreenBase
{
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

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }


}
