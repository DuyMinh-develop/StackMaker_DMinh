using UnityEngine;

public class CameraFLow : MonoBehaviour
{
    // Kéo GameObject của nhân vật vào đây trong Unity Editor
    [SerializeField] private Transform player;

    // Khoảng cách camera so với nhân vật
    public Vector3 offset = new Vector3(0f, 10f, -5f);

    // Tốc độ di chuyển của camera, giúp camera di chuyển mượt mà
    public float smoothSpeed = 0.125f;

    void LateUpdate()
    {
        // Kiểm tra xem đã gán nhân vật chưa
        if (player == null)
        {
            Debug.LogWarning("Player is not assigned to the camera script.");
            return;
        }

        // Tính toán vị trí mục tiêu mới của camera
        Vector3 desiredPosition = player.position + offset;

        // Di chuyển camera mượt mà đến vị trí mục tiêu
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Quay camera để luôn nhìn vào nhân vật
        transform.LookAt(player.position);
    }
}

