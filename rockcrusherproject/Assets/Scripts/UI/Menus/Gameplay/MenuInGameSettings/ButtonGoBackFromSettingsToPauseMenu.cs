using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonGoBackFromSettingsToPauseMenu : BaseButton
{
    protected override void DoThisOnButtonClicked()
    {
        GameEvents.OnShowVisibleUIElementsFromParameterAndHideOther.Invoke(new List<UIVisibleElementsGameplay>() { UIVisibleElementsGameplay.InGameUI, UIVisibleElementsGameplay.MenuPause });
    }
}
