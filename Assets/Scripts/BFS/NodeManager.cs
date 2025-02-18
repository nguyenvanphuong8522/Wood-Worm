//using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    public Dictionary<Vector2Int, Node> dictionaryNode;
    //Danh sách tất cả node.
    public List<Node> listOfNode;

    private void Awake()
    {
        GetNodeIntoList();
        dictionaryNode = new Dictionary<Vector2Int, Node>();
        InitializeNodeConnections();
    }

    /// <summary>
    /// Hàm này lấy tất cả node vào list.
    /// </summary>
    private void GetNodeIntoList()
    {
        foreach (Transform child in transform)
        {
            if (child == null) continue;

            if (child.TryGetComponent(out Node node))
            {
                listOfNode.Add(node);
            }
        }
    }


    /// <summary>
    /// Khởi tạo
    /// </summary>
    public void InitializeNodeConnections()
    {
        UpdatePosAllNode();
        SetupNodesDictionary();
        SetupNodeNeighbors();
    }


    /// <summary>
    /// Lấy những node hàng xóm của tất cả các node.
    /// </summary>
    private void SetupNodeNeighbors()
    {
        foreach (Vector2Int position in dictionaryNode.Keys)
        {
            dictionaryNode[position].neighbors.Clear();
            dictionaryNode[position].neighbors = GetNeighbors(position);
        }
    }

    /// <summary>
    /// Lấy tất cả node hàng xóm của một node.
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
    /// Hàm này xóa một node.
    /// </summary>
    /// <param name="gridPosition"></param>
    public void RemoveNode(Vector2Int gridPosition)
    {
        listOfNode.Remove(dictionaryNode[gridPosition]);
        dictionaryNode.Remove(gridPosition);
    }


    /// <summary>
    /// Lấy một node thông qua tham số là vị trí node.
    /// </summary>
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
        foreach (Node node in listOfNode)
        {
            if (node == null) continue;

            if (dictionaryNode.ContainsKey(node.pos))
            {
                Debug.LogWarning($"Trùng lặp node tại vị trí {node.pos}", node);
                continue;
            }
            dictionaryNode.Add(node.pos, node);
        }
    }


    /// <summary>
    /// Hàm này cập nhật lại vị trí của node hiện tại.
    /// </summary>
    public void UpdatePosAllNode()
    {
        foreach (Node node in listOfNode)
        {
            if (node != null)
            {
                node.UpdateRowAndCol();
            }
        }
    }

    //Hàm này lấy danh sách những key nằm trên một cột.
    private List<Vector2Int> GetNodeHasCol(int col)
    {
        List<Vector2Int> listOfKey = new List<Vector2Int>();
        foreach (Vector2Int key in dictionaryNode.Keys)
        {
            if (key.y == col)
            {
                listOfKey.Add(key);
            }
        }
        return listOfKey;
    }


    //Hàm này tìm node có vị trí thấp nhất trong cột.
    private Vector2Int GetKeyMin(int col)
    {
        List<Vector2Int> listOfKey = GetNodeHasCol(col);

        int min = int.MaxValue;

        for (int i = 0; i < listOfKey.Count; i++)
        {
            if (listOfKey[i].x < min)
            {
                min = listOfKey[i].x;
            }
        }
        Vector2Int keyNeedFind = new Vector2Int(min, col);
        return keyNeedFind;
    }

    //[Button(ButtonSizes.Gigantic)]
    public void Test(Node node)
    {
        Debug.Log(GetYMin(node.pos));
    }

    //Hàm này lấy ra giá trị thấp nhất mà node tại key này có thể di chuyển xuống.
    private float GetYMin(Vector2Int key)
    {
        //Đây là key có y thấp nhất tại cột này.
        Vector2Int min = GetKeyMin(key.y);
        //Nếu key là key thấp nhất.
        if(key == min)
        {
            return 0;
        }
        else
        {
            return min.y;
        }
    }
}