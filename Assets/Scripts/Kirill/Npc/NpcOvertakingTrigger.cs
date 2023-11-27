using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcOvertakingTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            NpcManager.Instance.SpawnAllNpc(1);
    }
}
