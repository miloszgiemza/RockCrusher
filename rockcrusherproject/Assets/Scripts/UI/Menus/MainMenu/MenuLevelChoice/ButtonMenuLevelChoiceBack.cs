using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMenuLevelChoiceBack : BaseButton
{
    protected override void DoThisOnButtonClicked()
    {
        MainMenuEvents.OnShowVisibleUIElementsFromParameterAndHideOther.Invoke(new List<UIVisibleElementsMainMenu> { UIVisibleElementsMainMenu.MenuMain});
    }
}
