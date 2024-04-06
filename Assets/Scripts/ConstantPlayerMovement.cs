using UnityEngine;

public class ConstantPlayerMovement : MonoBehaviour
{
    public float moveVelocity = 200f;
    public float turnForce = 200f;

    private Rigidbody2D rb;
    private InputMaster _playerActions;
    private float horizontalInput;
    private Vector2 lastDirection;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _playerActions = new InputMaster();
    }

    void FixedUpdate()
    {
        Vector2 moveInput = _playerActions.Player_Map.Movement.ReadValue<Vector2>();
        horizontalInput = moveInput.x;
        
        rb.AddForce(transform.up * moveVelocity);
        rb.angularVelocity = -horizontalInput * turnForce;

        if (horizontalInput == 0)
        {
            lastDirection = rb.velocity.normalized;
        }
    }

    private void OnEnable()
    {
        _playerActions.Enable();
    }

    private void OnDisable()
    {
        _playerActions.Disable();
    }
}