using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AchievementBox : MonoBehaviour
{
    public float showDuration = 150f;
    public TMP_Text achievementName, achievementDescription;

    public void SetAchievementInfo(Achievement achievement)
    {
        achievementName.text = achievement.GetName();
        achievementDescription.text = achievement.GetDescription() != null ? achievement.GetDescription() : "---";
    }
}
