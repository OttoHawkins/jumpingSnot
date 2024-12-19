using UnityEngine;

public class PlayerWrap : MonoBehaviour
{
    private float screenLeftBound;
    private float screenRightBound; 

    public float playerWidth = 0.5f;

    void Start()
    {
        screenLeftBound = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x - playerWidth;
        screenRightBound = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x + playerWidth;
    }

    void Update()
    {
        if (transform.position.x < screenLeftBound)
        {
            transform.position = new Vector3(screenRightBound, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > screenRightBound)
        {
            transform.position = new Vector3(screenLeftBound, transform.position.y, transform.position.z);
        }
    }
}
