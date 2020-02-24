using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongEnemyController : MonoBehaviour
{
    // Update is called once per frame
    public int EnemyType = 2;
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
        HP = 1500*EnemyType;
        _objectPool = gameObject.AddComponent<ObjectPool>();
        _objectPool.CreatePool(100,Resources.Load<GameObject>("beam2_2"));
        ExplosionGameObject = Instantiate(Resources.Load<GameObject>("char_enemy2_5"));
        ExplosionGameObject.SetActive(false);
    }

    void Update()
    {
        if (EnemyType%2!=0)
        {
            if (transform.position.x < -3 && movedirection<0) movedirection *= -1;
            if (transform.position.x > 3 && movedirection>0) movedirection *= -1;
            transform.position += new Vector3(movedirection*Time.deltaTime,0,0);
        }
        else
        {
            Debug.Log("Yes");
            if (transform.position.x > 3)
            {
                transform.position += new Vector3(movedirection*Time.deltaTime,0,0);
            }
            else
            {
                if (transform.position.y < -3 && movedirection<0) movedirection *= -1;
                if (transform.position.y > 3 && movedirection>0) movedirection *= -1;
                transform.position += new Vector3(0,movedirection*Time.deltaTime,0);

            }
        }
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
            ExplosionGameObject.SetActive(true);
            ExplosionGameObject.transform.position = gameObject.transform.position;
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
//            Debug.Log(HP);
        }
    }
}
