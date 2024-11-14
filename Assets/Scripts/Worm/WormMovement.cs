using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormMovement : MonoBehaviour
{
    public wormChild child1;


    private Vector2 dirLook = Vector2.right;
    //y là hàng
    //x là cột
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move(Vector2.up, 3);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
           Move(Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(Vector2.right);
        }
    }

    public void Move(Vector2 direction, int value = 2)
    {
        if (dirLook != -direction)
        {
            Vector3 temPos = transform.localPosition;
            transform.Translate(direction, Space.World);
            dirLook = direction;
            ChangeDataGrid(transform.localPosition, value);
            //ChangeDataGrid(temPos);
            child1.SetPos(temPos);
            CountConnectIsLand.MoveDownLoop();
        }
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
