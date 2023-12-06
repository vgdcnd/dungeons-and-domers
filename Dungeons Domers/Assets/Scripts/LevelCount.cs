using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCount : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    //  
     GetComponent<Text>().text = "You made it to level " + GameObject.Find("ScoreFloater").GetComponent<Floater>().GetScore();


    }
}
