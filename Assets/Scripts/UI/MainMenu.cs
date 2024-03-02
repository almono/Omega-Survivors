using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string firstLevelName;
    public GameObject mainMenu, characterSelector;

    private void Start()
    {
        mainMenu.SetActive(true);
        characterSelector.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(firstLevelName);
    }

    public void OpenCharacterSelection()
    {
        mainMenu.SetActive(false);
        characterSelector.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
