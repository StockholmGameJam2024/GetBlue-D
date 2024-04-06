using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CommittedDirectionSpringPlayerController : MonoBehaviour
{
    [SerializeField]
    private float maxTension;
    [SerializeField]
    private float minimumRotationPerFrame;
    [SerializeField]
    private float maximumRotationPerFrame;
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
     * Top: 1/2 PI
     * Left PI
     * Bottom: 3/2 PI
     */
    private static double VecToAngle(Vector2 vector)
    {
        return Math.Atan2(vector.y, vector.x);
    }
    
    private static Vector2 AngleToVec(double angle)
    {
        return new Vector2(
            (float)Math.Cos(angle),
            (float)Math.Sin(angle)
        );
    }

    private void FixedUpdate()
    {
        _moveInput = _playerActions.Player_Map.Movement.ReadValue<Vector2>();
        var inputMagnitude = Math.Sqrt(Math.Pow(_moveInput.x, 2) + Math.Pow(_moveInput.y, 2));
        // Scale the analog stick so that if less than halfway out, then decelerate (in a scaled way)
        inputMagnitude -= 0.5;

        double tensionAngle = VecToAngle(_tension);
        double inputAngle = VecToAngle(_moveInput);
        
        var currentMagnitude = Math.Sqrt(Math.Pow(_tension.x, 2) + Math.Pow(_tension.y, 2));
        
        if (inputMagnitude > 0)
        {
            // The closer to the "maximum tension", the slower tension should charge
            inputMagnitude *= 1 - currentMagnitude / maxTension;
        }
        else
        {
            // Decelerate tension faster than you accelerate it
            inputMagnitude *= 6;
        }
        currentMagnitude = Math.Max(currentMagnitude + inputMagnitude, 0);

        if (inputMagnitude > 0)
        {
            // Rotate a bit according to current input, but decrease "control-ability" according to how much tension there is
            var tensionAngleDegrees = tensionAngle * Mathf.Rad2Deg;
            var inputAngleDegrees = inputAngle * Mathf.Rad2Deg;
            
            var maxDelta = (maximumRotationPerFrame - minimumRotationPerFrame) * (1 - Math.Pow(currentMagnitude / maxTension, 0.1)) + minimumRotationPerFrame;
            
            var newTensionAngleDegrees = Mathf.MoveTowardsAngle((float)tensionAngleDegrees, (float) inputAngleDegrees, (float)maxDelta);
            tensionAngle = newTensionAngleDegrees * Mathf.Deg2Rad;
        }
        
        _tension = AngleToVec(tensionAngle) * (float)currentMagnitude;

        var color = Color.red;
        color.a = 1f;
        lineRenderer.transform.position = _rbody.position;
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        lineRenderer.positionCount = 2;
        // var lineVector = Vector3.zero;
        // lineVector.x += _tension.x * 0.2f;
        // lineVector.y += _tension.y * 0.2f;
        lineRenderer.SetPosition(0, _rbody.position);
        lineRenderer.SetPosition(1, _rbody.position + (_tension * 0.2f));
    }

    void OnSpring()
    {
        var currentMagnitude = Math.Sqrt(Math.Pow(_tension.x, 2) + Math.Pow(_tension.y, 2));
        var currentAngle = VecToAngle(_tension);
        var accelerationMagnitude = Math.Pow(currentMagnitude, 2) / 10; // The longer you hold down, the even bigger payoff. Exponential.
        var acceleration = AngleToVec(currentAngle) * (float)accelerationMagnitude;
        
        _rbody.velocity += acceleration;
        _tension = new Vector2();
    }
}
