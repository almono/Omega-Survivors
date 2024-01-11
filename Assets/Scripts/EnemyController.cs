using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 1f, damageValue = 10f, hitWaitTime = 0.5f, health = 5f, knockbackTime = 0.05f;
    private Rigidbody2D body;
    private Transform target;
    public SpriteRenderer enemySprite;

    private float hitCounter, knockbackCounter;
    public bool despawnAtFarDistance = true; // flag to despawn at far distance

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerHealthController.instance)
        {
            target = PlayerHealthController.instance.transform;
        }
       
        body = GetComponent<Rigidbody2D>();
        enemySprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(knockbackCounter > 0)
        {
            knockbackCounter -= Time.deltaTime;

            if(moveSpeed > 0)
            {
                moveSpeed = -moveSpeed * 10f; // knockback
            }

            if(knockbackCounter <= 0)
            {
                moveSpeed = Mathf.Abs(moveSpeed * 0.1f);
            }
        }

        if(target != null)
        {
            FlipSprite();
            body.velocity = (target.position - transform.position).normalized * moveSpeed;
        }
        
        if(hitCounter > 0)
        {
            hitCounter -= Time.deltaTime;
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player") && hitCounter <= 0)
        {
            PlayerHealthController.instance.TakeDamage(damageValue);
            hitCounter = hitWaitTime;
        }
    }

    public void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && hitCounter <= 0)
        {
            PlayerHealthController.instance.TakeDamage(damageValue);
            hitCounter = hitWaitTime;
        }
    }

    public void FlipSprite()
    {
        var currentScale = enemySprite.transform.localScale.x;

        // flip sprite if the enemy is on the left/right of player
        if (transform.position.x > target.position.x)
        {
            enemySprite.transform.localScale = new Vector2(Mathf.Abs(currentScale), 1f);
        }
        else
        {
            enemySprite.transform.localScale = new Vector2(Mathf.Abs(currentScale) * -1, 1f);
        }
    }

    public void TakeDamage(float damageValue)
    {
        health -= damageValue;

        if(health <= 0)
        {
            Destroy(gameObject);
        }

        DamageNumberController.instance.SpawnDamageNumber(damageValue, transform.position);
    }

    public void TakeDamage(float damageValue, bool applyKnockback)
    {
        TakeDamage(damageValue);

        if(applyKnockback)
        {
            knockbackCounter = knockbackTime;
        }
    }
}
