using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InvokeDevelopementMessageEvent
{
    public static void FireEvent(string messageText)
    {
        if(GameEvents.OnDisplayDevelopementHelperMessage!=null)
        {
            GameEvents.OnDisplayDevelopementHelperMessage.Invoke(messageText);
        }
    }
}
