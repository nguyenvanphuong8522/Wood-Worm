using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityDemo : MonoBehaviour
{
    // Vận tốc ban đầu
    private Vector3 velocity = Vector3.zero;

    // Gia tốc trọng trường
    public float gravity = -9.81f;

    // Vị trí Y để dừng
    public float stopPositionY = 0f;

    void Update()
    {
        // Thay đổi vận tốc theo trọng lực
        velocity.y += gravity * Time.deltaTime;

        // Tính toán vị trí mới sau khi áp dụng vận tốc
        float newYPosition = transform.position.y + velocity.y * Time.deltaTime;

        // Kiểm tra nếu vị trí mới sẽ nhỏ hơn stopPositionY
        if (newYPosition <= stopPositionY)
        {
            // Đặt y bằng stopPositionY và dừng trọng lực
            transform.position = new Vector3(transform.position.x, stopPositionY, transform.position.z);
            velocity.y = 0; // Dừng vận tốc
        }
        else
        {
            // Cập nhật vị trí bình thường nếu chưa chạm stopPositionY
            transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);
        }
    }
}
