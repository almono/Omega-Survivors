using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 4f, pickupRange = 1.5f;
    public static PlayerController instance;
    public BaseWeapon activeWeapon;
    
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
}
