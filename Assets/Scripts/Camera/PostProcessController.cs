using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessController : MonoBehaviour
{
    private Volume volum;
    private ColorAdjustments saturacio;
    private ChromaticAberration distorsio;
    private Vignette ombresCantonades;

    [Header("DISTÒPIA")]
    public float saturacioDystopia = -20f;
    public float distorsioDystopia = 0.5f;
    public float ombresDystopia = 0.4f;

    [Header("UTÒPIA")]
    public float saturacioUtopia = 50f;
    public float ombresUtopia = 0.05f;

    [Header("NORMAL")]
    public float saturacioNormal = 0f;
    public float ombresNormal = 0.2f;

    void Awake()
    {
        volum = GetComponent<Volume>();
        volum.profile.TryGet(out saturacio);
        volum.profile.TryGet(out distorsio);
        volum.profile.TryGet(out ombresCantonades);
    }

    private void OnEnable()
    {
        WorldState.OnWorldStateChanged += CanviarEfectes;
        if (WorldState.Instance != null) CanviarEfectes(WorldState.Instance.state);
    }

    private void OnDisable()
    {
        WorldState.OnWorldStateChanged -= CanviarEfectes;
    }

    private void CanviarEfectes(float estatActual)
    {
        if (estatActual < 0)
        {
            float t = Mathf.InverseLerp(0f, -1f, estatActual);

            saturacio.saturation.value = Mathf.Lerp(saturacioNormal, saturacioDystopia, t);
            distorsio.intensity.value = Mathf.Lerp(0f, distorsioDystopia, t);
            ombresCantonades.intensity.value = Mathf.Lerp(ombresNormal, ombresDystopia, t);
        }
        else
        {
            float t = Mathf.InverseLerp(0f, 1f, estatActual);

            saturacio.saturation.value = Mathf.Lerp(saturacioNormal, saturacioUtopia, t);
            distorsio.intensity.value = 0f; 
            ombresCantonades.intensity.value = Mathf.Lerp(ombresNormal, ombresUtopia, t);
        }
    }
}