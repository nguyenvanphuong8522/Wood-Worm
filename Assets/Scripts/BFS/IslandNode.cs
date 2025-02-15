using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IslandNode : MonoBehaviour
{
    private void Awake()
    {
        Init();
    }
    public void Init()
    {
        SetPositionAVGChildPosition();
        AddGravity();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(transform.position.y < collision.transform.position.y)
        {
            Debug.Log("colli");
        }
    }


    private void AddGravity()
    {
        //Nếu chưa có component GravitySimulator.
        if (!transform.TryGetComponent(out GravitySimulator gs))
        {
            transform.AddComponent<GravitySimulator>();
        }
    }

    private void SetPositionAVGChildPosition()
    {
        // Lưu vị trí thế giới ban đầu của đối tượng A
        Vector3 originalPosition = transform.position;

        // Tính toán vị trí trung bình của các đối tượng con trong không gian thế giới
        Vector3 averagePosition = Vector3.zero;
        foreach (Transform child in transform)
        {
            averagePosition += child.position;
        }
        averagePosition /= transform.childCount;

        // Di chuyển đối tượng A sao cho pivot mới nằm giữa các con
        Vector3 offset = originalPosition - averagePosition;
        transform.position = averagePosition;  // Cập nhật vị trí mới của A

        // Đảm bảo các đối tượng con không bị thay đổi vị trí trong thế giới
        foreach (Transform child in transform)
        {
            child.position += offset;  // Điều chỉnh các con về lại vị trí thế giới ban đầu
        }
    }

    /// <summary>
    /// Hàm này tìm vị trí thấp nhất có thể di chuyển xuống.
    /// </summary>
    private float CalculatorYMin()
    {

        return 0;
    }
}
