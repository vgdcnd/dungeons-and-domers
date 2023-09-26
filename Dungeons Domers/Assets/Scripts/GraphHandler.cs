using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public float updateTime;
    void Start()
    {
             StartCoroutine(UpdateGraph());

    }

    // Update is called once per frame
 private IEnumerator UpdateGraph(){
      {
          while (true){
            AstarPath.active.Scan();
            yield return new WaitForSeconds(updateTime);
          }
      }

    }
}