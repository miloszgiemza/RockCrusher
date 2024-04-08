using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLevelMiniatureChooseLevel : BaseButton
{
    private LevelMiniature levelMiniature;

    private void Start()
    {
        levelMiniature = GetComponentInParent<LevelMiniature>();
        if(levelMiniature.LevelNumber > GameManager.Instance.ProvideCurrentProgress())
        {
            button.interactable = false;
        }
    }
    protected override void DoThisOnButtonClicked()
    {
        GameEvents.OnLoadLevelAndGameplayScene.Invoke(levelMiniature.LevelNumber);
    }
}
