using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class lesserEnemyController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private int HP=100;
    private GameObject obj;
    private ObjectPool _objectPool;
    private float waitTime = 0.5f;
    private float nowTime = 0;
    private int _changeRad = 120;
    private float speed = 3f;
    private void Start()
    {
        _objectPool = new ObjectPool();
        _objectPool.CreatePool(100,Resources.Load<GameObject>("beam2_1"));
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
         obj = Instantiate(Resources.Load<GameObject>("Explosion"));
         obj.SetActive(false);

    }

    void Update()
    {
        nowTime += Time.deltaTime;
        if (waitTime < nowTime)
        {
            for (int i = 0; i < 4; i++)
            {
                var obj = _objectPool.GetObject();
                obj.transform.position = gameObject.transform.position;
                obj.GetComponent<Rigidbody2D>().velocity = new Vector3(Mathf.Cos(Mathf.Deg2Rad*(i*40+_changeRad))*speed-2,Mathf.Sin(Mathf.Deg2Rad*(i*40+_changeRad))*speed);
            }

            nowTime = 0;
        }

        if (_rigidbody2D.velocity.x <= 0) _rigidbody2D.velocity = Vector2.left * 2;
        if (gameObject.transform.position.x < -10)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("beam") && HP < 1)
        {
            obj.SetActive(true);
            obj.transform.position = gameObject.transform.position;
            gameObject.SetActive(false);
            GameController.Instance.lesserEnemyDeath();
        }
        else
        {
            HP -= 20;
        }
    }
}
