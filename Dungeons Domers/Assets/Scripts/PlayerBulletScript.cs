using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : ProjectileScript
{
    // Start is called before the first frame update
 
    [SerializeField] private int damage;

    public override void OnTriggerEnter2D(Collider2D hit){
         if(hit.gameObject.tag == "Enemy"){ // 
        hit.gameObject.GetComponent<enemyScript>().TakeDamage(damage);
    //    Debug.Log("Dealt: " + damage.ToString());

        }
       else if (hit.gameObject.GetComponent<Collider2D>().isTrigger) return;
       
        base.OnTriggerEnter2D(hit);
    }

}



