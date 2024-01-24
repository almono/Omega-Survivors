using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyAura : BaseWeapon
{
    public EnemyDamager enemyDamager;
    private float spawnTime, spawnCounter;

    void Start()
    {
        SetStats();
    }

    void Update()
    {
        if (statsUpdated)
        {
            statsUpdated = false;
            SetStats();
        }

        spawnCounter -= Time.deltaTime;

        if(spawnCounter <= 0f)
        {
            spawnCounter = spawnTime;

            Instantiate(enemyDamager, transform.position, Quaternion.identity, transform).gameObject.SetActive(true);
            SFXManager.instance.PlaySFXPitched(10);
        }
    }

    public void SetStats()
    {
        enemyDamager.damageValue = stats[weaponLevel].damage;
        enemyDamager.lifetime = stats[weaponLevel].duration;
        enemyDamager.damageOverTimeFrequency = stats[weaponLevel].speed;
        enemyDamager.transform.localScale = Vector3.one * stats[weaponLevel].range;
        spawnTime = stats[weaponLevel].attackCooldown;
        spawnCounter = 0f;
    }
}
