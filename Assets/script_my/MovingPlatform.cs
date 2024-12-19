using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveSpeed = 2f;     
    public float moveRange = 2f;     

    private Vector3 startPosition;
    private bool movingRight = true;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (movingRight)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            if (transform.position.x > startPosition.x + moveRange)
                movingRight = false;
        }
        else
        {
            transform.position -= Vector3.right * moveSpeed * Time.deltaTime;
            if (transform.position.x < startPosition.x - moveRange)
                movingRight = true;
        }
    }
}
