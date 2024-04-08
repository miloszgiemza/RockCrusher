using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseScoreController : MonoBehaviour
{
    public int Score => score;

    protected int score = 0;

    protected virtual void OnEnable()
    {
        SubscribeEvents();
    }

    protected virtual void OnDisable()
    {
        UnsubscribeEvents();
    }

     public virtual void IncreaseScore(Vector2 rockPos, int points)
    {
        score += points;
        GameEvents.OnRefreshPointsCounter.Invoke(score);
    }

    protected virtual void SubscribeEvents()
    {
        GameEvents.OnPointsGainedFromShatter += IncreaseScore;
    }

    protected virtual void UnsubscribeEvents()
    {
        GameEvents.OnPointsGainedFromShatter -= IncreaseScore;
    }
}
