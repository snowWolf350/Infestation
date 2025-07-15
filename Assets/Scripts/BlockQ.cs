using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BlockQ : MonoBehaviour
{
    public float bumpHeight = 1f;
    public float speed = 0.5f; // Speed should be faster if you want a snappier bump

    public int hitmax = 1;

    private Vector3 originalPos;
    private Vector3 coinPos;
    public GameObject silverCoin;
    public GameObject Coin;
    private GameObject spawnedCoin;
    private bool isBumping = false;
    public int chance;
    private void Start()
    {
        originalPos = transform.position;
        coinPos = originalPos + Vector3.up * 1;
        chance = Random.Range(0,10);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (Vector2.Dot(contact.normal, Vector2.up) > 0.9f && collision.gameObject.CompareTag("Player"))
            {

                if (!isBumping && hitmax ==1)
                {
                    StartCoroutine(Bump());
                    hitmax--;
                }
            }
        }

    }

    private IEnumerator Bump()
    {
        isBumping = true;

        Vector3 targetPos = originalPos + Vector3.up * bumpHeight;
        if (chance < 7)
        {
            spawnedCoin = Instantiate(Coin,coinPos,Quaternion.identity);
        }
        else
            spawnedCoin = Instantiate(silverCoin,coinPos, Quaternion.identity);
          
        
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
        if(spawnedCoin == Coin)
        Destroy(spawnedCoin);
        isBumping = false;
    }
}
