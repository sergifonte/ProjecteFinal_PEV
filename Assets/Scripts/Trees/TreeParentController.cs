using UnityEngine;

public class TreeParentController : MonoBehaviour
{
    public GameObject trunk;
    public GameObject tree;

    private void OnEnable()
    {
        WorldState.OnWorldStateChanged += ActualizarVisibilidadArbol;
    }

    private void OnDisable()
    {
        WorldState.OnWorldStateChanged -= ActualizarVisibilidadArbol;
    }

    void Start()
    {
        if (WorldState.Instance != null)
        {
            ActualizarVisibilidadArbol(WorldState.Instance.state);
        }
    }

    private void ActualizarVisibilidadArbol(float state)
    {
        if (tree == null) return;

        bool deberiaEstarActivo = state > -0.8f;

        if (tree.activeSelf != deberiaEstarActivo)
        {
            tree.SetActive(deberiaEstarActivo);
        }
    }
}