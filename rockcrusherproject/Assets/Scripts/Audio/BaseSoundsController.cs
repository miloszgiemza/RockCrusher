using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSoundsController<EnumWithAudioClipsNames> : MonoBehaviour
{
    public abstract AudioMixerGroup ControllerAudioMixerGroup { get; }

    protected AudioManager audioManager; 
    protected AudioSourcesPooler audioSourcesPooler;

    [SerializeField] protected List<AudioClip> audioClips;
    protected Dictionary<string, AudioClip> audioClipsKeyed = new Dictionary<string, AudioClip>();

    protected virtual void Awake()
    {
        audioManager = GetComponentInParent<AudioManager>();
        audioSourcesPooler = GetComponentInParent<AudioSourcesPooler>();

        KeyAudioClipsWithTheirNames();
    }

    protected virtual void OnEnable()
    {
        SubscribeControllerEvent();
    }

    protected virtual void OnDisable()
    {
        UnsubscribeControllerEvent();
        StopAllCoroutines();
    }

    protected void KeyAudioClipsWithTheirNames()
    {
        foreach(AudioClip audioClip in audioClips)
        {
            AudioClip newAudioClip = audioClip;
            audioClipsKeyed.Add(newAudioClip.name.ToString(), newAudioClip);
        };
    }

    protected IEnumerator ReturnAudioSourceAfterAudioClipPlayed(AudioSource audioSource, float audioClipDuration)
    {
        yield return new WaitForSeconds(audioClipDuration);
        audioSourcesPooler.ReturnAudioSourceToPool(audioSource);
    }

    protected virtual void PlayAudioClip(EnumWithAudioClipsNames audioClipKey)
    {
        AudioSource audioSource = audioSourcesPooler.ProvideAudioSource();
        audioSource.outputAudioMixerGroup = audioManager.ReturnAudioMixerGroup(ControllerAudioMixerGroup);
        audioSource.PlayOneShot(audioClipsKeyed[audioClipKey.ToString()]);
        StartCoroutine(ReturnAudioSourceAfterAudioClipPlayed(audioSource, audioClipsKeyed[audioClipKey.ToString()].length));
    }

    protected abstract void SubscribeControllerEvent();
    protected abstract void UnsubscribeControllerEvent();
}
