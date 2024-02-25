using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Base Player Stats")]
    public float moveSpeed = 4f;
    public float pickupRange = 1.5f;
    public float critChance = 0f;
    public float critMultiplier = 2f; // by default dmg x2
    public int maxWeapons = 3;
    public static PlayerController instance;
    //public BaseWeapon activeWeapon;

    public List<BaseWeapon> unassignedWeapons, assignedWeapons;
    public List<BaseWeapon> fullyUpgradedWeapons = new List<BaseWeapon>();
    public Vector3 movementDirections = Vector3.zero;

    Animator playerAnim;
    CapsuleCollider2D playerBody;
    public SpriteRenderer playerIcon;

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
        playerBody = GetComponent<CapsuleCollider2D>();

        // if no weapon is assigned then add a random weapon to the player
        SetupStartingWeapons();

        moveSpeed = PlayerStatsController.instance.moveSpeed[0].value;
        pickupRange = PlayerStatsController.instance.pickupRange[0].value;
        maxWeapons = Mathf.RoundToInt(PlayerStatsController.instance.maxWeapons[0].value);
    }

    void Update()
    {
        Vector3 moveInput = new Vector3(0f, 0f, 0f);
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        movementDirections = moveInput;

        // fix diagonal movement speed
        // so you dont move faster diagonally
        moveInput.Normalize();

        transform.position += moveInput * moveSpeed * TempBuffController.instance.moveSpeedBuffMultiplier * Time.deltaTime;

        if(moveInput != Vector3.zero)
        {
            playerAnim.SetBool("isMoving", true);
        } else
        {
            playerAnim.SetBool("isMoving", false);
        }
    }

    public void SetupStartingWeapons()
    {
        if (assignedWeapons.Count < 1)
        {
            AddWeapon(Random.Range(0, unassignedWeapons.Count));
        }
        else
        {
            foreach (BaseWeapon weapon in assignedWeapons)
            {
                weapon.gameObject.SetActive(true);
            }
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

    public void OnCollisionEnter2D(Collision2D other)
    {
        // open chest on touching it
        if(other.gameObject.CompareTag("Chest"))
        {
            Chest chestObject = other.gameObject.GetComponent<Chest>();
            
            if(chestObject != null)
            {
                chestObject.OpenChest();
            }
        }
    }

    
}
