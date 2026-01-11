using UnityEngine;

public class StreetLights : MonoBehaviour
{
    private Light luzFisica;

    [Header("Ajustes de la Luz (Suelo)")]
    public Color colorUtopia = new Color(1f, 0.7f, 0.3f); // Naranja
    public Color colorDistopia = new Color(0.1f, 0.4f, 1f); // Azul
    
    public float intensidadUtopia = 10f;
    public float intensidadDistopia = 5f;

    void Start()
    {
        // Solo buscamos la bombilla
        luzFisica = GetComponentInChildren<Light>();
    }

    void Update()
    {
        if (WorldState.Instance == null || luzFisica == null) return;

        // t va de 0 (Distopía) a 1 (Utopía)
        float t = (WorldState.Instance.state + 1f) / 2f;

        // Cambiamos el color e intensidad de la luz física
        luzFisica.color = Color.Lerp(colorDistopia, colorUtopia, t);
        luzFisica.intensity = Mathf.Lerp(intensidadDistopia, intensidadUtopia, t);
    }
}