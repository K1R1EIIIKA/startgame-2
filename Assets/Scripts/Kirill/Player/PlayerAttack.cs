using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Collider _hitCollider;
    private bool _canAttack;
    public static int NumAttacks = 0;

    private void Awake()
    {
        _hitCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (EnemyMovement.IsLinked)
        {
            _canAttack = true;
        }
    }

    public void StartAttack()
    {
        if (!EnemySpawn.IsEnemy || !_canAttack || NumAttacks == 0)
            return;
        
        Debug.Log("player attack");
        StartCoroutine(Attack());
        _canAttack = false;
        NumAttacks = 0;
    }

    private IEnumerator Attack()
    {
         Movement.animator.SetBool("", true);// a
        yield return new WaitForSeconds(1); 
         FindObjectOfType<AudioManager>().PlaySound("HitPan");
        _hitCollider.enabled = true;
        yield return new WaitForSeconds(0.5f);
        _hitCollider.enabled = false;
    }
}
