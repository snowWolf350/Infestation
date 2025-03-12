using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class enemyScript : MonoBehaviour
{
    public GameObject Player;
    public SpriteRenderer enemySprite;
    public Rigidbody2D erb;
    private const float moveSpeed = 2;
    private const float stopDistance = 3;
    public float distance;
    void Start()
    {
        
    }

    
    void Update()
    {
        float distance = Player.transform.position.x - transform.position.x;
        float absDistance = Mathf.Abs(distance);

        if (absDistance > stopDistance)
        {
            float direction = Mathf.Sign(distance); // 1 if player is right, -1 if left

            switch(direction)
            {
                case 1:
                    enemySprite.flipX = true;
                    break;
                case -1:
                    enemySprite.flipX = false;
                    break;
            }


            erb.velocity = new Vector2(direction * moveSpeed, erb.velocity.y);
        }
        else
        {
            erb.velocity = Vector2.zero; // Stops movement
        }
    }
}
