using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerWaves : MonoBehaviour
{
    private Level level;

    private void Awake()
    {
        level = GameManager.Instance.LevelToLoad;
    }

    private void Start()
    {
        StartCoroutine(RunSpawner());
    }

    private void OnDisable()
    {
        StopCoroutine(RunSpawner());
    }

    private IEnumerator RunSpawner()
    {
        yield return new WaitForSeconds(level.StartDelay);

        for(int i = 0; i < level.Repeats; i++)
        {
           foreach(Level.WaveWithDelay currentWave in level.WavesWithDelay)
            {
                if(currentWave.Wave.ComboEnabled)
                {
                    GameEvents.OnNewComboWave.Invoke(currentWave.Wave.ReturnRocksCountInWave());
                    Debug.Log("W combo fali jest ska³: " + currentWave.Wave.ReturnRocksCountInWave());
                }
                else
                {
                    GameEvents.OnNonComboWave.Invoke();
                }
                
                 Instantiate(currentWave.Wave, transform.position, Quaternion.identity);

                if (currentWave.DelayAfter == 0) yield return new WaitForSeconds(level.StandardDelayAfter);
                else yield return new WaitForSeconds(currentWave.DelayAfter);
            }
        }

        GameEvents.OnWavesEnd.Invoke();
    }
}
