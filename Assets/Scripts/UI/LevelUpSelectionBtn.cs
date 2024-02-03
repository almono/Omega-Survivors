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
        if(weapon.gameObject.activeSelf)
        {
            // if weapon is active then it means player already has it
            // so we upgrade it
            upgradeDesc.text = weapon.stats[weapon.weaponLevel].nextUpgradeText;
            nameLevelText.text = weapon.name + " - " + weapon.weaponLevel;
        } else
        {
            // we dont have that weapon yet
            upgradeDesc.text = "Unlock " + weapon.name;
            nameLevelText.text = weapon.name;
            
        }

        weaponIcon.sprite = weapon.weaponIcon;
        assignedWeapon = weapon;
    }

    public void SelectUpgrade()
    {
        if(assignedWeapon != null)
        {
            if(assignedWeapon.gameObject.activeSelf == false)
            {
                // we dont have that weapon yet
                PlayerController.instance.AddWeapon(assignedWeapon);
            } else
            {
                // we have that weapon
                assignedWeapon.LevelUp();
            }

            // after selecting upgrade hide the panel and resume time flow
            UIController.instance.levelUpPanel.SetActive(false);
            Time.timeScale = 1.0f;

            ExperienceController.instance.CheckForLevelUp();
        }
    }
}
