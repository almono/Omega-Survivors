using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStatsInformation : MonoBehaviour
{
    private List<WeaponStatsInfo> weaponDamageStats = new List<WeaponStatsInfo>();

    public void AddDamage(EnemyDamager weapon, float damage)
    {
        //bool containsWeapon = weaponDamageStats.Any(weapon => weaponName == weapon.name);
    }
}

public class WeaponStatsInfo
{
    public SpriteRenderer weaponIcon;
    public string weaponName { get; set; }
    public float weaponDamage, weaponTime;
}
