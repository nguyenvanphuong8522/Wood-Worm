using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Island = Island.
public class CountConnectIsLand
{
    //List Island.
    //Mỗi Island bao gồm nhiều ô nhỏ.
    //Mỗi ô nhỏ chính là chỉ số hàng và chỉ số cột.
    public static List<List<(int, int)>> listArrayPair = new List<List<(int, int)>>();

    //Đây là mảng 2 chiều ban đầu
    public static int[][] grid = new int[][]
    {
        new int[] { 0, 0, 1, 1, 1 },
        new int[] { 1, 1, 0, 0, 0 },
        new int[] { 0, 0, 0, 0, 0 },
        new int[] { 1, 0, 0, 0, 0 },
        new int[] { 1, 1, 1, 0, 0 }
    };


    public static void Count()
    {
        IslandCount(grid);

        foreach (var arrayPair in listArrayPair)
        {
            string tmp = null;
            foreach (var pair in arrayPair)
            {
                tmp += pair;
            }
            Debug.Log(tmp);
        }
    }


    /// <summary>
    /// Hàm này đếm Island.
    /// </summary>
    /// <param name="grid"></param>
    public static void IslandCount(int[][] grid)
    {
        //Mỗi lần tìm sẽ xóa list Island cũ đi.
        listArrayPair.Clear();
        //Danh sách những ô trên grid đã thăm.
        //Cái nào đã thăm rồi thì bỏ qua.
        var visited = new HashSet<string>();

        for (int r = 0; r < grid.Length; r++)
        {
            for (int c = 0; c < grid[0].Length; c++)
            {
                Explore(grid, r, c, visited);
            }
        }
    }

    /// <summary>
    /// Hàm này tìm tất cả những ô liền kề từ một ô bắt đầu.
    /// Những ô liền kề này chính là một Island.
    /// Sau khi tìm được một Island thì thêm nó vào list Island.
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="startRow"></param>
    /// <param name="startCol"></param>
    /// <param name="visited"></param>
    private static void Explore(int[][] grid, int startRow, int startCol, HashSet<string> visited)
    {
        //Nếu lớn hơn kích thước mảng hoặc vị trí đó bằng 0 thì return false luôn.
        if (grid[startRow][startCol] == 0) return;
        //Số 2 là vị trí mà cơ thể con sâu đang nằm trên đó.
        if (grid[startRow][startCol] == 2) return;

        //Kiểm tra xem hàng và cột có lớn hơn kích thước của mảng không.
        bool rowInbounds = 0 <= startRow && startRow < grid.Length;
        bool colInbounds = 0 <= startCol && startCol < grid[0].Length;
        if (!rowInbounds || !colInbounds) return;


        //Kiểm tra xem cái này đã thăm chưa.
        string startPos = $"{startRow},{startCol}";
        //Nếu thăm rồi thì return false.
        if (visited.Contains(startPos)) return;

        // Sử dụng ngăn xếp để duyệt DFS
        var stack = new Stack<(int, int)>();
        //Tạo một cái đảo mới
        List<(int, int)> listPair = new List<(int, int)>();
        //Thêm một ô đất vào đảo.
        listPair.Add((startRow, startCol));
        stack.Push((startRow, startCol));
        //Đánh dấu là đã thăm.
        visited.Add(startPos);

        // Các hướng di chuyển: lên, xuống, trái, phải
        var directions = new (int, int)[] { (1, 0), (-1, 0), (0, -1), (0, 1) };

        while (stack.Count > 0)
        {
            var (r, c) = stack.Pop();

            // Kiểm tra các ô liền kề
            //Cộng trừ x, y để đi đến 4 ô xung quanh.
            foreach (var (dr, dc) in directions)
            {
                int newRow = r + dr;
                int newCol = c + dc;
                //Kiểm tra xem index có lớn hơn kích thước mảng không.
                bool newRowInbounds = 0 <= newRow && newRow < grid.Length;
                bool newColInbounds = 0 <= newCol && newCol < grid[0].Length;

                if (newRowInbounds && newColInbounds && grid[newRow][newCol] == 1)
                {
                    string pos = $"{newRow},{newCol}";
                    //Nếu chưa thăm.
                    if (!visited.Contains(pos))
                    {
                        visited.Add(pos);
                        stack.Push((newRow, newCol));
                        //Thêm một ô đất vào đảo.
                        listPair.Add((newRow, newCol));
                    }
                }
            }
        }
        listPair.Sort((a, b) => b.Item1.CompareTo(a.Item1));
        //Thêm đảo mới vào danh sách các đảo.
        listArrayPair.Add(listPair);
    }

