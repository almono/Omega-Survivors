using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public Slider experienceSlider;
    public TMP_Text experienceText;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateExperience(float currentExperience, float levelExperience, int currentLevel)
    {
        experienceSlider.maxValue = levelExperience;
        experienceSlider.value = currentExperience;

        experienceText.text = "Level: " + currentLevel.ToString();
    }
}
