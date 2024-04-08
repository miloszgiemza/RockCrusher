using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelMiniature : MonoBehaviour
{
    public int LevelNumber => levelNumber;

    private int levelNumber;    

    public void InitialiseMiniature(int levelNumberValue)
    {
        GetComponentInChildren<TextMeshProUGUI>().text = levelNumberValue.ToString();
        levelNumber = levelNumberValue;
        if (levelNumber > GameManager.Instance.ProvideCurrentProgress()) GetComponentInChildren<TextMeshProUGUI>().text = "Locked";
    }
}
