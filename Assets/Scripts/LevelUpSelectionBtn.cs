using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpSelectionBtn : MonoBehaviour
{
    public TMP_Text upgradeDesc, nameLevelText;
    public Image weaponIcon;

    private BaseWeapon assignedWeapon;

    public void UpdateBtnDisplay(BaseWeapon weapon)
    {
        upgradeDesc.text = weapon.stats[weapon.weaponLevel].upgradeText;
        weaponIcon.sprite = weapon.weaponIcon;
        nameLevelText.text = weapon.name + " - " + weapon.weaponLevel.ToString();

        assignedWeapon = weapon;
    }

    public void SelectUpgrade()
    {
        if(assignedWeapon != null)
        {
            assignedWeapon.LevelUp();

            // after selecting upgrade hide the panel and resume time flow
            UIController.instance.levelUpPanel.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }
}
