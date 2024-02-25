using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastingAreaWeapon : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            //other.GetComponent<EnemyController>().TakeDamage(finalDamage, hasKnockback, isCrit);
        }
    }
}
