using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    private Animator _animator;
    private CharacterMovement _movement;
    private InputHandler _input;

    void Awake() {
        _animator = GetComponent<Animator>();
        _input = GetComponent<InputHandler>();
        _movement = GetComponent<CharacterMovement>();
    }

    private void OnEnable() => WorldState.OnWorldStateChanged += ActualizarEstadoVisual;
    private void OnDisable() => WorldState.OnWorldStateChanged -= ActualizarEstadoVisual;

    void Update() {
        if (_animator == null) return;

        _animator.SetFloat("Speed", _input.MoveInput.magnitude * 0.1f);

        bool aTerra = !_movement.EstaEnAire();
        _animator.SetBool("isGrounded", aTerra);

        if (aTerra && Input.GetButtonDown("Jump")) {
            _animator.SetTrigger("Jump");
        }
    }

    private void ActualizarEstadoVisual(float nuevoEstado) {
        _animator.SetFloat("State", nuevoEstado);
    }
}