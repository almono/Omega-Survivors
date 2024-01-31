using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyAura : BaseWeapon
{
    // Holy aura is a "permanent" weapon type so once it is spawned it should stay active all the time
    // Only updating stats should change it
    public EnemyDamager enemyDamager;
    public EnemyDamager currentAura;

    void Start()
    {
        SetStats();
        SpawnAura();
    }

    void Update()
    {
        if (statsUpdated)
        {
            statsUpdated = false;
            SetStats();
            UpdateAuraStats();
        }
    }

    private void SpawnAura()
    {
        currentAura = Instantiate(enemyDamager, transform.position, Quaternion.identity, transform);
        currentAura.gameObject.SetActive(true);

        SFXManager.instance.PlaySFXPitched(10);
    }

    private void UpdateAuraStats()
    {
        currentAura.damageValue = stats[weaponLevel].damage;
        currentAura.damageOverTimeFrequency = stats[weaponLevel].speed;
        currentAura.transform.localScale = Vector3.one * stats[weaponLevel].range;
    }

    public void SetStats()
    {
        enemyDamager.damageValue = stats[weaponLevel].damage;
        enemyDamager.lifetime = stats[weaponLevel].duration;
        enemyDamager.damageOverTimeFrequency = stats[weaponLevel].speed;
        enemyDamager.transform.localScale = Vector3.one * stats[weaponLevel].range;
        enemyDamager.isPermanent = true;
    }
}
