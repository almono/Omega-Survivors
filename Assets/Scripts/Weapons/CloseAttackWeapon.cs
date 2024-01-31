using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAttackWeapon : BaseWeapon
{
    public EnemyDamager enemyDamager;
    private float attackCounter, direction; // cooldown
    [SerializeField] private float lastDirection; // allow player to stand in one place and still attack

    private void Start()
    {
        SetStats();
    }

    private void Update()
    {
        if (statsUpdated)
        {
            statsUpdated = false;
            SetStats();
        }

        attackCounter -= Time.deltaTime;

        if(attackCounter <= 0)
        {
            attackCounter = stats[weaponLevel].attackCooldown;
            direction = Input.GetAxisRaw("Horizontal");

            // you need to be moving so sword gets direction
            if (direction != 0 || lastDirection != 0)
            {
                direction = direction == 0 ? lastDirection : direction;

                // pressing right
                if(direction > 0)
                {
                    enemyDamager.transform.rotation = Quaternion.identity;
                } else
                {
                    // rotate by 180 degrees
                    enemyDamager.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                }

                lastDirection = direction;
                Instantiate(enemyDamager, enemyDamager.transform.position, enemyDamager.transform.rotation, transform).gameObject.SetActive(true);
            }        

            // we already instantiated the main sword
            for (int i = 1; i < stats[weaponLevel].amount; i++)
            {
                float swordRotation = (360f / stats[weaponLevel].amount) * i; // make sure fireballs are evenly spaced out
                Instantiate(enemyDamager, enemyDamager.transform.position, Quaternion.Euler(0f, 0f, enemyDamager.transform.rotation.eulerAngles.z + swordRotation), transform).gameObject.SetActive(true);
            }

            SFXManager.instance.PlaySFXPitched(9);
        }
    }

    public void SetStats()
    {
        enemyDamager.damageValue = stats[weaponLevel].damage;
        enemyDamager.lifetime = stats[weaponLevel].duration;
        enemyDamager.piercingWeapon = false;

        enemyDamager.transform.localScale = Vector3.one * stats[weaponLevel].range;
        attackCounter = 0f;
    }
}
