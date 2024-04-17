using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    private StateMachine stateMachine;

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


    #region StateMachine
    private class StateMachine
    {
        public enum GameStates
        {
            MainMenuState,
            GameplayState,
            PauseState,
            GameOverState
        }


        private Stack<BaseGameState> stateStack = new Stack<BaseGameState>();

        private BaseGameState gameplayState = new GameplayState();
        private BaseGameState mainMenuState = new MainMenuState();
        public StateMachine()
        {
            PushState(GameStates.GameplayState);
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
                case GameStates.PauseState:
                    //stateStack.Push(pauseState);
                    break;
                case GameStates.GameOverState:
                    //stateStack.Push(gameOverState);
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
