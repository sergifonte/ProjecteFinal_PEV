using UnityEngine;

public class ObjectScaleController : MonoBehaviour
{
    [Header("Escales (Mides)")]
    public Vector3 midaDistopia = new Vector3(2f, 2f, 2f);
    public Vector3 midaNormal = new Vector3(1.5f, 1.5f, 1.5f);
    public Vector3 midaUtopia = new Vector3(1f, 1f, 1f);

    [Header("Ajustaments")]
    public float velocitatCanvi = 5f;
    private Vector3 midaObjectiu;

    void Start()
    {
        if (WorldState.Instance != null)
        {
            ActualitzarMida(WorldState.Instance.state);
            transform.localScale = midaObjectiu;
        }
    }

    private void OnEnable()
    {
        WorldState.OnWorldStateChanged += ActualitzarMida;
    }

    private void OnDisable()
    {
        WorldState.OnWorldStateChanged -= ActualitzarMida;
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, midaObjectiu, Time.deltaTime * velocitatCanvi);
    }

    private void ActualitzarMida(float estatActual)
    {
        if (estatActual < 0)
        {
            float t = Mathf.InverseLerp(0f, -1f, estatActual);
            midaObjectiu = Vector3.Lerp(midaNormal, midaDistopia, t);
        }
        else
        {
            float t = Mathf.InverseLerp(0f, 1f, estatActual);
            midaObjectiu = Vector3.Lerp(midaNormal, midaUtopia, t);
        }
    }
}