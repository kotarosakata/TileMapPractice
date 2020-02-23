using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private ObjectPool _objectPool;
    private float waitTime = 0.1f;
    private AudioSource _audioSource;
    private float nowtime = 0;
    private int speedOffset = 7;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _objectPool = gameObject.AddComponent<ObjectPool>();
        _objectPool.CreatePool(20,Resources.Load<GameObject>("beam"));
    }

   

    // Update is called once per frame
    void Update()
    {
        float keyDownX = Input.GetAxisRaw("Horizontal");
        float keyDownY = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(KeyCode.LeftShift)) speedOffset = 3;
        else speedOffset = 7;
        Vector3 vec = new Vector3(keyDownX, keyDownY).normalized * (Time.deltaTime * speedOffset);
        Vector3 vec2 = new Vector3(Mathf.Clamp(vec.x + transform.position.x, -8, 8),
            Mathf.Clamp(vec.y + transform.position.y, -4, 4));
        transform.position = vec2;

        if (Input.GetKey(KeyCode.Space)&& Time.timeScale>0)
        {
            if (waitTime <= nowtime)
            {
                _audioSource.PlayDelayed(0);
                var obj = _objectPool.GetObject();
                obj.transform.position = gameObject.transform.position;
                nowtime = 0;
            }
            else
            {
                nowtime += Time.deltaTime;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("EnemyBeam")) 
        {
            GameController.Instance.GameOver();
            gameObject.SetActive(false);
            var obj =Instantiate(Resources.Load<GameObject>("Explosion"));
            obj.transform.position = transform.position;
        }
        
    }
}
