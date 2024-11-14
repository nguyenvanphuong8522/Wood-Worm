using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wormChild : MonoBehaviour
{
    public wormChild child1;
    public void SetPos(Vector3 newPos)
    {
        if(child1 != null)
        {
            child1.SetPos(transform.localPosition);
        }
        else
        {
            ChangeDataGrid(transform.localPosition);
        }
        
        transform.localPosition = newPos;
        ChangeDataGrid(transform.localPosition, 2);

    }
    public void ChangeDataGrid(Vector3 pos, int value = 0)
    {
        if (pos.y > 0)
        {
            return;
        }
        int row = (int)Mathf.Abs(pos.y);
        int col = (int)pos.x;
        if (col < 0) return;
        int length = CountConnectIsLand.grid.Length;
        if (row < length && col < length)
        {
            Debug.Log(row + ", " + col);
            CountConnectIsLand.grid[row][col] = value;

            //Lúc mới vào game thì sẽ đếm số thành phần liên thông và log ra.
            CountConnectIsLand.MoveDownLoop();
        }
    }
}
