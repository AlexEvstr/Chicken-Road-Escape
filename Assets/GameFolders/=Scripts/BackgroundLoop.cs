using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    private float backgroundWidth;
    public Transform cameraTransform;

    void Start()
    {
        backgroundWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        if (transform.position.x < cameraTransform.position.x - backgroundWidth)
        {
            transform.position += Vector3.right * backgroundWidth * 2;
        }
    }
}