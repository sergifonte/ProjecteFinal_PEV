using UnityEngine;

public class worldState : MonoBehaviour
{
    [SerializeField]
    private float state = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeState()
    {
        if (Input.GetKeyDown(KeyCode.Plus))
        {
            state += 0.1f;
            if(state > 1f)
            {
                state = 1f;
            }
        }
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            state -= 0.1f;
            if (state < -1f)
            {
                state = -1f;
            }
        }



    }
}
