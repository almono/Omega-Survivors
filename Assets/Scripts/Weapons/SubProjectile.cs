using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubProjectile : Projectile
{
    void Update()
    {
        // move towards rotated angle
        transform.position += transform.up * moveSpeed * Time.deltaTime;
    }
}
