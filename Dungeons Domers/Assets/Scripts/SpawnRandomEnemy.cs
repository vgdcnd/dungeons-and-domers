using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomEnemy : MonoBehaviour
{
    // Start is called before the first frame update


    public GameObject[] enemyList;
    private int selectEnemy;

    void Start(){
        
            selectEnemy = Random.Range(0, enemyList.Length);
            Instantiate(enemyList[selectEnemy], transform.position, Quaternion.identity);
            Destroy(gameObject);
    }
    
}
