using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class Door : MonoBehaviour
{
    // may want to have door be a sepearte object outside of tile set so that can easily change sprite depenign on if it is locked or not
    private GameObject Hall, Walls;

    private SpriteRenderer spriteRenderer;
    public GameObject Room;

    //private GameObject Walls;

    private Collider2D doorCollider; // can hard set in editor
    private int roomState = 0; 
    /*
    ROOM STATE:
    0 -> player can enter room. if they enter room becomes locked until they clear. 
    1 -> player bouta exit the room and go to hallway
    2 -> player re-enters room after already clearing, door doesn't lock
    */
    private Vector3 pushDirection;
    [SerializeField] private int doorNumber;
    [SerializeField] private bool downFacing, upFacing, leftFacing, rightFacing;

    [SerializeField] private Sprite openSprite, closedSprite;
    void Start(){
           // can cange from using four different  booleans to an integer and switch case if wanted
            // make sure player gets pushed into room fully especially needed when door is set to nontrigger collider
            if (downFacing) pushDirection = new Vector3(0,1f,0); 
            else if (upFacing) pushDirection = new Vector3(0, -1f,0);
            else if(rightFacing) pushDirection = new Vector3(-1f, 0, 0);
            else pushDirection = new Vector3(1f, 0, 0);

            Walls = GameObject.Find("WallsGrid");
            Hall = GameObject.FindWithTag("Hall");
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            doorCollider = gameObject.GetComponent<Collider2D>();
    
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            if(roomState == 1) { // ENTERING HALLWAY
                    Hall.SetActive(true);
            Room.SetActive(false);

                    foreach (Transform child in Walls.transform){
                        child.gameObject.SetActive(true);
                    }
                other.gameObject.transform.position += (pushDirection * -1);
                roomState = 2;

            }
            // deal with player trying to re enter room after clearing it
            else { // ENTERING ROOM     
                    // lock player 
                     // want to do some cute camera transition thingy to pan to room ?  
                    Hall.SetActive(false);
                    Room.SetActive(true);
                    foreach (Transform child in Walls.transform){
                        GameObject currentObject = child.gameObject;
                        if (Regex.IsMatch(currentObject.name, ".*" + doorNumber + ".*")) continue;
                        currentObject.SetActive(false);
                    }

                    if (roomState ==0){ // lock up to room. 
                    other.gameObject.transform.position += pushDirection;
                        ToggleDoors(false); 
                    spriteRenderer.sprite = closedSprite;

                        // if the door has children loop through and change animation for those too 
                        foreach(Transform childDoor in gameObject.transform){
                                SpriteRenderer childSprite = childDoor.gameObject.GetComponent<SpriteRenderer>();
                                if (childSprite) childSprite.sprite=  closedSprite;
                        }
                    }  
                    if (roomState ==2){
                        other.gameObject.transform.position += pushDirection;
                     roomState =1;
                    }

            }
        
        }
    }
    public void OpenDoor(){ //player has cleared room 
               ToggleDoors(true);
        doorCollider.isTrigger = true; // can pass through
        roomState =1;
        spriteRenderer.sprite = openSprite;
                 foreach(Transform childDoor in gameObject.transform){
                                SpriteRenderer childSprite = childDoor.gameObject.GetComponent<SpriteRenderer>();
                                if (childSprite) childSprite.sprite=  openSprite;
                        }
    }
    private void ToggleDoors(bool toggle){
            doorCollider.isTrigger = toggle;

            foreach(Transform childDoor in gameObject.transform){
                Collider2D col = childDoor.gameObject.GetComponent<Collider2D>();
                if(col) col.isTrigger = toggle;
            
            }

    }


}
