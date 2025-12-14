using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float Speed = 4.2f;
    public float RotationSpeed = 10f;

    private CharacterController _controller;
    private InputHandler _input;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _input = GetComponent<InputHandler>();
    }

    void Update()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        Vector3 direction = _input.GetInputInHorizontalPlane().normalized;
        Vector3 velocity = direction * Speed;

        _controller.Move(velocity * Time.deltaTime);
    }

    private void Rotate()
    {
        Vector3 direction = _input.GetInputInHorizontalPlane();

        if (direction.sqrMagnitude < 0.01f)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            RotationSpeed * Time.deltaTime
        );
    }
}
