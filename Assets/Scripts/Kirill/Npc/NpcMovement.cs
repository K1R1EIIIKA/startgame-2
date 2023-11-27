using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMovement : MonoBehaviour
{
    [NonSerialized] public float Speed;
    [NonSerialized] public int Direction;

    private CharacterController _controller;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        _controller.Move(Vector3.forward * (Direction * Speed * Time.deltaTime));
    }
    
}