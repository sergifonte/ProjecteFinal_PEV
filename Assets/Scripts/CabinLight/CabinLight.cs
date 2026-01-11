using UnityEngine;

public class CabinLightController : MonoBehaviour
{
    private Light _light;

    [Header("Colores personalizados")]
    
    // Naranja cálido (255, 185, 83)
    public Color colorUtopia = new Color(1f, 0.725f, 0.325f); 
    public Color colorNeutral = new Color(1f, 0.98f, 0.85f); 

    [Header("Ajustes de intensidad")]
    public float intensidadMaxima = 3000f;
    public float transitionSpeed = 3f;
    
    private float currentState;

    void Start()
    {
        _light = GetComponent<Light>();
    }

    void Update()
    {
        if (WorldState.Instance == null) return;

        currentState = Mathf.Lerp(currentState, WorldState.Instance.state, Time.deltaTime * transitionSpeed);

        if (currentState >= 0)
        {
            _light.color = Color.Lerp(colorNeutral, colorUtopia, currentState);
        }
        else
        {
            _light.color = colorNeutral;
        }

        float tIntensidad = Mathf.InverseLerp(-1f, 0.5f, currentState); 
        _light.intensity = Mathf.Lerp(0f, intensidadMaxima, tIntensidad);

        _light.enabled = (_light.intensity > 0.001f);
    }
}