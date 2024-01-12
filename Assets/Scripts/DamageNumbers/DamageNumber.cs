using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageNumber : MonoBehaviour
{
    public TMP_Text damageText;
    public float textLifetime = 0.3f, floatSpeed = 2f;

    private float lifeCounter;

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;
        if(lifeCounter > 0)
        {
            lifeCounter -= Time.deltaTime;

            if(lifeCounter <= 0)
            {
                // instead of destroying the object we add it to the pool
                //Destroy(gameObject);
                DamageNumberController.instance.PlaceInPool(this);
            }
        }
    }

    public void Setup(int damageToDisplay)
    {
        lifeCounter = textLifetime;
        damageText.text = damageToDisplay.ToString();
    }
}
