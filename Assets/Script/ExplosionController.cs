using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    // Start is called before the first frame update
    private float waittime=0;
    private bool issound = false;
    private AudioSource _audioSource;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {

        if (!issound)
        {

            _audioSource.clip = Resources.Load<AudioClip>("Sounds/bomb1");
            _audioSource.PlayDelayed(0);
            issound = true;
        }
        waittime += Time.deltaTime;
        if (waittime > 0.5f)
        {
            waittime = 0;
            gameObject.SetActive(false);
            issound = false;
        }
        
    }
}
