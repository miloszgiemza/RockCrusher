using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialControllerInMenu : BaseTutorial
{
    public override UIVisibleElementsGameplay TutorialElementIdentifier => UIVisibleElementsGameplay.TutorialInMenu;

    protected override void GoToNextStep()
    {
        if (currentStep < tutorialSlidesTexts.Length)
        {
            tutorialText.text = tutorialSlidesTexts[currentStep];
            currentStep++;
        }
        else
        {
            GameEvents.OnShowVisibleUIElementsFromParameterAndHideOther.Invoke(new List<UIVisibleElementsGameplay>() { UIVisibleElementsGameplay.InGameUI, UIVisibleElementsGameplay.MenuPause });
        }
    }
}
