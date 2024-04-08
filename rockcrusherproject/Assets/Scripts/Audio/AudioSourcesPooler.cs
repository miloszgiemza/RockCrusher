using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourcesPooler : MonoBehaviour
{
    private int poolerSize = 20;
    private List<AudioSource> audioSourcesPool = new List<AudioSource>();

    private void Awake()
    {
        CreatePooler();
    }

    private  void CreatePooler()
    {
        for(int i = 0; i < poolerSize; i++)
        {
            AudioSource newAudioSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
            audioSourcesPool.Add(newAudioSource);
            audioSourcesPool[audioSourcesPool.Count - 1].enabled = false;
        }

        Debug.Log(audioSourcesPool.Count);
    }

    public AudioSource ProvideAudioSource()
    {
        AudioSource audioSource = audioSourcesPool[0];
        bool avaliable = false;

        for(int i = 0; i < audioSourcesPool.Count; i++)
        {
            if(audioSourcesPool[i].enabled == false)
            {
                audioSource = audioSourcesPool[i];
                audioSource.enabled = true;
                avaliable = true;
            }
        }

        if(!avaliable)
        {
            audioSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
            audioSourcesPool.Add(audioSource);
        }

        return audioSource;
    }

    public void ReturnAudioSourceToPool(AudioSource audioSource)
    {
        audioSource.enabled = false;
    }
}
