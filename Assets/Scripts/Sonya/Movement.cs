using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Movement : MonoBehaviour
{
    private CharacterController controller;
    private PlayerInput _input;
    public static Animator animator;
    private static Vector3 direction;
    public static float forwardSpeed;
    [SerializeField] private float _moveSpeed = 10;
    [SerializeField] private TextMeshProUGUI speedText;
    public Vector2 _moveDirection;
    //[SerializeField] private float Gravity = -20;

    public static Movement Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        forwardSpeed = 4;
        speedText.text = forwardSpeed.ToString();
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    public void GravityChange(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!MagnitTrigger.magnitActive)
                return;
            
            animator.SetBool("MoveUp", true);
            direction.y = 10;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!PlayerManager.isGameStarted)
            return;
        // Debug.Log("move");
        _moveDirection = context.ReadValue<Vector2>();
        float scaledMoveSpeed = _moveSpeed;
        //Vector3 moveDirection = new Vector3(_moveDirection.x,0,0);
        //transform.position+=moveDirection*scaledMoveSpeed;
        direction.x = _moveDirection.x * scaledMoveSpeed;
        //controller.Move(moveDirection*scaledMoveSpeed);
    }

    public static void GravityChangeDown()
    {
        if (direction.y > 0)
            direction.y -= 20;
        
        Debug.Log("yes");
        animator.SetBool("MoveUp", false);
    }

    void Update()
    {
        if (!PlayerManager.isGameStarted)
            return;

        float speed = forwardSpeed * 3.33f;
        speedText.text = speed.ToString("0");
        forwardSpeed += 0.1f * Time.deltaTime;
        direction.z = forwardSpeed;
//Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        //    if (transform.position != targetPosition)
        // {
        // Vector3 diff = targetPosition - transform.position;
        //Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        //if (moveDir.sqrMagnitude < diff.sqrMagnitude)
        //     controller.Move(moveDir);
        // else
        //   controller.Move(diff);
        //}
        controller.Move(direction * Time.deltaTime);
    }
}