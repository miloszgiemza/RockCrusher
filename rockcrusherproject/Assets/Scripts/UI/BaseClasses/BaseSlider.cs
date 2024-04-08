using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseSlider : MonoBehaviour
{
    protected Slider slider;

    protected virtual void Awake()
    {
        slider = GetComponent<Slider>();
    }

    protected virtual void OnEnable()
    {
        slider.onValueChanged.AddListener(DoThisWhenPlayerSwipesSlider);
    }

    protected virtual void OnDisable()
    {
        slider.onValueChanged.RemoveListener(DoThisWhenPlayerSwipesSlider);
    }

    protected abstract void ConfigureSlider(float minValue, float maxValue);
    protected abstract void LoadSliderInitialValue(float initialValue);
    protected abstract void DoThisWhenPlayerSwipesSlider(float sliderNewValue);
}
