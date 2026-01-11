using UnityEngine;
using System.Collections.Generic;

public class ProgressionBlocker : MonoBehaviour
{
    public List<GameObject> rocasDeBloqueo; 
    public float intervalo = -0.2f; 
    public float masaFijaBloqueo = 500f; // <--- Nueva variable

    void Update()
    {
        if (WorldState.Instance == null) return;

        float estadoActual = WorldState.Instance.state;

        for (int i = 0; i < rocasDeBloqueo.Count; i++)
        {
            float umbralParaEstaRoca = (i + 1) * intervalo;

            if (estadoActual <= umbralParaEstaRoca)
            {
                if (!rocasDeBloqueo[i].activeSelf)
                {
                    rocasDeBloqueo[i].SetActive(true);
                    
                    // Al activarse, buscamos su Rigidbody y le ponemos la masa de 500
                    Rigidbody rb = rocasDeBloqueo[i].GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.mass = masaFijaBloqueo;
                    }
                }
            }
            else
            {
                if (rocasDeBloqueo[i].activeSelf)
                {
                    rocasDeBloqueo[i].SetActive(false);
                }
            }
        }
    }
}