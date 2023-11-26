using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyTrigger : MonoBehaviour
{
    private GameObject _enemy;

    private void OnTriggerEnter(Collider other)
    {
        if (EnemySpawn.IsEnemy)
            return;
        
        if (other.CompareTag("Player"))
        {
            Vector3 playerPos = other.transform.position;
            int randNum;

            if (Math.Abs(playerPos.x) < 0.75f)
                randNum = new List<int> { -1, 1 }[Random.Range(0, 2)];
            else if (playerPos.x >= 0.75f)
                randNum = new List<int> { -1, 0 }[Random.Range(0, 2)];
            else
                randNum = new List<int> { 0, 1 }[Random.Range(0, 2)];
            
            Vector3 spawnPoint = new Vector3(2.5f * randNum, 0, playerPos.z - EnemySpawn.Instance.SpawnOffset);
            _enemy = EnemySpawn.Instance.SpawnEnemy(spawnPoint);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && _enemy)
            EnemySpawn.Instance.RemoveEnemy();
    }
}