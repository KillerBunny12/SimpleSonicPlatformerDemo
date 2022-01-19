using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    public Animator SonicAnimation;
    public GameObject Sonic;
    public Canvas ui;
    public Animator Goal;
    public Animator EndLevelCard;
    public GameObject EndLevelObjects;
    public Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StartLevelEnding()
    {
        StartCoroutine(EndLevelAnimation());

    }


    IEnumerator EndLevelAnimation()
    {
        FindObjectOfType<AudioManager>().SignSound();
        Goal.SetTrigger("GoalStart");
        yield return new WaitForSeconds(3);
        Goal.SetTrigger("GoalFinish");
        yield return new WaitForSeconds(0.5f);
        Sonic.GetComponent<PMovement>().enabled = false;
        SonicAnimation.SetBool("IsMoving", false);
        SonicAnimation.SetBool("IsJumping", false);
        SonicAnimation.SetBool("PressingMove", false);
        Sonic.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        SonicAnimation.SetTrigger("Win1");
        ui.enabled = false;
        yield return new WaitForSeconds(0.2f);
        SonicAnimation.SetTrigger("Win2");
        FindObjectOfType<AudioManager>().EndLevelMusic();
        EndLevelObjects.SetActive(true);
        EndLevelCard.SetTrigger("StartAnimation");
        yield return new WaitForSeconds(6f);

        //TRANSITION AND GO BACK TO THE MAIN MENU
        //TODO
    }
}
