using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceController : MonoBehaviour
{
    public static ExperienceController instance;
    public float currentExperience, levelExpIncreaseModifier = 1.1f;

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
    }
}
