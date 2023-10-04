using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverSwitch : MonoBehaviour
{
    // Start is called before the first frame update


    // if player press flip lever sprite

    public Sprite rightSprite, leftSprite;
    public GameObject OffBlocks, OnBlocks;
        // objects will just be a 2 tilemaps put over each other, on blocks will have a collider and rigid body while off blocks only got a renderer
            // manuelly place the two objects so lever knows which blocks it should be interacting with
            // --------ON BLOCK WILL NEED TO HAVE THE DYNAMIC GRAPH OBJECT SCRIPT --------
    private float minDistance= 1.5f;
    private SpriteRenderer spriteRenderer;
    private Transform playerPos;

    private bool right = true; // if true switch is on right side, otherwise switch on left

    void Start()
    {

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact") && Mathf.Abs(Vector3.Distance(playerPos.position, transform.position)) <= minDistance ){
            FlipSwitch();
        }
    }
    private void FlipSwitch(){
              if (right) spriteRenderer.sprite = leftSprite;
            else spriteRenderer.sprite = rightSprite;
            right = !right; // go to opposite side
            OffBlocks.SetActive(!right);  
            OnBlocks.SetActive(right);
          //  Debug.Log("Flipped");
    }
}
