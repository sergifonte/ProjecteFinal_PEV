using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public float stateValue;
    private MushroomSpawner _spawner;

    void Start()
    {
        _spawner = FindObjectOfType<MushroomSpawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WorldState.Instance.AddState(stateValue);
            
            Debug.Log("State changed by " + stateValue);
            
            Destroy(gameObject);

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