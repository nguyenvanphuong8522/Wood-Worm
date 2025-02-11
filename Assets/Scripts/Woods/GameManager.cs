using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public NodeManager nodeManager;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) ReFindIsland();
    }


    //Tìm lại danh sách island.
    public void ReFindIsland()
    {
        //Khởi tại lại vị trí node, hàng xóm node.
        nodeManager.InitializeNodeConnections();

        List<NodeGroup> nodeGroups = GetNodeGroup(nodeManager.dictionaryNode);

        foreach (NodeGroup nodeGroup in nodeGroups)
        {
            GameObject newParent = new GameObject($"Parent: {nodeGroups.Count}");
            newParent.transform.SetParent(nodeManager.transform);
            AddAllNodeToParent(nodeGroup, newParent.transform);
        }
        ChangeColor(nodeGroups);
    }


    //Đổi màu tất cả các node đã tìm thấy.
    private void ChangeColor(List<NodeGroup> nodeGroups)
    {
        //Đổi màu những island.
        foreach (NodeGroup nodeGroup in nodeGroups)
        {
            foreach (Node node in nodeGroup.Nodes)
            {
                node.IndicatorChecked();
            }
        }
    }


    /// <summary>
    /// Hàm này lấy tất cả các đảo.
    /// </summary>
    /// <param name="dictionaryNode"></param>
    /// <returns></returns>
    public List<NodeGroup> GetNodeGroup(Dictionary<Vector2Int, Node> dictionaryNode)
    {
        P_Count.visited.Clear();
        List<NodeGroup> nodeGroups = new List<NodeGroup>();
        foreach (Vector2Int key in dictionaryNode.Keys)
        {
            NodeGroup nodeGroup = P_Count.Explore(dictionaryNode[key]);
            if (nodeGroup != null && nodeGroup.Nodes.Count > 0)
            {
                nodeGroups.Add(nodeGroup);
            }
        }
        return nodeGroups;
    }

    //Hàm này sẽ add tất cả node của một đảo vào một gameObject cha.
    private void AddAllNodeToParent(NodeGroup nodeGroup, Transform newParent)
    {
        if (nodeGroup.Nodes.Count > 0)
        {
            Transform oldParent = nodeGroup.Nodes[0].transform.parent;
            foreach (Node node in nodeGroup.Nodes)
            {
                node.AddToParent(newParent);
            }

            if (oldParent != nodeManager.transform)
            {
                Destroy(oldParent.gameObject, 0.1f);
            }
        }

    }
}
