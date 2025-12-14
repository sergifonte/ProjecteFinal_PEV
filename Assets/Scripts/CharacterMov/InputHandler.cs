using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public Vector2 MoveInput;

    void Update()
    {
        MoveInput.x = Input.GetAxisRaw("Horizontal");
        MoveInput.y = Input.GetAxisRaw("Vertical");
    }

    public Vector3 GetInputInHorizontalPlane()
    {
        return new Vector3(MoveInput.x, 0f, MoveInput.y);
    }

    public bool HasMovement()
    {
        return MoveInput.sqrMagnitude > 0.01f;
    }
}
