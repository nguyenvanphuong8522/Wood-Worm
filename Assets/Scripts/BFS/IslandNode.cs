using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IslandNode : MonoBehaviour
{
    public NodeGroup nodeGroup;

    private void Awake()
    {
        Init();
    }
    public void Init()
    {
        SetPositionAVGChildPosition();
        InitNodeGroup();
    }

    public void InitNodeGroup()
    {
        nodeGroup = new NodeGroup();
        foreach(Transform child in transform)
        {
            if(child.TryGetComponent(out Node node)) {
                nodeGroup.Nodes.Add(node);
                node.NodeGroup = nodeGroup;
            }
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
        Vector3 vectorMinY = new Vector3(0, int.MaxValue, 0);
        // Lưu vị trí thế giới ban đầu của đối tượng A
        Vector3 originalPosition = transform.position;

        // Tính toán vị trí trung bình của các đối tượng con trong không gian thế giới
        foreach (Transform child in transform)
        {
            if(child.position.y < vectorMinY.y)
            {
                vectorMinY = child.position;
            }
        }
        // Di chuyển đối tượng A sao cho pivot mới nằm giữa các con
        Vector3 offset = originalPosition - vectorMinY;

        transform.position = vectorMinY;
        // Đảm bảo các đối tượng con không bị thay đổi vị trí trong thế giới
        foreach (Transform child in transform)
        {
            child.position += offset;  // Điều chỉnh các con về lại vị trí thế giới ban đầu
        }
    }


    /// <summary>
    /// Hàm này sẽ di chuyển island
    /// step là số bước mà island có thể di chuyển xuống.
    /// </summary>
    /// <param name="step"></param>    
    private void MoveIsland(int step)
    {
        transform.position += Vector3.down * step;
    }


    /// <summary>
    /// Hàm này sẽ tìm ra số step mà island này có thể di chuyển.
    /// </summary>
    /// <returns></returns>
    private int GetStepMove()
    {
        return 0;
    }

    //Hàm này lấy ra tất cả các node trong một column.
    private List<Node> GetNodeInColumn(int index)
    {
        List<Node> nodesOfColumn = new List<Node>();
        foreach (Node node in nodeGroup.Nodes)
        {
            if (node.pos.y == index)
            {
                nodesOfColumn.Add(node);
            }
        }
        nodesOfColumn.Sort((a, b) => a.pos.x.CompareTo(b.pos.x));
        return nodesOfColumn;
    }
}
