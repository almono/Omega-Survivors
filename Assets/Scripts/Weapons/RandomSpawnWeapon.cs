using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RandomWeaponSpawn : BaseWeapon
{
    public EnemyDamager enemyDamager;
    private float spawnCounter;

    // Start is called before the first frame update
    void Start()
    {
        SetStats();
    }

    // Update is called once per frame
    void Update()
    {
        if (statsUpdated)
        {
            statsUpdated = false;
            SetStats();
        }

        spawnCounter -= Time.deltaTime;

        if (spawnCounter <= 0)
        {
            spawnCounter = stats[weaponLevel].attackCooldown;

            for (int i = 0; i < stats[weaponLevel].amount; i++)
            {
                Vector3 randomPosition = GetRandomCameraPosition();
                Instantiate(enemyDamager, randomPosition, enemyDamager.transform.rotation).gameObject.SetActive(true);
            }

            SFXManager.instance.PlaySFXPitched(4);
        }
    }

    public void SetStats()
    {
        enemyDamager.damageValue = stats[weaponLevel].damage;
        enemyDamager.lifetime = stats[weaponLevel].duration;
        enemyDamager.transform.localScale = Vector3.one * stats[weaponLevel].range;
    }

    private Vector3 GetRandomCameraPosition()
    {
        Camera mainCamera = Camera.main;

        if (mainCamera != null)
        {
            float cameraHeight = 2f * mainCamera.orthographicSize;
            float cameraWidth = cameraHeight * mainCamera.aspect;

            float randomX = Random.Range(-cameraWidth / 2f, cameraWidth / 2f);
            float randomY = Random.Range(-cameraHeight / 2f, cameraHeight / 2f);

            return new Vector3(randomX, randomY, 0f);
        }
        else
        {
            return Vector3.zero;
        }
    }
}
