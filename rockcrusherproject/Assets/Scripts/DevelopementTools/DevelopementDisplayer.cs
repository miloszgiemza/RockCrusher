using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DevelopementDisplayer : MonoBehaviour
{
    private TextMeshProUGUI displayedText;

    private void Awake()
    {
        displayedText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void DisplayDevelopomentMessage(string messsageText)
    {
        displayedText.text = messsageText;
    }

    private void SubscribeEvents()
    {
        GameEvents.OnDisplayDevelopementHelperMessage += DisplayDevelopomentMessage;
    }

    private void UnsubscribeEvents()
    {
        GameEvents.OnDisplayDevelopementHelperMessage -= DisplayDevelopomentMessage;
    }
}
