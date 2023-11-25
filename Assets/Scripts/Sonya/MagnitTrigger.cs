using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnitTrigger : MonoBehaviour
{
    
     public static bool magnitActive;
     void Start()
     {
      
     }

     void OnTriggerEnter(Collider other) {
        
        if (other.tag=="Player"){
          magnitActive=true;
        }
    }
    void  OnTriggerExit(Collider other) {
        if (other.tag=="Player"){
          magnitActive=false;
        }
    }
}
