using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAchievement", menuName = "New Achievement")]
public class Achievement : ScriptableObject
{
    public string achievementName;
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
}
