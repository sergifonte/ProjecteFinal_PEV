using UnityEngine;

public class StreetLights : MonoBehaviour
{
    private Light luzFisica;

    [Header("Ajustes de la Luz (Suelo)")]
    public Color colorUtopia = new Color(1f, 0.7f, 0.3f); 
    public Color colorDistopia = Color.blue; 
    public float intensidadUtopia = 10f;
    public float intensidadDistopia = 5f;

    void Awake()
    {
        luzFisica = GetComponentInChildren<Light>();
    }

    private void OnEnable()
    {
        WorldState.OnWorldStateChanged += ActualizarLuz;
    }

    private void OnDisable()
    {
        WorldState.OnWorldStateChanged -= ActualizarLuz;
    }

    void Start()
    {
        if (WorldState.Instance != null && luzFisica != null)
        {
            ActualizarLuz(WorldState.Instance.state);
        }
    }

    private void ActualizarLuz(float estado)
    {
        if (luzFisica == null) return;

        float t = (estado + 1f) / 2f;

        luzFisica.color = Color.Lerp(colorDistopia, colorUtopia, t);
        luzFisica.intensity = Mathf.Lerp(intensidadDistopia, intensidadUtopia, t);
    }
}