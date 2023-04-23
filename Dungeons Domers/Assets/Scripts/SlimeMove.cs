using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMove : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed; 

    private int direction = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
         // can do raycast but ew 

    void FixedUpdate(){
        rb.velocity = new Vector2(direction * speed, 0);
    }

    void OnCollisionEnter2D(Collision2D hit){

        if (hit.gameObject.tag == "Wall") {
            transform.Rotate(0,180,0);
            direction *= -1;
        }

    }
}
