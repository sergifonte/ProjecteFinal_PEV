using UnityEngine;
using System.Collections.Generic;

public class MushroomSpawner : MonoBehaviour
{
    public GameObject blueMushroomPrefab;
    public GameObject redMushroomPrefab;

    public Transform[] bluePositions;
    public Transform[] redPositions;

    public int maxMushrooms = 3;

    public List<GameObject> activeBlueMushrooms = new List<GameObject>();
    public List<GameObject> activeRedMushrooms = new List<GameObject>();

    void Start()
    {
        InitializeMushrooms(blueMushroomPrefab, bluePositions, activeBlueMushrooms);
        InitializeMushrooms(redMushroomPrefab, redPositions, activeRedMushrooms);
    }

    void InitializeMushrooms(GameObject mushroomPrefab, Transform[] positions, List<GameObject> activeMushrooms)
    {
        int amount = Mathf.Min(maxMushrooms, positions.Length);

        for (int i = 0; i < amount; i++)
        {
            int posIndex = Random.Range(0, positions.Length); 
            if (!IsPositionOccupied(positions[posIndex].position))
            {
                GameObject newMushroom = Instantiate(mushroomPrefab, positions[posIndex].position, Quaternion.identity);
                activeMushrooms.Add(newMushroom);
            }
            else
            {
                i--;
            }
        }
    }

    public void RespawnMushroom(GameObject mushroomPrefab, Transform[] positions, List<GameObject> activeMushrooms)
    {
        bool respawned = false;
        while (!respawned)
        {
            int posIndex = Random.Range(0, positions.Length);
            if (!IsPositionOccupied(positions[posIndex].position))
            {
                GameObject newMushroom = Instantiate(mushroomPrefab, positions[posIndex].position, Quaternion.identity);
                activeMushrooms.Add(newMushroom);
                respawned = true;
            }
        }

        Debug.Log("Active Mushrooms: " + activeMushrooms.Count);
    }

    bool IsPositionOccupied(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, 0.5f);
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Blue") || col.CompareTag("Red"))
            {
                return true;
            }
        }
        return false;
    }
}