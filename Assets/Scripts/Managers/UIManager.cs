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
    private GameObject activeScreen;
    [Header("UI Screens")]
    [SerializeField]
    private GameObject MenuMenuScreen;
    [SerializeField]
    private GameObject GameScreen;
    [SerializeField]
    private GameObject ShopScreen;
    [SerializeField]
    private GameObject GameOverScreen;


    public void ChangeWindow(UIScreens screenToLoad)
    {
        activeScreen?.SetActive(false);

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
        activeScreen?.SetActive(true);
    }   
}
