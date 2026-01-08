using UnityEngine;

public class TreeMaterialController : MonoBehaviour
{
    private Renderer _renderer;
    
    [Header("Textures")]
    public Texture2D darkTexture;   // La de -0.8
    public Texture2D normalTexture; // La de 0
    public Texture2D colorTexture;  // La de 1

    public float transitionSpeed = 2f;
    private float _visualState;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        // Això crea una còpia del material perquè cada arbre pugui ser únic
        if (_renderer != null)
            _visualState = WorldState.Instance.state;
    }

    void Update()
    {
        if (WorldState.Instance == null || _renderer == null) return;

        // Suavitzem el moviment de la variable
        _visualState = Mathf.Lerp(_visualState, WorldState.Instance.state, Time.deltaTime * transitionSpeed);

        // Lògica de canvi de textures i progrés
        if (_visualState < 0)
        {
            // Entre fosc i normal
            float t = Mathf.InverseLerp(-0.8f, 0f, _visualState);
            _renderer.material.SetTexture("_Texture1", darkTexture);
            _renderer.material.SetTexture("_Texture2", normalTexture);
            _renderer.material.SetFloat("_Progress", t);
        }
        else
        {
            // Entre normal i colors
            float t = Mathf.InverseLerp(0f, 1f, _visualState);
            _renderer.material.SetTexture("_Texture1", normalTexture);
            _renderer.material.SetTexture("_Texture2", colorTexture);
            _renderer.material.SetFloat("_Progress", t);
        }
    }
}