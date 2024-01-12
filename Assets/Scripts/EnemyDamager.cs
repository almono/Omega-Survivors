using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : MonoBehaviour
{
    public float damageValue = 10f;
    public float lifetime = 3f, growSpeed = 3f;
    public bool hasKnockback = true, destroyParent;
    private Vector3 targetSize;

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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyController>().TakeDamage(damageValue, hasKnockback);
        }
    }
}
