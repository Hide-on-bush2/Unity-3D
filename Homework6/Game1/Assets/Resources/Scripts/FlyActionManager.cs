using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyActionManager : SSActionManager
{

    public DiskFlyAction fly;                            
    public FirstController scene_controller;            

    protected void Start()
    {
        scene_controller = (FirstController)SSDirector.GetInstance().CurrentScenceController;
        scene_controller.action_manager = this;
    }

    //飞行
    public void UFOFly(GameObject disk, float angle, float power, bool flag)
    {
        fly = DiskFlyAction.GetSSAction(disk.GetComponent<DiskComponent>().direction, angle, power,flag);
        this.RunAction(disk, fly, this);
    }
}