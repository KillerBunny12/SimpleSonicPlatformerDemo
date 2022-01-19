using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator animator;
    bool collected = false;
    
  

    private void OnTriggerEnter2D(Collider2D other)
    {

        //If collected by player
        //Play animation and destroy object
        //Change score
        if (other.gameObject.CompareTag("Player"))
        {
            if (collected == false)
            {
             
                ScoreManager.instance.ChangeScore(1);
                animator.SetBool("Collected", true);
                collected = true;
                FindObjectOfType<AudioManager>().RingSound();
                StartCoroutine(WaitForAnimation());
            }
            
            
         
        }
    }


    IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(0.19f);
       
        Object.Destroy(this.gameObject);
    }
}
