using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomChildWatcher : MonoBehaviour
{
    // Start is called before the first frame update

    private bool roomCleared = false;
    [SerializeField]private GameObject doorToOpen;
    [SerializeField] private bool demoCleared = false;
 
    // can try to do another way, each time any enemey dies they call a function to alert that a counter has to decrease
    // can be weird tho if two enemies died at the same time so idk 



    void Update()
    {
        if (roomCleared) return; 

        if (gameObject.transform.childCount <=0){
            roomCleared = true;
            doorToOpen.GetComponent<Door>().OpenDoor(); 
            if (demoCleared)    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +2) ;

            

        }
        
    }
}
