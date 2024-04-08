using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public abstract class BaseVolumeSlider : BaseSlider
{
    public abstract AudioMixerGroup SliderAudioMixerGroup  { get; }

    protected override void OnEnable()
    {
        SubscribeEvents();
        base.OnEnable();

    }

    protected override void OnDisable()
    {
        UnsubscribeEvents();
        base.OnDisable();

    }

    protected override void ConfigureSlider(float minValue, float maxValue)
    {
        slider.minValue = minValue;
        slider.maxValue = maxValue;
    }

    protected override void LoadSliderInitialValue(float initialValue)
    {
    }

    protected void LoadSliderInitialValue(Dictionary<AudioMixerGroup, float> currentAudioGroupsLinearValues)
    {
        slider.value = currentAudioGroupsLinearValues[SliderAudioMixerGroup];
    }

    protected void InitializeSlider(float minValue, float maxValue, Dictionary<AudioMixerGroup, float> currentAudioGroupsLinearValues)
    {
        ConfigureSlider(minValue, maxValue);
        LoadSliderInitialValue(currentAudioGroupsLinearValues);
        GameEvents.OnNextAudioSliderInitialised.Invoke();
    }

    protected override void DoThisWhenPlayerSwipesSlider(float sliderNewValue)
    {
        GameEvents.OnAudioMixerGroupVolumeChange.Invoke(SliderAudioMixerGroup, sliderNewValue);
    }

    protected void SubscribeEvents()
    {
        GameEvents.OnInitializeVolumeSliders += InitializeSlider;
    }

    protected void UnsubscribeEvents()
    {
        GameEvents.OnInitializeVolumeSliders -= InitializeSlider;
    }
}
