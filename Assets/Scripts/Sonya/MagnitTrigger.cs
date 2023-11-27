using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnitTrigger : MonoBehaviour
{
    public static bool magnitActive;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            magnitActive = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            magnitActive = false;
    }
}