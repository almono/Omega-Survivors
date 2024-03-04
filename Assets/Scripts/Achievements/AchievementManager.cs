using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public AchievementManager instance;
    public Achievement[] achievements;

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
}
