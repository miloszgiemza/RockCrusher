using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXController : MonoBehaviour
{
    [SerializeField] private GameObject explosionEffect;
    private float explosionLifetime = 3f;

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private IEnumerator PlayeExplosionEffect(Vector2 position)
    {
        GameObject explosion = Instantiate(explosionEffect, position, Quaternion.identity);
        yield return new WaitForSeconds(explosionLifetime);
        Destroy(explosion);
    }

    private void OnBombExplosion(Vector2 position, float bombPower)
    {
        StartCoroutine(PlayeExplosionEffect(position));
    }

    private void SubscribeEvents()
    {
        GameEvents.OnBombTouched += OnBombExplosion;
    }

    private void UnsubscribeEvents()
    {
        GameEvents.OnBombTouched -= OnBombExplosion;
    }
}
