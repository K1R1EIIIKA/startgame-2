using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float lerpSpeed = 2f;
        
    private Transform _player;
    private CharacterController _controller;
    
    public static bool IsHit;
    public static float Speed;
    public static bool IsLinked;
    public static bool CanAttack;
    public static bool IsAttacked;

    private void Awake()
    {
        Speed = Movement.forwardSpeed * 2;
        _player = Movement.Instance.transform;
        _controller = GetComponent<CharacterController>();

        IsAttacked = false;
        IsLinked = false;
    }

    private void Update()
    {
        if (IsHit)
        {
            HitMove();
            return;
        }
        
        if (!IsAttacked && transform.position.z > _player.position.z)
        {
            IsLinked = true;
            CanAttack = true;
        }

        if (IsLinked)
            LinkMove();
        else
            Move();
    }

    private void Move()
    {
        _controller.Move(Vector3.forward * (Speed * Time.deltaTime));
    }
    
    private void LinkMove()
    {
        Vector3 targetPosition = new Vector3(_player.position.x, transform.position.y, transform.position.z);

        if (transform.position.x > _player.position.x)
            targetPosition.x += 1.5f;
        else
            targetPosition.x -= 1.5f;

        transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, 0, _player.position.z);
    }

    private void HitMove()
    {
        if (transform.position.x > _player.position.x)
            transform.position += new Vector3(EnemySpawn.Instance.HitForce, 0, 20) * Time.deltaTime;
        else
            transform.position += new Vector3(-EnemySpawn.Instance.HitForce, 0, 20) * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BoxCollider boxCollider) && other.CompareTag("Player"))
        {
            Debug.Log("attacked");
            IsHit = true;
        }
    }
}