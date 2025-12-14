using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(InputHandler))]
public class CharacterAnimator : MonoBehaviour
{
    private Animator _animator;
    private InputHandler _input;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _input = GetComponent<InputHandler>();
    }

    void Update()
    {
        float speed = _input.GetInputInHorizontalPlane().magnitude;
        _animator.SetFloat("Speed", speed);
    }
}
