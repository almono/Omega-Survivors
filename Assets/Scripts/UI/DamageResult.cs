using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageResult : MonoBehaviour
{
    public float damageValue = 0f, dpsValue = 0f;
    public TMP_Text damageText, dpsText;

    public void SetDamage(float damage)
    {
        damageValue = damage;
        damageText.text = damage.ToString();
    }

    public void SetDPS()
    {
        dpsValue = 0f;
        dpsText.text = dpsValue.ToString();
    }
}
