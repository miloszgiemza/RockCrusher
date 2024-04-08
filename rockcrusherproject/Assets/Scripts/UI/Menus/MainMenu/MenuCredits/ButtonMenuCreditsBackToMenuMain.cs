using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMenuCreditsBackToMenuMain : BaseButton
{
    protected override void DoThisOnButtonClicked()
    {
        MainMenuEvents.OnShowVisibleUIElementsFromParameterAndHideOther.Invoke(new List<UIVisibleElementsMainMenu>() { UIVisibleElementsMainMenu.MenuMain });
    }
}
