using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : MonoBehaviour
{
    public float damageValue = 10f;
    public float lifetime = 3f, growSpeed = 3f;
    public bool hasKnockback = true, destroyParent;
    private Vector3 targetSize;

    [Header("AOE Attacks")]
    public bool isDamageOverTime;
    public float damageOverTimeFrequency;
    private float damageOverTimeCounter;
    private List<EnemyController> enemiesInRange = new List<EnemyController>();

    // Start is called before the first frame update
    void Start()
    {
        //Destroy(transform.parent.gameObject, lifetime);
        targetSize = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, growSpeed * Time.deltaTime);

        lifetime -= Time.deltaTime;

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
            }
        } else
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.GetComponent<EnemyController>().TakeDamage(damageValue, hasKnockback);
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
