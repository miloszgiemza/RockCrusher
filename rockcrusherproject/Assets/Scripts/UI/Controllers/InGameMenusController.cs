using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenusController : MonoBehaviour
{
    private bool settingsMenuInitialised = false;
    private bool pauseMenuInitialised = true;

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void Start()
    {
        StartCoroutine(SignalThatAllMenusAreInitialised());
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void MarkSettingsMenuAsInitialised()
    {
        settingsMenuInitialised = true;
    }

    private IEnumerator SignalThatAllMenusAreInitialised()
    {
        yield return new WaitUntil(() => settingsMenuInitialised && pauseMenuInitialised);
        GameEvents.OnAllInGameMenusInitialised.Invoke();
    }

    private void SubscribeEvents()
    {
        GameEvents.OnMenuSettingsInitilised += MarkSettingsMenuAsInitialised;
    }

    private void UnsubscribeEvents()
    {
        GameEvents.OnMenuSettingsInitilised -= MarkSettingsMenuAsInitialised;
    }
}
