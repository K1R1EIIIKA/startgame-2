using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    
    private GameObject _enemy;

    public static EnemySpawn Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    
    public GameObject SpawnEnemy(Vector3 position)
    {
        _enemy = Instantiate(_enemyPrefab, position, Quaternion.identity);
        return _enemy;
    }

    public void RemoveEnemy()
    {
        Destroy(_enemy);
    }
}
