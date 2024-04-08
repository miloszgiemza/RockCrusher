using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControllerGameplay : MonoBehaviour
{
    private IPaused iPaused;

    private bool playerControlsDisabled = false;

    private Vector2 twoFramesSwipeValue;
    private Vector2 valueOfCurrentFullSwipe = new Vector2(0f, 0f);

    private Vector2 previousCursorPos;
    private Vector2 currentCursorPos;

    private bool swiping = false;

    private float maxInactivityTime = 1f;
    private float inactivityCounter = 0f;

    private bool firstRun;
    private Vector2 previousDirection;
    private Vector2 currentDirection;

    private void Awake()
    {
        iPaused = GetComponent<IPaused>();
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void Update()
    {
        if(!iPaused.Paused && !playerControlsDisabled)
        {
            if (swiping)
            {
                ContinueSwipe();
            }
            //if (Input.GetMouseButtonDown(0))
            if(Input.GetMouseButton(0) && !swiping)
            {
                StartSwipe();
            }
            if (Input.GetMouseButtonUp(0))
            {
                StopSwipe();
            }
            
            if(!swiping)
            {
                if(Input.GetMouseButton(0))
                {
                    StartSwipe();
                }
            }
        }
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private Vector2 GetSwipeDirection()
    {
        Vector2 direction = new Vector2(1f, 1f);

        if (currentCursorPos.x > previousCursorPos.x) direction = new Vector2(1f, direction.y);
        if (currentCursorPos.x < previousCursorPos.x) direction = new Vector2(-1f, direction.y);
        if (currentCursorPos.y > previousCursorPos.y) direction = new Vector2(direction.x, 1f);
        if (currentCursorPos.y < previousCursorPos.y) direction = new Vector2(direction.x, -1f);

        return direction;
    }

    private void StartSwipe()
    {
        swiping = true;
        firstRun = true;
        previousCursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        GameEvents.OnTouchStarted.Invoke(previousCursorPos);
    }

    private void SwipeInactivity()
    {
        if (previousCursorPos.x == currentCursorPos.x && previousCursorPos.y == currentCursorPos.y)
        {
            inactivityCounter += Time.deltaTime;
        }
        else
        {
            inactivityCounter = 0f;
        }
        if (inactivityCounter >= maxInactivityTime)
        {
            StopSwipe();
            StartSwipe();
        }
    }

    private void ResetSwipeOnDirectionChange()
    {
        if (currentDirection.x != previousDirection.x)
        {
            valueOfCurrentFullSwipe = new Vector2(0f, valueOfCurrentFullSwipe.y);
            GameEvents.OnDirectionXChanged.Invoke();
        }

        if (currentDirection.y != previousDirection.y)
        {
            valueOfCurrentFullSwipe = new Vector2(valueOfCurrentFullSwipe.x, 0f);
            GameEvents.OnDirectionYChanged.Invoke();
        }
    }

    private void ContinueSwipe()
    {
        currentCursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(currentCursorPos != previousCursorPos)
        {
            currentDirection = GetSwipeDirection();

            twoFramesSwipeValue = new Vector2(Mathf.Abs(currentCursorPos.x - previousCursorPos.x), Mathf.Abs(currentCursorPos.y - previousCursorPos.y));
            if (currentCursorPos.x < previousCursorPos.x) twoFramesSwipeValue = new Vector2(-(twoFramesSwipeValue.x), twoFramesSwipeValue.y);
            if (currentCursorPos.y < previousCursorPos.y) twoFramesSwipeValue = new Vector2(twoFramesSwipeValue.x, -(twoFramesSwipeValue.y));

            valueOfCurrentFullSwipe = new Vector2(valueOfCurrentFullSwipe.x + twoFramesSwipeValue.x, valueOfCurrentFullSwipe.y + twoFramesSwipeValue.y);

            GameEvents.OnSwiping.Invoke(currentCursorPos, currentDirection, twoFramesSwipeValue, valueOfCurrentFullSwipe);

            SwipeInactivity();
            if (!firstRun) ResetSwipeOnDirectionChange();

            previousDirection = currentDirection;
        }


        previousCursorPos = currentCursorPos;

        firstRun = false;
    }

    private void StopSwipe()
    {
        swiping = false;
        valueOfCurrentFullSwipe = new Vector2(0f, 0f);
        GameEvents.OnTouchEnded.Invoke();
    }

    private void DisableControlsOnBrokenSwing(Vector2 unusedValue)
    {
        playerControlsDisabled = true;
        valueOfCurrentFullSwipe = new Vector2(0f, 0f);
        swiping = false;
    }

    private void EnableControlsOnBrokenSwingEnded()
    {
        playerControlsDisabled = false;
    }

    private void SubscribeEvents()
    {
        GameEvents.OnPauseGame += StopSwipe;
        GameEvents.OnPickReboundedOnHardRock += DisableControlsOnBrokenSwing;
        GameEvents.OnBrokenSwingEnded += EnableControlsOnBrokenSwingEnded;
    }

    private void UnsubscribeEvents()
    {
        GameEvents.OnPauseGame -= StopSwipe;
        GameEvents.OnPickReboundedOnHardRock -= DisableControlsOnBrokenSwing;
        GameEvents.OnBrokenSwingEnded -= EnableControlsOnBrokenSwingEnded;
    }
}
