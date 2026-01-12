using UnityEngine;

public class AmbientAudioController : MonoBehaviour
{
    public AudioSource audioUtopia;
    public AudioSource audioDistopia;

    [Range(1f, 5f)] 
    public float suavizadoDistopia = 3f; 

    void Update()
    {
        if (WorldState.Instance == null) return;

        float estado = WorldState.Instance.state; 

        
        float tDist = Mathf.InverseLerp(0f, -1f, estado);
        
        audioDistopia.volume = Mathf.Pow(tDist, suavizadoDistopia);

        float tUtop = Mathf.InverseLerp(-0.8f, 1f, estado);
        audioUtopia.volume = tUtop; 
    }
}