using UnityEngine;

public class RockState : MonoBehaviour
{
    public Renderer rendererNormal;
    public Renderer rendererMusgo;
    private Rigidbody rb;

    [Header("Configuracion Fisica")]
    public float masaDistopia = 500f;
    public float masaUtopia = 2f;
    public float transitionSpeed = 5f; // Una mica més ràpid perquè es noti
    private float currentState;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        // Forçem que els materials siguin únics per a cada roca
        if(rendererNormal) rendererNormal.material = new Material(rendererNormal.material);
        if(rendererMusgo) rendererMusgo.material = new Material(rendererMusgo.material);
    }

    void Update()
    {
        if (WorldState.Instance == null) return;

        // Llegim el valor directament del WorldState
        currentState = Mathf.Lerp(currentState, WorldState.Instance.state, Time.deltaTime * transitionSpeed);

        // 1. FISICA
        float t = Mathf.InverseLerp(-1f, 1f, currentState);
        rb.mass = Mathf.Lerp(masaDistopia, masaUtopia, t);

        // 2. VISUAL (Alpha)
        // Utilitzem una corba simple: 
        // Normal: invisible a -1, visible a 0 i 1
        // Musgo: visible a -1, invisible a 0 i 1
        float alphaNormal = Mathf.Clamp01(currentState + 1f); 
        float alphaMusgo = Mathf.Clamp01(1f - (currentState + 1f));

        // En el punt 0 (normal), fem que les dues siguin visibles per seguretat
        if (currentState > -0.1f && currentState < 0.1f) {
            alphaNormal = 1f;
            alphaMusgo = 0f; // O 1 si vols que es barregin
        }

        SetAlpha(rendererNormal, alphaNormal);
        SetAlpha(rendererMusgo, alphaMusgo);
    }

    void SetAlpha(Renderer r, float a)
    {
        if (r == null) return;
        r.enabled = (a > 0.05f); // Si és molt transparent, l'apaguem
        
        // Accedim al color del material instanciat
        Color c = r.material.color;
        c.a = a;
        r.material.color = c;
    }
}