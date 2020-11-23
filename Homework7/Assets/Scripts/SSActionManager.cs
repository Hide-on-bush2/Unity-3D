using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSActionManager : MonoBehaviour,ISSActionCallback
{
    private Dictionary<int, SSAction> actions = new Dictionary<int, SSAction>();
    private List<SSAction> waitingAdd = new List<SSAction>();
    private List<int> waitingDelete = new List<int>();

    public void Update()
    {
        foreach(SSAction ac in waitingAdd)
        {
            actions[ac.GetInstanceID()] = ac;
        }
        waitingAdd.Clear();

        foreach(KeyValuePair<int, SSAction> kv in actions)
        {
            SSAction ac = kv.Value;
            if (ac.destroy)
            {
                waitingDelete.Add(ac.GetInstanceID());
            }
            else if(ac.enable){
                ac.Update();
            }
        }

        foreach(int key in waitingDelete)
        {
            SSAction ac = actions[key];
            actions.Remove(key);
            Destroy(ac);
        }

        waitingDelete.Clear();
    }

    public void DestroyAll()
    {
        foreach (KeyValuePair<int, SSAction> kv in actions)
        {
            SSAction ac = kv.Value;
            ac.destroy = true;
        }
    }

    public void RunAction(GameObject gameobject, SSAction action,ISSActionCallback manager)
    {
        
        action.gameobject = gameobject;
        action.transform = gameobject.transform;
        action.callback = manager;
        waitingAdd.Add(action);
        action.Start();
    }

    public void SSActionEvent(SSAction source, SSActionEventType events, int intParam, Object objectParam)
    {
        if(intParam == 0)//玩家被抓住
        {
            
        }
        else if(intParam == 1)//巡逻兵碰到障碍物需要转向
        {

        }
        
    }
}
