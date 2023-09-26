using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float attack;
    [SerializeField] private float speed;

    public Animator animator;

    GameObject player;

    // Update is called once per frame
    void Start(){
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate(){
        Vector3 movePosition = player.transform.position - transform.position;
        animator.SetFloat("moveX", movePosition.x);
        animator.SetFloat("moveY", movePosition.y);

        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed);
    }


    void OnTriggerEnter2D(Collider2D col){

        if (col.gameObject == player){
            col.gameObject.GetComponent<PlayerController>().TakeDamage(attack, transform.position);
            gameObject.GetComponent<enemyScript>().TakeDamage(10000f); // killing itself for 'explosion' 
        }

    }
}
