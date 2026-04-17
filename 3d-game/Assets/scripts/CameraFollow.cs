using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 offset;
    public Transform player; 
    void Update()
    {
        transform.position = player.position + offset;
    }
}
