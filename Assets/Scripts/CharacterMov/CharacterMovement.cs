using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float Speed = 5f;
    public float RotationSpeed = 2f;
    public float Gravity = -9.81f;

    private CharacterController _controller;
    private InputHandler _input;

    private float _verticalVelocity;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _input = GetComponent<InputHandler>();
    }

    void Update()
    {
        Rotate();
        ApplyGravity();
        Move();
    }

    private void Move()
    {
        Vector3 moveDirection = transform.forward * _input.MoveInput.y;
        Vector3 velocity = moveDirection * Speed;

        velocity.y = _verticalVelocity;

        _controller.Move(velocity * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        if (_controller.isGrounded)
        {
            if (_verticalVelocity < 0)
                _verticalVelocity = -2f;
        }
        else
        {
            _verticalVelocity += Gravity * Time.deltaTime;
        }
    }

    private void Rotate()
    {
        float rotationInput = _input.MoveInput.x;

        if (rotationInput != 0)
        {
            transform.Rotate(Vector3.up, rotationInput * RotationSpeed);
        }
    }
}