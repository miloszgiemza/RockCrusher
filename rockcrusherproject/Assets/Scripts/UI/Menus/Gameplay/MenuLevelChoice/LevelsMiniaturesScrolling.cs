using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsMiniaturesScrolling : MonoBehaviour
{
    [SerializeField] private LevelChoiceMiniaturesGenerator levelController;

    private RectTransform miniaturesParentRectTransform;

    private float marginFromTopAndBottom = 50f;

    private float movementPowerModifier = 60f;

    private void Awake()
    {
        miniaturesParentRectTransform = levelController.gameObject.GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void ContainMiniaturesToScreen()
    {
        
    }

    private void ScrollMiniatures(Vector2 twoFramesSwipe, Vector2 fullSwipe)
    {
        miniaturesParentRectTransform.position = new Vector2(miniaturesParentRectTransform.position.x, Mathf.Clamp(miniaturesParentRectTransform.position.y + twoFramesSwipe.y * movementPowerModifier, levelController.MaxBottomScrollingPosition, levelController.MaxTopScrollingPosition));
    }

    private void SubscribeEvents()
    {
        MainMenuEvents.OnSwipe += ScrollMiniatures;
    }

    private void UnsubscribeEvents()
    {
        MainMenuEvents.OnSwipe -= ScrollMiniatures;
    }
}
