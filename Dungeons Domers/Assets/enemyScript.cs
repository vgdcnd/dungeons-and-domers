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
        Debug.Log("Ouch");
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

}
