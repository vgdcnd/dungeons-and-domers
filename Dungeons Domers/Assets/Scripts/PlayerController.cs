using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public Animator animator;
    public float speed;      // public allows the variables to be set/adjusted within the unity engine as well as within other scripts
    public float attackDelay;

    private float lastAttack = -Mathf.Infinity;
    private Vector2 moveDirection;
    private bool canMove = true;
    private bool hitAgain = true;

    // get Inputs mostly, Debug.Log("output") if need 

        void Update()
    {
        GetInputs();

    }

    void FixedUpdate()
    {
        if (canMove) Move();

        Animate();
    }



    void GetInputs(){

        float moveX = Input.GetAxisRaw("Horizontal");
        float   moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;

        if(Input.GetButtonDown("Swing")) Swing();
    }
    void Move(){

        rb.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
    }

    void Swing(){
        if (Time.time - lastAttack <= attackDelay) return; // checking if enough time has based so that the pplayer can swing again

        DisableMove();
        lastAttack = Time.time; 
        animator.Play("Player_Attack_Up"); //would eventually need to make a whole blend tree for each animation
        // the hitboxs are changed based on the animations so it kinda eh, would be a decent amount of time to do all of the frames just to redo it but idk

        // 1) make sure attack doesnt call the on trigger enter multiple times for a single swing ? 

    }
   
   public void EnableMove(){ //called by the animator at the end of sword swing animations
        canMove = true ; 
        hitAgain = true;
    }

     public void DisableMove(){ //stops player from moving and resets velocity to be 0, could as be made to be called by animator but idk 
        canMove = false;
        rb.velocity = new Vector2(0,0);
    }

    void Animate(){
          animator.SetFloat("moveX", moveDirection.x);
            animator.SetFloat("moveY", moveDirection.y);
            animator.SetFloat("speed", moveDirection.sqrMagnitude);
            
             if ( Mathf.Abs(moveDirection.x) == 1|| Mathf.Abs(moveDirection.y) == 1)
                 {
            animator.SetFloat("lastMoveX", moveDirection.x);
            animator.SetFloat("lastMoveY", moveDirection.y);

             }
              //would want to be able to use animator.Play() eventually but still working on it 
    }

    void OnTriggerEnter2D(Collider2D hit){ // may want to move to a seperate script or sum
        
        if (hitAgain && hit.gameObject.tag == "Enemy"){
            hit.gameObject.GetComponent<enemyScript>().TakeDamage(50);
            hitAgain = false; //supposed to make sure single swing wont hit multiple times, kinda iffy still

        }

    }


    
}