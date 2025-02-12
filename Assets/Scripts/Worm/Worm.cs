using UnityEngine;

public class Worm : MonoBehaviour
{
    //Đây là hướng mà con sâu đang nhìn
    private Vector2 _lookDirection;

    //Danh sach những node con.
    [SerializeField]
    private Transform[] _listTail;

    //[SerializeField]
    //private Rigidbody2D _rb;

    private void Awake()
    {
        _lookDirection = Vector2.right;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move(Vector2.up);
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



    /// <summary>
    /// Hàm này di chuyển con sâu một đơn vị.
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="value"></param>
    private void Move(Vector2 direction)
    {
        //if (_rb.velocity.y < 0) return;
        //Nếu cái hướng di chuyển mà là hướng đi lùi thì return luôn.
        if (_lookDirection == - direction) return;

        MoveTails();
        //Di chuyển một đơn vị theo direction.
        _listTail[0].Translate(direction, Space.World);

        //Gán cái hướng nhìn của con sâu bằng cái hướng vừa di chuyển.
        _lookDirection = direction;
    }

    /// <summary>
    /// Hàm này di chuyển những node con.
    /// </summary>
    private void MoveTails()
    {
        for(int i = _listTail.Length-1; i >= 1; i--)
        {
            _listTail[i].localPosition = _listTail[i-1].localPosition;
        }
    }
}
