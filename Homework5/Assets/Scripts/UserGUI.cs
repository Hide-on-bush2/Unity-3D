using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour {
    private IUserAction action;   
    //每个GUI的style
    GUIStyle bold_style = new GUIStyle();
    GUIStyle text_style = new GUIStyle();
    GUIStyle over_style = new GUIStyle();
    private bool game_start = false;

    void Start () {
        action = SSDirector.GetInstance().CurrentScenceController as IUserAction;
    }
	
	void OnGUI () {
        bold_style.normal.textColor = new Color(1, 0, 0);
        bold_style.fontSize = 16;
        text_style.normal.textColor = new Color(0, 0, 0, 1);
        text_style.fontSize = 16;
        over_style.normal.textColor = new Color(1, 0, 0);
        over_style.fontSize = 25;

        if (game_start) {

            GUI.Label(new Rect(Screen.width - 150, 5, 200, 50), "Score:"+ action.GetScore().ToString(), text_style);
            GUI.Label(new Rect(100, 5, 50, 50), "Round:" + action.GetRound().ToString(), text_style);
            GUI.Label(new Rect(180, 5, 50, 50), "Trial:" + action.GetTrial().ToString(), text_style);

            if (action.GetRound() == 3 && action.GetTrial() == 10) {
                GUI.Label(new Rect(Screen.width / 2 - 20, Screen.height / 2 - 100, 100, 100), "GameOver", over_style);
                GUI.Label(new Rect(Screen.width / 2 - 10, Screen.height / 2 - 50, 50, 50), "Your Score:" + action.GetScore().ToString(), text_style);
                if (GUI.Button(new Rect(Screen.width / 2 - 20, Screen.height / 2, 100, 50), "Restart")) {
                    action.ReStart();
                    return;
                }
                action.GameOver();
            }
        }
        else {
            GUI.Label(new Rect(Screen.width / 2 - 60, Screen.height / 2 - 100, 100, 100), "Hit UFO", over_style);
            GUI.Label(new Rect(Screen.width / 2 - 50,  Screen.height / 2 - 50, 100, 100), "Hit the UFO by click mouse", text_style);
            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2, 100, 50), "Start")) {
                game_start = true;
                action.ReStart();
            }
        }
    }
   
}
