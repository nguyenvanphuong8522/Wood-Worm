using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public int row;
    public int col;

    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        if (CountConnectIsLand.grid[row][col] == 2)
        {
            spriteRenderer.color = Color.green;
        }
        else if (CountConnectIsLand.grid[row][col] == 1)
        {
            spriteRenderer.color = Color.gray;
        }
        else if (CountConnectIsLand.grid[row][col] == 3)
        {
            spriteRenderer.color = Color.red;
        }
        else 
        {
            spriteRenderer.color = Color.white;
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetKey(KeyCode.S))
        {
            CountConnectIsLand.grid[row][col] = 2;
            return;
        }
        if (Input.GetKey(KeyCode.F))
        {
            CountConnectIsLand.grid[row][col] = 3;
            return;
        }
        if (Input.GetKey(KeyCode.D))
        {
            CountConnectIsLand.grid[row][col] = 1;
        }
        else
        {
            CountConnectIsLand.grid[row][col] = 0;
        }
        
    }
}
