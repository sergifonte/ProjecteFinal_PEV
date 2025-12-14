using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float Speed = 1f;              // Velocitat de moviment
    public float RotationSpeed = 0.5f;      // Velocitat de rotació de l'eix Y. Augmenta aquest valor fins a obtenir la rotació desitjada.

    private CharacterController _controller;
    private InputHandler _input;
    private float _currentRotationY = 0f; // Mantenir l'angle de rotació actual en Y.

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _input = GetComponent<InputHandler>();
    }

    void Update()
    {
        Rotate();
        Move();
    }

    private void Move()
    {
        // Moviment endavant o enrere en funció de la direcció de l'orientació actual del personatge
        Vector3 moveDirection = transform.forward * _input.MoveInput.y;
        _controller.Move(moveDirection * Speed * Time.deltaTime);
    }

    private void Rotate()
    {
        // Control de rotació basat en l'entrada de les tecles "A" i "D"
        float rotationInput = _input.MoveInput.x;  // A és -1, D és 1

        // Si es prem "A" o "D", es gira el personatge ràpidament
        if (rotationInput != 0)
        {
            // Actualitzem l'angle de rotació en l'eix Y segons l'entrada
            _currentRotationY += rotationInput * RotationSpeed;

            // Aplicar la nova rotació al personatge
            transform.rotation = Quaternion.Euler(0, _currentRotationY, 0);
        }
    }
}
