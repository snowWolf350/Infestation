using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D playerrb;
    public SpriteRenderer playerSprite;
    public BoxCollider2D pbox;

    public GameObject camera;
    public float cameraSpeed = 2f;

    private const float movespeed = 5;
    private const float jumpspeed = 10;


    private bool isGrounded;
    private bool isfalling = false;

    private Vector3 originalPos;
    private Vector3 targetPos;
    public Vector3 spawnPos;
    public Vector3 cameraPos;

    public float height = 2f;
    public float speed = 25;

    private void Awake()
    {
        playerrb = GetComponent<Rigidbody2D>();
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
        playerrb.velocity = new Vector2(moveInput * movespeed, playerrb.velocity.y);

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerrb.velocity = new Vector2(playerrb.velocity.x, jumpspeed);
            isGrounded = false; 
        }

        if (gameManager.instance.isalive == true)
        {
            if (transform.position.x - camera.transform.position.x > 2)
            {
                camera.transform.position += Vector3.right * cameraSpeed * Time.deltaTime;

            }
            if (transform.position.x - camera.transform.position.x < -2)
            {
                camera.transform.position += Vector3.left * cameraSpeed * Time.deltaTime;
            }
            if (transform.position.y - camera.transform.position.y < -4.25f)
            {
                camera.transform.position += Vector3.down * cameraSpeed * Time.deltaTime;
            }
            if (transform.position.y - camera.transform.position.y >4)
            {
                camera.transform.position += Vector3.up * cameraSpeed * Time.deltaTime;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("block"))
            isGrounded = true;
        if (collision.gameObject.CompareTag("enemy") && isfalling == false)
            StartCoroutine(hit());
    }

    IEnumerator hit()
    {
            gameManager.instance.isalive = false;
            isfalling = true;
            pbox.enabled = false;
            originalPos = transform.position;
            targetPos = originalPos + Vector3.up * height;

            while (Vector3.Distance(transform.position, targetPos) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
                yield return null;
            }
            while (Vector3.Distance(transform.position, originalPos) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, originalPos, speed * Time.deltaTime);
                yield return null;
            }
            isfalling = false;
            transform.position = spawnPos;
            pbox.enabled = true;
            gameManager.updateLives();
            yield return new WaitForSeconds(3);
            camera.transform.position = cameraPos;
            gameManager.instance.isalive = true;
    }

}


