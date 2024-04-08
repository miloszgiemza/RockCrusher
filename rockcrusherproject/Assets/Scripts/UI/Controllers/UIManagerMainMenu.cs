using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIVisibleElementsMainMenu
{
    MenuMain,
    MenuLevelChoice,
    MenuCredits
}

public class UIManagerMainMenu : BaseUIManager<UIVisibleElementsMainMenu>
{
    private void Start()
    {
        MainMenuEvents.OnShowVisibleUIElementsFromParameterAndHideOther.Invoke(new List<UIVisibleElementsMainMenu> { UIVisibleElementsMainMenu.MenuMain });  
    }

    protected override void SubscribeEvents()
    {
        MainMenuEvents.OnShowVisibleUIElementsFromParameterAndHideOther += ShowUIElementsFromParameterAndHideAllOther;
    }

    protected override void UnsubscribeEvents()
    {
        MainMenuEvents.OnShowVisibleUIElementsFromParameterAndHideOther -= ShowUIElementsFromParameterAndHideAllOther;
    }
}
