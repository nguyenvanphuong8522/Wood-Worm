//using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class GravitySimulator : MonoBehaviour
{
    #region Define
    private const float _gravity = -9.8f;

    private Vector2 _velocity;
    #endregion

    #region Properties
    /// <summary>
    /// Giá trị thấp nhất có thể rơi xuống.
    /// </summary>
    [SerializeField]

    private int _yMin;
    public int YMin
    {
        get
        {
            return _yMin;
        }
        set
        {
            _yMin = value;
        }
    }
    #endregion

    private void Update()
    {
        FallCoroutine();
    }

    private void FallCoroutine()
    {
        // Tiếp tục di chuyển xuống cho đến khi đạt y <= 0

        Vector3 currentPosition = Fall();


        if (transform.position.y > _yMin)
        {
            transform.position = Fall(); ;
        }
        else
        {
            _velocity = Vector2.zero;

            currentPosition.y = _yMin;

            transform.position = currentPosition;

        }

    }

    /// <summary>
    /// Di chuyển hướng xuống dưới, mô phỏng trọng lực.
    /// </summary>
    private Vector3 Fall()
    {
        _velocity.y += _gravity * Time.deltaTime;
        Vector3 currentPosition = transform.position + (Vector3)_velocity * Time.deltaTime;
        return currentPosition;
    }
}
