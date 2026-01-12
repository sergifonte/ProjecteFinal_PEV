using UnityEngine;
using System; 

public class WorldState : MonoBehaviour
{
    public static WorldState Instance;

    // el evento: otros scripts se "suscribirán" aquí
    // el <float> indica que cuando avisemos, enviaremos el valor actual del mundo
    public static event Action<float> OnWorldStateChanged;

    [Range(-1f, 1f)]
    [SerializeField] private float _state = 0f;

    // propiedad para que al cambiar el estado desde cualquier sitio, se dispare el evento
    public float state
    {
        get => _state;
        set
        {
            _state = Mathf.Clamp(value, -1f, 1f);
            // si hay alguien escuchando (no es null)
            OnWorldStateChanged?.Invoke(_state);
        }
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // para que los cambios en el inspector también activen los eventos
    private void OnValidate()
    {
        OnWorldStateChanged?.Invoke(_state);
    }

    public void AddState(float value)
    {
        state += value; 
        Debug.Log("World State updated: " + state);
    }
}