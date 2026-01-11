
using UnityEngine;

public class GrassState : MonoBehaviour
{
    public Material terrainMaterial;

    public Color dystopiaColor = new Color(0.6f, 0.6f, 0.2f);
    public Color normalColor = new Color(0.376f, 0.545f, 0.29f);
    public Color utopiaColor = new Color(0.2f, 1f, 0.3f);

    public ParticleSystem magicParticles;

    public float transitionSpeed = 2f;
    float currentState;

    void Update()
    {
        currentState = Mathf.Lerp(currentState, WorldState.Instance.state, Time.deltaTime * transitionSpeed);
        UpdateGrass(currentState);
        Debug.Log(WorldState.Instance.state);
    }

    void UpdateGrass(float state)
    {
        if (state < 0)
        {
            float t = Mathf.InverseLerp(-1, 0, state);
            terrainMaterial.color = Color.Lerp(dystopiaColor, normalColor, t);
            magicParticles.gameObject.SetActive(false);
        }
        else
        {
            float t = Mathf.InverseLerp(0, 1, state);
            terrainMaterial.color = Color.Lerp(normalColor, utopiaColor, t);
            magicParticles.gameObject.SetActive(state > 0.7f);
        }
    }
}
