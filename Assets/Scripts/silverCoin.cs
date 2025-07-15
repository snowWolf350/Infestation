using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class silverCoin : MonoBehaviour
{
    private Vector3 originalPos;
    public BoxCollider2D coinTrig;
    public float bumpHeight = 0.25f;
    public float speed = 18;

    private bool isBumping = false;
    void Start()
    {
        originalPos = transform.position;
        StartCoroutine(Bump());
    }
    private IEnumerator Bump()
    {

        Vector3 targetPos = originalPos + Vector3.up * bumpHeight;

        
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


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isBumping == false)
        {
            StartCoroutine(Bump());
            gameManager.updateScore(400);
            Destroy(gameObject);
        }

    }
}
