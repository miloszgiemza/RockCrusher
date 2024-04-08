using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rock : RockLikeObject
{
    private List<RockFragment> rockFragments = new List<RockFragment>();

    private Vector2 minFragmentRandomizingForce = new Vector2(-5f, -5f);
    private Vector2 maxFragmentRandomizingForce = new Vector2(5f, 5f);

    private float flashDuration = 0.12f;

    protected override void Awake()
    {
        base.Awake();

        foreach(Transform child in transform)
        {
            rockFragments.Add(child.GetComponent<RockFragment>());
        }
    }

    private void Start()
    {
        foreach (RockFragment rockFragment in rockFragments)
        {
            rockFragment.gameObject.SetActive(false);
        }
    }

    #region PlayerCollisionBehaviour
    protected override void Shatter(Collider2D collision)
    {
        base.Shatter(collision);

        hitForce = collision.GetComponent<Pickaxe>().ReturnEntireSwingDistance();

        GameEvents.OnRockDestroyedCameraShake.Invoke(CalculateCollisionForce());

        StartCoroutine(ShatteredEffects());
    }

    private IEnumerator ShatteredEffects()
    {
        mainRenderer.material.SetInt("_RunEffect", 1);
        yield return new WaitForSeconds(flashDuration);

        mainRenderer.enabled = false;

        foreach (RockFragment rockFragment in rockFragments)
        {
            rockFragment.gameObject.SetActive(true);

            rockFragment.FragmentAddForce(CalculateCollisionForce() + DrawRandomForce(minFragmentRandomizingForce, maxFragmentRandomizingForce));
            rockFragment.FragmentSetRotation(DrawRandomRotation(minRotation, maxRotation));
            rockFragment.FragmentSetVelocity(CalculateCollisionForce() + DrawRandomForce(minFragmentRandomizingForce, maxFragmentRandomizingForce));
        }

        PlaySFXAudio(SFXAudioCLipsNames.RockSuccesfullyDestroyed);
    }
    #endregion
}
