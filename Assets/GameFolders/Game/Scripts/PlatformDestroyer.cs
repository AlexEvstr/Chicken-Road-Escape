using UnityEngine;

public class PlatformDestroyer : MonoBehaviour
{
    public GameObject destructionPoint;

    void Start()
    {
        destructionPoint = GameObject.Find("PlatformDestructionPoint");
    }

    void Update()
    {
        if(transform.position.x < destructionPoint.transform.position.x )
        {
            gameObject.SetActive(false);
        }
    }
}