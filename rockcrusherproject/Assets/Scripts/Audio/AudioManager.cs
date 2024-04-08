using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum AudioMixerGroup
{
    Master,
    Music,
    SoundEffects,
    Voice,
    UI
}

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    private float minLinearVolumeValue = 0.0001f;
    private float maxLinearVolumeValue = 1f;

    private bool slidersInitialised = false;

    private Dictionary<AudioMixerGroup, float> currentVolumeGroupsLinearValues = new Dictionary<AudioMixerGroup, float>()
    {
        {AudioMixerGroup.Master, 1f},
        {AudioMixerGroup.Music, 1f},
        {AudioMixerGroup.SoundEffects, 1f },
        {AudioMixerGroup.Voice, 1f },
        {AudioMixerGroup.UI, 1f }
    };

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void Start()
    {
        StartCoroutine(InvokeInitialisationWhenSlidersAreAlreadySubscribed());
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private float ConvertLinearVolumeToLogarithmic(float linearValue)
    {
        float logarithmicValue = Mathf.Log10(linearValue) * 20;
        return logarithmicValue;
    }

    public void ChangeGroupVolume(AudioMixerGroup audioMixerGroup, float linearVolumeValue)
    {
        if(slidersInitialised)
        {
            float newLinearVolumeValue = Mathf.Clamp(linearVolumeValue, minLinearVolumeValue, maxLinearVolumeValue);

            currentVolumeGroupsLinearValues[audioMixerGroup] = newLinearVolumeValue;
            audioMixer.SetFloat(audioMixerGroup.ToString(), ConvertLinearVolumeToLogarithmic(newLinearVolumeValue));
        }
    }

    public UnityEngine.Audio.AudioMixerGroup ReturnAudioMixerGroup(AudioMixerGroup audioMixerGroupIdentifier)
    {
        UnityEngine.Audio.AudioMixerGroup audioMixerGroup;
        audioMixerGroup = audioMixer.FindMatchingGroups(audioMixerGroupIdentifier.ToString())[0];
        return audioMixerGroup;
    }

    private IEnumerator InvokeInitialisationWhenSlidersAreAlreadySubscribed()
    {
        yield return new WaitUntil(() => GameEvents.OnInitializeVolumeSliders != null);
        GameEvents.OnInitializeVolumeSliders.Invoke(minLinearVolumeValue, maxLinearVolumeValue, currentVolumeGroupsLinearValues);
        slidersInitialised = true;
    }

    protected void SubscribeEvents()
    {
        GameEvents.OnAudioMixerGroupVolumeChange += ChangeGroupVolume;
    }

    protected void UnsubscribeEvents()
    {
        GameEvents.OnAudioMixerGroupVolumeChange -= ChangeGroupVolume;
    }
}
