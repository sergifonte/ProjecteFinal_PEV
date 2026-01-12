using UnityEngine;
using System.Collections;

public class CameraFOV : MonoBehaviour
{
    private Camera _cam;
    public float utopiaFOV = 80f;
    public float normalFOV = 60f;
    public float dystopiaFOV = 40f;
    public float transitionSpeed = 2f;

    
    private Coroutine fovCoroutine;

    void Awake()
    {
        _cam = GetComponent<Camera>();
    }

    private void OnEnable()
    {
        WorldState.OnWorldStateChanged += IniciarCambioFOV;
        // Sincronización inicial
        if (WorldState.Instance != null) IniciarCambioFOV(WorldState.Instance.state);
    }

    private void OnDisable()
    {
        WorldState.OnWorldStateChanged -= IniciarCambioFOV;
    }

    private void IniciarCambioFOV(float nuevoEstado)
    {
        if (fovCoroutine != null) StopCoroutine(fovCoroutine);
        fovCoroutine = StartCoroutine(TransicionFOV(nuevoEstado));
    }

    IEnumerator TransicionFOV(float s)
    {
        float targetFOV;
        if (s > 0) 
            targetFOV = Mathf.Lerp(normalFOV, utopiaFOV, s);
        else 
            targetFOV = Mathf.Lerp(normalFOV, dystopiaFOV, -s);

        while (Mathf.Abs(_cam.fieldOfView - targetFOV) > 0.01f)
        {
            _cam.fieldOfView = Mathf.Lerp(_cam.fieldOfView, targetFOV, Time.deltaTime * transitionSpeed);
            yield return null;
        }

        _cam.fieldOfView = targetFOV;
    }

    
}