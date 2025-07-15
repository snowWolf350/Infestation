using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorScript : MonoBehaviour
{
    public Animator dooranim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            dooranim.SetTrigger("doorTrig");
            gameManager.instance.gameOver = true;
            gameManager.instance.levelClear();
        }
   
    }
}
