using UnityEngine;

public class RockState : MonoBehaviour
{
    public Renderer rendererNormal;
    public Renderer rendererMusgo;
    private Rigidbody rb;

    [Header("Configuracion Fisica")]
    public float masaDistopia = 500f; // No se puede empujar
    public float masaUtopia = 2f;    // Muy ligera

    public float transitionSpeed = 2f;
    private float currentState;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Esto evita que atraviese el suelo
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    void Update()
    {
        if (WorldState.Instance == null) return;

        // Suavizamos el valor del estado global
        currentState = Mathf.Lerp(currentState, WorldState.Instance.state, Time.deltaTime * transitionSpeed);

        // 1. FISICA: Cambiar la masa
        float t = Mathf.InverseLerp(-1f, 1f, currentState);
        rb.mass = Mathf.Lerp(masaDistopia, masaUtopia, t);

        // 2. VISUAL: Cambio gradual de texturas (Alpha)
        // En el centro (0), ambas son opacas (1f) para que no sea transparente
        float alphaNormal = currentState >= 0 ? 1f : Mathf.InverseLerp(-0.8f, 0f, currentState);
        float alphaMusgo = currentState <= 0 ? 1f : Mathf.InverseLerp(0f, 0.8f, currentState);

        ActualizarMaterial(rendererNormal, alphaNormal);
        ActualizarMaterial(rendererMusgo, alphaMusgo);
    }

    void ActualizarMaterial(Renderer rend, float alpha)
    {
        if (rend == null) return;
        rend.enabled = (alpha > 0.01f); // Apagar si es invisible
        Color c = rend.material.color;
        c.a = alpha;
        rend.material.color = c;
    }

    // Permitir que el CharacterController del jugador empuje la roca
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic) return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        // Si la masa es 500, la velocidad resultante será casi 0
        body.linearVelocity = pushDir * (2.0f / body.mass);
    }
}