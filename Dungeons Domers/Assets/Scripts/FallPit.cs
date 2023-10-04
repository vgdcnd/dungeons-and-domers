using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPit : MonoBehaviour
{
    // Start is called before the first frame update

    public Collider2D pitCollider;
    void Start()
    {
        /*
        Collider2D[] allColliders = GameObject.FindObjectsOfType<Collider2D>();

        foreach (Collider2D collider in allColliders)
        {
            Physics2D.IgnoreCollision(pitCollider, collider, true);
        }
            */
        
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col){

        if (col.gameObject.name == "Player") col.gameObject.GetComponent<PlayerController>().Fall();


    }
}
