using UnityEngine;
using UnityEngine.UI; // Importante para usar el Slider
using TMPro; // Importante para usar TextMeshPro

public class WorldStateUI : MonoBehaviour
{
    public Slider worldSlider;
    public TextMeshProUGUI statusText;

    void Update()
    {
        if (WorldState.Instance == null) return;

        float currentState = WorldState.Instance.state;
        worldSlider.value = currentState;

        if (currentState < -0.1f)
        {
            statusText.text = "Distopía";
        }
        else if (currentState > 0.1f)
        {
            statusText.text = "Utopía";
        }
        else
        {
            statusText.text = "Neutral";

        }
    }
}