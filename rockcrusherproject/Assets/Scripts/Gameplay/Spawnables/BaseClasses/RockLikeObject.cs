using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RockLikeObject : InanimateObject
{
    public float Hardness => hardness;

    protected SpriteRenderer mainRenderer;
    protected BoxCollider2D mainCollider;

    [SerializeField] protected int points = 5;
    [SerializeField] protected float hardness = 0f;
     protected float maxFallingVelocity = -6f;

    protected float beingHitAnimationForceModifier = 1f;
    protected float minRotation = 0f;
    protected float maxRotation = 180f;

    protected Vector2 hitForce = new Vector2(1f, 1f);

    protected bool scoredByPlayer = false;
    protected bool velocityAffectedByPlayer = false;

    protected override void Awake()
    {
        base.Awake();
        mainRenderer = GetComponent<SpriteRenderer>();
        mainCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void Update()
    {
        ClampFallingVelocity();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !scoredByPlayer)
        {
            velocityAffectedByPlayer = true;
            HandleBeingHitByPlayer(collision);
        }
        if(collision.CompareTag("Trash"))
        {
            RemoveOutOfScreen();
        }
    }

    protected void RemoveOutOfScreen()
    {
        Destroy(gameObject);
    }

    protected void ClampFallingVelocity()
    {
        if(!velocityAffectedByPlayer && !affectedByBombExplosion)
        {
            if (localRigidbody.velocity.y < maxFallingVelocity)
            {
                localRigidbody.velocity = new Vector2(localRigidbody.velocity.x, maxFallingVelocity);
            }
        }
    }

    #region HitByPlayerBehaviour
    protected Vector2 DrawRandomForce(Vector2 minForce, Vector2 maxForce)
    {
        Vector2 randomForce = new Vector2(Random.Range(minForce.x, maxForce.x), Random.Range(minForce.y, maxForce.y));
        return randomForce;
    }

    protected Vector2 CalculateCollisionForce()
    {
        Vector2 collisionForce = hitForce;
        return collisionForce = new Vector2(collisionForce.x * beingHitAnimationForceModifier, collisionForce.y * beingHitAnimationForceModifier);
    }

    protected float DrawRandomRotation(float minRotation, float maxRotation)
    {
        float randomRotation = Random.Range(minRotation, maxRotation);
        return randomRotation;
    }


    protected virtual void Shatter(Collider2D collision)
    {
        if(points > 0)
        {
            GameEvents.OnPointsGainedFromShatter.Invoke(transform.position, points);
        }
    }

    protected virtual void HandleBeingHitByPlayer(Collider2D collision)
    {
        hitForce = collision.GetComponent<Pickaxe>().ReturnEntireSwingDistance();

        localRigidbody.AddForce(CalculateCollisionForce());
        localRigidbody.rotation = DrawRandomRotation(minRotation, maxRotation);
        localRigidbody.velocity = CalculateCollisionForce();

        if (Mathf.Abs(hitForce.x) >= hardness || hitForce.y >= hardness)
        {
            scoredByPlayer = true;
            Shatter(collision);
        }
        else
        {
            GameEvents.OnPickReboundedOnHardRock.Invoke(hitForce);
            PlaySFXAudio(SFXAudioCLipsNames.RockHitUnsuccesfull4);
        }
    }
    #endregion
}
