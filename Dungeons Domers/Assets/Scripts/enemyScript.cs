using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{

    [SerializeField]    private float health;     // The enemy's heath
    [SerializeField]    private float defense;      // Used to detemine how much damage it takes
    [SerializeField]    private float attack;     // Used to determine how much damage it deals
    private float flickerSeconds = .2f;

    [SerializeField]    GameObject deathEffect; // spawn the gameobject when enemy die
    public bool respawn;
    private SpriteRenderer spriteRenderer;


    public Material hurtEffect;
    private Material normalMaterial;

    void Start(){
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        normalMaterial = spriteRenderer.material;
    }

    // Start is called before the first frame update
   public void TakeDamage(float damage){
        health -= damage;
        if (health <= 0) Die();
        else{
            spriteRenderer.material = hurtEffect;
            Invoke("NormalSprite", flickerSeconds);

        }
       // Debug.Log("Hit");
    }

    void NormalSprite(){
        spriteRenderer.material = normalMaterial;

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

        if (gameObject.transform.parent && gameObject.transform.parent.gameObject.name != "Enemies"){ // if there is a parent (ie for zombie) also want to destroy
            // might change later so not hard coded like this and instead have it so that the responsibility is on the individual enemies 
            Destroy(gameObject.transform.parent.gameObject);
        }
        Destroy(gameObject);
    }

    void OnCollisionEnter2D (Collision2D hit){
            if (hit.gameObject.tag == "Player")   hit.gameObject.GetComponent<PlayerController>().TakeDamage(attack, transform.position);
    }


}




/*
    HIVE notes 
    1. assume hive is stationary  <---- ENEMEY PROTO
    2. hive spawns mini dudes (bees)  <----- WHAT DO
        2a. how to spawn a bee -> instantiate -> make sure bees have some parent as hive    <------ ENEMEY SPAWNER 
    3. where does bee spawn 
        a.  bee spawns in range 
            -> want bee to be within a certain area of the hive 
        assume bee will spawn around hive
           _____
          |  H  |
          |_____|
        b. bee spawns in block/obstacle/'thing' 
             look up 

    4. how often and how many bees spawn ????  prolly a coroutine  
        a) Assume there exists a max bee amount
                how do we make sure bees present dont break the max bee amount 
                -> if a bee dies how do we know to allow anothher one to come 
        b) hive determines bee amount ie summoning a bee hurts the hive 

    5. bees behave like mini ghosts ?  <----- STEAL CODE FROM GHOST

*/


















