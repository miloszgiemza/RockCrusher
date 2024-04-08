using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : RockLikeObject
{
    private float bombForce = 45f;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameEvents.OnPlaySFXAudioClip.Invoke(SFXAudioCLipsNames.Bomb);
            GameEvents.OnBombTouched.Invoke(transform.position, bombForce);
            Destroy(gameObject);
        }
        if (collision.CompareTag("Trash"))
        {
            RemoveOutOfScreen();
        }
    }
}
