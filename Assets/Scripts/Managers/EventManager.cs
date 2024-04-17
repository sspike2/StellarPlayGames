using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{

    public static event Action ScoreUpdated;
    public static void SendScoreUpdatedEvent()
    {
        ScoreUpdated?.Invoke();
    }

    public static event Action<int> HealthUpdated;

    public static void SendHealthUpdatedEvent(int currentHealth)
    {
        HealthUpdated?.Invoke(currentHealth);
    }
}
