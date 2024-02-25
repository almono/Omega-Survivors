using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float moveSpeed = 4f, releaseCooldown = 0f;
    public int subAmount = 0, releaseTimes = 0;

    [SerializeField] private float releaseSpeed = 0f;
    public SubProjectile subProjectile;

    void Update()
    {
        // move towards rotated angle
        transform.position += transform.up * moveSpeed * Time.deltaTime;

        if(releaseCooldown > 0 && (subProjectile && subAmount > 0 && releaseTimes > 0))
        {
            releaseCooldown -= Time.deltaTime;

            if(releaseCooldown <= 0 )
            {
                releaseTimes--;
                releaseCooldown = releaseSpeed;
                int angleDiff = (360 / subAmount) + Random.Range(0, 360);

                for (int i = 0; i < subAmount; i++)
                {
                    Instantiate(subProjectile, transform.position, Quaternion.AngleAxis(angleDiff, Vector3.forward)).gameObject.SetActive(true);
                    angleDiff += angleDiff;
                }
            }
        }
    }

    public void SetSubProjectiles(SubProjectile subProj, int projCount, int releaseCount, float cooldown)
    {
        subProjectile = subProj;
        subAmount = projCount;
        releaseTimes = releaseCount;
        releaseSpeed = cooldown;
        releaseCooldown = releaseSpeed;
    }
}
