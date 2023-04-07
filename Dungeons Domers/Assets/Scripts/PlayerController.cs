using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public Animator animator;
    public float speed;      // public allows the variables to be set/adjusted within the unity engine as well as within other scripts
    public float attackDelay =1f;

    private Vector2 moveDirection;

    // get Inputs mostly, Debug.Log("output") if need 

        void Update()
    {
        GetInputs();

    }

    void FixedUpdate()
    {
        Move();
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


        animator.Play("Player_Attack_Up"); //would eventually need to make a whole blend tree for each animation
        // the hitboxs are changed based on the animations so it kinda eh, would be a decent amount of time to do all of the frames just to redo it but idk

        // 1)attack delay
        // 2) make sure attack doesnt call the on trigger enter multiple times for a single swing ? 
        // 3) attack stops player velocity ie can not move and swing at same time 


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

    void OnTriggerEnter2D(Collider2D hit){
            
        if (hit.gameObject.tag == "Enemy"){
            hit.gameObject.GetComponent<enemyScript>().TakeDamage(50);
            // better way to call a method from another object ? 


        }

    }


    
}