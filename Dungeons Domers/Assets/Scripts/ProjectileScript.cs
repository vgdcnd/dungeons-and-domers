using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class  ProjectileScript : MonoBehaviour
{

    // this script is expected to be used along with a more specific bullet script.
    // this script handles getting the bullet to move in a direction and with a speed, when the bullet hits something it will destroy itself
    // other script will want to override the ontriggerenter function in order to deal damage and any extra effects
    // at the end of other script on trigger enter, you'd do base.OnTriggerEnter2D(hit) in order to get this generic hit, but if this generic on trigger enter
    // does not work with your projectile you can just overide the ontrigger enter and not call base. 
    // thhe player bullet scripts should show guidlines of how script is expected to be used.
    // would make a prefab variant of the projectile prefab just like you'd do with an enemy :)


    // Start is called before the first frame update
    private float speed; //currently set up so that whatever creates the bullet would set the bullets properties right after Instantiate() 
    private Vector2 direction;             
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject explodeEffect;

    // Update is called once per frame
    public void SetUpProjectile(float spd, Vector2 dir){
        speed= spd;
        direction = dir;
        MoveBullet();
    }

    void MoveBullet(){
        rb.velocity = direction* speed;
    }

    public virtual void OnTriggerEnter2D(Collider2D hit){
        
        
        if (hit.gameObject.tag == gameObject.tag) return; 
        /*
        if(hit.gameObject.tag == "Enemy"){

                hit.gameObject.GetComponent<enemyScript>().TakeDamage(damage);
        }
        */
        //when bullet touches something 'explodes', the inheirinted bullet will also have its own override on Trigger Enter to specify who to target/ what to do specifcally on hit 
       
        Instantiate(explodeEffect, transform.position, Quaternion.identity);    
        Destroy(gameObject);
    }






}
