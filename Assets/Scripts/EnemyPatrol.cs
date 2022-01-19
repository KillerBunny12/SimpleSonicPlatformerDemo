using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{

    public float speed = 4f;
    public bool movingRight = false;
    public float Distance = 2f;
    public Transform groundDetection;
    public Transform wallDetection;
    int layer = 1 << 10;
    
    
    

    
    void Update()
    {
        //Create 2 different Raycast2d that serve as detectors for when there is no floor or the enemy is in contact with a wall
        //If the enemy hits a wall turn the other way around and continue moving
        LayerMask mask = layer;
        transform.Translate(Vector2.left * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, Distance);
        RaycastHit2D wallInfo = Physics2D.Raycast(wallDetection.position, Vector2.left, 0.1f, mask);
        if (groundInfo.collider == false || wallInfo.collider == true)
        {
            if(movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }
}
