using UnityEngine;
using System.Collections.Generic;

public class ProgressionBlocker : MonoBehaviour
{
    public List<GameObject> rocasDeBloqueo; 
    public float intervalo = -0.2f; 
    public float masaFijaBloqueo = 500f;

    private void OnEnable()
    {
        WorldState.OnWorldStateChanged += ActualizarRocas;
    }

    private void OnDisable()
    {
        WorldState.OnWorldStateChanged -= ActualizarRocas;
    }

    void Start()
    {
        if (WorldState.Instance != null)
        {
            ActualizarRocas(WorldState.Instance.state);
        }
    }


    private void ActualizarRocas(float estadoActual)
    {
        for (int i = 0; i < rocasDeBloqueo.Count; i++)
        {
            if (rocasDeBloqueo[i] == null) continue;

            float umbralParaEstaRoca = (i + 1) * intervalo;

            
            if (estadoActual <= umbralParaEstaRoca)
            {
                if (!rocasDeBloqueo[i].activeSelf)
                {
                    rocasDeBloqueo[i].SetActive(true);
                    
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