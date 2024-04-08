using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SaveKeys
{
    UnlockedLevels
}

public static class SavesManager 
{
    public static int ReturnUnlockedLevels()
    {
        int unlockedLevels = 0;

        if (PlayerPrefs.HasKey(SaveKeys.UnlockedLevels.ToString())) unlockedLevels = PlayerPrefs.GetInt(SaveKeys.UnlockedLevels.ToString());

        return unlockedLevels;
    }

    public static void UnlockNextLevel()
    {
        if (PlayerPrefs.HasKey(SaveKeys.UnlockedLevels.ToString())) PlayerPrefs.SetInt(SaveKeys.UnlockedLevels.ToString(), PlayerPrefs.GetInt(SaveKeys.UnlockedLevels.ToString()) + 1);
        else PlayerPrefs.SetInt(SaveKeys.UnlockedLevels.ToString(), 1);
    }

    public static void UnlockAllLevels()
    {
        PlayerPrefs.SetInt(SaveKeys.UnlockedLevels.ToString(), GameManager.Instance.Levels.Count+1);
    }

    public static void ClearGameProgress()
    {
        PlayerPrefs.DeleteAll();
    }
}

