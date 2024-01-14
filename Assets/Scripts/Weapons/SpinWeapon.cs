using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinWeapon : BaseWeapon
{
    public float rotationSpeed = 180f;
    public Transform fireballHolder, fireballToSpawn;

    public float cooldownTime;
    private float cooldownCounter;

    public EnemyDamager enemyDamager;

    void Start()
    {
        SetStats();
    }

    void Update()
    {
        // Quaternion.Euler converts Quaternion to Vector3
        //fireballHolder.rotation = Quaternion.Euler(0f, 0f, fireballHolder.rotation.eulerAngles.z + (rotationSpeed * Time.deltaTime));
        fireballHolder.rotation = Quaternion.Euler(0f, 0f, fireballHolder.rotation.eulerAngles.z + (rotationSpeed * Time.deltaTime * stats[weaponLevel].speed));

        cooldownCounter -= Time.deltaTime;

        if(cooldownCounter <= 0)
        {
            cooldownCounter = cooldownTime;
            
            // randomize the Z rotation to make it so the fireball will spawn in different places
            Quaternion randomizedRotation = Quaternion.Euler(fireballHolder.rotation.x, fireballHolder.rotation.y, Random.Range(0, 360));
            Instantiate(fireballToSpawn, fireballHolder.position, randomizedRotation, fireballHolder).gameObject.SetActive(true);
        }

        if(statsUpdated)
        {
            statsUpdated = false;
            SetStats();
        }
    }

    // set stats based on parent class
    public void SetStats()
    {
        enemyDamager.damageValue = stats[weaponLevel].damage;

        transform.localScale = Vector3.one * stats[weaponLevel].range;

        cooldownTime = stats[weaponLevel].attackCooldown;

        enemyDamager.lifetime = stats[weaponLevel].duration;

        cooldownCounter = 0;
    }
}
