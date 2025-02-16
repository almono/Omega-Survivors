using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour, IGameData
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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            //ShowAchievement(achievements[0]);
            achievements[1].UpdateAchievementProgress(1);
        }
    }
    public void UpdateAchievementStatus(string achievementName, float progress)
    {

    }

    public void ShowAchievement(Achievement achievement)
    {
        if(achievementHolder != null)
        {
            GameObject newAchievement = Instantiate(achievementBox, achievementHolder.transform.position, Quaternion.identity, achievementHolder.transform);
            newAchievement.SetActive(true);
        }
    }

    public void LoadData(GameData gameData)
    {
        this.achievements = gameData.achievements;
    }

    public void SaveData(ref GameData data)
    {
        data.achievements = this.achievements;
    }
}
