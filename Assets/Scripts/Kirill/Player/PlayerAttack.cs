using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Collider _hitCollider;
    private bool _canAttack;
    public static int NumAttacks = 0;
    public static PlayerAttack Instance;
        
    private void Awake()
    {
        NumAttacks = 0;
        if (Instance == null)
            Instance = this;
        else 
            Destroy(gameObject);
        
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
        // Debug.Log(!EnemySpawn.IsEnemy + " " + !_canAttack  + " " + NumAttacks);
        if (!EnemySpawn.IsEnemy || !_canAttack || NumAttacks == 0)
            return;

        Debug.Log("player attack");
        StartCoroutine(Attack());
        _canAttack = false;
        NumAttacks = 0;
    }

    private IEnumerator Attack()
    {
        if (transform.position.x > EnemyMovement.Instance.transform.position.z)
            Movement.animator.SetBool("HitLeft", true); 
        else
            Movement.animator.SetBool("HitRight", true); 
        
        yield return new WaitForSeconds(1);
        FindObjectOfType<AudioManager>().PlaySound("HitPan");
        _hitCollider.enabled = true;

        yield return new WaitForSeconds(0.5f);
        _hitCollider.enabled = false;
        if (transform.position.x > EnemyMovement.Instance.transform.position.z)
            Movement.animator.SetBool("HitLeft", false); 
        else
            Movement.animator.SetBool("HitRight", false); 
    }
}