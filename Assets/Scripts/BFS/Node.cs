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
        pos.x = Mathf.RoundToInt(transform.localPosition.y);
        pos.y = Mathf.RoundToInt(transform.localPosition.x);
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
        GameManager.instance.nodeManager.RemoveNode(pos);
        if(transform.parent.childCount == 1)
        {
            Destroy(transform.parent.gameObject);
            return;
        }
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        DestroyCell();
    }
    public void AddToParent(Transform parent)
    {
        transform.SetParent(parent);
    }
}
