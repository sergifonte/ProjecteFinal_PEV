using UnityEngine;

public class TreeParentController : MonoBehaviour
{
    public GameObject trunk;
    public GameObject tree;

    void Update()
    {
        if (WorldState.Instance == null) return;

        float state = WorldState.Instance.state;

        // Si el state és molt baix, només tronc visible
        if (state <= -0.8f)
        {
            if (tree != null)
                tree.SetActive(false);
        }
        else
        {
            if (tree != null)
                tree.SetActive(true);
        }
    }
}
