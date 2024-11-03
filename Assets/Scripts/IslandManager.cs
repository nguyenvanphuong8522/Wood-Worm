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

public class IslandManager : MonoBehaviour
{
    [SerializeField]
    private List<ListGameObject> list;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            CountConnectIsLand.Count();
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            CountConnectIsLand.MoveDown();
        }
    }
}
