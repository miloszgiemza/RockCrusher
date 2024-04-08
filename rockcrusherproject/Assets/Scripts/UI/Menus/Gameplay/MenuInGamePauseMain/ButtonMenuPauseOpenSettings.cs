using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMenuPauseOpenSettings : BaseButton
{
    protected override void DoThisOnButtonClicked()
    {
        GameEvents.OnShowVisibleUIElementsFromParameterAndHideOther.Invoke(new List<UIVisibleElementsGameplay>() { UIVisibleElementsGameplay.MenuSettings, UIVisibleElementsGameplay.InGameUI});
    }
}
