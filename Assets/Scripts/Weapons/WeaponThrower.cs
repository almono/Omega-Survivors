using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponThrower : BaseWeapon
{
    public EnemyDamager enemyDamager;
    private float throwCounter;

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

        throwCounter -= Time.deltaTime;

        if(throwCounter <= 0)
        {
            throwCounter = stats[weaponLevel].attackCooldown;

            for(int i = 0; i < stats[weaponLevel].amount; i++)
            {
                Instantiate(enemyDamager, enemyDamager.transform.position, enemyDamager.transform.rotation).gameObject.SetActive(true);
            }

            SFXManager.instance.PlaySFXPitched(4);
        }
    }

    public void SetStats()
    {
        enemyDamager.damageValue = stats[weaponLevel].damage;
        enemyDamager.lifetime = stats[weaponLevel].duration;
        enemyDamager.piercingWeapon = true;
        enemyDamager.piercingCount = stats[weaponLevel].piercingCount;
        enemyDamager.transform.localScale = Vector3.one * stats[weaponLevel].range;
        throwCounter = 0f;
    }
}
