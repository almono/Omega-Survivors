using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float moveSpeed = 4f, timeBetweenChecks = 0.2f;
    public int coinAmount = 1;
    private bool movingToPlayer = false;
    private float checkCounter;

    private PlayerController player;

    private void Start()
    {
        player = PlayerController.instance;
    }

    private void Update()
    {
        // pickup update logic relies on existence of player object
        // if player does not exist then dont execute anything
        if (player == null)
        {
            return;
        }

        if (movingToPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            checkCounter -= Time.deltaTime;

            if (checkCounter <= 0)
            {
                checkCounter = timeBetweenChecks;

                if (Vector3.Distance(transform.position, player.transform.position) < player.pickupRange)
                {
                    movingToPlayer = true;
                    moveSpeed += player.moveSpeed;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CoinController.instance.AddCoins(coinAmount);
            Destroy(gameObject);
        }
    }
}
