using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// State machine States
/// </summary>
public enum GameStates
{
    MainMenuState,
    GameplayState,
    ShopState,
    GameOverState
}

public class GameManager : Singleton<GameManager>
{

    private StateMachine stateMachine;


    /// <summary>
    /// no of shop items per category 
    /// </summary>
    [SerializeField]
    private int itemsPerCategory;
    public int ItemsPerCategory { get { return itemsPerCategory; } }


    /// <summary>
    /// List of all Shop items
    /// </summary>
    [SerializeField]
    private ShopItemScriptableObj[] shopItems;

    public ShopItemScriptableObj[] ShopItems { get { return shopItems; } }

    private void Start()
    {
        stateMachine = new StateMachine();
    }


    private void Update()
    {
        stateMachine?.Update();
    }



    public BaseGameState GetCurrentState()
    {
        return stateMachine.GetCurrentState();
    }


    public void PushState(GameStates state, bool popCurrentState = false)
    {
        stateMachine.PushState(state, popCurrentState);
    }

    public void PopState(bool resumeCurrentState = true)
    {
        stateMachine.PopState(resumeCurrentState);
    }
    /// <summary>
    /// State Machine to control the game loop
    /// </summary>
    #region StateMachine
    private class StateMachine
    {

        private Stack<BaseGameState> stateStack = new Stack<BaseGameState>();
        #region States

        private BaseGameState gameplayState = new GameplayState();
        private BaseGameState mainMenuState = new MainMenuState();
        private BaseGameState shopState = new ShopState();
        private BaseGameState gameOverState = new GameOverState();
        #endregion

        public StateMachine()
        {
            PushState(GameStates.MainMenuState);
        }

        public void Update()
        {
            stateStack?.Peek()?.Update();
        }



        /// <summary>
        /// Push new State in machine
        /// </summary>
        /// <param name="gameState"> Next State</param>
        /// <param name="popCurrentState">whether to stack new state or replace current state</param>
        public void PushState(GameStates gameState, bool popCurrentState = false)
        {
            if (stateStack.Count > 0)
            {
                if (popCurrentState)
                {
                    PopState(false);
                }
                else
                {
                    stateStack.Peek()?.SuspendState();
                }
            }
            switch (gameState)
            {
                case GameStates.MainMenuState:
                    stateStack.Push(mainMenuState);
                    break;
                case GameStates.GameplayState:
                    stateStack.Push(gameplayState);
                    break;
                case GameStates.ShopState:
                    stateStack.Push(shopState);
                    break;
                case GameStates.GameOverState:
                    stateStack.Push(gameOverState);
                    break;
            }

            stateStack.Peek()?.EnterState();
        }

        /// <summary>
        /// Pop State
        /// </summary>
        /// <param name="resumeCurrentState"> Whether to resume if previous state was suspended</param>

        public void PopState(bool resumeCurrentState = true)
        {
            if (stateStack.Count > 0)
            {
                stateStack.Pop()?.ExitState(); //Exit of Previous State
            }


            if (stateStack.Count > 0 && resumeCurrentState)
            {
                stateStack.Peek()?.ResumeState(); // Resume of current State
            }
        }

        public BaseGameState GetCurrentState()
        {
            return stateStack.Peek();
        }
    }
    #endregion
}
