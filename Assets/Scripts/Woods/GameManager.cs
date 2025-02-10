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
        if (Input.GetKeyDown(KeyCode.Space)) Test();
    }

    public void Test()
    {
        List<NodeGroup> nodeGroups = GetNodeGroup(nodeManager.dictionaryNode);
        foreach(NodeGroup nodeGroup in nodeGroups)
        {
            foreach(Node node in nodeGroup.Nodes)
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
        foreach(Vector2Int key in dictionaryNode.Keys)
        {
            NodeGroup nodeGroup = P_Count.Explore(dictionaryNode[key]);
            if(nodeGroup != null)
            {
                nodeGroups.Add(nodeGroup);
            }
        }
        return nodeGroups;
    }
}
