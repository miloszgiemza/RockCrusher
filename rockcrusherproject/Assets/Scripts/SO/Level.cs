using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Level", fileName = "Level")]
public class Level : ScriptableObject
{
    #region Wave
    public float StartDelay => startDelay;
    public int Repeats => repeats;
    public List<WaveWithDelay> WavesWithDelay => wavesWithDelay;
    public float StandardDelayAfter => standardDeayAfter;
    #endregion

    #region Level
    public int LevelNumber => levelNumber;
    public Sprite LevelBackground => levelBackground;
    public int ScoreRequiredToWin => scoreRequiredToWin;
    #endregion

    #region Wave
    [Serializable]
    public class WaveWithDelay
    {
        public Wave Wave => wave;
        public float DelayAfter => delayAfter;

        [SerializeField] private Wave wave;
        [SerializeField] private float delayAfter = 1.2f;
    }

    [SerializeField] private float startDelay;
    [SerializeField] private int repeats; 
    [SerializeField] private List<WaveWithDelay> wavesWithDelay = new List<WaveWithDelay>();
    [SerializeField] private float standardDeayAfter = 60f;
    #endregion

    #region Level
    [SerializeField] private int levelNumber;
    [SerializeField] private Sprite levelBackground;
    [SerializeField] private int scoreRequiredToWin;
    #endregion
}
