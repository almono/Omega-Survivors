using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager instance;
    public Achievement[] achievements;
    public GameObject achievementBox, achievementHolder;

    void Start()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        } else
        {
            Destroy(gameObject);
        }
    }
    public void UpdateAchievementStatus(string achievementName, float progress)
    {

    }

    public void ShowAchievement(Achievement achievement)
    {
        GameObject newAchievement = Instantiate(achievementBox, transform.position, Quaternion.identity, achievementHolder.transform);
        newAchievement.SetActive(true);
    }
}
