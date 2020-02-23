using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongEnemyController : MonoBehaviour
{
    // Update is called once per frame
    private float waitTime = 0.5f;
    private float nowTime = 0;
    private ObjectPool _objectPool;
    private int _changeRad = 0;
    private float speed = 1.5f;
    private int movedirection=-1;
    private int HP;
    private GameObject ExplosionGameObject;
    private void Start()
    {
        HP = 10000;
        _objectPool = new ObjectPool();
        _objectPool.CreatePool(100,Resources.Load<GameObject>("beam2_2"));
        ExplosionGameObject = Instantiate(Resources.Load<GameObject>("char_enemy2_5"));
        ExplosionGameObject.SetActive(false);
    }

    void Update()
    {
        if (transform.position.x < -3) movedirection *= -1;
        if (transform.position.x > 3) movedirection *= -1;
        transform.position += new Vector3(movedirection*Time.deltaTime,0,0);
        nowTime += Time.deltaTime;
        if (waitTime < nowTime)
        {
            for (int i = 0; i < 10; i++)
            {
                var obj = _objectPool.GetObject();
                obj.transform.position = gameObject.transform.position;
                obj.GetComponent<Rigidbody2D>().velocity = new Vector3(Mathf.Cos(Mathf.Deg2Rad*(i*36+_changeRad))*speed,Mathf.Sin(Mathf.Deg2Rad*(i*36+_changeRad))*speed);
            }

            _changeRad+=5;
            nowTime = 0;
        }

        if (HP < 1)
        {
            GameController.Instance.PointUpdate(10000);
            ExplosionGameObject.SetActive(true);
            GameController.Instance.StrongEnemyDeath();
            gameObject.SetActive(false);
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("beam"))
        {
            GameController.Instance.PointUpdate(5);
            HP -= 10;
        }
    }
}
