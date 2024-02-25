using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivineBullet : BaseWeapon
{
    public float throwPower = 10f, rotationSpeed = 1f;
    public Rigidbody2D weaponBody;

    void Start()
    {
        weaponBody.velocity = new Vector2(Random.Range(-throwPower, throwPower), throwPower);
    }

    void Update()
    {
        // rotate properly based on whether weapon is thrown to the left or right
        transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + (rotationSpeed * 360f * Time.deltaTime * Mathf.Sign(weaponBody.velocity.x)));
    }
}
