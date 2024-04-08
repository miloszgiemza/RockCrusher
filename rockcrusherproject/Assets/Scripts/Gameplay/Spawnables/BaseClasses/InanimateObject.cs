using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InanimateObject : GameplayObject
{
    protected Rigidbody2D localRigidbody;

    protected bool affectedByBombExplosion = false;

    protected virtual void Awake()
    {
        localRigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void OnEnable()
    {
        SubscribeEvents();
    }

    protected virtual void OnDisable()
    {
        UnsubscribeEvents();
    }

    #region BombsBehaviour

    protected Vector2 CalculateDistanceFromBomb(Vector2 rockPosition, Vector2 bombPosition)
    {
        Vector2 calculatedDistance = new Vector2(Mathf.Abs(bombPosition.x - rockPosition.x), Mathf.Abs(bombPosition.y - rockPosition.y));

        return calculatedDistance;
    }

    protected Vector2 CalculateDirection(Vector2 rockPosition, Vector2 bombPosition)
    {
        Vector2 direction = new Vector2(0f, 0f);

        if (rockPosition.x < bombPosition.x) direction = new Vector2(-1f, direction.y);
        if (rockPosition.x > bombPosition.x) direction = new Vector2(1f, direction.y);
        if (rockPosition.y < bombPosition.y) direction = new Vector2(direction.x, -1f);
        if (rockPosition.y > bombPosition.y) direction = new Vector2(direction.x, 1f);

        return direction;
    }

    protected Vector2 CalculateBombFinalForce(Vector2 rockPosition, Vector2 bombPosition, float bombForce)
    {
        Vector2 finalForce = new Vector2(0f, 0f);

        if (rockPosition.y == bombPosition.x && rockPosition.y == bombPosition.y) finalForce = new Vector2(0f, 0f);
        else if (rockPosition.x != bombPosition.x && rockPosition.y == bombPosition.y) finalForce = new Vector2((bombForce / CalculateDistanceFromBomb(rockPosition, bombPosition).x) * CalculateDirection(rockPosition, bombPosition).x, 0f);
        else if (rockPosition.x == bombPosition.x && rockPosition.y != bombPosition.y) finalForce = new Vector2(0f, (bombForce / CalculateDistanceFromBomb(rockPosition, bombPosition).y) * CalculateDirection(rockPosition, bombPosition).y);
        else if (rockPosition.x != bombPosition.x && rockPosition.y != bombPosition.y) finalForce = new Vector2((bombForce / CalculateDistanceFromBomb(rockPosition, bombPosition).x) * CalculateDirection(rockPosition, bombPosition).x, (bombForce / CalculateDistanceFromBomb(rockPosition, bombPosition).y) * CalculateDirection(rockPosition, bombPosition).y);

        return finalForce;
    }

    protected void HandleBombExplosion(Vector2 bombPosition, float bombForce)
    {
        affectedByBombExplosion = true;
        Vector2 finalForce = CalculateBombFinalForce(transform.position, bombPosition, bombForce);
        localRigidbody.AddForce(finalForce);
        localRigidbody.velocity = finalForce;
    }

    #endregion

    protected virtual void SubscribeEvents()
    {
        GameEvents.OnBombTouched += HandleBombExplosion;
    }

    protected virtual void UnsubscribeEvents()
    {
        GameEvents.OnBombTouched -= HandleBombExplosion;
    }
}
