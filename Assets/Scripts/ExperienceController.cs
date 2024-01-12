using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceController : MonoBehaviour
{
    public static ExperienceController instance;
    public float currentExperience;

    void Awake()
    {
        if(instance != null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void GetExperience(float experience)
    {
        currentExperience += experience;
    }
}
