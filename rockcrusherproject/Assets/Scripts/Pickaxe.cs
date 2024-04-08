using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe : MonoBehaviour
{
    private CircleCollider2D pickaxeCollider;
    private TrailRenderer trailRenderer;

    private Vector2 currentSwingPower = new Vector2(0f, 0f);
    //max horizontal swing power 35
    //max vertical swing power 15

    private Vector2 maxSwingPower = new Vector2(35f, 18f);
    private Vector2 minSwingPower = new Vector2(-35f, -18f);

    private bool brokenSwing = false;
    private float brokenSwingDuration = 0.6f;

    private void Awake()
    {
        pickaxeCollider = GetComponent<CircleCollider2D>();
        pickaxeCollider.enabled = false;
        trailRenderer = GetComponentInChildren<TrailRenderer>();
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void Start()
    {
        GameEvents.OnSwingPowerBarInitialised.Invoke(maxSwingPower.x);
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    public Vector2 ReturnEntireSwingDistance()
    {
        return currentSwingPower;
    }

    protected IEnumerator EndSwingBeingBroken()
    {
        yield return new WaitForSeconds(brokenSwingDuration);

        trailRenderer.gameObject.SetActive(false);
        brokenSwing = false;
        GameEvents.OnBrokenSwingEnded.Invoke();
    }

    private void BreakSwingOnHardRock(Vector2 hitForce)
    {
        if(!brokenSwing)
        {
            brokenSwing = true;
            StopSwinging();

            float hitBackForceModifier = 4.3f;
            float reverseDirection = -1f;
            float maxEffectLength = 10f;
            float minEffectLength = 2f;

            transform.position = new Vector2(Mathf.Clamp(transform.position.x + hitForce.x * hitBackForceModifier * reverseDirection, minEffectLength, maxEffectLength), transform.position.y + hitForce.y * hitBackForceModifier * reverseDirection);

            StartCoroutine(EndSwingBeingBroken());
        }
    }

    private void SubscribeEvents()
    {
        GameEvents.OnTouchStarted += StartSwinging;
        GameEvents.OnSwiping += Swinging;
        GameEvents.OnTouchEnded += StopSwinging;

        GameEvents.OnDirectionXChanged += ResetPowerOnDirectionChangeX;
        GameEvents.OnDirectionYChanged += ResetPowerOnDirectionChangeY;

        GameEvents.OnPickReboundedOnHardRock += BreakSwingOnHardRock;
    }

    private void UnsubscribeEvents()
    {
        GameEvents.OnTouchStarted -= StartSwinging;
        GameEvents.OnSwiping -= Swinging;
        GameEvents.OnTouchEnded -= StopSwinging;

        GameEvents.OnDirectionXChanged -= ResetPowerOnDirectionChangeX;
        GameEvents.OnDirectionYChanged -= ResetPowerOnDirectionChangeY;

        GameEvents.OnPickReboundedOnHardRock -= BreakSwingOnHardRock;
    }

    private void StartSwinging(Vector2 inputPos)
    {
        if(!brokenSwing)
        {
            pickaxeCollider.enabled = true;
            trailRenderer.gameObject.SetActive(true);
            transform.position = inputPos;
        }
    }

    private void Swinging(Vector2 inputPos, Vector2 swipeDirection, Vector2 twoFramesSwipeLength, Vector2 swipeFullLength)
    {
        if(!brokenSwing)
        {
            Debug.Log("Swipe power: " + swipeFullLength);
            InvokeDevelopementMessageEvent.FireEvent(swipeFullLength.ToString());
            currentSwingPower = new Vector2(Mathf.Clamp(swipeFullLength.x, minSwingPower.x, maxSwingPower.x), Mathf.Clamp(swipeFullLength.y, minSwingPower.y, maxSwingPower.y));
            GameEvents.OnUpdateSwingPowerBar.Invoke(currentSwingPower);
            transform.position = new Vector3(transform.position.x + twoFramesSwipeLength.x, transform.position.y + twoFramesSwipeLength.y);
        }
    }

    private void StopSwinging()
    {
        if(!brokenSwing)
        {
            pickaxeCollider.enabled = false;
            trailRenderer.gameObject.SetActive(false);
            currentSwingPower = new Vector2(0f, 0f);
            GameEvents.OnUpdateSwingPowerBar.Invoke(currentSwingPower);
        }
    }

    private void ResetPowerOnDirectionChangeX()
    {
        currentSwingPower = new Vector2(0f, currentSwingPower.y);
    }

    private void ResetPowerOnDirectionChangeY()
    {
        currentSwingPower = new Vector2(currentSwingPower.x, 0f);
    }
}
