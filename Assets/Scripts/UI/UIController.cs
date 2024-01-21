using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public Slider experienceSlider;
    public TMP_Text experienceText, coinText;

    public LevelUpSelectionBtn[] levelUpButtons;
    public GameObject levelUpPanel;

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
}
