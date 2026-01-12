using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WorldStateUI : MonoBehaviour
{
    public Slider worldSlider;
    public TextMeshProUGUI statusText;

    private void OnEnable()
    {
        WorldState.OnWorldStateChanged += ActualizarInterfaz;
    }

    private void OnDisable()
    {
        WorldState.OnWorldStateChanged -= ActualizarInterfaz;
    }

    void Start()
    {
        if (WorldState.Instance != null)
        {
            ActualizarInterfaz(WorldState.Instance.state);
        }
    }

    private void ActualizarInterfaz(float estadoActual)
    {
        if (worldSlider != null)
        {
            worldSlider.value = estadoActual;
        }

        if (statusText != null)
        {
            if (estadoActual < -0.1f)
            {
                statusText.text = "Distopía";
                statusText.color = Color.red;
            }
            else if (estadoActual > 0.1f)
            {
                statusText.text = "Utopía";
                statusText.color = Color.green;
            }
            else
            {
                statusText.text = "Neutral";
                statusText.color = Color.white;
            }
        }
    }
}