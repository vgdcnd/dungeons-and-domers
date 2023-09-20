using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class Door : MonoBehaviour
{
    public string sceneName;
    public GameObject Hall;
    public GameObject Room;

    [SerializeField] GameObject walls;

    [SerializeField] private Collider2D doorCollider; // can hard set in editor
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
    void Start(){
           // can change from using four different  booleans to an integer and switch case if wanted
           if (downFacing) pushDirection = new Vector3(0,1.5f,0); // make sure player gets pushed into room fully
           else if (upFacing) pushDirection = new Vector3(0, -1f,0);
            else if(leftFacing) pushDirection = new Vector3(-1f, 0, 0);


        //if (!doorNumber) parse door name to get the number manually if number hasnt been chosen in editor 
            walls = GameObject.Find("WallsGrid");
                    
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(roomState == 1) { // ENTERING HALLWAY
                    Hall.SetActive(true);
            Room.SetActive(false);

                    foreach (Transform child in walls.transform){
                        child.gameObject.SetActive(true);
                    }
                other.gameObject.transform.position += pushDirection * -1;
                roomState = 2;

            }
            // deal with player trying to re enter room after clearing it
            else { // ENTERING ROOM     
                    // lock player 
                    
                   
                     // want to do some cute camera transition thingy to pan to room ?  
                    Hall.SetActive(false);
                    Room.SetActive(true);
                    foreach (Transform child in walls.transform){
                        GameObject currentObject = child.gameObject;
                        if (Regex.IsMatch(currentObject.name, ".*" + doorNumber + ".*")) continue;
                        currentObject.SetActive(false);
                    }

                    if (roomState ==0){
 other.gameObject.transform.position += pushDirection;
                        doorCollider.isTrigger = false; 
                    }  
                    if (roomState ==2){
                        other.gameObject.transform.position += pushDirection;
                     roomState =1;
                    }

            }
        
        }
    }
    public void OpenDoor(){ //player has cleared room 
        doorCollider.isTrigger = true; // can pass through
        roomState =1;
        
    }

}
