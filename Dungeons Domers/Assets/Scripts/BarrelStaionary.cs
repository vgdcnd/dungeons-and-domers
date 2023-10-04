using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelStaionary : MonoBehaviour
{
    // Start is called before the first frame update
    Transform playerPos;
     [SerializeField] private float minDistance =1.5f;
     [SerializeField] private float movingBarrelSpeed;


    public GameObject movingBarrel;

    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact") && Mathf.Abs(Vector3.Distance(playerPos.position, transform.position)) <= minDistance ){
                PrepBarrel();
        } 
    }

    void PrepBarrel(){
            Vector3 positions = playerPos.position - transform.position;
            Vector2 moveDirection;

            if (Mathf.Abs(positions.x) > Mathf.Abs(positions.y)){
                if (positions.x >0) moveDirection = new Vector2(-1,0); // player is to the right so move left
                else moveDirection = new Vector2(1,0);
            }
            else {
                if (positions.y >0 ) moveDirection = new Vector2(0,-1);
                else moveDirection = new Vector2(0,1);
            } // FIGURING OUT MOVE DIRECTION (+-X or +-Y )

            GameObject spawnedBarrel = Instantiate(movingBarrel, transform.position, Quaternion.identity);
            if (gameObject.transform.parent) spawnedBarrel.transform.parent= transform.parent; // parent checking

            spawnedBarrel.GetComponent<PlayerBulletScript>().SetUpProjectile(movingBarrelSpeed, moveDirection);
            Destroy(gameObject);
    }
    }

