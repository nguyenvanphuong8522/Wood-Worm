using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Island = Thành phần liên thông.

public class Island
{
    public int max;
    public bool canMove;
    public List<(int, int)> listCell;

    public Island()
    {
        listCell = new List<(int, int)>();
        canMove = true;
    }

    public int Count
    {
        get => listCell.Count;
    }
}
public class CountConnectIsLand
{
    //List Island.
    //Mỗi Island bao gồm nhiều ô nhỏ.
    //Mỗi ô nhỏ chính là chỉ số hàng và chỉ số cột.
    public static List<Island> listIsland = new List<Island>();

    public static bool canMoveDown = true;

    //Đây là mảng 2 chiều ban đầu
    public static int[][] grid = new int[][]
    {
        new int[] { 0, 0, 0, 0, 1 },
        new int[] { 0, 0, 0, 0, 0 },
        new int[] { 0, 0, 0, 0, 2 },
        new int[] { 0, 0, 0, 0, 0 },
        new int[] { 1, 1, 1, 0, 1 }
    };


    public static void Count()
    {
        IslandCount(grid);

        foreach (var arrayPair in listIsland)
        {
            string tmp = null;
            foreach (var pair in arrayPair.listCell)
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
        canMoveDown = true;
        //Mỗi lần tìm sẽ xóa list Island cũ đi.
        listIsland.Clear();
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
        Island newIsland = new Island();
        //Thêm một ô đất vào đảo.
        newIsland.listCell.Add((startRow, startCol));
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
                        newIsland.listCell.Add((newRow, newCol));
                    }
                }
            }
        }
        newIsland.listCell.Sort((a, b) => b.Item1.CompareTo(a.Item1));
        //Thêm đảo mới vào danh sách các đảo.
        listIsland.Add(newIsland);
    }


    /// <summary>
    /// Hàm này sẽ di chuyển island tức ô một đến khi nào không di chuyển được nữa thì thôi.
    /// </summary>
    public static void MoveDownLoop()
    {
        while (canMoveDown)
        {
            MoveDownMultiIsland();
        }
        Debug.Log("Move finished");
    }
    /// <summary>
    /// Hàm này để di chuyển island xuống dưới nếu có thể.
    /// </summary>
    public static void MoveDownMultiIsland()
    {
        int numberCanMove = 0;
        for (int i = 0; i < listIsland.Count; i++)
        {
            var island = listIsland[i];
            //Nếu vẫn có thể di chuyển xuống dưới.
            if (CanMoveDown(island))
            {
                numberCanMove++;
                MoveDownSingleIsland(island);
            }
            //Nếu nằm trên lưng con sâu thì không di chuyển xuống được.
            else
            {
                Debug.Log($"<color=Yellow>Error:</color> Không thể di chuyển đảo ở vị trí {i} xuống được nữa.");
            }
        }
        if(numberCanMove == 0)
        {
            canMoveDown = false;
        }
    }

    private static void MoveDownSingleIsland(Island island)
    {
        foreach (var (row, col) in island.listCell)
        {
            int newRow = row + 1;

            // Log vị trí di chuyển
            Debug.Log($"<color=Green>Moving:</color> ({row}:{col + 1}) to ({newRow}:{col + 1})");

            // Xóa vị trí cũ và cập nhật vị trí mới trong grid
            grid[row][col] = 0;
            grid[newRow][col] = 1;
        }

        // Cập nhật lại `innerList` với các vị trí mới
        for (int i = 0; i < island.Count; i++)
        {
            island.listCell[i] = (island.listCell[i].Item1 + 1, island.listCell[i].Item2);
        }
    }



    /// <summary>
    /// Kiểm tra xem Island đó có thể di chuyển xuống được không.
    /// </summary>
    /// <returns></returns>
    public static bool CanMoveDown(Island island)
    {
        // Bước 1: Tìm giá trị lớn nhất của Item1
        int maxValue = island.listCell.Max(item => item.Item1);

        // Bước 2: Lấy tập hợp những cái ô ở hàng dưới cùng của cái đảo đấy.
        // Sau đó sẽ kiểm tra những cái ô trong cái hàng đấy xem có cái ô nào bên dưới khác 0 không
        // nếu khác 0 tức là
        // không di chuyển được xuống.
        var minElements = island.listCell.Where(item => item.Item1 == maxValue).ToList();

        foreach (var (row, col) in minElements)
        {
            //Nếu có ô nào đang ở dưới cùng rồi thì không thể di chuyển xuống dưới được nữa.
            if (row == 4)
            {
                return false;
            }

            if (grid[row + 1][col] != 0)
            {
                return false;
            }
        }
        return true;
    }
}
