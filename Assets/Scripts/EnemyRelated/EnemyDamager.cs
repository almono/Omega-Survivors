using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : MonoBehaviour
{
    [Header("Basic Config")]
    public float damageValue = 10f;
    public float lifetime = 3f, growSpeed = 3f;
    public bool hasKnockback = true, destroyParent, changeSizeOverTime = false, piercingWeapon = false;
    public int piercingCount = 0;
    public bool isPermanent = false; // for weapons like auras, they should be permanent and only get destroyed/spawned when upgraded
    private Vector3 targetSize;

    // when enemy gets hit
    [Header("Hit Effects")]
    public GameObject hitEffect;
    public AudioClip hitEffectSound;
    public float hitEffectDamageScale = 1f;

    [Header("On hit projectile spawner")]
    public GameObject hitEffectObject;
    public int hitEffectObjectAmount = 1;

    // if the main damager should spawn any additional sub-damagers
    [Header("Subprojectile spawner")]
    public SubProjectile subDamager;
    public int subDamagerAmount = 1, subDamagerReleaseCount = 1;
    public float subDamagerReleaseCooldown = 1f;

    [Header("AOE Attacks")]
    public bool isDamageOverTime;
    public float damageOverTimeFrequency;
    private float damageOverTimeCounter;

    private List<EnemyController> enemiesInRange = new List<EnemyController>();
    private List<DestructibleItem> destructibleInRange = new List<DestructibleItem>();

    // Start is called before the first frame update
    void Start()
    {
        //Destroy(transform.parent.gameObject, lifetime);
        targetSize = transform.localScale;

        if(changeSizeOverTime)
        {
            transform.localScale = Vector3.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(changeSizeOverTime)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, growSpeed * Time.deltaTime);
        }        

        if(!isPermanent)
        {
            lifetime -= Time.deltaTime;
        }

        if(lifetime <= 0)
        {
            targetSize = Vector3.zero;
            Destroy(gameObject);

            if (destroyParent)
            {
                Destroy(transform.parent.gameObject);
            }
        }

        if(isDamageOverTime)
        {
            damageOverTimeCounter -= Time.deltaTime;

            if (damageOverTimeCounter <= 0)
            {
                damageOverTimeCounter = damageOverTimeFrequency;

                for(int i = 0; i < enemiesInRange.Count; i++)
                {
                    if (enemiesInRange[i] != null)
                    {
                        // damage over time attack CAN NOT crit
                        float finalDamage = damageValue * TempBuffController.instance.damageBuffMultiplier;
                        //damageDealt += finalDamage * enemiesInRange.Count;
                        enemiesInRange[i].TakeDamage(finalDamage);
                    } else
                    {
                        // when we remove element from list
                        // all other elements will move by one space
                        // i-- makes sure the enemy that replaces the just deleted enemy
                        // will get damaged since it will take its index value
                        enemiesInRange.RemoveAt(i);
                        i--;
                    }
                }

                for (int i = 0; i < destructibleInRange.Count; i++)
                {
                    if (destructibleInRange[i] != null)
                    {
                        destructibleInRange[i].TakeHit();
                    }
                    else
                    {
                        destructibleInRange.RemoveAt(i);
                        i--;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(isDamageOverTime)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                enemiesInRange.Add(other.GetComponent<EnemyController>());
            } else if(other.gameObject.CompareTag("DestructibleItem"))
            {
                destructibleInRange.Add(other.GetComponent<DestructibleItem>());
            }
        } else
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                float finalDamage = damageValue * TempBuffController.instance.damageBuffMultiplier;

                // check if the damage will be a crit
                bool isCrit = false;
                float critChanceRoll = Random.Range(0f, 1f);
                if (critChanceRoll <= (PlayerController.instance.critChance + TempBuffController.instance.critChanceMultiplier))
                {
                    finalDamage *= (PlayerController.instance.critMultiplier + TempBuffController.instance.critMultiplierValue); // multiply final damage by crit multiplier stat
                    isCrit = true;
                }

                if (hitEffect != null)
                {
                    CreateHitEffect();
                }

                //damageDealt += finalDamage;
                other.GetComponent<EnemyController>().TakeDamage(finalDamage, hasKnockback, isCrit);                

                // check for potential piercing attribute
                if(piercingWeapon)
                {
                    if (piercingCount > 0)
                    {
                        piercingCount--;
                    }
                    else if (piercingCount <= 0)
                    {
                        Destroy(gameObject);
                    }
                }
            } else if(other.gameObject.CompareTag("DestructibleItem"))
            {
                DestructibleItem destructible = other.GetComponent<DestructibleItem>();

                if(destructible.canBeDestroyed)
                {
                    if (hitEffect != null)
                    {
                        CreateHitEffect();
                    }

                    destructible.TakeHit();
                }

                if (piercingWeapon)
                {
                    if (piercingCount > 0)
                    {
                        piercingCount--;
                    }
                    else if (piercingCount <= 0)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(isDamageOverTime)
        {
            if(other.gameObject.CompareTag("Enemy"))
            {
                if (enemiesInRange.Contains(other.GetComponent<EnemyController>()))
                {
                    enemiesInRange.Remove(other.GetComponent<EnemyController>());
                }
            }
        }
    }

    private void CreateHitEffect()
    {
        GameObject newHitEffect = Instantiate(hitEffect, transform.position, transform.rotation);
        newHitEffect.SetActive(true);
        HitEffectController hitDamager = newHitEffect.GetComponent<HitEffectController>();

        if (hitDamager != null)
        {
            hitDamager.mainDamage = damageValue;
            hitDamager.damageScale = hitEffectDamageScale;
        }
    }
}
