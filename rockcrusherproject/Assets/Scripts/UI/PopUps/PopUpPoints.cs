using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpPoints : MonoBehaviour
{
    private RectTransform rectTransform;
    private TextMeshProUGUI textMeshProUGUI;

    private float fadeOutTime = 1f;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetRectTransform(Vector2 newPos)
    {
        rectTransform.position = newPos;
    }

    public void SetPopUpText(int pointsAmount)
    {
        textMeshProUGUI.text = "+" + pointsAmount.ToString();
    }

    public void StartFadeOutCounter()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(fadeOutTime);
        GameEvents.OnHidePopUuPoints(this);
    }
}
