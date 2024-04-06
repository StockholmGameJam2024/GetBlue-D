using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float acceleration;
    private Vector2 _tension;
    private InputMaster _playerActions;
    private Rigidbody2D _rbody;
    private Vector2 _moveInput;

    void Awake()
    {
        _playerActions = new InputMaster();
        _rbody = GetComponent<Rigidbody2D>();
        _tension = new Vector2();
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
        _tension += _moveInput * acceleration;
    }

    void OnSpring()
    {
        _rbody.velocity += _tension;
        _tension = new Vector2();
    }
}
