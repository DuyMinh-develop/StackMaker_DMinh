using UnityEngine;

public class CameraFLow : MonoBehaviour
{
    [SerializeField] private Transform player;

    public Vector3 offset = new Vector3(0f, 10f, -5f);

    public float smoothSpeed = 0.125f;

    void FixedUpdate()
    {
        if (player == null)
        {
            return;
        }

        // Tính toán vị trí mục tiêu mới của camera
        Vector3 desiredPosition = player.position + offset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(player.position);
    }
}

