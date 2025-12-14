using UnityEngine;
using System.Collections;

public class Mushroom : MonoBehaviour
{
    public bool isRed;  // Indica si el xampinyó és vermell o blau
    private WorldState worldState;  // Referència al script WorldState

    void Start()
    {
        // Obtenir referència al script WorldState
        worldState = FindObjectOfType<WorldState>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Comprovem si l'objecte que fa la col·lisió és el jugador
        {
            // Actualitzar la variable 'state' segons el tipus de xampinyó
            if (isRed)
            {
                worldState.UpdateState(-1);  // Restem 1 per xampinyó vermell
            }
            else
            {
                worldState.UpdateState(1);   // Sumem 1 per xampinyó blau
            }

            // Destruir el xampinyó un cop el jugador ha passat per sobre
            Destroy(gameObject);

            // Esperar uns segons abans d'instanciar un nou xampinyó
            StartCoroutine(RespawnMushroom());
        }
    }

    // Funció per generar un nou xampinyó després de cert temps
    IEnumerator RespawnMushroom()
    {
        yield return new WaitForSeconds(2f);  // Espera 2 segons
        Vector3 spawnPosition = new Vector3(Random.Range(-10f, 10f), 1f, Random.Range(-10f, 10f));  // Posició aleatòria
        if (isRed)
        {
            Instantiate(worldState.redMushroomPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Instantiate(worldState.blueMushroomPrefab, spawnPosition, Quaternion.identity);
        }
    }
}