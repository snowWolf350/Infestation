using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockB : MonoBehaviour
{
    public float bumpHeight = 1f;
    public float speed = 0.5f; // Speed should be faster if you want a snappier bump

    public int hitmax;

    private Vector3 originalPos;
    private Vector3 coinPos;
    private bool isBumping = false;

    public GameObject Coin;
    public GameObject particles;

    private void Start()
    {
        originalPos = transform.position;
        coinPos = originalPos + Vector3.up * 1;
        hitmax = Random.Range(1, 5);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (Vector2.Dot(contact.normal, Vector2.up) > 0.9f && collision.gameObject.CompareTag("Player"))
            {
                if (!isBumping && hitmax >= 1 )
                {
                    StartCoroutine(Bump());
                    
                    hitmax--;
                }
                else if (!isBumping && hitmax == 0)
                {
                    destroyBlock();
                }
            }
        }

    }

    private void destroyBlock()
    {
        Instantiate(particles,transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private IEnumerator Bump()
    {
        isBumping = true;

        

        Vector3 targetPos = originalPos + Vector3.up * bumpHeight;

        GameObject spawnedCoin = Instantiate(Coin, coinPos, Quaternion.identity);


        // Move Up
        while (Vector3.Distance(transform.position, targetPos) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime);
            yield return null;
        }

        // Move Down
        while (Vector3.Distance(transform.position, originalPos) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, originalPos, speed * Time.deltaTime);
            yield return null;
        }

        // Snap back to original position just to be safe
        transform.position = originalPos;
        Destroy(spawnedCoin);
        isBumping = false;
    }
}
