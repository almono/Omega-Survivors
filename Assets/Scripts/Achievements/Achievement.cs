using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAchievement", menuName = "New Achievement")]
public class Achievement : ScriptableObject
{
    public string achievementName, achievementDescription = null;
    public float currentProgress = 0f, progressNeeded = 1f;
    public bool isUnlocked;

    public void UpdateAchievementProgress(int progress)
    {
        if(!isUnlocked)
        {
            currentProgress += progress;

            if(currentProgress >= progressNeeded)
            {
                isUnlocked = true;
            }
        }
    }

    public string GetName() { return achievementName; }
    public string GetDescription() {  return achievementDescription; }
    public bool IsUnlocked() {  return isUnlocked; }
    public float GetCurrentProgress() {  return currentProgress; }
    public float GetProgressNeeded() { return progressNeeded; }
}
