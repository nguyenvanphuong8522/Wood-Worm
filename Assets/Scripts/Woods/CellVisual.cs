using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellVisual : MonoBehaviour
{
    // Vận tốc ban đầu
    private Vector3 velocity = Vector3.zero;

    // Gia tốc trọng trường
    public float gravity = -9.81f;

    // Vị trí Y để dừng
    private float _stopPositionY;

    public float StopPositionY
    {
        get
        {
            return _stopPositionY;
        }
        set
        {
            _stopPositionY = value;
        }
    }

    void Update()
    {
        // Thay đổi vận tốc theo trọng lực
        velocity.y += gravity * Time.deltaTime;

        // Tính toán vị trí mới sau khi áp dụng vận tốc
        float newYPosition = transform.localPosition.y + velocity.y * Time.deltaTime;

        // Kiểm tra nếu vị trí mới sẽ nhỏ hơn stopPositionY
        if (newYPosition <= _stopPositionY)
        {
            // Đặt y bằng stopPositionY và dừng trọng lực
            transform.localPosition = new Vector3(transform.localPosition.x, _stopPositionY, transform.localPosition.z);
            velocity.y = 0; // Dừng vận tốc
        }
        else
        {
            // Cập nhật vị trí bình thường nếu chưa chạm stopPositionY
            transform.localPosition = new Vector3(transform.localPosition.x, newYPosition, transform.localPosition.z);
        }
    }


}
