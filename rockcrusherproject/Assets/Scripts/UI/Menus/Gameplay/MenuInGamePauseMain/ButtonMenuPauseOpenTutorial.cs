using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMenuPauseOpenTutorial : BaseButton
{
    protected override void DoThisOnButtonClicked()
    {
        GameEvents.OnShowVisibleUIElementsFromParameterAndHideOther.Invoke(new List<UIVisibleElementsGameplay>() { UIVisibleElementsGameplay.InGameUI, UIVisibleElementsGameplay.TutorialInMenu });
    }
}
