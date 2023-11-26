using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    
    private GameObject _enemy;
    
    public float SpawnOffset = 3;
    public float AttackPrepareTime = 0.5f;
    public float AttackTime = 1.0f;

    public static EnemySpawn Instance;
    public static bool IsEnemy;

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
        IsEnemy = true;
        PlayerAttack.NumAttacks = 1;
        
        return _enemy;
    }

    public void RemoveEnemy()
    {
        IsEnemy = false;
        PlayerAttack.NumAttacks = 1;
        
        Destroy(_enemy);
    }
}
