using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!EnemyMovement.CanAttack || EnemyMovement.IsHit) return;
        
        EnemyMovement.CanAttack = false;
        StartCoroutine(PrepareAttack());
    }

    private IEnumerator PrepareAttack()
    {
        if (EnemyMovement.IsHit)
            yield break;
        
        PlayerManager.Instance.IconAttack.SetActive(true);
        
        yield return new WaitForSeconds(EnemySpawn.Instance.AttackPrepareTime);
        Debug.Log("enemy preparing");
        _animator.SetTrigger("ReadyHit");
        
        yield return new WaitForSeconds(EnemySpawn.Instance.AttackTime);
        Attack();
        PlayerManager.Instance.IconAttack.SetActive(false);
    }

    private void Attack()
    {
        if (EnemyMovement.IsHit) return;
        
        Debug.Log("enemy attacked");
        
        TerrainGenerator.Instance.SpawnEnemyOvertookTerrain();

        EnemySpawn.EnemyCount++;
        EnemyMovement.IsAttacked = true;
        EnemyMovement.IsLinked = false;
        EnemyMovement.Speed *= 2;
    }
}
