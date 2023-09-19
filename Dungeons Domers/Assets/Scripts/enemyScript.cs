using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{

    [SerializeField]
    private float health;     // The enemy's heath
    [SerializeField]
    private float defense;      // Used to detemine how much damage it takes
    [SerializeField]
    private float attack;     // Used to determine how much damage it deals
    
    [SerializeField] 
    GameObject deathEffect; // spawn the gameobject when enemy dies

    public bool respawn;

    // Start is called before the first frame update
   public void TakeDamage(float damage){
        health -= damage;
        if (health <= 0) Die();
       // Debug.Log("Hit");
    }

    void Die(){
        //TESTING 
        Vector2 randomPosition = new Vector2(
          Random.Range(-7, 7),
          Random.Range(-3, 3)
        );
        if (respawn) Instantiate(gameObject, randomPosition, Quaternion.identity); //problem where when instantiated the health carries over so the clone will have negative hp  
                                                                      // should not matter in the actual game tho, cuz this respawn code will be removed

        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D (Collision2D hit){
            if (hit.gameObject.tag == "Player"){
              //  hit.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * 5000);
                //prolly add function within player script of GotHit() that would mess with their rb, add -velocity, take health, etc
            hit.gameObject.GetComponent<PlayerController>().TakeDamage(50, transform.position);
                    }



    }


}
