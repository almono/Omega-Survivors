using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TempBuffController : MonoBehaviour
{
    public static TempBuffController instance;
    public List<GameObject> activeBuffs = new List<GameObject>();
    public int activeBuffCount = 0;

    [Header("Speed Buffs")]
    public GameObject speedBuffIcon;
    public TMP_Text speedBuffText;
    public float moveSpeedBuffCounter = 0f;
    public float moveSpeedBuffMultiplier = 1f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Transform uiTextTransform = speedBuffIcon.transform.Find("BuffCounter");
        speedBuffText = uiTextTransform.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        CheckTempBuffsStatus();
        //float minutes = Mathf.FloorToInt(currentTime / 60f);
        //float seconds = Mathf.FloorToInt(currentTime % 60);
    }

    public void ApplySpeedBuff(float speedMultiplier = 1.5f, float speedDuration = 60f)
    {
        activeBuffCount++;
        activeBuffs.Add(speedBuffIcon);
        moveSpeedBuffCounter = speedDuration; // length of buff in seconds
        moveSpeedBuffMultiplier = speedMultiplier;
        speedBuffIcon.SetActive(true);
    }

    public void CheckTempBuffsStatus()
    {
        if (moveSpeedBuffCounter > 0)
        {
            moveSpeedBuffCounter -= Time.deltaTime;
            speedBuffText.text = moveSpeedBuffCounter.ToString("00");
        }
        else
        {
            moveSpeedBuffMultiplier = 1f;

            if(speedBuffIcon.activeSelf)
            {
                activeBuffCount--;
                activeBuffs.Remove(speedBuffIcon);
                speedBuffIcon.SetActive(false);
            }
        }
    }
}
