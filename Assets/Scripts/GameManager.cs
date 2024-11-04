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

    private void Start()
    {
        CountConnectIsLand.Count();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CountConnectIsLand.MoveDown();
        }
    }
}
