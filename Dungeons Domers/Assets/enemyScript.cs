using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{

    public int health;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void TakeDamage(int damage){
        health -=damage;
        if (health <= 0) Die();
    }

    void Die(){
        //for testing purposes creating a new version of itself at a random position
        Vector2 randomPosition = new Vector2(
        Random.Range(-7, 7),
        Random.Range(-3, 3)
        );
        Instantiate(gameObject, randomPosition, Quaternion.identity);


        
               Destroy((gameObject));
    }

    void OnCollisionEnter2D (Collision2D hit){
            if (hit.gameObject.tag == "Player"){
              //  hit.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * 5000);
                //prolly add function within player script of GotHit() that would mess with their rb, add -velocity, take health, etc
                 Debug.Log("hit player");
                    }



    }


}
