using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class BaseTutorial : UIManagerGameplay.BaseVisibleUIElement
{
    public abstract UIVisibleElementsGameplay TutorialElementIdentifier {get;}
    public override UIVisibleElementsGameplay ElementIdentifier => TutorialElementIdentifier;

    protected int currentStep = 0;

    protected TextMeshProUGUI tutorialText;

    protected string[] tutorialSlidesTexts = { "Swipe to crush rocks and acquire points...",
        "Harder rock types require longer swipes to destroy...",
        "Failing to crush a rock will break your swing...",
        "Some waves give bonus points for crushing all rocks in wave...",
        "May your mining expeditions always be successful and your beard thick!" };

    protected void Awake()
    {
        tutorialText = GetComponentInChildren<TutorialInfo>().GetComponent<TextMeshProUGUI>();
    }

    protected void OnEnable()
    {
        SubscribeEvents();
        GoToNextStep();
    }

    protected void OnDisable()
    {
        UnsubscribeEvents();
        currentStep = 0;
    }

    protected virtual void GoToNextStep()
    {
        if (currentStep < tutorialSlidesTexts.Length)
        {
            tutorialText.text = tutorialSlidesTexts[currentStep];
            currentStep++;
        }
        else
        {
            GameEvents.OnShowVisibleUIElementsFromParameterAndHideOther.Invoke(new List<UIVisibleElementsGameplay>() { UIVisibleElementsGameplay.InGameUI });
            GameEvents.OnUnpauseGame.Invoke();
        }
    }

    protected void SubscribeEvents()
    {
        GameEvents.OnTutorialNextStep += GoToNextStep;
    }

    protected void UnsubscribeEvents()
    {
        GameEvents.OnTutorialNextStep -= GoToNextStep;
    }
}
