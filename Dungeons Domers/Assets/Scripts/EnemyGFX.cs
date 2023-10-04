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
    public GameObject rayStart;

    private Vector3 lastPosition;


    void Start(){
      rb = gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine(CheckPos());

    }
    void Update()
    {
      //AstarPath.active.Scan();
          animator.SetFloat("moveX", aiPath.desiredVelocity.x);
          animator.SetFloat("moveY", aiPath.desiredVelocity.y);
          if( Vector3.Dot(transform.position, lastPosition) <= 0.5f) Shuffle();
        //Shuffle();
        //aiPath.Scan();
    }

    void OnCollisionEnter2D(Collision2D col){ 
        if (col.gameObject.layer == 11 || col.gameObject.layer == 12){

          aiPath.isStopped=true;

          // velocity -> direction ai wanted 
          Invoke("MoveAgain", wallWaitTime);
        }
     }
     void MoveAgain(){
      aiPath.isStopped= false;
     }
    private void Shuffle(){

      Debug.Log("ray sent");
      float aiHorz = aiPath.desiredVelocity.x;
      float aiVert = aiPath.desiredVelocity.y;
      Vector2 rayDirection;
      if (Mathf.Abs(aiHorz) > Mathf.Abs(aiVert)){

    
        rb.velocity = new Vector2(0, aiVert);
     //  rayDirection = new Vector2(aiHorz, 0);
      } // stuck left or right
      else {
        rb.velocity = new Vector2(aiHorz, 0);
      }
      rayDirection = new Vector2(0, aiVert);
          aiPath.isStopped = true; //take control
      Invoke("MoveAgain", .3f);
      // know the direction we are facing / where we get stuck at 
      // ray direction up -> get stuck up -> cut y out. -> only x
      
            //Vector2 aiHorz = new Vector2(aiPath.desiredVelocity.x,0);
      Debug.DrawRay(transform.position, rayDirection*100, Color.yellow );
      /*()
       else if (Vector3.Dot(transform.forward, Vector3.down) >= 0.9f) Debug.DrawRay(transform.position, transform.up*-100, Color.yellow );
      else if (Vector3.Dot(transform.forward, Vector3.left) >= 0.9f) Debug.DrawRay(transform.position, transform.right*-100, Color.yellow );
       else if (Vector3.Dot(transform.forward, Vector3.right) >= 0.9f) Debug.DrawRay(transform.position, transform.right*100, Color.yellow );
      else {
        
          Debug.Log("RIGHT " + Vector3.Dot(transform.forward, Vector3.right));
 Debug.Log("LEFT " + Vector3.Dot(transform.forward, Vector3.left));
 Debug.Log("DOWN " + Vector3.Dot(transform.forward, Vector3.down));
 Debug.Log("UP " + Vector3.Dot(transform.forward, Vector3.up));

      }
      */
    }


    private IEnumerator CheckPos(){ // update an older position of the zombie
      while(true){

        lastPosition = transform.position;
      //  Debug.Log(lastPosition.x, lastPosition.y);
        yield return wallWaitTime;

      }

     }
    
}