using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMenuPauseResumeGame : BaseButton
{
    protected override void DoThisOnButtonClicked()
    {
        GameEvents.OnShowVisibleUIElementsFromParameterAndHideOther(new List<UIVisibleElementsGameplay>() { UIVisibleElementsGameplay.InGameUI });
        GameEvents.OnUnpauseGame.Invoke();
    }
}
