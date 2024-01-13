using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceController : MonoBehaviour
{
    public static ExperienceController instance;
    public float currentExperience;

    public Experience experiencePickup;

    void Awake()
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
        
    }

    private void Update()
    {
        
    }

    public void AddExperienceToPlayer(float experience)
    {
        currentExperience += experience;
    }

    public void SpawnExperiencePickup(Vector3 position, float expValue)
    {
        Experience expPickup = Instantiate(experiencePickup, position, Quaternion.identity);
        expPickup.SetExpValue(expValue);
    }
}
