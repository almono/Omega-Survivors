using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAchievement", menuName = "New Achievement")]
public class Achievement : ScriptableObject
{
    public string achievementName, achievementDescription = null;
    public float currentProgress = 0f;
    public int currentAchievementLevel = 0;
    public bool isFullyUnlocked = false;

    public AchievementStages[] achievementStages;

    public void UpdateAchievementProgress(int progress)
    {
        currentProgress += progress;

        if (!isFullyUnlocked)
        {
            CheckAchievementAdvancement(); // to check if the current achievement stage can go to next one
        }
    }

    public void CheckAchievementAdvancement()
    {
        if(currentAchievementLevel < achievementStages.Length && !isFullyUnlocked)
        {

            if (currentProgress >= achievementStages[currentAchievementLevel].progressRequired)
            {
                currentAchievementLevel++;
                currentProgress = 0f;
            }
        } else
        {
            isFullyUnlocked = true;
        }
    }

    public string GetName() { return achievementName; }
    public string GetDescription() {  return achievementDescription; }
    public bool IsUnlocked() {  return isFullyUnlocked; }
    public float GetCurrentProgress() {  return currentProgress; }
    public float GetProgressNeeded() 
    { 
        if (achievementStages[currentAchievementLevel] != null)
        {
            return achievementStages[currentAchievementLevel].progressRequired;
        } else
        {
            return 1f;
        }
        
    }
    public int GetAchievementStage() { return currentAchievementLevel; }
}

[System.Serializable]
public class AchievementStages
{
    public string subDescription;
    public int stageLevel = 1;
    public float progressRequired = 100f;
}
