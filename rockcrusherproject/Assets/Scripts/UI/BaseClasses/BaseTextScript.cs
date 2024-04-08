using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public abstract class BaseTextScript : MonoBehaviour
{
    protected TextMeshProUGUI textComponent;

    protected virtual void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }
}
