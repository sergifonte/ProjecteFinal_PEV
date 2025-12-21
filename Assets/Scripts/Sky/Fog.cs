using UnityEngine;

public class Fog : MonoBehaviour
{
    public Color dystopiaFogColor = new Color(0.86f, 0.99f, 1.18f);
    public Color normalFogColor = Color.clear;
    public Color utopiaFogColor = new Color(1f, 0.9f, 0.7f);


    public float dystopiaFogDensity = 0.04f;
    public float normalFogDensity = 0f;
    public float utopiaFogDensity = 0.01f;


    
    public float transition = 2f;

    private float currentFogDensity;
    private Color currentFogColor;

    void Start()
    {
        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.Exponential;

        ApplyImmediateFog();
    }

    void Update()
    {
        if (WorldState.Instance == null)
            return;

        float state = WorldState.Instance.state;

        if (state < 0f)
        {
            float t = Mathf.InverseLerp(-1f, 0f, state);

            currentFogDensity = Mathf.Lerp(
                dystopiaFogDensity,
                normalFogDensity,
                t
            );

            currentFogColor = Color.Lerp(
                dystopiaFogColor,
                normalFogColor,
                t
            );
        }
        else
        {
            float t = Mathf.InverseLerp(0f, 1f, state);

            currentFogDensity = Mathf.Lerp(
                normalFogDensity,
                utopiaFogDensity,
                t
            );

            currentFogColor = Color.Lerp(
                normalFogColor,
                utopiaFogColor,
                t
            );
        }

        RenderSettings.fogDensity = Mathf.Lerp(
            RenderSettings.fogDensity,
            currentFogDensity,
            Time.deltaTime * transition
        );

        RenderSettings.fogColor = Color.Lerp(
            RenderSettings.fogColor,
            currentFogColor,
            Time.deltaTime * transition
        );
    }


    void ApplyImmediateFog()
    {
        if (WorldState.Instance == null)
            return;

        float state = WorldState.Instance.state;

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
