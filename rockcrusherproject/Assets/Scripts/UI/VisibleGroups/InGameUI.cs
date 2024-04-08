using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : UIManagerGameplay.BaseVisibleUIElement
{
    public override UIVisibleElementsGameplay ElementIdentifier => UIVisibleElementsGameplay.InGameUI;

    private void Start()
    {
        SetPauseButtonGameWorldPositionToRightTopCornerOfScreenSpace();
    }

    private void SetPauseButtonGameWorldPositionToRightTopCornerOfScreenSpace()
    {
        Button buttonPause = GetComponentInChildren<Button>();
        RectTransform buttonRectTransform = buttonPause.GetComponent<RectTransform>();

        float screenGameWorldWidth = Mathf.Abs(Camera.main.ViewportToWorldPoint(new Vector2(1f, 1f)).x - Camera.main.ViewportToWorldPoint(new Vector2(0f, 1f)).x);
        float screenGameworldHeight = Mathf.Abs(Camera.main.ViewportToWorldPoint(new Vector2(1f, 1f)).y - Camera.main.ViewportToWorldPoint(new Vector2(1f, 0f)).y);

        float buttonGameWorldWidth = buttonRectTransform.rect.width * screenGameWorldWidth / Screen.width;
        float buttonGameWorldHeight = buttonRectTransform.rect.height * screenGameworldHeight / Screen.height;

        buttonRectTransform.position = new Vector2(screenGameWorldWidth / 2f - buttonGameWorldWidth / 2f, screenGameworldHeight / 2f - buttonGameWorldHeight / 2f);
    }
}
