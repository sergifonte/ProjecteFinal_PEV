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
        SpawnMushrooms(blueMushroomPrefab, bluePositions, activeBlueMushrooms);
        SpawnMushrooms(redMushroomPrefab, redPositions, activeRedMushrooms);
    }

    void SpawnMushrooms(GameObject mushroomPrefab, Transform[] positions, List<GameObject> activeMushrooms)
    {
        int amount = Mathf.Min(maxMushrooms, positions.Length);

        for (int i = 0; i < amount; i++)
        {
            int posIndex = Random.Range(0, positions.Length);
            GameObject newMushroom = Instantiate(mushroomPrefab, positions[posIndex].position, Quaternion.identity);
            activeMushrooms.Add(newMushroom);
        }
    }

    public void RespawnMushroom(GameObject mushroomPrefab, Transform[] positions, List<GameObject> activeMushrooms)
    {
        if (activeMushrooms.Count >= maxMushrooms)
        {
            Destroy(activeMushrooms[0]);
            activeMushrooms.RemoveAt(0);
        }

        int posIndex = Random.Range(0, positions.Length);
        GameObject newMushroom = Instantiate(mushroomPrefab, positions[posIndex].position, Quaternion.identity);
        activeMushrooms.Add(newMushroom);
    }
}