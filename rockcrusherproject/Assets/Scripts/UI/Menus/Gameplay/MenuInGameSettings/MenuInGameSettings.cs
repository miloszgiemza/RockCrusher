using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInGameSettings : UIManagerGameplay.BaseVisibleUIElement
{
    public override UIVisibleElementsGameplay ElementIdentifier => UIVisibleElementsGameplay.MenuSettings;

    private int audioSlidersInitialised = 0;

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void MarkNextAudioSliderAsInitialised()
    {
        audioSlidersInitialised++;

        if(audioSlidersInitialised >= Enum.GetNames(typeof(AudioMixerGroup)).Length)
        {
            GameEvents.OnMenuSettingsInitilised.Invoke();
        }
    }

    private void SubscribeEvents()
    {
        GameEvents.OnNextAudioSliderInitialised += MarkNextAudioSliderAsInitialised;
    }

    private void UnsubscribeEvents()
    {
        GameEvents.OnNextAudioSliderInitialised -= MarkNextAudioSliderAsInitialised;
    }
}
