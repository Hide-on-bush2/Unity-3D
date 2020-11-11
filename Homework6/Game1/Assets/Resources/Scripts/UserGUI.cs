using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour
{
    private IUserAction action;
    public int life = 5;  
    public int score = 0;  

    GUIStyle bold_style = new GUIStyle();
    GUIStyle score_style = new GUIStyle();
    GUIStyle text_style = new GUIStyle();
    GUIStyle over_style = new GUIStyle();
    private int high_score = 0;            
    private bool game_start = false;       

    void Start()
    {
        action = SSDirector.GetInstance().CurrentScenceController as IUserAction;
    }

    public void Record(GameObject disk)
    {
        int temp = disk.GetComponent<DiskComponent>().score;
        score = temp + score;
    }

    void OnGUI()
    {
        bold_style.normal.textColor = new Color(0, 0, 0);
        bold_style.fontSize = 20;
        text_style.normal.textColor = new Color(0, 0, 1);
        text_style.fontSize = 20;
        score_style.normal.textColor = new Color(0, 1, 0);
        score_style.fontSize = 20;
        over_style.normal.textColor = new Color(0, 1, 1);
        over_style.fontSize = 25;

        if (game_start)
        {
            //射击
            if (Input.GetButtonDown("Fire1"))
            {
                Vector3 pos = Input.mousePosition;
                action.Hit(pos);
            }

            GUI.Label(new Rect(300, 300, 200, 50), "Score:", text_style);
            GUI.Label(new Rect(370, 300, 200, 50), action.GetScore().ToString(), score_style);
            GUI.Label(new Rect(Screen.width - 500, 300, 50, 50), "Blood:", text_style);
            GUI.Label(new Rect(Screen.width - 440, 300, 50, 50), life.ToString(), bold_style);

            //游戏结束
            if (life == 0)
            {
                high_score = high_score > action.GetScore() ? high_score : action.GetScore();
                GUI.Label(new Rect(Screen.width / 2 - 20, Screen.width / 2 - 250, 100, 100), "GameOver", over_style);
                GUI.Label(new Rect(Screen.width / 2 - 10, Screen.width / 2 - 200, 50, 50), "Best Score:", text_style);
                GUI.Label(new Rect(Screen.width / 2 + 50, Screen.width / 2 - 200, 50, 50), high_score.ToString(), text_style);
                if (GUI.Button(new Rect(Screen.width / 2 - 20, Screen.width / 2 - 320, 100, 50), "Restart"))
                {
                    life = 6;
                    action.ReStart();
                    return;
                }
                action.GameOver();
            }
        }
        else
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 20, Screen.width / 2 - 300, 100, 50), "Start"))
            {
                game_start = true;
                action.BeginGame();
            }
        }
    }
    public void ReduceBlood()
    {
        if (life > 0)
            life--;
    }
}