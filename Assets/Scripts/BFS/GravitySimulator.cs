//using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class GravitySimulator : MonoBehaviour
{
    #region Define
    private const float _gravity = -9.8f;

    private Coroutine _coroutine;

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


    //[Button(ButtonSizes.Gigantic)]

    public void StartFall()
    {
        YMin = Mathf.RoundToInt(transform.position.y) - YMin;

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _velocity = Vector2.zero;
        _coroutine = StartCoroutine(FallCoroutine());
    }

    private IEnumerator FallCoroutine()
    {
        // Tiếp tục di chuyển xuống cho đến khi đạt y <= 0
        while (true)
        {
            Vector3 currentPosition = Fall();
            if (currentPosition.y <= _yMin)
            {
                currentPosition.y = _yMin;
                transform.position = currentPosition;
                if (_coroutine != null)
                {
                    StopCoroutine(_coroutine);
                }
            }
            transform.position = currentPosition;
            yield return null;
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
