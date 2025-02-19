using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player;
    public Vector3 offset;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
    }

    void Update()
    {
        transform.position = new Vector3(player.position.x + offset.x, 0, player.position.z + offset.z);
    }
}