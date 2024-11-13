using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public int row;
    public int col;

    private void FixedUpdate()
    {
        if (CountConnectIsLand.grid[row][col] == 2)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
        }
        else if(CountConnectIsLand.grid[row][col] == 1)
        {
            GetComponent<SpriteRenderer>().color = Color.gray;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetKey(KeyCode.S))
        {
            CountConnectIsLand.grid[row][col] = 2;
            CountConnectIsLand.MoveDownLoop();
            return;
        }
        if (Input.GetKey(KeyCode.D))
        {
            CountConnectIsLand.grid[row][col] = 1;
        }
        else
        {
            CountConnectIsLand.grid[row][col] = 0;

            CountConnectIsLand.Count();
            //Di chuyển các hòn đảo xuống thấp nhất có thể.
            CountConnectIsLand.MoveDownLoop();
        }
        
    }
}
