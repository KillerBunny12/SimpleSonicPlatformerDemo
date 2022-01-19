using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;
public class PMovement : MonoBehaviour
{

    public GameObject DebugEnemy;
    public string horizontalAxis = "Horizontal";
    public string jumpButton = "Jump";


    public float fallingSpeed = 30;
    private bool dead = false;
    public Transform playertransform;
    private bool ending = false;
    public GameObject camera;
    CinemachineVirtualCamera vc;
    public CharacterController2D controller;
    public float runSpeed = 20f;
    float horizontalMove = 0f;
    bool jump = false;
    public Animator animator;
    public Rigidbody2D body;
    public float bounceforce = 700f;
    bool jumping;
    float savedhorizontal;
    public SpriteRenderer SonicSprite;
    bool invincible = false;
   public bool intheair = false;
    float lastsaved;
    int lives;
 
    


    // Start is called before the first frame update
     void Start()
    {
        vc = camera.GetComponent<CinemachineVirtualCamera>();

    }

    // Update is called once per frame
    void Update()
    {

       
        //DEBUG

       
        if (Input.GetKeyDown(KeyCode.K))
        {
            FindObjectOfType<LivesManager>().loselife();
            FindObjectOfType<LivesManager>().loselife();
            FindObjectOfType<LivesManager>().loselife();
            FindObjectOfType<LivesManager>().loselife();
        }

        if (Input.GetKey(KeyCode.Q))
        {
            body.AddForce(new Vector2(0f, 100f));
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            if (runSpeed == 20f)
            {
                runSpeed = 90f;
            }
            else
            {
                runSpeed = 20f;
            }
        }

        if (Input.GetKeyDown(KeyCode.L)){
            if (!invincible)
            {
                invincible = true;
                Debug.Log("INVULNERABLE");
            }
            else
            {
                invincible = false;
                Debug.Log("VULNERABLE");
            }

            
            
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Trying to spawn enemy");
            GameObject enemydebug = Instantiate(DebugEnemy);
            enemydebug.transform.position = transform.position + Camera.main.transform.forward * 2;

            if (enemydebug)
            {
                Debug.Log("Enemy spawned");
            }
            else
            {
                Debug.Log("Enemy spawn faild");
            }
        }
        //DEBUG


        
        if (body.velocity.y < -fallingSpeed)
        {
            body.velocity = new Vector2(body.velocity.x, -fallingSpeed);
        }


        horizontalMove = SimpleInput.GetAxis(horizontalAxis) * runSpeed;

        //horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (horizontalMove != 0)
        {
            savedhorizontal = horizontalMove;
        }

       
        //Check if Sonic is moving
        if (body.velocity.x < 0.01 && body.velocity.x > -0.01) 
        {
            //If sonic is not moving but is trying to move, play push animation
            animator.SetFloat("Speed", body.velocity.x);
            
            if(animator.GetBool("Hurt") == false) { 
            if(Mathf.Abs(horizontalMove) > 0)
            {
                animator.SetBool("PressingMove", true);
                animator.SetBool("IsMoving", false);
            }
            else
            {
                    if (animator.GetBool("Hurt") == false)
                    {
                        animator.SetBool("PressingMove", false);
                        animator.SetBool("IsMoving", false);
                    }
            }
            }
        }
        else
        {

            //If sonic is moving play running animation
            if (animator.GetBool("Hurt") == false)
            {
                animator.SetBool("IsMoving", true);
                animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
            }
        }



        //Check if sonic is jumping
        if (SimpleInput.GetButtonDown(jumpButton))
        {

            if (animator.GetBool("Hurt") == false && animator.GetBool("Dead") == false)
            {
                if (animator.GetBool("IsJumping") == false)
                {
                    FindObjectOfType<AudioManager>().JumpSound();
                    jump = true;
                    animator.SetBool("IsJumping", true);
                }

                

            }

            jumping = animator.GetBool("IsJumping");

        }
    }



    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {

            Debug.Log("??");
            this.transform.parent = other.gameObject.transform;
        }
    }


    void OnCollisionExit2D(Collision2D collision)
    {
        this.transform.parent = null;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playertransform = null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("DeathZone"))
        {
            while (!dead)
            {
                FindObjectOfType<LivesManager>().loselife();
            }
        }
        if (other.gameObject.CompareTag("Platform"))
        {
            playertransform = other.gameObject.transform;
        }

        if (other.gameObject.CompareTag("Spring"))
        {

            float bounce = other.gameObject.GetComponent<Spring>().bounce();
            other.gameObject.GetComponent<Spring>().play();
            body.velocity = new Vector2(body.velocity.x, 0.1f);
            body.AddForce(new Vector2(0, bounce));
            animator.SetBool("Spring", true);
        }

        if (other.gameObject.CompareTag("Goal"))
        {
            if (!ending) {
                ending = true;
            FindObjectOfType<GoalManager>().StartLevelEnding();
            }
        }
        lives = FindObjectOfType<LivesManager>().getcurrentlives();

        if (other.gameObject.CompareTag("SpikeBox"))
        {
            if (invincible == false)
            {


                FindObjectOfType<LivesManager>().loselife();
                FindObjectOfType<AudioManager>().SpikeSOund();

                if (lives != 0) { 
                //Play hurt animation and lose a live
                lastsaved = savedhorizontal;
                body.AddForce(new Vector2(10f * -savedhorizontal, 10f), ForceMode2D.Impulse);
                intheair = true;

                animator.SetBool("Hurt", true);
                
                StartCoroutine(InvencibilityFrames());
                Debug.Log("HURT");
                }
            }
        }

            //If sonic contacts with an enemy


            if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Touching enemy");

            //If sonic is jumping
            //kill the enemy
            if (animator.GetBool("IsJumping") == true)
            {
                Debug.Log("IS JUMPING");
                float lastvelocityy = System.Math.Abs(body.velocity.y);
                
                body.velocity = new Vector2(body.velocity.x, 0f);
                
                body.AddForce(new Vector2(0f, bounceforce/3 * lastvelocityy));
                animator.SetBool("IsJumping", true);
                other.gameObject.GetComponent<Enemy>().killenemy();
            }
            //If sonic is not jumping
            //Hurt sonic
            else
            {
                if (invincible == false) {
                    FindObjectOfType<LivesManager>().loselife();
                    if (lives != 0)
                    {

                        //Play hurt animation and lose a live
                        lastsaved = savedhorizontal;
                        body.AddForce(new Vector2(10f * -savedhorizontal, 10f), ForceMode2D.Impulse);
                        intheair = true;

                        animator.SetBool("Hurt", true);

                        StartCoroutine(InvencibilityFrames());
                        Debug.Log("HURT");
                    }
                }
            }



        }
    }

  

    public void OnLading()
    {
        animator.SetBool("IsJumping", false);
        animator.SetBool("Hurt", false);
        intheair = false;
        jumping = false;
        animator.SetBool("Spring", false);
    }


    public void Die()
    {
        dead = true;
        runSpeed = 0;
        animator.SetBool("Dead", true);
        vc.m_Follow = null;
        body.velocity = new Vector2(0f, 0f);
        body.AddForce(new Vector2(0f, 600f));
        GetComponent<BoxCollider2D>().enabled = false;

    }

    //Start invencibility frames after getting hit
    IEnumerator InvencibilityFrames()
    {
        invincible = true;

        yield return new WaitForSeconds(0.5f);

        float timePassed = 0;
        while(timePassed < 3)
        {
        if(SonicSprite.enabled == true)
            {
                SonicSprite.enabled = false;
            }
            else
            {
                SonicSprite.enabled = true;
            }
            timePassed += Time.deltaTime;
            yield return null;
        }

        SonicSprite.enabled = true;
        invincible = false;


        //Reset closest enemy collision
        //This fixes a bug that prevented the player from taking damage again if the invencibility frames finished inside the enemy collision box
        GameObject closest = closestEnemy();
        GameObject closestSpikes = closestSpike();
        if (closest != null)
        {
            closest.GetComponent<Enemy>().resetcollision();
        }

        if (closestSpikes != null)
        {
            closestSpikes.GetComponent<Spikes>().resetcollision();
        }
        
       

    }



    //Get closest enemy to the player
    private GameObject closestEnemy()
    {
        GameObject[] enemies;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        if(enemies != null) 
        
        foreach (GameObject go in enemies)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }


        return closest;
    }

    private GameObject closestSpike()
    {
        GameObject[] Spikes;
        Spikes = GameObject.FindGameObjectsWithTag("SpikeBox");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (GameObject go in Spikes)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }


        return closest;
    }

    private void FixedUpdate()

        


    {
        if (animator.GetBool("Hurt") == false) {
            controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        }
        else {
           // controller.Move(horizontalMove * Time.fixedDeltaTime/2, false, jump);
            
           
                float x = -lastsaved * 10;
                
            intheair = true;
           
            body.velocity = new Vector2(x * 0.9f * Time.deltaTime, body.velocity.y);
        }
        
        
        jump = false;
        jumping = false;
    }

}
