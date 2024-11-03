using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateTxt : MonoBehaviour
{
    private string content;

    [SerializeField]
    private TextMeshProUGUI txtContent;
    void FixedUpdate()
    {
        content = string.Empty;

        int[][] grid = CountConnectIsLand.grid;
        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[i].Length; j++)
            {
                content += grid[i][j] + " ";
            }
            content += '\n';
        }
        txtContent.SetText(content);
    }
}
