using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTutorialNextStep : BaseButton
{
    protected override void DoThisOnButtonClicked()
    {
        GameEvents.OnTutorialNextStep.Invoke();
    }
}
