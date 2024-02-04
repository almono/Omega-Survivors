using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TempBuffController : MonoBehaviour
{
    public static TempBuffController instance;
    public List<GameObject> activeBuffs = new List<GameObject>();

    [Header("Speed Buff")]
    public GameObject speedBuffIcon;
    public TMP_Text speedBuffText;
    public float moveSpeedBuffCounter = 0f;
    public float moveSpeedBuffMultiplier = 1f;

    [Header("Damage Buff")]
    public GameObject damageBuffIcon;
    public TMP_Text damageBuffText;
    public float damageBuffCounter = 0f;
    public float damageBuffMultiplier = 1f;

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
        activeBuffs.Add(speedBuffIcon);
        moveSpeedBuffCounter = speedDuration; // length of buff in seconds
        moveSpeedBuffMultiplier = speedMultiplier;
        speedBuffIcon.SetActive(true);
    }

    public void ApplyDamageBuff(float attackMultiplier = 1.2f, float attackDuration = 30f)
    {
        activeBuffs.Add(damageBuffIcon);
        damageBuffCounter = attackDuration; // length of buff in seconds
        damageBuffMultiplier = attackMultiplier;
        damageBuffIcon.SetActive(true);
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
                activeBuffs.Remove(speedBuffIcon);
                speedBuffIcon.SetActive(false);
            }
        }

        if (damageBuffCounter > 0)
        {
            damageBuffCounter -= Time.deltaTime;
            damageBuffText.text = damageBuffCounter.ToString("00");
        }
        else
        {
            damageBuffMultiplier = 1f;

            if (damageBuffIcon.activeSelf)
            {
                activeBuffs.Remove(damageBuffIcon);
                damageBuffIcon.SetActive(false);
            }
        }
    }
}
