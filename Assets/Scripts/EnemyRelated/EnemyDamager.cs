using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : MonoBehaviour
{
    public float damageValue = 10f;
    public float lifetime = 3f, growSpeed = 3f;
    public bool hasKnockback = true, destroyParent, changeSizeOverTime = false, piercingWeapon = false;
    public int piercingCount = 0;
    public bool isPermanent = false; // for weapons like auras, they should be permanent and only get destroyed/spawned when upgraded
    private Vector3 targetSize;

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

            if(transform.localScale.x <= 0)
            {
                Destroy(gameObject);

                if (destroyParent)
                {
                    Destroy(transform.parent.gameObject);
                }               
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
                        enemiesInRange[i].TakeDamage(damageValue);
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
                other.GetComponent<EnemyController>().TakeDamage(damageValue, hasKnockback);

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
                other.GetComponent<DestructibleItem>().TakeHit();

                if (piercingWeapon)
                {
                    if (piercingCount > 0)
                    {
                        piercingCount--;
                    }
                    else if (piercingCount <= 0)
                    {
                        Debug.Log("TEST");
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
}
