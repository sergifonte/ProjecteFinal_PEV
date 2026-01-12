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

    private void IniciarTransicion(float nuevoEstado)
    {
        if (transitionCoroutine != null) StopCoroutine(transitionCoroutine);
        
        transitionCoroutine = StartCoroutine(TransicionSuave(nuevoEstado));
    }

    IEnumerator TransicionSuave(float targetState)
    {
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