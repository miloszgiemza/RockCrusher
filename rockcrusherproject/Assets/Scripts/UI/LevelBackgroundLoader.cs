using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelBackgroundLoader : MonoBehaviour
{
    private SpriteRenderer backgroundSpriteRenderer;

    private Bounds screenBounds;
    private float screenWidth;
    private float screenHeight;

    private void Awake()
    {
        backgroundSpriteRenderer = GetComponent<SpriteRenderer>();
        backgroundSpriteRenderer.sprite = GameManager.Instance.LevelToLoad.LevelBackground;
        GetScreenSize();
    }

    private void Start()
    {
        RescaleSpriteRendererToFitWholeScreen();
    }

    private void GetScreenSize()
    {
        screenBounds.min = Camera.main.ViewportToWorldPoint(new Vector2(0f, 0f));
        screenBounds.max = Camera.main.ViewportToWorldPoint(new Vector2(1f, 1f));

        screenWidth = Mathf.Abs(screenBounds.max.x - screenBounds.min.x);
        screenHeight = Mathf.Abs(screenBounds.max.y - screenBounds.min.y);
    }

    private void RescaleSpriteRendererToFitWholeScreen()
    {
        backgroundSpriteRenderer.transform.localScale = new Vector3(screenWidth / backgroundSpriteRenderer.size.x, screenHeight / backgroundSpriteRenderer.size.y, 0f);
    }

    private float CalculateDownscalingToFitCamera(float fullDefaultCanvasSize, float gameWorldCanvasSize)
    {
        float newScale = gameWorldCanvasSize / fullDefaultCanvasSize;
        return newScale;
    }
}
