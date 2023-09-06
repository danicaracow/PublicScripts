using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private const float Z = 0f;
    public Transform player;
    public Transform CameraGuide;
    public Transform ViewDirection;

    //public Vector3 cameraPosition = new Vector3(-10f, 2.5f, Z);
    public float followSpeed = 0.125f;

    private void FixedUpdate()
    {
        Vector3 newPosition = CameraGuide.position;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, newPosition, followSpeed);
        transform.position = smoothPosition;

        transform.LookAt(ViewDirection);
    }
}
