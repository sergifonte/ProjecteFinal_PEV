using UnityEngine;

public class AmbientAudioController : MonoBehaviour
{
    public AudioSource audioUtopia;
    public AudioSource audioDistopia;

    [Range(1f, 5f)] 
    public float suavizadoDistopia = 3f; 

    private void OnEnable()
    {
        WorldState.OnWorldStateChanged += ActualizarAudio;
        
        if (WorldState.Instance != null) ActualizarAudio(WorldState.Instance.state);
    }

    private void OnDisable()
    {

        WorldState.OnWorldStateChanged -= ActualizarAudio;
    }

    private void ActualizarAudio(float estado)
    {

        if (audioUtopia == null || audioDistopia == null) return;

        float tDist = Mathf.InverseLerp(0f, -1f, estado);
        audioDistopia.volume = Mathf.Pow(tDist, suavizadoDistopia);

        float tUtop = Mathf.InverseLerp(-0.8f, 1f, estado);
        audioUtopia.volume = tUtop; 
    }
}