using UnityEngine;
using System.Collections; 

public class CabinLight : MonoBehaviour
{
    private Light _light;
    private Coroutine transitionCoroutine;

    [Header("Colores personalizados")]
    public Color colorUtopia = new Color(1f, 0.725f, 0.325f); 
    public Color colorNeutral = new Color(1f, 0.98f, 0.85f); 

    [Header("Ajustes de intensidad")]
    public float intensidadMaxima = 3000f;
    public float transitionSpeed = 3f;
    
    private float currentState = 0f;

    void Awake()
    {
        _light = GetComponent<Light>();
    }

    private void OnEnable()
    {
        WorldState.OnWorldStateChanged += IniciarTransicion;
        // Sincronización inicial
        if (WorldState.Instance != null) IniciarTransicion(WorldState.Instance.state);
    }

    private void OnDisable()
    {
        WorldState.OnWorldStateChanged -= IniciarTransicion;
    }

    // Este método se activa con el evento
    private void IniciarTransicion(float nuevoEstado)
    {
        // Si ya había una transición en marcha, la paramos para empezar la nueva
        if (transitionCoroutine != null) StopCoroutine(transitionCoroutine);
        
        // Arrancamos el proceso de cambio suave
        transitionCoroutine = StartCoroutine(TransicionSuave(nuevoEstado));
    }

    // La Corrutina: Funciona como un Update pero solo cuando hace falta
    IEnumerator TransicionSuave(float targetState)
    {
        // Mientras no hayamos llegado al valor objetivo (con un margen de error pequeño)
        while (Mathf.Abs(currentState - targetState) > 0.001f)
        {

            currentState = Mathf.Lerp(currentState, targetState, Time.deltaTime * transitionSpeed);

            if (currentState >= 0)
                _light.color = Color.Lerp(colorNeutral, colorUtopia, currentState);
            else
                _light.color = colorNeutral;

            float tIntensidad = Mathf.InverseLerp(-1f, 0.5f, currentState); 
            _light.intensity = Mathf.Lerp(0f, intensidadMaxima, tIntensidad);

            _light.enabled = (_light.intensity > 0.001f);

            yield return null; 
        }
        

        currentState = targetState;
    }
}