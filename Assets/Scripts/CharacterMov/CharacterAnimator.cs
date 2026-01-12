using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    private Animator _animator;
    private InputHandler _input;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _input = GetComponent<InputHandler>();
    }

    private void OnEnable()
    {
        
        WorldState.OnWorldStateChanged += ActualizarParametroMundo;
        
        if (WorldState.Instance != null) ActualizarParametroMundo(WorldState.Instance.state);
    }

    private void OnDisable()
    {
        WorldState.OnWorldStateChanged -= ActualizarParametroMundo;
    }

    void Update()
    {
        float speed = _input.GetInputInHorizontalPlane().magnitude;
        _animator.SetFloat("Speed", speed);
    }

    private void ActualizarParametroMundo(float nuevoEstado)
    {
        if (_animator != null)
        {
            _animator.SetFloat("State", nuevoEstado);
        }
    }
}