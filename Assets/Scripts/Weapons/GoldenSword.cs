using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenSword : BaseWeapon
{
    public EnemyDamager enemyDamager;
    public Transform swordHolder, swordToSpawn;
    public float rotationSpeed = 1f;
    private float attackCounter, direction; // cooldown
    private float lastDirection; // allow player to stand in one place and still attack

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

        PerformAttackChecks();
        RotateWeapon();
    }

    private void PerformAttackChecks()
    {
        attackCounter -= Time.deltaTime;

        if (attackCounter <= 0)
        {
            attackCounter = stats[weaponLevel].attackCooldown;
            direction = Input.GetAxisRaw("Horizontal");

            // you need to be moving so sword gets direction
            if (direction != 0 || lastDirection != 0)
            {
                direction = direction == 0 ? lastDirection : direction;

                // pressing right
                if (direction > 0)
                {
                    enemyDamager.transform.localScale = new Vector3(1f, 1f, 1f);
                }
                else
                {
                    enemyDamager.transform.localScale = new Vector3(-1f, 1f, 1f);
                }

                lastDirection = direction;
            }

            Debug.Log(enemyDamager.transform.rotation);
            Instantiate(enemyDamager, enemyDamager.transform.position, enemyDamager.transform.rotation, swordHolder).gameObject.SetActive(true);
            SFXManager.instance.PlaySFXPitched(9);
        }
    }

    private void RotateWeapon()
    {
        if(swordHolder != null)
        {
            swordHolder.rotation = Quaternion.Euler(0f, 0f, swordHolder.rotation.eulerAngles.z + (rotationSpeed * Time.deltaTime * stats[weaponLevel].speed));
        }
    }

    public void SetStats()
    {
        enemyDamager.damageValue = stats[weaponLevel].damage;
        enemyDamager.lifetime = stats[weaponLevel].duration;
        enemyDamager.piercingWeapon = false;

        enemyDamager.transform.localScale = Vector3.one * stats[weaponLevel].range;

        rotationSpeed *= rotationSpeed * stats[weaponLevel].speed;
    }
}
