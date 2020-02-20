using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : SingletonMonoBehaviour<GameController>
{
    // Start is called before the first frame update
    public Text _pointviewer;
    private int _point=0;
    public Text GameOverText;
    public Text HighScore;
    private float waittime = 0.6f;
    private void Start()
    {
        PointUpdate(0);
    }

    public void lesserEnemyDeath()
    {
        PointUpdate(20);
    }
    public void StrongEnemyDeath()
    {
        PointUpdate(10000);
    }
    public void GameOver()
    {
        StartCoroutine(WaitBullet());
    }

    IEnumerator WaitBullet()
    {
        yield return new WaitForSeconds(waittime);
        HighScore.text = _point.ToString();
        GameOverText.text = $"GameOver \nYour Score {_point}\n";
    }

    public void PointUpdate(int addpoint)
    {
        _point += addpoint;
        _pointviewer.text = _point.ToString();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }
}
