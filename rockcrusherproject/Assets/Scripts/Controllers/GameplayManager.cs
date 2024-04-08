using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPaused
{
    public bool Paused { get; }
}

public class GameplayManager : MonoBehaviour, IPaused
{
    private ScoreController scoreController;
    private IUIManager iUIManager;

    public bool Paused => paused;

    private bool paused = false;

    private void Awake()
    {
        scoreController = GetComponent<ScoreController>();
        iUIManager = GetComponentInChildren<IUIManager>();
    }

    private void OnEnable()
    {
        SubscribeEvents();
        GameEvents.OnPauseGame.Invoke();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void Start()
    {
        StartCoroutine(WaitForInitialisation());
    }

    private void PauseGame()
    {
        paused = true;
        Time.timeScale = 0f;
    }

    private void UnpauseGame()
    {
        paused = false;
        Time.timeScale = 1f;
    }

    private IEnumerator WaitForInitialisation()
    {
        yield return new WaitUntil(() => iUIManager.UIInitialised);
        if (GameManager.Instance.LevelToLoad.LevelNumber == 1) GameEvents.OnShowVisibleUIElementsFromParameterAndHideOther.Invoke(new List<UIVisibleElementsGameplay>() { UIVisibleElementsGameplay.TutorialGameStart, UIVisibleElementsGameplay.InGameUI });
        else GameEvents.OnUnpauseGame.Invoke();
    }

    private void HandleWavesEnd()
    {
        GameEvents.OnPauseGame.Invoke();
        GameEvents.OnLevelComplited.Invoke(scoreController.Score);
    }

    private void SubscribeEvents()
    {
        GameEvents.OnPauseGame += PauseGame;
        GameEvents.OnUnpauseGame += UnpauseGame;
        GameEvents.OnWavesEnd += HandleWavesEnd;
    }

    private void UnsubscribeEvents()
    {
        GameEvents.OnPauseGame -= PauseGame;
        GameEvents.OnUnpauseGame -= UnpauseGame;
        GameEvents.OnWavesEnd -= HandleWavesEnd;
    }
}
