using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupRadius : MonoBehaviour
{
    public PickupRadius instance;

    public Transform player;
    LineRenderer playerPickupRadius;
    private int pickupRadiusVertexCount = 360;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        playerPickupRadius = GetComponent<LineRenderer>();
        playerPickupRadius.positionCount = pickupRadiusVertexCount;
        playerPickupRadius.useWorldSpace = false; // Set to false to use local space
        playerPickupRadius.startWidth = 0.02f;
        playerPickupRadius.endWidth = 0.02f;
        DrawPickupRange();
    }

    private void Update()
    {
        if (playerPickupRadius)
        {
            transform.position = player.position;
        }
    }

    public void DrawPickupRange()
    {
        float deltaTheta = (2f * Mathf.PI) / pickupRadiusVertexCount;
        float theta = 0f;

        for (int i = 0; i < pickupRadiusVertexCount; i++)
        {
            float x = PlayerController.instance.pickupRange * Mathf.Cos(theta);
            float z = PlayerController.instance.pickupRange * Mathf.Sin(theta);

            Vector3 circlePoint = new Vector3(x, z, 0f);
            playerPickupRadius.SetPosition(i, circlePoint);

            theta += deltaTheta;
        }

        // Move the circle to follow the player
        transform.position = player.position;

        if(!SettingsController.instance.showPickupRange)
        {
            gameObject.SetActive(false);
        }
    }
}
