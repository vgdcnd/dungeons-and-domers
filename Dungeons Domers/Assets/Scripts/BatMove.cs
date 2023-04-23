using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){

        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

        transform.position = Vector3.MoveTowards(transform.position, playerPos, speed);

    }

}
