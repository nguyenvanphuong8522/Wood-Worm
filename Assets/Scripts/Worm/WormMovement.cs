using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormMovement : MonoBehaviour
{
    //y là hàng
    //x là cột
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Vector3 temPos = transform.localPosition;
            transform.Translate(Vector2.up, Space.World);
            ChangeDataGrid(2);
            ChangeDataGrid(temPos);
            CountConnectIsLand.MoveDownLoop();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Vector3 temPos = transform.localPosition;
            transform.Translate(Vector2.down, Space.World);
            ChangeDataGrid(2);
            ChangeDataGrid(temPos);
            CountConnectIsLand.MoveDownLoop();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Vector3 temPos = transform.localPosition;
            transform.Translate(Vector2.left, Space.World);
            ChangeDataGrid(2);
            ChangeDataGrid(temPos);
            CountConnectIsLand.MoveDownLoop();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Vector3 temPos = transform.localPosition;
            transform.Translate(Vector2.right, Space.World);
            ChangeDataGrid(2);
            ChangeDataGrid(temPos);
            CountConnectIsLand.MoveDownLoop();
        }
    }

    public void ChangeDataGrid(int value)
    {
        Vector2 pos = transform.localPosition;
        int row = (int)Mathf.Abs(pos.y);
        int col = (int)pos.x;
        if (col < 0) return;
        int length = CountConnectIsLand.grid.Length;
        if (row < length && col < length)
        {
            Debug.Log(row + ", " + col);
            CountConnectIsLand.grid[row][col] = value;

            //Lúc mới vào game thì sẽ đếm số thành phần liên thông và log ra.
            CountConnectIsLand.Count();
            //Di chuyển các hòn đảo xuống thấp nhất có thể.
            
        }


    }

    public void ChangeDataGrid(Vector3 pos)
    {
        int row = (int)Mathf.Abs(pos.y);
        int col = (int)pos.x;
        if (col < 0) return;
        int length = CountConnectIsLand.grid.Length;
        if (row < length && col < length)
        {
            Debug.Log(row + ", " + col);
            CountConnectIsLand.grid[row][col] = 0;

            //Lúc mới vào game thì sẽ đếm số thành phần liên thông và log ra.
            CountConnectIsLand.MoveDownLoop();
        }
    }
}
