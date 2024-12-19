using UnityEngine;

public class PulsatingPlatform : MonoBehaviour
{
    public float visibleTime = 2f;   
    public float invisibleTime = 2f; 

    private bool isVisible = true;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (isVisible && timer > visibleTime)
        {
            SetPlatformActive(false);
            isVisible = false;
            timer = 0f;
        }
        else if (!isVisible && timer > invisibleTime)
        {
            SetPlatformActive(true);
            isVisible = true;
            timer = 0f;
        }
    }

    void SetPlatformActive(bool active)
    {
        GetComponent<Renderer>().enabled = active;
        GetComponent<Collider2D>().enabled = active;
    }
}
