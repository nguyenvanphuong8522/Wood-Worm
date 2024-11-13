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
        new int[] { 1, 1, 1, 1, 1 },
        new int[] { 1, 1, 1, 1, 1 },
        new int[] { 1, 1, 1, 1, 1 },
        new int[] { 1, 1, 1, 1, 1 },
        new int[] { 1, 1, 1, 1, 1 }
    };


    public static void Count()
    {
        IslandCount(grid);

    }

    //Show danh sách Island.
    public static void LogIsland()
    {
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
    /// Hàm này đếm thành phần liên thông và lưu 1 thành phần liên thông là 1 list.
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
    /// Hàm này sẽ di chuyển island từng ô một đến khi nào không di chuyển được nữa thì thôi.
    /// </summary>
    public static void MoveDownLoop()
    {
        int count = 10;
        while (count > 0)
        {
            MoveDownMultiIsland();
            count--;
        }
        Count();
        Debug.Log("Move finished");
    }
    /// <summary>
    /// Mỗi lần gọi hàm này sẽ di chuyển tất cả các đảo xuống một hàng.
    /// </summary>
    public static void MoveDownMultiIsland()
    {
        for (int i = 0; i < listIsland.Count; i++)
        {
            var island = listIsland[i];
            //Nếu vẫn có thể di chuyển xuống dưới.
            if (CanMoveDown(island))
            {
                MoveDownSingleIsland(island);
            }
            //Nếu nằm trên lưng con sâu thì không di chuyển xuống được.
            else
            {
                Debug.Log($"<color=Yellow>Error:</color> Không thể di chuyển đảo ở vị trí {i} xuống được nữa.");
            }
        }
    }

    //Di chuyển một cái đảo xuống dưới 1 hàng.
    // Để di chuyển cả cái đảo xuống một hàng thì sẽ di chuyển từng phần tử 1 xuống dưới 1 hàng.
    private static void MoveDownSingleIsland(Island island)
    {
        foreach (var (row, col) in island.listCell)
        {
            //Lấy index hiện tại cộng thêm một đơn vị.
            int newRow = row + 1;

            // Log vị trí di chuyển
            Debug.Log($"<color=Green>Moving:</color> ({row}:{col + 1}) to ({newRow}:{col + 1})");

            // gán cái ô ban đầu bằng 0.
            grid[row][col] = 0;
            // gán cái ô bên dưới ô ban đầu bằng 1.
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
        var minElements = island.listCell.ToList();

        foreach (var (row, col) in minElements)
        {
            //Nếu có ô nào đang ở dưới cùng rồi thì không thể di chuyển xuống dưới được nữa.
            if (row == 4)
            {
                return false;
            }

            if (grid[row + 1][col] != 0 && !minElements.Contains((row+1, col)))
            {
                return false;
            }
        }
        return true;
    }


}
