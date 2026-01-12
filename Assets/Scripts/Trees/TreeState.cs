using UnityEngine;

public class TreeMaterialController : MonoBehaviour
{
    private Renderer _renderer;
    
    [Header("Textures")]
    public Texture2D darkTexture;   
    public Texture2D normalTexture; 
    public Texture2D colorTexture;  

    public float transitionSpeed = 2f;
    private float _visualState;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
    
        if (_renderer != null)
            _visualState = WorldState.Instance.state;
    }

    void Update()
    {
        if (WorldState.Instance == null || _renderer == null) return;

        _visualState = Mathf.Lerp(_visualState, WorldState.Instance.state, Time.deltaTime * transitionSpeed);

        if (_visualState < 0)
        {
            float t = Mathf.InverseLerp(-0.8f, 0f, _visualState);
            _renderer.material.SetTexture("_Texture1", darkTexture);
            _renderer.material.SetTexture("_Texture2", normalTexture);
            _renderer.material.SetFloat("_Progress", t);
        }
        else
        {
            float t = Mathf.InverseLerp(0f, 1f, _visualState);
            _renderer.material.SetTexture("_Texture1", normalTexture);
            _renderer.material.SetTexture("_Texture2", colorTexture);
            _renderer.material.SetFloat("_Progress", t);
        }
    }
}