using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum UIScreens
{
    MainMenu,
    Game,
    Shop,
    GameOverMenu
}
public class UIManager : Singleton<UIManager>
{
    private UIScreenBase activeScreen;
    [Header("UI Screens")]
    [SerializeField]
    private UIScreenBase MenuMenuScreen;
    [SerializeField]
    private UIScreenBase GameScreen;
    [SerializeField]
    private UIScreenBase ShopScreen;
    [SerializeField]
    private UIScreenBase GameOverScreen;


    public void ChangeWindow(UIScreens screenToLoad)
    {
        activeScreen?.gameObject.SetActive(false);

        switch (screenToLoad)
        {
            case UIScreens.MainMenu:
                activeScreen = MenuMenuScreen;
                break;
            case UIScreens.Game:
                activeScreen = GameScreen;
                break;
            case UIScreens.Shop:
                activeScreen = ShopScreen;
                break;
            case UIScreens.GameOverMenu:
                activeScreen = GameOverScreen;
                break;
        }
        activeScreen.Initialize();
        activeScreen?.gameObject.SetActive(true);
    }
}
