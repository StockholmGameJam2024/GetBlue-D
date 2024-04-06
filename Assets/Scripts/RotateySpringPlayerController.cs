using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RotateySpringPlayerController : MonoBehaviour
{
    [SerializeField]
    private float maxTension;
    [SerializeField]
    private float inputAcceleration;
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

    /**
     * Right: 0
     * Bottom: -1/2 PI
     * Left: PI
     * Top: 1/2 PI
     */
    private double vecToAngle(Vector2 vector)
    {
        var isLeft = vector.x < 0;
        if (vector.x != 0)
        {
            var angle = Math.Atan(vector.y / Math.Abs(vector.x));
            return angle switch
            {
                > 0 when isLeft => angle + Math.PI / 2,
                < 0 when isLeft => angle - Math.PI / 2,
                _ => angle,
            };

        }
        if (vector.y < 0)
        {
            return -Math.PI / 2;
        }
        return Math.PI / 2;
    }
    
    private Vector2 angleToVec(double angle)
    {
        var isLeft = false;
        if (Math.Abs(angle) > Math.PI / 2)
        {
            isLeft = true;
            if (angle > 0)
            {
                angle -= Math.PI / 2;
            }
            else
            {
                angle += Math.PI / 2;
            }
        }
        return new Vector2
        {
            x = (float)Math.Cos(angle) * (isLeft ? -1 : 1),
            y = (float)Math.Sin(angle),
        };
    }

    private void FixedUpdate()
    {
        _moveInput = _playerActions.Player_Map.Movement.ReadValue<Vector2>();
        var inputMagnitude = Math.Sqrt(Math.Pow(_moveInput.x, 2) + Math.Pow(_moveInput.y, 2));
        // Scale the analog stick so that if less than halfway out, then decelerate (in a scaled way)
        inputMagnitude -= 0.5;

        // If the analog stick is not accelerating, then re-use the last angle
        double inputAngle = vecToAngle(inputMagnitude < 0 ? _tension : _moveInput);
        var currentMagnitude = Math.Sqrt(Math.Pow(_tension.x, 2) + Math.Pow(_tension.y, 2));
        
        inputMagnitude *= inputAcceleration;
        if (inputMagnitude > 0)
        {
            // The closer to the "maximum tension", the slower tension should charge
            inputMagnitude *= 1 - currentMagnitude / maxTension;
        }
        else
        {
            // Decelerate tension faster than you accelerate it
            inputMagnitude *= 2;
        }
        currentMagnitude = Math.Max(currentMagnitude + inputMagnitude, 0);

        _tension = angleToVec(inputAngle) * (float)currentMagnitude;

        var color = Color.red;
        color.a = 1f;
        lineRenderer.transform.position = _rbody.position;
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        lineRenderer.positionCount = 2;
        var lineVector = Vector3.zero;
        lineVector.x += _tension.x * 0.2f;
        lineVector.y += _tension.y * 0.2f;
        lineRenderer.SetPosition(0, Vector3.zero);
        lineRenderer.SetPosition(0, lineVector);
    }

    void OnSpring()
    {
        var currentMagnitude = Math.Sqrt(Math.Pow(_tension.x, 2) + Math.Pow(_tension.y, 2));
        var currentAngle = vecToAngle(_tension);
        var accelerationMagnitude = Math.Pow(currentMagnitude, 2) / 10; // The longer you hold down, the even bigger payoff. Exponential.
        var acceleration = angleToVec(currentAngle) * (float)accelerationMagnitude;
        
        _rbody.velocity += acceleration;
        _tension = new Vector2();
    }
}
