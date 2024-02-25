using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumberController : MonoBehaviour
{
    public static DamageNumberController instance;

    public DamageNumber numberToSpawn;
    public Transform numberCanvas;

    private List<DamageNumber> numberPool = new List<DamageNumber>();

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

    public void SpawnDamageNumber(float damage, Vector3 location, bool isCritNumber = false)
    {
        int numberToShow = Mathf.RoundToInt(damage);

        //DamageNumber damageNumber = Instantiate(numberToSpawn, location, Quaternion.identity, numberCanvas);
        DamageNumber damageNumber = GetFromPool();

        damageNumber.Setup(numberToShow, isCritNumber);
        damageNumber.gameObject.SetActive(true);
        damageNumber.transform.position = location;
    }

    public DamageNumber GetFromPool()
    {
        DamageNumber numberToOutput = null;

        if(numberPool.Count == 0)
        {
            numberToOutput = Instantiate(numberToSpawn, numberCanvas);
        } else
        {
            numberToOutput = numberPool[0];
            numberPool.RemoveAt(0);
        }

        return numberToOutput;
    }

    public void PlaceInPool(DamageNumber numberToPlace)
    {
        numberToPlace.gameObject.SetActive(false); // need to deactivate the object before adding to the list
        numberPool.Add(numberToPlace);
    }
}
