using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Header("Ajustos de Salt")]
    public float tempsAnticipacio = 0.6f; 
    private bool preparantSalt = false;

    [Header("Ajustos de Moviment")]
    public float SpeedUtopia = 8f;
    public float GravityUtopia = -9.81f;
    public float JumpUtopia = 12f;
    public float SpeedDistopia = 4f;
    public float GravityDistopia = -35f;
    public float JumpDistopia = 4f;

    private float currentSpeed, currentGravity, currentJumpForce;
    private CharacterController _controller;
    private InputHandler _input;
    private float _verticalVelocity;
    public float RotationSpeed = 0.5f;

    void Awake() {
        _controller = GetComponent<CharacterController>();
        _input = GetComponent<InputHandler>();
    }

    void Start() {
        if (WorldState.Instance != null) ActualizarParametrosFisicos(WorldState.Instance.state);
    }

    private void OnEnable() => WorldState.OnWorldStateChanged += ActualizarParametrosFisicos;
    private void OnDisable() => WorldState.OnWorldStateChanged -= ActualizarParametrosFisicos;

    void Update() {
        Rotate();
        ApplyGravity();
        CheckJump();
        Move();
    }

    private void CheckJump() {
        if (_controller.isGrounded && Input.GetButtonDown("Jump") && !preparantSalt) {
            preparantSalt = true;
            Invoke("ExecutarSaltFisic", tempsAnticipacio);
        }
    }

    private void ExecutarSaltFisic() {
        _verticalVelocity = currentJumpForce;
        Invoke("FinalitzarGuardaSalt", 0.2f);
    }

    private void FinalitzarGuardaSalt() {
        preparantSalt = false;
    }

    public bool EstaEnAire() {
        return !_controller.isGrounded || preparantSalt || _verticalVelocity > 0;
    }

    private void ActualizarParametrosFisicos(float estadoActual) {
        float t = (estadoActual + 1f) / 2f;
        currentSpeed = Mathf.Lerp(SpeedDistopia, SpeedUtopia, t);
        currentGravity = Mathf.Lerp(GravityDistopia, GravityUtopia, t);
        currentJumpForce = Mathf.Lerp(JumpDistopia, JumpUtopia, t);
    }

    private void Move() {
        Vector3 moveDirection = transform.forward * _input.MoveInput.y;
        Vector3 velocity = moveDirection * currentSpeed;
        velocity.y = _verticalVelocity;
        _controller.Move(velocity * Time.deltaTime);
    }

    private void ApplyGravity() {
        if (_controller.isGrounded && _verticalVelocity < 0) _verticalVelocity = -2f;
        else _verticalVelocity += currentGravity * Time.deltaTime;
    }

    private void Rotate() {
        float rotationInput = _input.MoveInput.x;
        if (rotationInput != 0) transform.Rotate(Vector3.up, rotationInput * RotationSpeed);
    }
}