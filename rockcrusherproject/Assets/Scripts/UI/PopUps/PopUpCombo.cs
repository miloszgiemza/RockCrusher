using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpCombo : MonoBehaviour
{
    private TextMeshProUGUI popUp;
    private float popUpDuration = 0.7f;
    private bool comboActive = false;
    private float timer = 0f;

    private void Awake()
    {
        popUp = GetComponentInChildren<TextMeshProUGUI>();
        popUp.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void Update()
    {
        if(timer>0)
        {
            timer -= Time.deltaTime;
        }
        if(timer<=0f)
        {
            popUp.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void ShowPopUp(int points)
    {
        popUp.gameObject.SetActive(true);
        timer = popUpDuration;
    }

    private void SubscribeEvents()
    {
        GameEvents.OnComboComplited += ShowPopUp;
    }

    private void UnsubscribeEvents()
    {
        GameEvents.OnComboComplited -= ShowPopUp;
    }
}
