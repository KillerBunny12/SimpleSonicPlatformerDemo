using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{

    public float BounceForce = 20f;
    public Animator animator;
    // Start is called before the first frame update


    // Update is called once per frame
    public float bounce()
    {
        return BounceForce;
    }
    public void play()
    {
        animator.SetBool("Used", true);
        FindObjectOfType<AudioManager>().SpringSound();
        StartCoroutine(waitanimation());
    }
   

    IEnumerator waitanimation()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Used", false);
    }

}
