using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : BaseScoreController
{
    private int currentWaveCount = 0;
    private int currentComboCount;
    private float waveScore = 0f;

    private bool comboActive = false;

    private void UpdateWaveCount(int waveCount)
    {
        comboActive = true;

        currentComboCount = 0;
        waveScore = 0f;
        currentWaveCount = waveCount;
    }

    private void CompliteCombo(int points)
    {
        score += points;
        GameEvents.OnRefreshPointsCounter.Invoke(score);
        GameEvents.OnComboComplited.Invoke(points);
    }

    private void HandleComboChecking(int points)
    {
        waveScore += points;
        currentComboCount++;
        Debug.Log("Current combo count: " + currentComboCount);

        if (currentComboCount >= currentWaveCount)
        {
            CompliteCombo(points);
        }
    }

    public override void IncreaseScore(Vector2 rockPos, int points)
    {
        base.IncreaseScore(rockPos, points);
        if(comboActive)HandleComboChecking(points);
    }

    private void EndComboWindow()
    {
        comboActive = false;
    }

    protected override void SubscribeEvents()
    {
        base.SubscribeEvents();
        GameEvents.OnNewComboWave += UpdateWaveCount;
        GameEvents.OnNonComboWave += EndComboWindow;
    }

    protected override void UnsubscribeEvents()
    {
        base.UnsubscribeEvents();
        GameEvents.OnNewComboWave -= UpdateWaveCount;
        GameEvents.OnNonComboWave -= EndComboWindow;
    }
}
