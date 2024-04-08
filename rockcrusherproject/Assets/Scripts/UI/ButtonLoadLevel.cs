using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLoadLevel : BaseButton
{
    protected override void DoThisOnButtonClicked()
    {
        GameEvents.OnLoadScene.Invoke(Scenes.Gameplay);
    }
}
