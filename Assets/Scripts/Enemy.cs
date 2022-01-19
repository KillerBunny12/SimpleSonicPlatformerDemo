using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Animator animator;
    bool dead = false;
    
    

    //If this method is called
    //Enemy death animaiton is played and object gets destroyed
    public void killenemy()
    {
        if (dead == false)
        {


            animator.SetBool("Dead", true);
            dead = true;
            FindObjectOfType<AudioManager>().DeadSound();
            StartCoroutine(WaitForAnimation());
        }
    }
    IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(0.27f);

        Object.Destroy(this.gameObject);
    }


    //Reset collision of the enemy
    //Used by PMovement script for fixing a bug
    public void resetcollision()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
