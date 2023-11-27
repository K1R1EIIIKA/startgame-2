using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            Movement.GravityChangeDown();
    }
}