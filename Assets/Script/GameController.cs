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
    private int EnemyTypeNow = 1;
    private  static int _point=0;
    public Text Titletext;
    private static bool isFirstPlay = true;
    public Text GameOverText;
    public Text HighScore;
    private float waittime = 1f;
    private bool IsOver = false;
    private void Start()
    {
       var bossGameObjectObj= Instantiate(Resources.Load<GameObject>("char_enemy2_4"));
       bossGameObjectObj.transform.position=new Vector3(10,2,0);
       bossGameObjectObj.GetComponent<StrongEnemyController>().EnemyType = EnemyTypeNow;
        if (isFirstPlay)
        {
            Titletext.text = "Shooting Game 1";
            Time.timeScale = 0;
            GameOverText.text = "Press Space to start the game";
        }
        IsOver = false;
        HighScore.text = _point.ToString();
        _point = 0;
        PointUpdate(0);
    }

    public void lesserEnemyDeath()
    {
        PointUpdate(20);
    }
    public void StrongEnemyDeath()
    {
        PointUpdate(EnemyTypeNow*EnemyTypeNow*1000);
        EnemyTypeNow++;
        StrongEnemyGenerate();
    }

    private void StrongEnemyGenerate()
    {
        if (EnemyTypeNow%2==0)
        {
            var bossGameObjectObj= Instantiate(Resources.Load<GameObject>("char_enemy2_10"));
            bossGameObjectObj.transform.position=new Vector3(10,2,0);
            bossGameObjectObj.GetComponent<StrongEnemyController>().EnemyType = EnemyTypeNow;
        }
        else
        {
            var bossGameObjectObj= Instantiate(Resources.Load<GameObject>("char_enemy2_4"));
            bossGameObjectObj.transform.position=new Vector3(10,2,0);
            bossGameObjectObj.GetComponent<StrongEnemyController>().EnemyType = EnemyTypeNow;
        }
        
    }
    public void GameOver()
    {
        StartCoroutine(WaitBullet());
    }

    IEnumerator WaitBullet()
    {
        yield return new WaitForSeconds(waittime);
        GameOverText.text = $"GameOver \nYour Score {_point}\n";
        if (int.Parse(HighScore.text) < _point) HighScore.text = _point.ToString();
        else _point = int.Parse(HighScore.text);
        IsOver = true;
    }

    public void PointUpdate(int addpoint)
    {
        _point += addpoint;
        _pointviewer.text = _point.ToString();
    }

    private void Update()
    {
        if ((Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.R))&&IsOver)
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKey(KeyCode.Space) && isFirstPlay)
        {
            isFirstPlay = false;
            Time.timeScale = 1;
            GameOverText.text = "";
            Titletext.text = "";
        }
    }
}
