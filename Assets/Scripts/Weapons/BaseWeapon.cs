using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    public List<WeaponStats> stats;
    public int weaponLevel;

    public Sprite weaponIcon;

    [HideInInspector]
    public bool statsUpdated;

    public void LevelUp()
    {
        if(weaponLevel < stats.Count - 1)
        {
            weaponLevel++;
            statsUpdated = true;

            // check if the weapon reached max level
            // if yes  then add it to max upgrade weapon list
            // and remove from assigned weapon list so they cant be upgraded/selected anymore
            if(weaponLevel >= stats.Count - 1)
            {
                PlayerController.instance.fullyUpgradedWeapons.Add(this);
                PlayerController.instance.assignedWeapons.Remove(this);
            }
        }
    }
}

[System.Serializable]
public class WeaponStats
{
    public float speed, damage, range, attackCooldown, amount, duration, subDamagerReleaseCooldown = 1f;
    public int piercingCount, subProjectileCount = 0, subProjectileReleaseCount = 0;
    public string nextUpgradeText;
}