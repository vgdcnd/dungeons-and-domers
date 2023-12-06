using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update

    /* 
    public static MenuManager Instance { get; private set; } //its a singleton or sum idk fancy 
    
    private void Awake() 
    { 
    // If there is an instance, and it's not me, delete myself.
    
     if (Instance != null && Instance != this) 
        { 
        Destroy(this); 
        } 
    else 
    { 
        Instance = this; 
        DontDestroyOnLoad(this);
    } 
}
    */
    // Update is called once per frame
    
    public void NextScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);   
    }
    public void PrevScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);   
    }


        public void PrevPrevScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -2);   
    }
 
    public void MainScene(){
        SceneManager.LoadScene(1);   
    }
}
