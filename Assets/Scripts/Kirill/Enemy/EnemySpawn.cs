using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemySpawn : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] private GameObject _enemyPrefab;
    public float SpawnOffset = 3;
    public float AttackPrepareTime = 0.5f;
    public float AttackTime = 1.0f;

    [Header("Player")]
    public float HitForce = 30;

    public static EnemySpawn Instance;
    public static bool IsEnemy;
    public static int EnemyCount = 0;
    
    private GameObject _enemy;

    private void Awake()
    {
        EnemyCount = 0;
        
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    
    public GameObject SpawnEnemy(Vector3 position)
    {
        _enemy = Instantiate(_enemyPrefab, position, Quaternion.identity);
        IsEnemy = true;
        EnemyMovement.IsHit = false;
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
