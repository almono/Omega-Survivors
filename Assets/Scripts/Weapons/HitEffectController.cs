using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffectController : MonoBehaviour
{
    public float mainDamage = 0f; // damage of main projectile that creates this effect
    public float damageScale = 1f; // % of main damage that should be applied to hit objects
    public AudioClip hitEffectSound; // sound to play on hit
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other != null && other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyController>().TakeDamage(mainDamage * damageScale);

            if (hitEffectSound != null)
            {
                SFXManager.instance.PlaySFXPitched(6);
                //hitEffectSound.Play();
            }
        }
    }
}
