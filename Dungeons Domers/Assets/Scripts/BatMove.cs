using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    private Transform playerPos;
    void Start()
    {
 playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){

        

        transform.position = Vector3.MoveTowards(transform.position, playerPos.position, speed);

    }

}
