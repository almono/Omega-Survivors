using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAchievement", menuName = "New Achievement")]
public class Achievement : ScriptableObject
{
    public string achievementName;
    public int currentProgress, progressNeeded;
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
