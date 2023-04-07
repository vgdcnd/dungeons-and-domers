using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Swing_Script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D hit){

        if (hit.gameObject.tag == "Enemy"){
            hit.gameObject.GetComponent<enemyScript>().TakeDamage(50);
            // better way to call a method from another object ? 
        }


    }
}
