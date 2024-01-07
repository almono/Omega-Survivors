using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    public Transform sprite;
    public float animSpeed = 0.5f, minSize = 0.9f, maxSize = 1.1f;

    private float activeSize;

    void Start()
    {
        activeSize = maxSize;
        animSpeed = animSpeed * Random.Range(0.75f, 1.25f);
    }

    void Update()
    {
        sprite.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one * activeSize, animSpeed * Time.deltaTime);

        if(sprite.localScale.x == activeSize) 
        {
            if(activeSize == maxSize)
            {
                activeSize = minSize;
            } else
            {
                activeSize = maxSize;
            }
        }
    }
}
