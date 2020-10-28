using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneController {
    void LoadResources();                                  
}

public interface IUserAction {
    void Hit(Vector3 pos);
    float GetScore();
    int GetRound();
    int GetTrial();
    void GameOver();
    void ReStart();
}

public enum SSActionEventType : int { Started, Competeted }

public interface ISSActionCallback {
    void SSActionEvent(SSAction source, 
        SSActionEventType events = SSActionEventType.Competeted,
        int intParam = 0, string strParam = null, Object objectParam = null);
}
