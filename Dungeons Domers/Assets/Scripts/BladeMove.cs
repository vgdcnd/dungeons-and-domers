using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeMove : MonoBehaviour
{
    // Start is called before the first frame update
    private float blockSize = 1;
    [SerializeField] private float blockSpeed, blockResetTime, blockDamage;
                        //   found that 5, .4, and 25 feel pretty solid
    private float minCheck= .1f;    


    private Rigidbody2D rb;
    private Vector3 startingPoint;

    private Vector2 direction; // when block hits static object it loses velocity, will save the velocity before it hits to restore it later

    private bool searching = true; // says if block is at starting point and looking for player
    private bool reverse = false; // says if block is going back to find starting point

    private bool moving = false;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        startingPoint = gameObject.transform.position; // used to find out where to return to
        // Debug.Log("START: " + startingPoint.x + " " + startingPoint.y);
    }

    // Update is called once per frame
    void FixedUpdate(){ 
        //if (rb.velocity) Debug.Log("moving");
        
       if (searching) FireRays();

       else if (reverse) {
         transform.position = Vector3.MoveTowards(transform.position, new Vector3(startingPoint.x , startingPoint.y, transform.position.z), blockSpeed);
        CheckPoint();
        Debug.Log("BACK");
       }
       if (moving)  transform.position = Vector3.MoveTowards(transform.position, new Vector3(direction.x, direction.y, transform.position.z), blockSpeed);
    }

   void FireRays(){        
                /*      UNCOMMENT TO SEE VISUALLY THE RAY CASTS
                Debug.DrawRay(transform.position + new Vector3(.5f,0,0), transform.up * 100, Color.yellow); // fire up, right side of box
                Debug.DrawRay(transform.position + new Vector3(-.5f,0,0), transform.up * 100, Color.yellow); // fire up, left side of box

                Debug.DrawRay(transform.position + new Vector3(.5f,0,0), transform.up *-100, Color.yellow); //fire down, right side
                Debug.DrawRay(transform.position + new Vector3(-.5f,0,0), transform.up * -100, Color.yellow); //fire down, left side

                Debug.DrawRay(transform.position + new Vector3(0,.5f,0), transform.right * 100, Color.yellow); //fire right, top side
                Debug.DrawRay(transform.position + new Vector3(0f,-.5f,0), transform.right * 100, Color.yellow); //fire right down side

                Debug.DrawRay(transform.position + new Vector3(0,.5f,0), transform.right * -100, Color.yellow); //fire left, Up side of box
                Debug.DrawRay(transform.position + new Vector3(0f,-.5f,0), transform.right * -100, Color.yellow); //fire left, down side of box
              */
              // code is pretty chunky, might comeback and make it a cute loop to reduce stuff
                if (rb.velocity.sqrMagnitude !=0) return;
                RaycastHit2D hit; 
                hit= Physics2D.Raycast(transform.position + new Vector3(blockSize/2, blockSize,0), transform.up , 100); 
                //UP
                if (hit.collider != null && hit.collider.gameObject.name == "Player") {
                    FoundPlayer(new Vector2(0,1));
                    return;
                }
                hit = Physics2D.Raycast(transform.position + new Vector3(-blockSize/2, blockSize,0), transform.up , 100);
                   if (hit.collider != null && hit.collider.gameObject.name == "Player") {
                    FoundPlayer(new Vector2(0,1));
                    return;
                }

                //DOWN
                hit= Physics2D.Raycast(transform.position + new Vector3(blockSize/2, -blockSize,0), -1 *transform.up , 100); 
                if (hit.collider != null && hit.collider.gameObject.name == "Player") {
                    FoundPlayer(new Vector2(0,-1));
                    return;
                }
                hit = Physics2D.Raycast(transform.position + new Vector3(-blockSize/2, -blockSize,0), -1*transform.up , 100);
                   if (hit.collider != null && hit.collider.gameObject.name == "Player") {
                    FoundPlayer(new Vector2(0,-1));
                    return;
                }

        //RIGHT
                hit= Physics2D.Raycast(transform.position + new Vector3(blockSize, blockSize/2,0), transform.right , 100); 
                if (hit.collider != null && hit.collider.gameObject.name == "Player") {
                    FoundPlayer(new Vector2(100f + transform.position.x,transform.position.y));
                    return;
                }
                hit = Physics2D.Raycast(transform.position + new Vector3(blockSize, -blockSize/2,0), transform.right , 100);
                   if (hit.collider != null && hit.collider.gameObject.name == "Player") {
                    FoundPlayer(new Vector2(100f + transform.position.x,transform.position.y));
                    return;
                }
        // LEFT
           hit= Physics2D.Raycast(transform.position + new Vector3(-blockSize, blockSize/2,0), -1*transform.right , 100); 
                if (hit.collider != null && hit.collider.gameObject.name == "Player") {
                    FoundPlayer(new Vector2(-100f + transform.position.x,transform.position.y));
                    return;
                }
                hit = Physics2D.Raycast(transform.position + new Vector3(-blockSize, -blockSize/2,0), -1 *transform.right , 100);
                   if (hit.collider != null && hit.collider.gameObject.name == "Player") {
                    FoundPlayer(new Vector2(-100f + transform.position.x,transform.position.y));
                    return;
                }

    } 
    void FoundPlayer(Vector2 dir){
        searching = false;
        moving = true;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(dir.x, dir.y, transform.position.z), blockSpeed);
        direction = dir;
        Debug.Log("Found");
        //rb.velocity = direction * blockSpeed;
        //preTurnVelocity = rb.velocity;
    }
    void CheckPoint(){
        if (Mathf.Abs(Vector3.Distance(transform.position, startingPoint)) <= minCheck) // if we are close to starting point reset block
            {
                rb.velocity= new Vector2(0,0);
                transform.position = startingPoint;
                reverse = false;
                Invoke("Reset", blockResetTime);
                Debug.Log("YUR");
            }
    }

    void Reset(){ // set block back to searching after certain time
        searching = true;
    }

    void OnCollisionEnter2D(Collision2D col){
                //preTurnVelocity = rb.velocity;
                
        if (!reverse){ 
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(startingPoint.x , startingPoint.y, transform.position.z), blockSpeed);
        reverse = true; //going back now
        //Debug.Log(col.gameObject.name);
        }
 
        if (col.gameObject.name == "Player")col.gameObject.GetComponent<PlayerController>().TakeDamage(blockDamage, transform.position);
        else if (col.gameObject.tag == "Enemy")col.gameObject.GetComponent<enemyScript>().TakeDamage(blockDamage * 100);
        

        // if collider is player take damage
        // if col is enemey kill
    }


    // send 8 raycasts up
        // each edge of box sends one in both directions
    // if raycast has hit player have box travel in that direction
    // if box hit player, push player and deal damage
    // if box hits enemey, kill enemey 
    // go back 
        // what if something is in box way on the way back ?
            // ?
    // once box is back, refire raycasts and reset loop


}
