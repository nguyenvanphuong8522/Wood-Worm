using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Node : MonoBehaviour
{
    public Vector2Int pos;

    public List<Node> neighbors = new List<Node>();

    private void Start()
    {
        UpdateRowAndCol();
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
        pos.x = (int)transform.localPosition.y;
        pos.y = (int)transform.localPosition.x;
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
        GameManager.instance.nodeManager.RemoveNode(pos);
        Destroy(gameObject);
    }
}
