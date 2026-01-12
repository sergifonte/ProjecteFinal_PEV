using UnityEngine;
using System.Collections;

public class RockState : MonoBehaviour
{
    public Renderer rendererNormal;
    public Renderer rendererMusgo;

    [Header("Ajustes de Transición")]
    public float transitionSpeed = 5f; 
    
    private float currentState = 0f;
    private Coroutine transitionCoroutine;

    void Awake()
    {
        if(rendererNormal) rendererNormal.material = new Material(rendererNormal.material);
        if(rendererMusgo) rendererMusgo.material = new Material(rendererMusgo.material);
    }

    private void OnEnable()
    {
        WorldState.OnWorldStateChanged += IniciarTransicion;
    }

    private void OnDisable()
    {
        WorldState.OnWorldStateChanged -= IniciarTransicion;
    }

    void Start()
    {
        if (WorldState.Instance != null)
        {
            ActualizarVisuales(WorldState.Instance.state);
        }
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
            ActualizarVisuales(currentState);
            yield return null;
        }
        currentState = targetState;
        ActualizarVisuales(currentState);
    }

    private void ActualizarVisuales(float estado)
    {
        float alphaNormal;
        float alphaMusgo;

        if (estado >= 0) 
        {
            alphaNormal = 1f; 
            alphaMusgo = 0f;  
        }
        else 
        {
            alphaMusgo = Mathf.Abs(estado); 
            alphaNormal = 1f - alphaMusgo;
        }

        SetAlpha(rendererNormal, alphaNormal);
        SetAlpha(rendererMusgo, alphaMusgo);
    }

    void SetAlpha(Renderer r, float a)
    {
        if (r == null) return;

       
        r.enabled = (a > 0.01f); 
        
        if (r.enabled)
        {
            Color c = r.material.color;
            c.a = a;
            r.material.color = c;
        }
    }
}