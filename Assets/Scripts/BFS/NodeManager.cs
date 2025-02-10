using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    public Dictionary<Vector2Int, Node> dictionaryNode;

    private void Awake()
    {
        dictionaryNode = new Dictionary<Vector2Int, Node>();
        InitializeNodeConnections();
    }


    /// <summary>
    /// Khởi tạo
    /// </summary>
    private void InitializeNodeConnections()
    {
        UpdatePosAllNode();
        SetupNodeNeighbors();
        SetupNodesDictionary();
    }


    /// <summary>
    /// Lấy những node hàng xóm của tất cả các node.
    /// </summary>
    private void SetupNodeNeighbors()
    {
        foreach (Vector2Int position in dictionaryNode.Keys)
        {
            dictionaryNode[position].neighbors = GetNeighbors(position);
        }
    }

    /// <summary>
    /// Lấy những node hàng xóm của một node
    /// </summary>
    private List<Node> GetNeighbors(Vector2Int gridPosition)
    {
        List<Node> neighbors = new List<Node>();
        Vector2Int[] directions = new Vector2Int[]
        {
            new Vector2Int(0, 1),  // Trên
            new Vector2Int(0, -1), // Dưới
            new Vector2Int(-1, 0), // Trái
            new Vector2Int(1, 0)   // Phải
        };

        foreach (var direction in directions)
        {
            Vector2Int neighborPos = gridPosition + direction;
            Node node = GetNode(neighborPos);
            if (node != null)
            {
                neighbors.Add(node);
            }
        }
        return neighbors;
    }

    /// <summary>
    /// Hàm này xóa một node khỏi dictionary.
    /// </summary>
    /// <param name="gridPosition"></param>
    public void RemoveNode(Vector2Int gridPosition)
    {
        dictionaryNode.Remove(gridPosition);
    }


    /// <summary>
    /// Lấy node từ vị trí trên lưới.
    /// </summary>
    /// <param name="gridPosition">Vị trí cần kiểm tra.</param>
    /// <returns>Node tại vị trí chỉ định. Trả về null nếu không tồn tại.</returns>
    private Node GetNode(Vector2Int gridPosition)
    {
        dictionaryNode.TryGetValue(gridPosition, out Node node);
        return node;
    }

    /// <summary>
    /// Đưa node vào trong dictionary với key là pos của node.
    /// </summary>
    private void SetupNodesDictionary()
    {
        dictionaryNode.Clear();
        foreach (Transform child in transform)
        {
            if (child == null) continue;

            if (child.TryGetComponent(out Node node))
            {
                if (dictionaryNode.ContainsKey(node.pos))
                {
                    Debug.LogWarning($"Trùng lặp node tại vị trí {node.pos}", child);
                    continue;
                }
                dictionaryNode.Add(node.pos, node);
            }
        }
    }

    private void UpdatePosAllNode()
    {
        foreach (Transform child in transform)
        {
            if (child == null) continue;

            if (child.TryGetComponent(out Node node))
            {
                node.UpdateRowAndCol();
            }
        }
    }
}