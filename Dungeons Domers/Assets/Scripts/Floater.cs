using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    // Start is called before the first frame updatepublic static MenuManager Instance { get; private set; } //its a singleton or sum idk fancy 
     public static Floater Instance { get; private set; } //its a singleton or sum idk fancy 
     private int score;
    private void Awake() 
    { 
    // If there is an instance, and it's not me, delete myself.
     if (Instance != null && Instance != this)    Destroy(this); 
    
    else 
      { 
        Instance = this; 
        DontDestroyOnLoad(this);
        }    
    }

    public void SetScore(int s){
        score = s;
    }
    public int GetScore(){
        return score;
    }
}
