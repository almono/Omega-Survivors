using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string firstLevelName;
    public GameObject mainMenu, characterSelector, achievementDisplay;

    private void Start()
    {
        mainMenu.SetActive(true);
        characterSelector.SetActive(false);
        achievementDisplay.SetActive(false);

        Debug.Log(AchievementManager.instance.achievements);
        GameDataManager.instance.LoadGame();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(firstLevelName);
    }

    public void OpenCharacterSelection()
    {
        mainMenu.SetActive(false);
        characterSelector.SetActive(true);
        achievementDisplay.SetActive(false);
    }

    public void OpenAchievementWindow()
    {
        mainMenu.SetActive(false);
        characterSelector.SetActive(false);
        achievementDisplay.SetActive(true);
    }

    public void CloseAchievementWindow()
    {
        mainMenu.SetActive(true);
        characterSelector.SetActive(false);
        achievementDisplay.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
