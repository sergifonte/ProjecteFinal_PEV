using UnityEngine;
using System.Collections;

public class WorldState : MonoBehaviour
{
    public int state = 0;  // Variable de control de l'estat
    public GameObject[] redMushrooms;  // Array per xampinyons vermells
    public GameObject[] blueMushrooms; // Array per xampinyons blaus
    public GameObject redMushroomPrefab;  // Prefab xampinyó vermell
    public GameObject blueMushroomPrefab; // Prefab xampinyó blau
    public int maxRedMushrooms = 3; // Nombre màxim de xampinyons vermells
    public int maxBlueMushrooms = 3; // Nombre màxim de xampinyons blaus
    private Vector3[] redPositions; // Posicions per xampinyons vermells
    private Vector3[] bluePositions; // Posicions per xampinyons blaus

    void Start()
    {
        // Definir les posicions de generació per xampinyons vermells i blaus
        redPositions = new Vector3[maxRedMushrooms];
        bluePositions = new Vector3[maxBlueMushrooms];

        // Generar les posicions aleatòries per xampinyons vermells
        for (int i = 0; i < maxRedMushrooms; i++)
        {
            redPositions[i] = new Vector3(Random.Range(-10f, 10f), 1f, Random.Range(-10f, 10f));
        }

        // Generar les posicions aleatòries per xampinyons blaus
        for (int i = 0; i < maxBlueMushrooms; i++)
        {
            bluePositions[i] = new Vector3(Random.Range(-10f, 10f), 1f, Random.Range(-10f, 10f));
        }

        // Crear els xampinyons vermells i blaus
        SpawnMushrooms();
    }

    void SpawnMushrooms()
    {
        // Crear els xampinyons vermells
        for (int i = 0; i < maxRedMushrooms; i++)
        {
            Instantiate(redMushroomPrefab, redPositions[i], Quaternion.identity);
        }

        // Crear els xampinyons blaus
        for (int i = 0; i < maxBlueMushrooms; i++)
        {
            Instantiate(blueMushroomPrefab, bluePositions[i], Quaternion.identity);
        }
    }

    public void UpdateState(int change)
    {
        state += change;
        Debug.Log("State actualitzat: " + state);
    }
}