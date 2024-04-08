using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuEvents
{
    public static Action<Vector2, Vector2> OnSwipe;

    #region UI
    public static Action<List<UIVisibleElementsMainMenu>> OnShowVisibleUIElementsFromParameterAndHideOther;
    #endregion
}
