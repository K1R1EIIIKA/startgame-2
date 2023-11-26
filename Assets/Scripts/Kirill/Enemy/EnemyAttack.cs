using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private void Update()
    {
        if (!EnemyMovement.CanAttack) return;
        
        EnemyMovement.CanAttack = false;
        StartCoroutine(PrepareAttack());
    }

    private IEnumerator PrepareAttack()
    {
        yield return new WaitForSeconds(EnemySpawn.Instance.AttackPrepareTime);
        Debug.Log("enemy preparing");
        yield return new WaitForSeconds(EnemySpawn.Instance.AttackTime);
        Attack();
    }

    private void Attack()
    {
        Debug.Log("enemy attacked");
        
        EnemyMovement.IsAttacked = true;
        EnemyMovement.IsLinked = false;
        EnemyMovement.Speed *= 2;
    }
}
