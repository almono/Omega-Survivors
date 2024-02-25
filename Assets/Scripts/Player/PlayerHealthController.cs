using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public float currentHealth = 100f, maxHealth = 100f;

    public Slider healthSlider;
    public GameObject deathEffect;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        currentHealth = maxHealth;
        maxHealth = PlayerStatsController.instance.health[0].value;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthSlider.value = currentHealth;

        if (currentHealth <= 0) 
        {
            UIController.instance.SetDamageResults();
            SFXManager.instance.PlaySFXPitched(3);
            Destroy(gameObject);
            GameManager.instance.EndLevel();
            Instantiate(deathEffect, transform.position, transform.rotation);
        }
    }

    public void Heal(float healthAmount)
    {
        currentHealth += healthAmount;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        healthSlider.value = currentHealth;
    }
}
