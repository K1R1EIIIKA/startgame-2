using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDebuff : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Movement.forwardSpeed -= 1f;
            Destroy(gameObject);
        }
    }
}