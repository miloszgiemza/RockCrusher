using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class SwingPowerLoadbar : BaseLoadbar
{
    protected override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    protected override void Start()
    {
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    protected override void SetMaxValue(){}

    protected void SetMaxValue(float maxLoadbarValue)
    {
        slider.maxValue = maxLoadbarValue;
        slider.maxValue = 25f;
    }

    protected void UpdateValue(Vector2 currentSwingPowerValue)
    {
        if (Mathf.Abs(currentSwingPowerValue.x) > Mathf.Abs(currentSwingPowerValue.y))
        {
            slider.value = Mathf.Abs(currentSwingPowerValue.x);
        }
        else
        {
            slider.value = Mathf.Abs(currentSwingPowerValue.y);
        }
    }

    private void SubscribeEvents()
    {
        GameEvents.OnSwingPowerBarInitialised += SetMaxValue;
        GameEvents.OnUpdateSwingPowerBar += UpdateValue;
    }

    private void UnsubscribeEvents()
    {
        GameEvents.OnSwingPowerBarInitialised -= SetMaxValue;
        GameEvents.OnUpdateSwingPowerBar -= UpdateValue;
    }
}
