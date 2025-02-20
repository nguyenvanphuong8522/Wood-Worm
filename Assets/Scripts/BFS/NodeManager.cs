using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    #region Init Node
    public int indexColumn;
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
    #endregion






    //Hàm này trả về quãng đường mà island có thể di chuyển xuống.
    [Button(ButtonSizes.Gigantic)]
    public int GetYMinOfNodeGroup(IslandNode islandNode)
    {
        int minDistance = int.MaxValue;
        foreach (Node node in islandNode.nodeGroup.Nodes)
        {
            //Lấy ra quãng đường của node này.
            int distance = GetDistanceYMinOfNode(node, islandNode);
            if (distance == 8522)
            {
                continue;
            }
            //Nếu quãng đường nhỏ hơn min.
            if(distance < minDistance)
            {
                minDistance = distance;
            }
            //Set điểm di chuyển thấp nhất của node.
            node.YMin = node.pos.x - distance;
        }

        //Trả về quãng đường ngắn nhất.
        return minDistance;
    }


    //Hàm này tìm ra quãng đường mà node có thể di chuyển xuống.
    public int GetDistanceYMinOfNode(Node node, IslandNode nodeGroup)
    {
        //Nếu đang ở dưới cùng.
        if (node.pos.x == 0)
        {
            node.YMin = 0;
            return 0;
        }
        else
        {
            //Index cột của node này.
            int col = node.pos.y;
            //Duyệt các node ở bên dưới node này.
            for (int i = node.pos.x - 1; i >= 0; i--)
            {
                Vector2Int key = new Vector2Int(i, col);
                if (dictionaryNode.TryGetValue(key, out Node nodeFinded))
                {
                    //Nếu node tìm thấy nằm trong cùng một island thì thôi.
                    if (nodeGroup.nodeGroup.Nodes.Contains(nodeFinded))
                    {
                        return 8522;
                    }
                    //Nếu node tìm thấy là node ở island khác.
                    else
                    {
                        return node.pos.x - nodeFinded.pos.x - 1;
                    }
                }
            }
            //Nếu dưới node này không có node nào.
            return node.pos.x;
        }
    }
}