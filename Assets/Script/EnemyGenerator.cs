using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    private ObjectPool _objectPool;
    private float _generateTime = 2;
    private float _nowTime = 0;
    void Start()
    {
        _objectPool = gameObject.AddComponent<ObjectPool>();
        _objectPool.CreatePool(20,Resources.Load<GameObject>("LesserEnemy"));
    }

    private void Update()
    {
        if (_generateTime <= _nowTime)
        {
            var obj = _objectPool.GetObject();
            obj.transform.position = new Vector3(10,Random.Range(-4f,4f));
            _nowTime = 0;
        }
        else
        {
            _nowTime+=Time.deltaTime;
        }
        
        
    }
}
