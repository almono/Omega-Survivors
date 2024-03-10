using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuAchievementIcon : MonoBehaviour
{
    public TMP_Text achievementName;
    public bool isUnlocked;
    public Image icon;
    public Slider achievementProgress;

    public void SetAchievementData(Achievement achievement)
    {
        achievementName.text = achievement.GetName();
        achievementProgress.value = achievement.GetCurrentProgress();
        achievementProgress.maxValue = achievement.GetProgressNeeded();
        isUnlocked = achievement.IsUnlocked();

        if(!isUnlocked)
        {
            Image iconImage = icon.GetComponent<Image>();

            Color currentColor = iconImage.color;
            currentColor.a = 0.3f;
            iconImage.color = currentColor;
        }
    }
}
