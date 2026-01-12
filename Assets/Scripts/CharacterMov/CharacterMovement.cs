using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Header("Ajustes Utopía (Estado 1)")]
    public float SpeedUtopia = 20f;
    public float GravityUtopia = -9.81f;
    public float JumpUtopia = 12f;

    [Header("Ajustes Distopía (Estado -1)")]
    public float SpeedDistopia = 5f;
    public float GravityDistopia = -35f;
    public float JumpDistopia = 2f;

    [Header("Variables Actuales")]
    public float RotationSpeed = 0.2f;
    private float currentSpeed;
    private float currentGravity;
    private float currentJumpForce;

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
        ActualizarAtributosFisicos();
        Rotate();
        ApplyGravity();
        CheckJump(); // Nueva función de salto
        Move();
    }

    private void ActualizarAtributosFisicos()
    {
        if (WorldState.Instance == null) return;

        // t va de 0 (Distopía) a 1 (Utopía)
        float t = (WorldState.Instance.state + 1f) / 2f;

        // Interpolamos los tres valores físicos
        currentSpeed = Mathf.Lerp(SpeedDistopia, SpeedUtopia, t);
        currentGravity = Mathf.Lerp(GravityDistopia, GravityUtopia, t);
        currentJumpForce = Mathf.Lerp(JumpDistopia, JumpUtopia, t);
    }

    private void Move()
    {
        Vector3 moveDirection = transform.forward * _input.MoveInput.y;
        // Usamos la velocidad calculada según el mundo
        Vector3 velocity = moveDirection * currentSpeed;

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
            // Usamos la gravedad pesada o ligera según el mundo
            _verticalVelocity += currentGravity * Time.deltaTime;
        }
    }

    private void CheckJump()
    {
        // Salta si está en el suelo y pulsas la barra espaciadora
        if (_controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            _verticalVelocity = currentJumpForce;
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