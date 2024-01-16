using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : BaseWeapon
{
    public EnemyDamager enemyDamager;
    public Projectile projectile;

    private float shotCounter; // how often we fire
    public float weaponRange; // how far it goes

    public LayerMask whatIsEnemy; // to find out nearest enemy

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

        shotCounter -= Time.deltaTime;

        if(shotCounter <= 0 )
        {
            shotCounter = stats[weaponLevel].attackCooldown;

            // fire only if there is an enemy in a range
            // get all enemies on that layer in that range
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, weaponRange * stats[weaponLevel].range, whatIsEnemy);

            if(enemies.Length > 0 )
            {
                // for on each PROJECTILE / WEAPON so we can set all of their position ( amount upgrade )
                for(int i = 0; i < stats[weaponLevel].amount; i++)
                {
                    Vector3 targetPosition = enemies[Random.Range(0, enemies.Length)].transform.position; // get position of random enemy in rage

                    // get direction towards enemy
                    Vector3 directionTowardEnemy = targetPosition - transform.position;
                    float angle = Mathf.Atan2(directionTowardEnemy.y, directionTowardEnemy.x) * Mathf.Rad2Deg; // Rad2Deg converts to 360 degree adnotation
                    angle -= 90; // because sprite is facing up
                    projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); // set the rotation of projectile

                    Instantiate(projectile, projectile.transform.position, projectile.transform.rotation).gameObject.SetActive(true);
                    
                }
            }
        }
    }

    public void SetStats()
    {
        enemyDamager.damageValue = stats[weaponLevel].damage;
        enemyDamager.lifetime = stats[weaponLevel].duration;
        enemyDamager.piercingWeapon = true;
        enemyDamager.piercingCount = stats[weaponLevel].piercingCount;
        projectile.moveSpeed = stats[weaponLevel].speed;
        enemyDamager.transform.localScale = Vector3.one * stats[weaponLevel].range;
        shotCounter = 0f;
    }
}
