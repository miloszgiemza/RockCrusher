using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPause : BaseButton
{
    protected override void DoThisOnButtonClicked()
    {
        GameEvents.OnPauseGame.Invoke();
        GameEvents.OnShowVisibleUIElementsFromParameterAndHideOther(new List<UIVisibleElementsGameplay>() { UIVisibleElementsGameplay.MenuPause, UIVisibleElementsGameplay.InGameUI});
    }
}
