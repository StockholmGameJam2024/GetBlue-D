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
    private float maxTension;
    [SerializeField]
    private float tensionAcceleration;
    [SerializeField]
    private float tensionFrictionDeceleration;
    private Vector2 _tension;
    private InputMaster _playerActions;
    private Rigidbody2D _rbody;
    private Vector2 _moveInput;
    public LineRenderer lineRenderer;

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
        _tension += _moveInput * tensionAcceleration;
        _tension *= 1 - tensionFrictionDeceleration;

        var color = Color.red;
        color.a = 1f;
        lineRenderer.transform.position = _rbody.position;
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        lineRenderer.positionCount = 2;
        var vector = Vector3.zero;
        vector.x += _tension.x * 0.1f;
        vector.y += _tension.y * 0.1f;
        lineRenderer.SetPosition(0, Vector3.zero);
        lineRenderer.SetPosition(0, vector);
    }

    void OnSpring()
    {
        _rbody.velocity += _tension;
        _tension = new Vector2();
    }
}
