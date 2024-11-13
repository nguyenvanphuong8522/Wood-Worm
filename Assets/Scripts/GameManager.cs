using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[Serializable]
public class ListGameObject
{
    public List<GameObject> list;
}

public class GameManager : MonoBehaviour
{
    [SerializeField] 
    private List<ListGameObject> list;



    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //Lúc mới vào game thì sẽ đếm số thành phần liên thông và log ra.
            CountConnectIsLand.Count();
            //Di chuyển các hòn đảo xuống thấp nhất có thể.
            CountConnectIsLand.MoveDownLoop();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            CountConnectIsLand.MoveDownLoop();
        }
    }

}
