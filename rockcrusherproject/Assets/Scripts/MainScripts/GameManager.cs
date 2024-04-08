using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scenes
{
    MainMenu,
    Gameplay
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => instance;
    public List<Level> Levels => levels;
    public Level LevelToLoad => levelToLoad;

    
    private static GameManager instance;

    private Level levelToLoad;

    [SerializeField] private List<Level> levels = new List<Level>();  

    private void Awake()
    {
        if(GameManager.Instance!=null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void OnEnable()
    {
        SubscribeEvents();
        //SavesManager.UnlockAllLevels();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void OnApplicationQuit()
    {
        //SavesManager.ClearGameProgress();
    }

    public int ProvideCurrentProgress()
    {
        int currentUnlockedLevel = 1;
        currentUnlockedLevel += SavesManager.ReturnUnlockedLevels();
        return currentUnlockedLevel;
    }

    private void LoadScene(Scenes sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad.ToString());
    }

    private Level ReturnLevelByNumber(int levelToFind)
    {
        Level levelToReturn = levels[0];

        foreach (Level level in levels)
        {
            if (level.LevelNumber == levelToFind)
            {
                levelToReturn = level;
            }
        }

        return levelToReturn;
    }

    private void LoadLevelAndGameplayScene(int levelToLoadValue)
    {
        levelToLoad = ReturnLevelByNumber(levelToLoadValue);

        LoadScene(Scenes.Gameplay);
    }

    private void HandleLevelComplited(int score)
    {
        if(score >= levelToLoad.ScoreRequiredToWin && levelToLoad.LevelNumber == levels.Count)
        {
            GameEvents.OnShowVisibleUIElementsFromParameterAndHideOther.Invoke(new List<UIVisibleElementsGameplay>() { UIVisibleElementsGameplay.PopUpWindowGameWon });
        }
        else if(score >= levelToLoad.ScoreRequiredToWin)
        {
            if(levelToLoad.LevelNumber <= levels.Count && levelToLoad.LevelNumber == SavesManager.ReturnUnlockedLevels() + 1) SavesManager.UnlockNextLevel();
            if(levelToLoad.LevelNumber <= levels.Count) levelToLoad = ReturnLevelByNumber(levelToLoad.LevelNumber + 1);
            GameEvents.OnShowVisibleUIElementsFromParameterAndHideOther.Invoke(new List<UIVisibleElementsGameplay>() { UIVisibleElementsGameplay.PopUpWindowLevelWon });
        }
        else
        {
            GameEvents.OnShowVisibleUIElementsFromParameterAndHideOther.Invoke(new List<UIVisibleElementsGameplay>() { UIVisibleElementsGameplay.PopUpWindowLevelFailed });
        }

    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private void SubscribeEvents()
    {
        GameEvents.OnLoadScene += LoadScene;
        GameEvents.OnLoadLevelAndGameplayScene += LoadLevelAndGameplayScene;
        GameEvents.OnLevelComplited += HandleLevelComplited;
        GameEvents.OnGameQuit += QuitGame;
    }

    private void UnsubscribeEvents()
    {
        GameEvents.OnLoadLevelAndGameplayScene -= LoadLevelAndGameplayScene;
        GameEvents.OnLevelComplited -= HandleLevelComplited;
        GameEvents.OnLoadScene -= LoadScene;
        GameEvents.OnGameQuit -= QuitGame;
    }
}
