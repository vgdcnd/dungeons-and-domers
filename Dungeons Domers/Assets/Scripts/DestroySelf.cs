using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    public void DestroyObject(){ // used for enemy death and any other effects prolly maybe particles could work but idk 
        Destroy(gameObject);
    }

}
