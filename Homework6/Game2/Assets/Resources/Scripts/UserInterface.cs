using Scene;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class UserInterface : MonoBehaviour {

    private IUserAction action;
    private ISceneController scene;
    bool gameStart = false;
    int timeCount = 0;

    // Use this for initialization
    void Start () {
        action = SceneController.getInstance() as IUserAction;
        scene = SceneController.getInstance() as ISceneController;
	}
	
	void Update () {
        if (!gameStart)
        {
            action.getArrow();
            gameStart = true;
        }

        if (timeCount%75==0) action.getArrow();
        
           
        if (scene.ifReadyToShoot())
        {
            
            //左键松开，射出弓箭
            if (Input.GetMouseButtonUp(0))
            {
                Vector3 mousePos = Camera.main.ScreenPointToRay(Input.mousePosition).direction;
                action.shootArrow(mousePos);
                timeCount = 0;
            }
        }
        timeCount++;
	}
}
