using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public float stateValue; // +0.2f blau, -0.2f vermell
    private MushroomSpawner _spawner;

    void Start()
    {
        // Troba el MushroomSpawner
        _spawner = FindObjectOfType<MushroomSpawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WorldState.Instance.AddState(stateValue);
            Destroy(gameObject);
            
            // Respawn del xampinyó a una nova posició
            if (gameObject.CompareTag("Blue"))
            {
                _spawner.RespawnMushroom(_spawner.blueMushroomPrefab, _spawner.bluePositions, _spawner.activeBlueMushrooms);
            }
            else if (gameObject.CompareTag("Red"))
            {
                _spawner.RespawnMushroom(_spawner.redMushroomPrefab, _spawner.redPositions, _spawner.activeRedMushrooms);
            }
        }
    }
}