using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyActionManager : SSActionManager {
    public DiskFlyAction fly;  
    public FirstController scene_controller;           

    protected void Start() {
        scene_controller = (FirstController)SSDirector.GetInstance().CurrentScenceController;
        scene_controller.action_manager = this;     
    }

    //飞碟飞行
    public void DiskFly(GameObject disk, float angle, float power) {
        int lor = 1;
        if (disk.transform.position.x > 0) lor = -1;
        fly = DiskFlyAction.GetSSAction(lor, angle, power);
        this.RunAction(disk, fly, this);
    }
}
