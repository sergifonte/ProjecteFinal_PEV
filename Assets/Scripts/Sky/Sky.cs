using UnityEngine;
using System.Collections;

public class Sky : MonoBehaviour
{
    public Light sunLight;

    public Material dystopiaSkybox;
    public Material normalSkybox;
    public Material utopiaSkybox;

    public Color dystopiaSunColor = new Color(0.5f, 0.5f, 0.5f);
    public Color normalSunColor = Color.white;
    public Color utopiaSunColor = new Color(1f, 0.9f, 0.7f);

    public float dystopiaSunIntensity = 0.4f;
    public float normalSunIntensity = 1f;
    public float utopiaSunIntensity = 1.6f;

    public Color dystopiaAmbient = new Color(0.25f, 0.25f, 0.25f);
    public Color normalAmbient = new Color(0.6f, 0.6f, 0.6f);
    public Color utopiaAmbient = new Color(0.9f, 0.85f, 0.7f);

    public float transitionSpeed = 2f;

    private float currentState;
    private Coroutine skyCoroutine;

    private void OnEnable()
    {
        WorldState.OnWorldStateChanged += IniciarTransicionSky;
    }

    private void OnDisable()
    {
        WorldState.OnWorldStateChanged -= IniciarTransicionSky;
    }

    void Start()
    {
        if (WorldState.Instance != null)
        {
            currentState = WorldState.Instance.state;
            UpdateLighting(currentState);
        }
    }

    private void IniciarTransicionSky(float nuevoEstado)
    {
        if (skyCoroutine != null) StopCoroutine(skyCoroutine);
        skyCoroutine = StartCoroutine(TransicionSuaveSky(nuevoEstado));
    }

    IEnumerator TransicionSuaveSky(float targetState)
    {
       
        while (Mathf.Abs(currentState - targetState) > 0.001f)
        {
          
            currentState = Mathf.Lerp(currentState, targetState, Time.deltaTime * transitionSpeed);
            
            
            UpdateLighting(currentState);
            
            yield return null;
        }
        
        currentState = targetState;
        UpdateLighting(currentState);
    }

    void UpdateLighting(float state)
    {
        if (sunLight == null) return;

        if (state < 0)
        {
            float t = Mathf.InverseLerp(-1f, 0f, state);

            sunLight.color = Color.Lerp(dystopiaSunColor, normalSunColor, t);
            sunLight.intensity = Mathf.Lerp(dystopiaSunIntensity, normalSunIntensity, t);
            RenderSettings.ambientLight = Color.Lerp(dystopiaAmbient, normalAmbient, t);

            if (RenderSettings.skybox != null)
                RenderSettings.skybox.Lerp(dystopiaSkybox, normalSkybox, t);
        }
        else
        {
            float t = Mathf.InverseLerp(0f, 1f, state);

            sunLight.color = Color.Lerp(normalSunColor, utopiaSunColor, t);
            sunLight.intensity = Mathf.Lerp(normalSunIntensity, utopiaSunIntensity, t);
            RenderSettings.ambientLight = Color.Lerp(normalAmbient, utopiaAmbient, t);

            if (RenderSettings.skybox != null)
                RenderSettings.skybox.Lerp(normalSkybox, utopiaSkybox, t);
        }

       
        DynamicGI.UpdateEnvironment();
    }
}