using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelChoiceMiniaturesGenerator : MonoBehaviour
{
    public float MaxTopScrollingPosition => maxTopScrollingPosition;
    public float MaxBottomScrollingPosition => maxBottomScrollingPosition;
    public bool MiniaturesFinishedGenerating => miniaturesFinishedGenerating;

    [SerializeField] private GameObject levelsRowPrefab;
    [SerializeField] private GameObject levelMiniaturePrefab;

    private RectTransform thisRectTransform;

    private float miniatureWidth = 540f;
    private float miniatureHeight = 303.75f;

    private int maxNumberOfMiniaturesInRow = 3;
    private float defaultRowXPos = 0f;

    private float firstRowMarginFromTopOfScreen = 1.5f;
    private float topOfTheScreenY;
    private float spacingBetweenRows = 50f;

    private float maxTopScrollingPosition;
    private float maxBottomScrollingPosition;

    private Color greyedOutColor = new Color32(58, 53, 53, 255);

    private bool firstRun = true;

    private bool miniaturesFinishedGenerating = false;

    private void Awake()
    {
        thisRectTransform = GetComponent<RectTransform>();
        defaultRowXPos = CastFloatFromWorldToScreenPoint(defaultRowXPos);

        maxBottomScrollingPosition = thisRectTransform.position.y;
        maxTopScrollingPosition = thisRectTransform.position.y;
    }

    private void OnEnable()
    {
        if(!firstRun)Generate();
        firstRun = false;
    }

    private void OnDisable()
    {
        miniaturesFinishedGenerating = false;
        DestroyGenerated();
    }

    private float CastFloatFromWorldToScreenPoint(float floatToCast)
    {
        float floatToreturn = Camera.main.WorldToScreenPoint(new Vector2(floatToCast, 0f)).x;
        return floatToreturn;
    }

    private void CreateLevelMiniature(Level levelPrefab, GameObject row, int levelNumber)
    {
        GameObject newLevelMiniature = Instantiate(levelMiniaturePrefab, row.transform);
        newLevelMiniature.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, miniatureHeight);
        newLevelMiniature.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, miniatureWidth);
        newLevelMiniature.GetComponentInChildren<Image>().sprite = levelPrefab.LevelBackground;

        if(levelNumber > GameManager.Instance.ProvideCurrentProgress()) newLevelMiniature.GetComponentInChildren<Image>().color = greyedOutColor; 

        newLevelMiniature.GetComponent<LevelMiniature>().InitialiseMiniature(levelNumber);
    }

    private void Generate()
    {
        int miniaturesInCurrentRow = 0;

        GameObject currentRowPrefab = Instantiate(levelsRowPrefab, gameObject.transform);
        float verticalRowAbsolutePos = CastFloatFromWorldToScreenPoint(thisRectTransform.anchoredPosition.y - firstRowMarginFromTopOfScreen);
        currentRowPrefab.GetComponent<RectTransform>().position = new Vector2(defaultRowXPos, verticalRowAbsolutePos);

        for (int currentLevelToCreateMiniature = 1; currentLevelToCreateMiniature <= GameManager.Instance.Levels.Count; currentLevelToCreateMiniature++)
        {
            foreach(Level currentLevelInList in GameManager.Instance.Levels)
            {
                if(currentLevelInList.LevelNumber == currentLevelToCreateMiniature)
                {
                    miniaturesInCurrentRow++;
                    CreateLevelMiniature(currentLevelInList, currentRowPrefab, currentLevelToCreateMiniature);
                }
            }

            if(miniaturesInCurrentRow >= maxNumberOfMiniaturesInRow)
            {
                miniaturesInCurrentRow = 0;
                verticalRowAbsolutePos = verticalRowAbsolutePos - (currentRowPrefab.GetComponent<RectTransform>().rect.size.y + spacingBetweenRows);
                currentRowPrefab = Instantiate(levelsRowPrefab, new Vector2(defaultRowXPos, verticalRowAbsolutePos), Quaternion.identity, gameObject.transform);

                maxTopScrollingPosition += miniatureHeight + spacingBetweenRows;
            }
        }

        miniaturesFinishedGenerating = true;
    }

    private void DestroyGenerated()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
