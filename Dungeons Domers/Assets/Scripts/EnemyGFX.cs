using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    public AIPath aiPath;
    public Animator animator;
    public Rigidbody2D rb;
    public float pushForce;

    public float wallWaitTime;
    void Update()
    {
      //AstarPath.active.Scan();
          animator.SetFloat("moveX", aiPath.desiredVelocity.x);
          animator.SetFloat("moveY", aiPath.desiredVelocity.y);
        //aiPath.Scan();
    }

    void OnCollisionEnter2D(Collision2D col){ 
        if (col.gameObject.layer == 11 || col.gameObject.layer == 12){
           // temp fix that kinda sucks
            // want the enemy to bounce off the wall and into direction of desired velocity
            // ie if collision is above enemy, bounce backwards and towards the direction of the desired velocity
            // getting the collision position when the collision is from a tile map is weird tho so temp fix
          aiPath.isStopped=true;
          Invoke("MoveAgain", wallWaitTime);
        }
     }
     void MoveAgain(){
      aiPath.isStopped= false;
     }
    
}