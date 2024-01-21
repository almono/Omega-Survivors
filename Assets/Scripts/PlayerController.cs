using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 4f, pickupRange = 1.5f;
    public int maxWeapons = 3;
    public static PlayerController instance;
    //public BaseWeapon activeWeapon;

    public List<BaseWeapon> unassignedWeapons, assignedWeapons;
    public List<BaseWeapon> fullyUpgradedWeapons = new List<BaseWeapon>();

    Animator playerAnim;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        playerAnim = GetComponent<Animator>();
        AddWeapon(Random.Range(0, unassignedWeapons.Count));

        moveSpeed = PlayerStatsController.instance.moveSpeed[0].value;
        pickupRange = PlayerStatsController.instance.pickupRange[0].value;
        maxWeapons = Mathf.RoundToInt(PlayerStatsController.instance.maxWeapons[0].value);
    }

    void Update()
    {
        Vector3 moveInput = new Vector3(0f, 0f, 0f);
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        // fix diagonal movement speed
        // so you dont move faster diagonally
        moveInput.Normalize();

        transform.position += moveInput * moveSpeed * Time.deltaTime;

        if(moveInput != Vector3.zero)
        {
            playerAnim.SetBool("isMoving", true);
        } else
        {
            playerAnim.SetBool("isMoving", false);
        }
    }

    public void AddWeapon(int weaponNumber)
    {
        if(weaponNumber < unassignedWeapons.Count)
        {
            unassignedWeapons[weaponNumber].gameObject.SetActive(true);
            assignedWeapons.Add(unassignedWeapons[weaponNumber]);
            unassignedWeapons.RemoveAt(weaponNumber);
        }
    }

    public void AddWeapon(BaseWeapon weaponToAdd)
    {
        // in case we are on unlock screen and want to add a new weapon to the player
        // we use weapon object to show what we are unlocking
        weaponToAdd.gameObject.SetActive(true);
        assignedWeapons.Add(weaponToAdd);
        unassignedWeapons.Remove(weaponToAdd);
    }
}
