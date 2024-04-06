using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    private InputMaster _playerActions;
    private Rigidbody2D _rbody;
    private Vector2 _moveInput;

    void Awake()
    {
        _playerActions = new InputMaster();
        _rbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _playerActions.Enable();
    }
    
    private void OnDisable()
    {
        _playerActions.Disable();
    }

    private void FixedUpdate()
    {
        _moveInput = _playerActions.Player_Map.Movement.ReadValue<Vector2>();
        _rbody.velocity = _moveInput * _speed;
    }
}
