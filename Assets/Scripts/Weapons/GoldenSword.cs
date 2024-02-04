using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenSword : BaseWeapon
{
    public EnemyDamager enemyDamager, goldenSword;
    public Transform swordHolder, swordToSpawn;
    public float rotationSpeed = 360f;
    private float attackCounter, direction; // cooldown
    private float lastDirection; // allow player to stand in one place and still attack
    private bool swingingRight = true;

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
        direction = Input.GetAxisRaw("Horizontal");

        // you need to be moving so sword gets direction
        if (direction != 0 || lastDirection != 0)
        {
            direction = direction == 0 ? lastDirection : direction;
            lastDirection = direction;
        }

        if (attackCounter <= 0)
        {
            attackCounter = stats[weaponLevel].attackCooldown;

            // pressing right
            if (direction > 0)
            {
                swingingRight = true;
            }
            else
            {
                swingingRight = false;
            }

            goldenSword = Instantiate(enemyDamager, enemyDamager.transform.position, Quaternion.Euler(0f, 0f, (swingingRight ? 40f : -40f)), swordHolder);
            goldenSword.gameObject.SetActive(true);
            SFXManager.instance.PlaySFXPitched(9);
        }
    }

    private void RotateWeapon()
    {
        if(swordHolder != null && goldenSword != null)
        {
            if(swingingRight)
            {
                goldenSword.transform.rotation = Quaternion.Euler(0f, 0f, (goldenSword.transform.rotation.eulerAngles.z - (rotationSpeed * Time.deltaTime * stats[weaponLevel].speed)));
            } else
            {
                goldenSword.transform.rotation = Quaternion.Euler(0f, 0f, (goldenSword.transform.rotation.eulerAngles.z + (rotationSpeed * Time.deltaTime * stats[weaponLevel].speed)));
            }
        }
    }

    public void SetStats()
    {
        enemyDamager.damageValue = stats[weaponLevel].damage;
        enemyDamager.lifetime = stats[weaponLevel].duration;
        enemyDamager.piercingWeapon = false;

        enemyDamager.transform.localScale = Vector3.one * stats[weaponLevel].range;

        rotationSpeed *= stats[weaponLevel].speed;
    }
}
