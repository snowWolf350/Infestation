using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class enemyScript : MonoBehaviour
{
  
    public SpriteRenderer enemySprite;
    public Rigidbody2D erb;
    public CapsuleCollider2D ebox;

    Vector3 originalPos,targetPos;

    public float height = 0.25f;
    public float speed = 18;
    public bool isfalling = false;
    public bool movingRight = true;
    
    void Update()
    {
        if (isfalling == false)
        {
            if (movingRight)
            {
                erb.velocity = new Vector2(1 * speed, erb.velocity.y);
                enemySprite.flipX = true;
            }
            else
            {
                erb.velocity = new Vector2(-1 * speed, erb.velocity.y);
                enemySprite.flipX = false;
            }
                
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("block") && isfalling == false)
        {
            movingRight = !movingRight;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isfalling == false && collision.gameObject.CompareTag("Player"))
            StartCoroutine(hit());
    }

    IEnumerator hit()
    {
        gameManager.updateScore(200);
        isfalling = true;
        ebox.enabled = false;
        originalPos = transform.position;
        targetPos = originalPos + Vector3.up * height;

        while (Vector3.Distance(transform.position, targetPos) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            yield return null;
        }
        while (transform.position.y > -6.5)
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
            yield return null;
        }
        isfalling = false;
        Destroy(gameObject);
    }
}
