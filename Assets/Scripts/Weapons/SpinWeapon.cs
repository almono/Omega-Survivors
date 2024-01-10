using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinWeapon : MonoBehaviour
{
    public float rotationSpeed = 180f;
    public Transform fireballHolder, fireballToSpawn;

    public float cooldownTime;
    private float cooldownCounter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Quaternion.Euler converts Quaternion to Vector3
        fireballHolder.rotation = Quaternion.Euler(0f, 0f, fireballHolder.rotation.eulerAngles.z + (rotationSpeed * Time.deltaTime));

        cooldownCounter -= Time.deltaTime;

        if(cooldownCounter <= 0)
        {
            cooldownCounter = cooldownTime;
            Instantiate(fireballToSpawn, fireballHolder.position, fireballHolder.rotation, fireballHolder).gameObject.SetActive(true);
        }
    }
}
