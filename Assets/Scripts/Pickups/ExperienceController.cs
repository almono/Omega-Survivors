using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceController : MonoBehaviour
{
    public static ExperienceController instance;
    public float currentExperience, levelExpIncreaseModifier = 1.1f;
    public List<BaseWeapon> upgradeOptions;

    public Experience experiencePickup;

    public List<float> experienceLevels;
    public int currentLevel = 1, levelCount = 100;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        while(experienceLevels.Count < levelCount)
        {
            // every 5 levels incrase the requirements by 5% of what it is
            if(experienceLevels.Count % 5f == 0)
            {
                levelExpIncreaseModifier += (levelExpIncreaseModifier * 0.001f);
            }

            experienceLevels.Add(Mathf.Ceil(experienceLevels[experienceLevels.Count - 1] * levelExpIncreaseModifier));
        }
    }

    public void AddExperienceToPlayer(float experience)
    {
        currentExperience += experience;

        if(currentExperience >= experienceLevels[currentLevel])
        {
            LevelUp();
        }

        UIController.instance.UpdateExperience(currentExperience, experienceLevels[currentLevel], currentLevel);
        SFXManager.instance.PlaySFXPitched(2);
    }

    public void SpawnExperiencePickup(Vector3 position, float expValue)
    {
        Experience expPickup = Instantiate(experiencePickup, position, Quaternion.identity);
        expPickup.SetExpValue(expValue);
    }

    public void LevelUp()
    {
        // if there is any extra experience leftover then move it to the next level
        currentExperience -= experienceLevels[currentLevel];
        currentLevel++;

        // if we reached "max" level
        // make every next lvl require same xp amount as max level
        if(currentLevel >= experienceLevels.Count)
        {
            currentLevel = experienceLevels.Count - 1;
        }

        //PlayerController.instance.activeWeapon.LevelUp();

        UIController.instance.levelUpPanel.SetActive(true);
        Time.timeScale = 0; // stop time during level up screen

        //UIController.instance.levelUpButtons[1].UpdateBtnDisplay(PlayerController.instance.activeWeapon);

        // with every upgrade create new list
        upgradeOptions.Clear();

        List<BaseWeapon> availableWeapons = new List<BaseWeapon>();
        availableWeapons.AddRange(PlayerController.instance.assignedWeapons); // add currently used weapons

        if(availableWeapons.Count > 0)
        {
            int selected = Random.Range(0, availableWeapons.Count);
            upgradeOptions.Add(availableWeapons[selected]);
            availableWeapons.RemoveAt(selected);
        }

        // for adding new weapon unlock
        // fully upgraded weapons should be taken into consideration as well as already used weapons
        if ((PlayerController.instance.assignedWeapons.Count + PlayerController.instance.fullyUpgradedWeapons.Count) < PlayerController.instance.maxWeapons)
        {
            availableWeapons.AddRange(PlayerController.instance.unassignedWeapons);
        }        

        // for available weapon upgrades
        for(int i = upgradeOptions.Count; i < 3; i++)
        {
            if (availableWeapons.Count > 0)
            {
                int selected = Random.Range(0, availableWeapons.Count);
                upgradeOptions.Add(availableWeapons[selected]);
                availableWeapons.RemoveAt(selected);
            }
        }

        // prepare UI buttons
        for (int i = 0; i < upgradeOptions.Count; i++)
        {
            UIController.instance.levelUpButtons[i].UpdateBtnDisplay(upgradeOptions[i]);
        }

        for(int i = 0; i < UIController.instance.levelUpButtons.Length; i++)
        {
            if(i < upgradeOptions.Count)
            {
                UIController.instance.levelUpButtons[i].gameObject.SetActive(true);
            } else
            {
                UIController.instance.levelUpButtons[i].gameObject.SetActive(false);
            }
        }

        PlayerStatsController.instance.UpdateUpgradesDisplay();
    }
}
