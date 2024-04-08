using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayObject : MonoBehaviour
{
    #region Audio
    protected void PlaySFXAudio(SFXAudioCLipsNames soundName)
    {
        GameEvents.OnPlaySFXAudioClip.Invoke(soundName);
    }
    #endregion
}
