using UnityEngine;

public class Fog : MonoBehaviour
{
    public Color dystopiaFogColor = new Color(0.86f, 0.99f, 1.18f);
    public Color normalFogColor = Color.clear;

    
    public float dystopiaFogDensity = 0.04f;
    public float normalFogDensity = 0f;

    
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
            currentFogDensity = Mathf.Lerp(
                currentFogDensity,
                normalFogDensity,
                Time.deltaTime * transition
            );

            currentFogColor = Color.Lerp(
                currentFogColor,
                normalFogColor,
                Time.deltaTime * transition
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
            RenderSettings.fogDensity = normalFogDensity;
            RenderSettings.fogColor = normalFogColor;
        }
    }
}
