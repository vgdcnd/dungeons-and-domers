using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public float updateTime;

    public GameObject block;
    void Start()
    {
             StartCoroutine(UpdateGraph());

    }

    // Update is called once per frame
 private IEnumerator UpdateGraph(){
      {
          while (true){

            // scan a segment -> need a custom scan method. scan      
            // startCoroutine(ScanSegment);
              // scan a segment
                // come out here and wait for a few secs
                //scan the next segment    

            AstarPath.active.Scan();
            yield return new WaitForSeconds(updateTime);
          }
      }

    }
  void ScanSegment(){



  }


}