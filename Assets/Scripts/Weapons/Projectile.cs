using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float moveSpeed = 4f;

    void Update()
    {
        // move towards rotated angle
        transform.position += transform.up * moveSpeed * Time.deltaTime;
    }
}
