using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayState : BaseGameState
{
    /// <summary>
    /// Results in 2D array
    /// </summary>
    private int[,] results = new int[,]
{
        // R   P   S   L   Sp
        {  0, -1,  1,  1, -1 }, // Rock
        {  1,  0, -1, -1,  1 }, // Paper
        { -1,  1,  0,  1, -1 }, // Scissors
        { -1,  1, -1,  0,  1 }, // Lizard
        {  1, -1,  1, -1,  0 }  // Spock
};

    public override void EnterState()
    {
        base.EnterState();
        UIManager.Instance.ChangeWindow(UIScreens.Game);
        EventManager.SendScoreUpdatedEvent(false);
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

    public int GetResult(int playerChoice, out int aiChoice)
    {
        // AI Choice
        aiChoice = Random.Range(0, results.GetLength(0));
        // return result
        return (results[playerChoice, aiChoice]);

    }
}
