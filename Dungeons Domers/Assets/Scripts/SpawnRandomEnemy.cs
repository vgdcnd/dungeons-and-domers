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
            GameObject newEnemy = Instantiate(enemyList[selectEnemy], transform.position, Quaternion.identity);
            if (gameObject.transform.parent) newEnemy.transform.parent= transform.parent;
            /// if spawner is assigned a parent make sure that the new enemy is placed under same parent, otherwise can just spawn like normal
            Destroy(gameObject);
    }
    
}
