using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpsPointsSpawner : MonoBehaviour
{
    private PopUpsPointsPooler popUpsPointsPooler;
    private Vector2 comboPopUpPosition = new Vector2(0f, 6.1f);

    private void Awake()
    {
        popUpsPointsPooler = GetComponent<PopUpsPointsPooler>();
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void SpawnPopUp(Vector2 position, int points)
    {
        /*
        PopUpPoints spawnedPopUp = popUpsPointsPooler.ProvidePopUpPoints();
        spawnedPopUp.SetPopUpText(points);
        spawnedPopUp.SetRectTransform(Camera.main.WorldToScreenPoint(new Vector2(position.x, position.y)));
        spawnedPopUp.StartFadeOutCounter();
        */

        PopUpPoints spawnedPopUp = popUpsPointsPooler.ProvidePopUpPoints();
        spawnedPopUp.SetPopUpText(points);
        spawnedPopUp.SetRectTransform(new Vector2(position.x, position.y));
        spawnedPopUp.StartFadeOutCounter();
    }

    private void SpawnComboPointsValuePopUp(int points)
    {
        PopUpPoints spawnedPopUp = popUpsPointsPooler.ProvidePopUpPoints();
        spawnedPopUp.SetPopUpText(points);
        spawnedPopUp.SetRectTransform(Camera.main.WorldToScreenPoint(new Vector2(comboPopUpPosition.x, comboPopUpPosition.y)));
        spawnedPopUp.StartFadeOutCounter();
    }

    private void SubscribeEvents()
    {
        GameEvents.OnPointsGainedFromShatter += SpawnPopUp;
        GameEvents.OnComboComplited += SpawnComboPointsValuePopUp;
    }

    private void UnsubscribeEvents()
    {
        GameEvents.OnPointsGainedFromShatter -= SpawnPopUp;
        GameEvents.OnComboComplited -= SpawnComboPointsValuePopUp;
    }
}
