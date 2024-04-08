using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseLoadbar : MonoBehaviour
{
    protected Slider slider;

    protected virtual void Awake()
    {
        slider = GetComponent<Slider>();
    }

    protected virtual void Start()
    {
        SetMaxValue();
    }

    protected abstract void SetMaxValue();
}
