using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private List<GameObject> _objectPool;
    private GameObject _createpoolobj;
    public void CreatePool(int size,GameObject CreatePoolObject)
    {
        _createpoolobj = CreatePoolObject;
        _objectPool = new List<GameObject>(size);
        for (int i = 0; i < size; i++)
        {
            _objectPool.Add(CreateObject());
        }
    }

    public GameObject GetObject()
    {
        foreach (var VARIABLE in _objectPool)
        {
            if (!VARIABLE.activeSelf)
            {
                VARIABLE.SetActive(true);
                return VARIABLE;
            }
        }

        GameObject obj = CreateObject();
        _objectPool.Add(obj);
        obj.SetActive(true);
        return obj;
    }

    private GameObject CreateObject()
    {
        var obj = Instantiate(_createpoolobj);
        obj.SetActive(false);
        return obj;
    }


}
