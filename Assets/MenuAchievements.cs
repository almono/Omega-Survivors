using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAchievements : MonoBehaviour
{
    public GameObject achievementIcon, achievementHolder;

    private void Start()
    {
        if(achievementIcon != null)
        {
            Achievement[] availableAchievements = AchievementManager.instance.achievements;

            if(availableAchievements.Length > 0 )
            {
                foreach(Achievement achievement in availableAchievements)
                {
                    GameObject newAchievement = Instantiate(achievementIcon, achievementHolder.transform.position, Quaternion.identity, achievementHolder.transform);
                    MenuAchievementIcon newAchievementData = newAchievement.GetComponent<MenuAchievementIcon>();
                    newAchievementData.SetAchievementData(achievement);

                    newAchievement.SetActive(true);
                }
            }
        }
    }
}
