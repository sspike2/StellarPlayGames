using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{

    /// <summary>
    /// Event to Update Score
    /// </summary>
    public static event Action<bool> ScoreUpdated;
    public static void SendScoreUpdatedEvent(bool addScore)
    {
        ScoreUpdated?.Invoke(addScore);
    }

    /// <summary>
    /// Event to update Score UI
    /// </summary>
    public static Action<int> UpdateScoreUI;
    public static void SendUpdateScoreUI(int score)
    {
        UpdateScoreUI?.Invoke(score);
    }
}
