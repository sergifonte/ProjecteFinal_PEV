using UnityEngine;

public class CameraEffects : MonoBehaviour
{
    public Camera _cam;
    public float utopiaFOV = 75f;
    public float normalFOV = 60f;
    public float dystopiaFOV = 45f;
    public float transitionSpeed = 2f;

    void Start() => _cam = GetComponent<Camera>();

    void Update()
    {
        if (WorldState.Instance == null) return;

        float targetFOV;
        float s = WorldState.Instance.state;

        if (s > 0) 
            targetFOV = Mathf.Lerp(normalFOV, utopiaFOV, s);
        else 
            targetFOV = Mathf.Lerp(normalFOV, dystopiaFOV, -s);

        _cam.fieldOfView = Mathf.Lerp(_cam.fieldOfView, targetFOV, Time.deltaTime * transitionSpeed);
    }
}
