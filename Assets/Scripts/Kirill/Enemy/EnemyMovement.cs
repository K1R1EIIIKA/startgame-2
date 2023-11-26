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
    private float _speed;
    private bool _isLinked ;

    private void Awake()
    {
        _speed = Movement.forwardSpeed * 2;
        _player = Movement.Instance.transform;
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (transform.position.z > _player.position.z)
            _isLinked = true;

        if (_isLinked)
            PrepareHit();
        else
            _controller.Move(Vector3.forward * (_speed * Time.deltaTime));
    }

    private void PrepareHit()
    {
        Vector3 targetPosition = new Vector3(_player.position.x, transform.position.y, transform.position.z);

        if (transform.position.x > _player.position.x)
            targetPosition.x += 1.5f;
        else
            targetPosition.x -= 1.5f;

        transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, 0, _player.position.z);
    }
}