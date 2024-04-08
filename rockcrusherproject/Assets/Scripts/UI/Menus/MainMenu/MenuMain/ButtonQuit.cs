using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonQuit : BaseButton
{
    protected override void DoThisOnButtonClicked()
    {
        GameEvents.OnGameQuit.Invoke();
    }
}
