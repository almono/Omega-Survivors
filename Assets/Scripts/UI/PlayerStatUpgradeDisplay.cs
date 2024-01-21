using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatUpgradeDisplay : MonoBehaviour
{
    public TMP_Text valueText, costText;
    public GameObject upgradeButton;

    // disable button if not enough money to ugprade
    public void DisableButton()
    {

    }

    public void UpdateDisplay(int cost, float oldValue, float newValue)
    {
        // ToString F1 shows up to one decimal place, also rounds number
        valueText.text = "Value: " + oldValue.ToString("F1") + " -> " + newValue.ToString("F1");
        costText.text = "Cost: " + cost;

        // check if we can afford the upgrade and enable/disable button based on that
        if(cost <= CoinController.instance.currentCoins)
        {
            upgradeButton.SetActive(true);
        } else
        {
            upgradeButton.SetActive(false);
        }
    }

    public void ShowMaxLevel()
    {
        valueText.text = "Max Level";
        costText.text = "---";
        upgradeButton.SetActive(false);
    }
}
