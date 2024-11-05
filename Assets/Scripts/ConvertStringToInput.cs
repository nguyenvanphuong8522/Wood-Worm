using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConvertStringToInput : MonoBehaviour
{
    public TextMeshProUGUI txt;
    public void Move()
    {
        string input = txt.text;

        int[][] grid = ConvertTo2DArray(input);
    }

    public int[][] ConvertTo2DArray(string input)
    {
        // Tách chuỗi thành các dòng
        string[] rows = input.Split(new[] {'\n'}, StringSplitOptions.RemoveEmptyEntries);
        int[][] result = new int[rows.Length][];

        for (int i = 0; i < rows.Length; i++)
        {
            // Tách từng dòng thành các phần tử
            string[] values = rows[i].Trim().Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            result[i] = Array.ConvertAll(values, int.Parse);
        }

        CountConnectIsLand.grid = result;
        CountConnectIsLand.canMoveDown = true;
        return result;
    }
    public static string ConvertToString(int[][] grid)
    {
        string result = "";
        for (int i = 0; i < grid.Length; i++)
        {
            result += string.Join(" ", grid[i]) + "\n";
        }
        return result.Trim(); // Bỏ ký tự xuống dòng cuối cùng
    }
}
