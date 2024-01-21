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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthSlider.value = currentHealth;

        if (currentHealth <= 0) 
        {
            Destroy(gameObject);
            GameManager.instance.EndLevel();
            Instantiate(deathEffect, transform.position, transform.rotation);
        }
    }
}
