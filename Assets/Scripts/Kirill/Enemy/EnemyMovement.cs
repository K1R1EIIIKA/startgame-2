using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Enemy _enemyData;
    
    private void Update()
    {
        transform.position += Vector3.forward * (_enemyData.Speed * Time.deltaTime);
    }
}
