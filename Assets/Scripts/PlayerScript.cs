using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D prb;
    public SpriteRenderer playerSprite;
    private const float movespeed = 5;
    private const float jumpspeed = 10;
    private bool isGrounded;
    void Start()
    {
        
    }

    
    void Update()
    {
        //right left movement
        int moveInput = 0;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveInput = 1;
            playerSprite.flipX = false;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveInput = -1;
            playerSprite.flipX = true;
        }
        prb.velocity = new Vector2(moveInput * movespeed, prb.velocity.y);

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            prb.velocity = new Vector2(prb.velocity.x, jumpspeed);
            isGrounded = false; 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
  
        }
    }

}