    /// <summary>
    /// Hàm này để di chuyển island xuống dưới nếu có thể.
    /// </summary>
    public static void MoveDown()
    {
        for(int i = 0; i < listArrayPair.Count; i++)
        {
            //Nếu có ô nào đang ở dưới cùng rồi thì không thể di chuyển xuống dưới được nữa.
            bool hasItemWithFirstValue4 = listArrayPair[i].Any(pair => pair.Item1 == 4);
            //Nếu vẫn có thể di chuyển xuống dưới.
            if (!hasItemWithFirstValue4)
            {
                if (!HasWorm(listArrayPair[i]))
                {
                    for (int j = 0; j < listArrayPair[i].Count; j++)
                    {

                        int newRow = listArrayPair[i][j].Item1 + 1;
                        int col = listArrayPair[i][j].Item2;
                        Debug.Log($"({listArrayPair[i][j].Item1}:{col + 1})");
                        if (newRow < 5)
                        {
                            grid[newRow-1][col] = 0;
                            grid[newRow][col] = 1;
                        }
                        listArrayPair[i][j] = (newRow,col);
                    }
                }
                //Nếu nằm trên lưng con sâu thì không di chuyển xuống được.
                else
                {
                    Debug.Log("<color=Yellow>Error:</color> Nằm trên con sâu");
                }
            }
            //Nếu k thể di chuyển xuống dưới.
            else
            {
                Debug.Log("<color=red>Error:</color> khong down duoc nua");
            }
        }


        //foreach (var innerList in listArrayPair)
        //{
        //    //Nếu có ô nào đang ở dưới cùng rồi thì không thể di chuyển xuống dưới được nữa.
        //    bool hasItemWithFirstValue4 = innerList.Any(pair => pair.Item1 == 4);
        //    //Nếu vẫn có thể di chuyển xuống dưới.
        //    if(!hasItemWithFirstValue4)
        //    {
        //        //Nếu Island này không có ô nào đang nằm trên lưng con sâu.
        //        if(!HasWorm(innerList)) {
        //            //Di chuyển tất cả phần tử xuống 1 đơn vị.
        //            foreach (var (row, col) in innerList)
        //            {
        //                int newRow = row + 1;
        //                Debug.Log($"({row}:{col + 1})");
        //                if (newRow < 5)
        //                {
        //                    grid[row][col] = 0;
        //                    grid[newRow][col] = 1;
        //                }
        //            }
        //        }
        //        //Nếu nằm trên lưng con sâu thì không di chuyển xuống được.
        //        else
        //        {
        //            Debug.Log("<color=Yellow>Error:</color> Nằm trên con sâu");
        //        }
                
        //    }
        //    //Nếu k thể di chuyển xuống dưới.
        //    else
        //    {
        //        Debug.Log("<color=red>Error:</color> khong down duoc nua");
        //    }
        //}
        //IslandCount(grid);
    }



    /// <summary>
    /// Kiểm tra xem Island đó có nằm trên con sâu không.
    /// </summary>
    /// <returns></returns>
    public static bool HasWorm(List<(int, int)> innerList)
    {
        // Bước 1: Tìm giá trị nhỏ nhất của Item1
        int maxValue = innerList.Max(item => item.Item1);

        // Bước 2: Lấy tất cả các phần tử có Item1 bằng minValue
        var minElements = innerList.Where(item => item.Item1 == maxValue).ToList();
        //Duyệt qua tất cả danh sách nếu có một ô trong Island đang nằm trên con sâu.
        //Nằm trên tức là ô bên dưới nó bằng 2.
        foreach (var (row, col) in minElements)
        {
            int newRow = row + 1;
            if(grid[newRow][col] == 2 || grid[newRow][col] == 1)
            {
                return true;
            }
        }
        return false;
    }

}
