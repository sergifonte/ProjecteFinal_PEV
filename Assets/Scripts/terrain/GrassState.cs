using UnityEngine;
using System.Collections;

public class GrassState : MonoBehaviour
{
    public Material terrainMaterial;

    [Header("Colores de la hierba")]
    public Color dystopiaColor = new Color(0.6f, 0.6f, 0.2f);
    public Color normalColor = new Color(0.376f, 0.545f, 0.29f);
    public Color utopiaColor = new Color(0.2f, 1f, 0.3f);

    [Header("Colores de partículas")]

    public Color magicColor = new Color(1f, 0.85f, 0.3f, 1f); 
    public Color pollutionColor = new Color(0.15f, 0.15f, 0.15f, 1f); 

    [Header("Ajustes")]
    public float transitionSpeed = 2f;
    public float umbralUtopia = 0.6f;
    public float umbralDistopia = -0.6f;


    private float currentState;
    private Coroutine transitionCoroutine;
    private ParticleSystem[] magicParticles;

    void Awake()
    {
        magicParticles = GetComponentsInChildren<ParticleSystem>();
    }

    private void OnEnable()
    {
        WorldState.OnWorldStateChanged += IniciarTransicionGrass;
    }

    private void OnDisable()
    {
        WorldState.OnWorldStateChanged -= IniciarTransicionGrass;
    }

    void Start()
    {
        if (WorldState.Instance != null)
        {
            currentState = WorldState.Instance.state;
            ActualizarTodo(currentState);
        }
    }

    private void IniciarTransicionGrass(float nuevoEstado)
    {
        if (transitionCoroutine != null) StopCoroutine(transitionCoroutine);
        transitionCoroutine = StartCoroutine(TransicionSuave(nuevoEstado));
    }

    IEnumerator TransicionSuave(float targetState)
    {
        while (Mathf.Abs(currentState - targetState) > 0.001f)
        {
            currentState = Mathf.Lerp(currentState, targetState, Time.deltaTime * transitionSpeed);
            ActualizarTodo(currentState);
            yield return null;
        }
        currentState = targetState;
        ActualizarTodo(currentState);
    }

    void ActualizarTodo(float state)
    {
        if (state < 0)
        {
            float t = Mathf.InverseLerp(-1, 0, state);
            terrainMaterial.color = Color.Lerp(dystopiaColor, normalColor, t);
        }
        else
        {
            float t = Mathf.InverseLerp(0, 1, state);
            terrainMaterial.color = Color.Lerp(normalColor, utopiaColor, t);
        }

        if (state <= umbralDistopia) 
        {
        
            SetParticlesState(true, pollutionColor);
        }
        else if (state >= umbralUtopia) 
        {

            SetParticlesState(true, magicColor);
        }
        else 
        {
           
            SetParticlesState(false, Color.white); 
        }
    }

    void SetParticlesState(bool active, Color colorActual)
    {
        foreach (var ps in magicParticles)
        {
            if (ps == null) continue;

            if (active)
            {
               
                var main = ps.main;
                main.startColor = colorActual;

                if (!ps.isPlaying) ps.Play();
            }
            else
            {
                if (ps.isPlaying) ps.Stop();
            }
        }
    }
}