using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementBox : MonoBehaviour
{
    public float showDuration = 450f;
    public TMP_Text achievementName, achievementDescription;
    public CanvasRenderer achievementLine;
    private float lifeTime;
    private CanvasRenderer render;

    private void Start()
    {
        lifeTime = showDuration;

        render = GetComponent<CanvasRenderer>();
        
    }

    private void Update()
    {
        if(lifeTime >= 0)
        {
            lifeTime--;
            render.SetAlpha(lifeTime / showDuration);
            achievementName.alpha = lifeTime / showDuration;
            achievementDescription.alpha = lifeTime / showDuration;
            achievementLine.SetAlpha(lifeTime / showDuration);
        }

        if(lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetAchievementInfo(Achievement achievement)
    {
        achievementName.text = achievement.GetName();
        achievementDescription.text = achievement.GetDescription() != null ? achievement.GetDescription() : "---";
    }
}
