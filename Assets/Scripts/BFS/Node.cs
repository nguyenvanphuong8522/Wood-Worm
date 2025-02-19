using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Node : MonoBehaviour
{
    public Vector2Int pos;

    public List<Node> neighbors = new List<Node>();

    private NodeGroup nodeGroup;

    
    public NodeGroup NodeGroup
    {
        get
        {
            return nodeGroup;
        }
        set
        {
            nodeGroup = value;
        }
    }

    private void OnMouseDown()
    {
        DestroyCell();
    }



    /// <summary>
    /// Hàm này setup row và col, dựa theo vị trí.
    /// </summary>
    public void UpdateRowAndCol()
    {
        pos.x = Mathf.RoundToInt(transform.position.y);
        pos.y = Mathf.RoundToInt(transform.position.x);
        transform.position = new Vector3(pos.y, pos.x, 0);
    }

    /// <summary>
    /// Hàm này đổi màu node thành màu xanh lá.
    /// </summary>
    public void IndicatorChecked()
    {
        GetComponent<SpriteRenderer>().color = Color.green;
    }


    //Destroy cell.
    private void DestroyCell()
    {
        GameManager.Instance.nodeManager.RemoveNode(pos);
        if(transform.parent.childCount == 1)
        {
            Destroy(transform.parent.gameObject);
            return;
        }
        Destroy(gameObject);
    }

    /// <summary>
    /// Hàm này setparent cho node.
    /// </summary>
    /// <param name="parent"></param>
    public void AddToParent(Transform parent)
    {
        transform.SetParent(parent);
    }

    /// <summary>
    /// Hàm này tìm y nhỏ nhất mà ô này có thể di chuyển xuống.
    /// </summary>
    /// <returns></returns>
    private float GetYMin()
    {
        //Hàng của node này.
        int col = pos.y;

        return 0;
    }

}
