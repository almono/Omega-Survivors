using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using UnityEngine;

public class Experience : MonoBehaviour
{
    public float experienceValue = 1f, moveSpeed = 4f, timeBetweenChecks = 0.2f;
    private bool movingToPlayer = false;
    private float checkCounter;
    private float experienceCheckRange = 1.5f, mergeCounter, timeBetweenMergeChecks = 2f; // check if there is a dropped experience nearby, if yes then merge it together to avoid item spam
    public LayerMask whatIsExperience;
    public SpriteRenderer experienceSprite;

    private PlayerController player;

    private void Start()
    {
        if(PlayerHealthController.instance != null)
        {
            player = PlayerHealthController.instance.GetComponent<PlayerController>();
        }
    }

    private void Update()
    {
        // pickup update logic relies on existence of player object
        // if player does not exist then dont execute anything
        if (player == null)
        {
            return;
        }

        if(movingToPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        } else
        {
            checkCounter -= Time.deltaTime;
            mergeCounter -= Time.deltaTime;

            if(checkCounter <= 0)
            {
                checkCounter = timeBetweenChecks;

                if(Vector3.Distance(transform.position, player.transform.position) < player.pickupRange)
                {
                    movingToPlayer = true;
                    moveSpeed += player.moveSpeed;
                } else if(mergeCounter <= 0 && !movingToPlayer)
                {
                    StartCoroutine(MergeNearbyExperience());
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ExperienceController.instance.AddExperienceToPlayer(experienceValue);
            Destroy(gameObject);
        }
    }

    public void SetExpValue(float value)
    {
        experienceValue = value;
    }

    private IEnumerator MergeNearbyExperience()
    {
        // start check every 3 seconds but block coroutine for 10 secs after its finished
        mergeCounter = timeBetweenMergeChecks;

        // check for potential nearby xp objects
        Collider2D[] nearbyXpObjects = Physics2D.OverlapCircleAll(transform.position, experienceCheckRange, whatIsExperience);

        if (nearbyXpObjects.Length > 1)
        {
            foreach (Collider2D xpObject in nearbyXpObjects)
            {
                if (xpObject != null)
                {
                    if (xpObject.gameObject == gameObject)
                    {
                        continue;
                    }

                    Experience xpPickup = xpObject.GetComponent<Experience>();
                    if (xpPickup != null && !xpPickup.movingToPlayer)
                    {
                        experienceValue += xpPickup.experienceValue;

                        // Change only the green and blue values based on value of the xp pickup
                        Color currentColor = experienceSprite.color;
                        Color newColor = new Color(currentColor.r, ((currentColor.g * 255f) - experienceValue * 2) / 255f, ((currentColor.b * 255f) - experienceValue * 2) / 255f, currentColor.a);
                        experienceSprite.color = newColor;

                        Destroy(xpObject.gameObject);
                    }
                }
            }
        }

        yield return new WaitForSeconds(10f);
    }
}
