using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*动作管理基类*/
public class SSActionManager : MonoBehaviour, ISSActionCallback {
    private Dictionary<int, SSAction> actions = new Dictionary<int, SSAction>();    //动作字典
    private List<SSAction> waitingAdd = new List<SSAction>();                       //等待执行的动作列表
    private List<int> waitingDelete = new List<int>();                              //等待删除动作的key的列表                

    protected void Update() {
        //获取动作实例将等待执行的动作加入字典并清空待执行列表
        foreach (SSAction ac in waitingAdd) {
            actions[ac.GetInstanceID()] = ac;
        }
        waitingAdd.Clear();

        //对于字典中每一个pair，看是执行还是删除
        foreach (KeyValuePair<int, SSAction> kv in actions) {
            SSAction ac = kv.Value;
            if (ac.destroy) {
                waitingDelete.Add(ac.GetInstanceID());
            }
            else if (ac.enable) {
                ac.Update();
            }
        }

        //删除所有已完成的动作并清空待删除列表
        foreach (int key in waitingDelete) {
            SSAction ac = actions[key];
            actions.Remove(key);
            Object.Destroy(ac);
        }
        waitingDelete.Clear();
    }

    public void RunAction(GameObject gameobject, SSAction action, ISSActionCallback manager) {
        action.gameobject = gameobject;
        action.transform = gameobject.transform;
        action.callback = manager;
        waitingAdd.Add(action);
        action.Start();
    }

    public void SSActionEvent(
        SSAction source, SSActionEventType events = SSActionEventType.Competeted,
        int intParam = 0, string strParam = null, Object objectParam = null) {
    }
}
