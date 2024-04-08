using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMenuMainOpenCredits : BaseButton
{
    protected override void DoThisOnButtonClicked()
    {
        MainMenuEvents.OnShowVisibleUIElementsFromParameterAndHideOther.Invoke(new List<UIVisibleElementsMainMenu>() { UIVisibleElementsMainMenu.MenuCredits });
    }
}
