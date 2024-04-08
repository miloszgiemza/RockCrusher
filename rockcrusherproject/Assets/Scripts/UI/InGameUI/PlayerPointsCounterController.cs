using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerPointsCounterController : MonoBehaviour
{
    private TextMeshProUGUI counterText;

    private void Awake()
    {
        counterText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void SubscribeEvents()
    {
        GameEvents.OnRefreshPointsCounter += RefreshCounter;
    }

    private void UnsubscribeEvents()
    {
        GameEvents.OnRefreshPointsCounter -= RefreshCounter;
    }

    private void RefreshCounter(int points)
    {
        counterText.text = points.ToString();
    }
}
