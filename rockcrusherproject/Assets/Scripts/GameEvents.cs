using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
    public static Action OnPauseGame;
    public static Action OnUnpauseGame;

    public static Action<Scenes> OnLoadScene;
    public static Action<int> OnLoadLevelAndGameplayScene;

    #region Debugging
    public static Action<string> OnDisplayDevelopementHelperMessage;
    #endregion

    #region InputGameplay
    public static Action<Vector2> OnTouchStarted;
    public static Action OnTouchEnded;
    public static Action<Vector2, Vector2, Vector2, Vector2> OnSwiping;
    public static Action OnDirectionXChanged;
    public static Action OnDirectionYChanged;
    #endregion

    #region UIInitialisation
    public static Action OnAudioSlidersInitialised;
    public static Action<float> OnSwingPowerBarInitialised;
    #endregion

    #region UI
    public static Action<List<UIVisibleElementsGameplay>> OnShowVisibleUIElementsFromParameterAndHideOther;
    public static Action<Vector2> OnUpdateSwingPowerBar;

    public static Action OnTutorialNextStep;

    public static Action OnNextAudioSliderInitialised;
    public static Action OnMenuSettingsInitilised;
    public static Action OnAllInGameMenusInitialised;
    #endregion

    public static Action<int> OnNewComboWave;
    public static Action OnNonComboWave;

    public static Action<Vector2> OnRockDestroyedCameraShake;
    public static Action<Vector2> OnPickReboundedOnHardRock;
    public static Action OnBrokenSwingEnded;

    public static Action<Vector2, int> OnPointsGainedFromShatter;
    public static Action<int> OnRefreshPointsCounter;
    public static Action<PopUpPoints> OnHidePopUuPoints;

    public static Action<int> OnComboComplited;


    public static Action<AudioMixerGroup, float> OnAudioMixerGroupVolumeChange;
    public static Action<float, float, Dictionary<AudioMixerGroup, float>> OnInitializeVolumeSliders;

#region SoundControllers
    public static Action<SFXAudioCLipsNames> OnPlaySFXAudioClip;
    #endregion

    public static Action<Vector2, float> OnBombTouched;

    public static Action OnWavesEnd;
    public static Action<int> OnLevelComplited;

    public static Action OnGameQuit;
}
