using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public Slider experienceSlider;
    public TMP_Text experienceText, coinText, timerText, endTimeText, levelsLeftText;
    public string mainMenuName = "MainMenu";

    public LevelUpSelectionBtn[] levelUpButtons;
    public GameObject levelUpPanel, levelEndScreen, pauseScreen, levelUpsLeft;

    public PlayerStatUpgradeDisplay moveSpeedUpgradeDisplay, healthUpgradeDisplay, rangeUpgradeDisplay, maxWeaponsUpgradeDisplay;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            PauseUnpauseGame();
        }
    }

    public void UpdateExperience(float currentExperience, float levelExperience, int currentLevel)
    {
        experienceSlider.maxValue = levelExperience;
        experienceSlider.value = currentExperience;

        experienceText.text = "Level: " + currentLevel.ToString();
    }

    public void SkipLevelUp()
    {
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;

        ExperienceController.instance.CheckForLevelUp();
    }

    public void UpdateCoins()
    {
        coinText.text = "Coins: " + CoinController.instance.currentCoins;
    }

    public void PurchaseMoveSpeed()
    {
        PlayerStatsController.instance.PurchaseMoveSpeed();
        SkipLevelUp();
    }

    public void PurchaseHealth()
    {
        PlayerStatsController.instance.PurchaseHealth();
        SkipLevelUp();
    }

    public void PurchasePickupRange()
    {
        PlayerStatsController.instance.PurchasePickupRange();
        SkipLevelUp();
    }

    public void PurchaseMaxWeapons()
    {
        PlayerStatsController.instance.PurchaseMaxWeapons();
        SkipLevelUp();
    }

    public void UpdateTimerText(float currentTime)
    {
        float minutes = Mathf.FloorToInt(currentTime / 60f);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = "Time: " + minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuName);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseUnpauseGame()
    {
        if(pauseScreen.activeSelf)
        {
            pauseScreen.SetActive(false);

            if (levelUpPanel.activeSelf == false)
            {
                Time.timeScale = 1f;
            }
        } else
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
