using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    private GameObject _enemy;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            Debug.Log(1);
            _enemy = EnemySpawn.Instance.SpawnEnemy(Vector3.zero);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_enemy)
            EnemySpawn.Instance.RemoveEnemy();
    }
}
