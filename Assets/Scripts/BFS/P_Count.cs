using System.Collections.Generic;
using UnityEngine;

public class P_Count : MonoBehaviour
{
    public static HashSet<string> visited = new HashSet<string>();
    public static NodeGroup Explore(Node cellStart)
    {
        //Kiểm tra xem cell này đã thăm chưa.
        string position = $"{cellStart.pos.x},{cellStart.pos.y}";

        //Nếu thăm rồi thì return.
        if (visited.Contains(position)) return null;

        // Khai báo queue mới
        var queue = new Queue<Node>();

        //Khai báo cellGroup mới
        NodeGroup cellGroup = new NodeGroup();

        //Đã xét cell này.
        cellGroup.Nodes.Add(cellStart);
        
        //Đẩy cell vào queue.
        queue.Enqueue(cellStart);

        //Đánh dấu là đã thăm.
        visited.Add(position);

        //Khi nào trong queue vẫn còn cell chưa thăm thì tiếp tục.
        while (queue.Count > 0)
        {
            //Lấy cell ở đầu queue ra để xét.
            Node cellCurrent = queue.Dequeue();

            //Duyệt qua các cell lân cận của cell đó.
            foreach (Node neighbor in cellCurrent.neighbors)
            {
                string pos = $"{neighbor.pos.x},{neighbor.pos.y}";

                //Nếu cell này chưa thăm.
                if (!visited.Contains(pos))
                {
                    //Đã xét cell này.
                    cellGroup.Nodes.Add(neighbor);
                    
                    //Thêm cell này vào queue để tí duyệt hàng xóm của nó sau.
                    queue.Enqueue(neighbor);
                    //Đánh dấu là đã thăm.
                    visited.Add(pos);

                }
            }
        }

        //Trả về một cellGroup gồm một mảnh nhiều cell kề nhau.
        return cellGroup;
    }
}
