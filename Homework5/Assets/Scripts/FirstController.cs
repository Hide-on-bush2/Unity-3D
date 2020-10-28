using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstController : MonoBehaviour, ISceneController, IUserAction {
    public FlyActionManager action_manager;
    public DiskFactory disk_factory;
    public UserGUI user_gui;
    public ScoreRecorder score_recorder;
    private int round = 1;                                                  
    private int trial = 0;
    //private float speed = 1f;                                             
    private bool running = false;

    void Start () {
        SSDirector director = SSDirector.GetInstance();     
        director.CurrentScenceController = this;
        disk_factory = Singleton<DiskFactory>.Instance;
        score_recorder = Singleton<ScoreRecorder>.Instance;
        action_manager = gameObject.AddComponent<FlyActionManager>() as FlyActionManager;
        user_gui = gameObject.AddComponent<UserGUI>() as UserGUI;
    }

    int count = 0;
	void Update () {
        if(running) {
            count++;
            if (Input.GetButtonDown("Fire1")) {
                Vector3 pos = Input.mousePosition;
                Hit(pos);
            }
            switch (round) {
                case 1: {
                        if (count >= 150) {
                            count = 0;
                            SendDisk(1);
                            trial += 1;
                            if (trial == 10) {
                                round += 1;
                                trial = 0;
                            }
                        }
                        break;
                    }
                case 2: {
                        if (count >= 100) {
                            count = 0;
                            if (trial % 2 == 0) SendDisk(1);
                            else SendDisk(2);
                            trial += 1;
                            if (trial == 10) {
                                round += 1;
                                trial = 0;
                            }
                        }
                        break;
                    }
                case 3: {
                        if (count >= 50) {
                            count = 0;
                            if (trial % 3 == 0) SendDisk(1);
                            else if(trial % 3 == 1) SendDisk(2);
                            else SendDisk(3);
                            trial += 1;
                            if (trial == 10) {
                                running = false;
                            }
                        }
                        break;
                    }
                default:break;
            } 
            disk_factory.FreeDisk();
        }
    }

    public void LoadResources() {
        disk_factory.GetDisk(round);
        disk_factory.FreeDisk();
    }

    private void SendDisk(int type) {
        //从工厂中拿一个飞碟
        GameObject disk = disk_factory.GetDisk(type);

        //飞碟位置
        float ran_y = 0;
        float ran_x = Random.Range(-1f, 1f) < 0 ? -1 : 1;
 
        //飞碟初始所受的力和角度
        float power = 0;
        float angle = 0;
        if (type == 1) {
            ran_y = Random.Range(1f, 5f);
            power = Random.Range(5f, 7f);
            angle = Random.Range(25f,30f);
        }
        else if (type == 2) {
            ran_y = Random.Range(2f, 3f);
            power = Random.Range(10f, 12f);
            angle = Random.Range(15f, 17f);
        }
        else {
            ran_y = Random.Range(5f, 6f);
            power = Random.Range(15f, 20f);
            angle = Random.Range(10f, 12f);
        }
        disk.transform.position = new Vector3(ran_x*16f, ran_y, 0);
        action_manager.DiskFly(disk, angle, power);
    }

    public void Hit(Vector3 pos) {
        Ray ray = Camera.main.ScreenPointToRay(pos);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray);
        for (int i = 0; i < hits.Length; i++) {
            RaycastHit hit = hits[i];
            if (hit.collider.gameObject.GetComponent<Disk>() != null) {
                score_recorder.Record(hit.collider.gameObject);
                hit.collider.gameObject.transform.position = new Vector3(0, -10, 0);
            }
        }
    }

    public float GetScore() {
        return score_recorder.GetScore();
    }

    public int GetRound() {
        return round;
    }

    public int GetTrial() {
        return trial;
    }

    //重新开始
    public void ReStart() {
        running = true;
        score_recorder.Reset();
        disk_factory.Reset();
        round = 1;
        trial = 1;
        //speed = 2f;
    }
    //游戏结束
    public void GameOver() {
        running = false;
    }
}
