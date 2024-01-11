using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumberController : MonoBehaviour
{
    public static DamageNumberController instance;

    public DamageNumber numberToSpawn;
    public Transform numberCanvas;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        } else
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnDamageNumber(float damage, Vector3 location)
    {
        int numberToShow = Mathf.RoundToInt(damage);

        DamageNumber damageNumber = Instantiate(numberToSpawn, location, Quaternion.identity, numberCanvas);
        damageNumber.Setup(numberToShow);
        damageNumber.gameObject.SetActive(true);
    }
}
