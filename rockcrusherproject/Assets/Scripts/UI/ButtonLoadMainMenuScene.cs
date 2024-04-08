using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLoadMainMenuScene : BaseButton
{
    protected override void DoThisOnButtonClicked()
    {
        GameEvents.OnLoadScene.Invoke(Scenes.MainMenu);
    }
}
