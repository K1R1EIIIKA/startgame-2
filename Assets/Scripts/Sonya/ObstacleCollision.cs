using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    

     void OnTriggerEnter(Collider other) {
        
        if (other.tag=="Player"){
           PlayerManager.hitHappened=true;
           StartCoroutine(Destroy());
          
        }
    }
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.6f);
       Destroy(gameObject);
    }
}
