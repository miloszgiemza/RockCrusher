using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public interface IUIManager
{
    public bool UIInitialised { get; }
}

public enum UIVisibleElementsGameplay
{
    InGameUI,
    MenuPause,
    MenuSettings,
    TutorialGameStart,
    TutorialInMenu,
    PopUpWindowLevelWon,
    PopUpWindowLevelFailed,
    PopUpWindowGameWon
}


public class UIManagerGameplay : BaseUIManager<UIVisibleElementsGameplay>, IUIManager
{
    public bool UIInitialised => uIInitialised;

    private bool uIInitialised = false;

    private bool menusInitialised = false;
    private bool inGameUIInitialised = true;

    private void Start()
    {
        StartCoroutine(HideUIAfterInitialisation());
    }

    private IEnumerator HideUIAfterInitialisation()
    {
        yield return new WaitUntil(() => menusInitialised && inGameUIInitialised);
        GameEvents.OnShowVisibleUIElementsFromParameterAndHideOther.Invoke(new List<UIVisibleElementsGameplay>() { UIVisibleElementsGameplay.InGameUI });
        uIInitialised = true;
    }

    private void MarkInGameMenusAsInitialised()
    {
        menusInitialised = true;
    }

    protected override void SubscribeEvents()
    {
        GameEvents.OnShowVisibleUIElementsFromParameterAndHideOther += ShowUIElementsFromParameterAndHideAllOther;
        GameEvents.OnAllInGameMenusInitialised += MarkInGameMenusAsInitialised;
    }

    protected override void UnsubscribeEvents()
    {
        GameEvents.OnShowVisibleUIElementsFromParameterAndHideOther -= ShowUIElementsFromParameterAndHideAllOther;
        GameEvents.OnAllInGameMenusInitialised -= MarkInGameMenusAsInitialised;
    }
}
