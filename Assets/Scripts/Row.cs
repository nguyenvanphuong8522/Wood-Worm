using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour
{
    public int indexRow;

    [SerializeField]
    private List<Cell> cells;
    private void Awake()
    {
        SetUp();
    }
    public void SetUp()
    {
        for (int i = 0; i < cells.Count; i++)
        {
            cells[i].row = indexRow;
            cells[i].col = i;
        }
    }

    private void OnValidate()
    {
        indexRow = transform.GetSiblingIndex();
        cells = new List<Cell>();

        foreach (Transform transformChild in transform)
        {
            Cell cell = transformChild.GetComponent<Cell>();
            if (cell != null)
            {
                cells.Add(cell);
            }
        }
    }
}
