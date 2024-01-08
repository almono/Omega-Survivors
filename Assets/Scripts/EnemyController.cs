using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 1f, damageValue = 10f, hitWaitTime = 0.5f;
    private Rigidbody2D body;
    private Transform target;
    private SpriteRenderer enemySprite;

    private float hitCounter;

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;
        body = GetComponent<Rigidbody2D>();
        enemySprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
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
            enemySprite.transform.localScale = new Vector2(currentScale, 1f);
        }
        else
        {
            enemySprite.transform.localScale = new Vector2(-currentScale, 1f);
        }
    }
}
