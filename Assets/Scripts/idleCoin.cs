using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idleCoin : MonoBehaviour
{

    private Vector3 originalPos;
    public BoxCollider2D coinTrig;
    public float bumpHeight = 0.25f;
    public float speed = 18;

    private bool isBumping = false;

    void Start()
    {
        originalPos = transform.position;
    }
    private IEnumerator Bump()
    {

        if (isBumping) yield break; // Prevents multiple coroutines
        isBumping = true;
        gameManager.updateCoin();
        gameManager.updateScore(50);

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

        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&& isBumping == false)
        {
            StartCoroutine(Bump());
            
            Destroy(coinTrig);
        }

    }
}
