using UnityEngine;
using System.Collections;

public class Fog : MonoBehaviour
{
    [Header("Colores")]
    public Color dystopiaFogColor = new Color(0.86f, 0.99f, 1.18f);
    public Color normalFogColor = Color.gray; // Cambiado de Clear para que se vea
    public Color utopiaFogColor = new Color(1f, 0.9f, 0.7f);

    [Header("Densidades")]
    public float dystopiaFogDensity = 0.04f;
    public float normalFogDensity = 0.01f;
    public float utopiaFogDensity = 0.015f;
    
    [Header("Ajustes")]
    public float transitionSpeed = 2f;

    private Coroutine fogCoroutine;

    void Awake()
    {
        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.Exponential;
    }

    private void OnEnable()
    {
        WorldState.OnWorldStateChanged += IniciarTransicionFog;
    }

    private void OnDisable()
    {

        WorldState.OnWorldStateChanged -= IniciarTransicionFog;
    }

    void Start()
    {
  
        if (WorldState.Instance != null)
        {
            CalcularYAplicarFog(WorldState.Instance.state);
        }
    }

    private void IniciarTransicionFog(float nuevoEstado)
    {
        if (fogCoroutine != null) StopCoroutine(fogCoroutine);
        fogCoroutine = StartCoroutine(TransicionSuaveFog(nuevoEstado));
    }

    IEnumerator TransicionSuaveFog(float targetState)
    {

        float elapsed = 0f;
        float duration = 1f / transitionSpeed;

        Color startColor = RenderSettings.fogColor;
        float startDensity = RenderSettings.fogDensity;


        float targetDensity;
        Color targetColor;

        if (targetState < 0f)
        {
            float t = Mathf.InverseLerp(-1f, 0f, targetState);
            targetDensity = Mathf.Lerp(dystopiaFogDensity, normalFogDensity, t);
            targetColor = Color.Lerp(dystopiaFogColor, normalFogColor, t);
        }
        else
        {
            float t = Mathf.InverseLerp(0f, 1f, targetState);
            targetDensity = Mathf.Lerp(normalFogDensity, utopiaFogDensity, t);
            targetColor = Color.Lerp(normalFogColor, utopiaFogColor, t);
        }


        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float step = elapsed / duration;

            RenderSettings.fogDensity = Mathf.Lerp(startDensity, targetDensity, step);
            RenderSettings.fogColor = Color.Lerp(startColor, targetColor, step);
            
            yield return null;
        }


        RenderSettings.fogDensity = targetDensity;
        RenderSettings.fogColor = targetColor;
    }


    void CalcularYAplicarFog(float state)
    {
        if (state < 0f)
        {
            float t = Mathf.InverseLerp(-1f, 0f, state);
            RenderSettings.fogDensity = Mathf.Lerp(dystopiaFogDensity, normalFogDensity, t);
            RenderSettings.fogColor = Color.Lerp(dystopiaFogColor, normalFogColor, t);
        }
        else
        {
            float t = Mathf.InverseLerp(0f, 1f, state);
            RenderSettings.fogDensity = Mathf.Lerp(normalFogDensity, utopiaFogDensity, t);
            RenderSettings.fogColor = Color.Lerp(normalFogColor, utopiaFogColor, t);
        }
    }
}