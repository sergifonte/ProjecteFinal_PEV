using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Header("ajustes utopía (estado 1)")]
    public float SpeedUtopia = 20f;
    public float GravityUtopia = -9.81f;
    public float JumpUtopia = 12f;

    [Header("ajustes distopía (estado -1)")]
    public float SpeedDistopia = 5f;
    public float GravityDistopia = -35f;
    public float JumpDistopia = 2f;

    [Header("variables actuales (calculadas por evento)")]
    public float RotationSpeed = 0.2f;
    private float currentSpeed;
    private float currentGravity;
    private float currentJumpForce;

    private CharacterController _controller;
    private InputHandler _input;
    private float _verticalVelocity;

    void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _input = GetComponent<InputHandler>();
    }

    void Start()
    {
        if (WorldState.Instance != null)
        {
            ActualizarParametrosFisicos(WorldState.Instance.state);
        }
    }

    private void OnEnable()
    {
        WorldState.OnWorldStateChanged += ActualizarParametrosFisicos;
    }

    private void OnDisable()
    {
        WorldState.OnWorldStateChanged -= ActualizarParametrosFisicos;
    }

    void Update()
    {
        Rotate();
        ApplyGravity();
        CheckJump();
        Move();
    }

    private void ActualizarParametrosFisicos(float estadoActual)
    {
        float t = (estadoActual + 1f) / 2f;
        currentSpeed = Mathf.Lerp(SpeedDistopia, SpeedUtopia, t);
        currentGravity = Mathf.Lerp(GravityDistopia, GravityUtopia, t);
        currentJumpForce = Mathf.Lerp(JumpDistopia, JumpUtopia, t);
    }

    private void Move()
    {
       
        Vector3 moveDirection = transform.forward * _input.MoveInput.y;
        Vector3 velocity = moveDirection * currentSpeed;

        velocity.y = _verticalVelocity;
        _controller.Move(velocity * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        if (_controller.isGrounded)
        {
            if (_verticalVelocity < 0) _verticalVelocity = -2f;
        }
        else
        {
            _verticalVelocity += currentGravity * Time.deltaTime;
        }
    }

    private void CheckJump()
    {
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