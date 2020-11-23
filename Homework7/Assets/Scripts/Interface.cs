using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneController
{
    void LoadResources();
    void Pause();
    void Resume();
}

public enum SSActionEventType : int { Started,Competeted}

public interface ISSActionCallback
{
    void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Competeted, int intParam = 0, Object objectParam = null);
}

public interface IUserAction
{
    FirstController.Status getStatus();
}