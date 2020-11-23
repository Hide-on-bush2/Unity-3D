using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour
{
    private IUserAction action;
    private GUIStyle over_style = new GUIStyle();
    public void Start()
    {
        action = Director.getInstance().currentSceneController as IUserAction;
        over_style.fontSize = 25;
    }
    public void OnGUI()
    {
        
        if(action.getStatus() == FirstController.Status.Win)
        {
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.width / 2 - 250, 100, 100), "恭喜胜利！", over_style);
        }
        else if(action.getStatus() == FirstController.Status.Loss)
        {
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.width / 2 - 250, 100, 100), "遗憾失败！", over_style);
        }
        //GUI.Label(new Rect(Screen.width / 2 - 50, Screen.width / 2 - 250, 100, 100), "恭喜胜利！", over_style);
    }
}
