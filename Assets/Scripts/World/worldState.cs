using UnityEngine;

public class WorldState : MonoBehaviour
{
    public static WorldState Instance;

    [Range(-1f, 1f)]
    public float state = 0f;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddState(float value)
    {
        state += value;
        state = Mathf.Clamp(state, -1f, 1f);

        Debug.Log("World State updated: " + state);
    }
}
