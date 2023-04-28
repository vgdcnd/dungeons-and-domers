using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string sceneName;
    public GameObject doorObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player collided with door");
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if (this.tag == "Door_Corridor") { 
                player.lastPosition = other.gameObject.transform.position;
            }
            SceneManager.LoadScene(sceneName);
            player.SpawnPlayer();
        }
            
    }
}
