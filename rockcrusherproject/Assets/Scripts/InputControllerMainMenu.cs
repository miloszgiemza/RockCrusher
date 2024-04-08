using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControllerMainMenu : MonoBehaviour
{
    private LevelChoiceMiniaturesGenerator levelChoiceMiniaturesGenerator;

    private Vector2 twoFramesSwipeValue;
    private Vector2 valueOfCurrentSwipe = new Vector2(0f, 0f);

    private Vector2 previousCursorPos;
    private Vector2 currentCursorPos;

    private bool swiping = false;

    private void Awake()
    {
        levelChoiceMiniaturesGenerator = GetComponentInChildren<LevelChoiceMiniaturesGenerator>();
    }

    private void Update()
    {
        if(levelChoiceMiniaturesGenerator.MiniaturesFinishedGenerating)
        {
            if (swiping)
            {
                ContinueSwipe();
            }
            if (Input.GetMouseButtonDown(0))
            {
                StartSwipe();
            }
            if (Input.GetMouseButtonUp(0))
            {
                StopSwipe();
            }
        }
    }

    private void StartSwipe()
    {
        swiping = true;
        previousCursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void ContinueSwipe()
    {
        currentCursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        twoFramesSwipeValue = new Vector2(Mathf.Abs(currentCursorPos.x - previousCursorPos.x), Mathf.Abs(currentCursorPos.y - previousCursorPos.y));
        if (currentCursorPos.x < previousCursorPos.x) twoFramesSwipeValue = new Vector2(-twoFramesSwipeValue.x, twoFramesSwipeValue.y);
        if (currentCursorPos.y < previousCursorPos.y) twoFramesSwipeValue = new Vector2(twoFramesSwipeValue.x, -twoFramesSwipeValue.y);

        valueOfCurrentSwipe = new Vector2(valueOfCurrentSwipe.x + twoFramesSwipeValue.x, valueOfCurrentSwipe.y + twoFramesSwipeValue.y);

        MainMenuEvents.OnSwipe.Invoke(twoFramesSwipeValue, valueOfCurrentSwipe);

        previousCursorPos = currentCursorPos;
    }

    private void StopSwipe()
    {
        swiping = false;
        valueOfCurrentSwipe = new Vector2(0f, 0f);
    }
}
