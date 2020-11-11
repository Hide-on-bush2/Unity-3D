using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FirstController : MonoBehaviour, ISceneController, IUserAction
{
    public FlyActionManager action_manager;
    public DiskFactory disk_factory;
    public UserGUI user_gui;

    private Queue<GameObject> disk_queue = new Queue<GameObject>();          //飞碟队列
    private List<GameObject> disk_notshot = new List<GameObject>();          //没有被打中的飞碟队列
    private int round = 1;                                                   
    private float speed = 2f;                                                //发射时间间隔
    private bool playing_game = false;                                       
    private bool game_over = false;                                          
    private bool game_start = false;                                        
    private int score_round2 = 10;                                           
    private int score_round3 = 20;                                          

    void Start()
    {
        SSDirector director = SSDirector.GetInstance();
        director.CurrentScenceController = this;
        disk_factory = Singleton<DiskFactory>.Instance;
        action_manager = gameObject.AddComponent<FlyActionManager>() as FlyActionManager;
        user_gui = gameObject.AddComponent<UserGUI>() as UserGUI;
    }

    void Update()
    {
        if (game_start)
        {
            if (game_over)
            {
                CancelInvoke("LoadResources");
            }
            if (!playing_game)
            {
                InvokeRepeating("LoadResources", 1f, speed);
                playing_game = true;
            }
            SendDisk();
            if (user_gui.score >= score_round2 && round == 1)
            {
                round = 2;
                speed = speed - 0.6f;
                CancelInvoke("LoadResources");
                playing_game = false;
            }
            else if (user_gui.score >= score_round3 && round == 2)
            {
                round = 3;
                speed = speed - 0.5f;
                CancelInvoke("LoadResources");
                playing_game = false;
            }
        }
    }

    public void LoadResources()
    {
        disk_queue.Enqueue(disk_factory.GetDisk(round));
    }

    private void SendDisk()
    {
        float position_x = 16;
        if (disk_queue.Count != 0)
        {
            GameObject disk = disk_queue.Dequeue();
            disk_notshot.Add(disk);
            disk.SetActive(true);
            float ran_y = 6;
            float ran_x = Random.Range(-1f, 1f) < 0 ? -1 : 1;
            if (round == 2)
            {
                disk.GetComponent<DiskComponent>().score = 2;
                float diff = Random.Range(1, 5);
                ran_y -= diff;   
            }
            else if(round == 3)
            {
                disk.GetComponent<DiskComponent>().score = 3;
                float diff = Random.Range(6, 9);
                ran_y -= 6;
            }
            disk.GetComponent<DiskComponent>().direction = new Vector3(ran_x, ran_y, 10);
            Vector3 position = new Vector3(-disk.GetComponent<DiskComponent>().direction.x * position_x, ran_y, 15);
            disk.transform.position = position;
            float power = Random.Range(10f, 15f);
            float angle = Random.Range(0, 0);
            bool flag = false;
            action_manager.UFOFly(disk, angle, power, flag);
        }

        for (int i = 0; i < disk_notshot.Count; i++)
        {
            GameObject temp = disk_notshot[i];
            if (temp.transform.position.y < -10 && temp.gameObject.activeSelf == true)
            {
                disk_factory.FreeDisk(disk_notshot[i]);
                disk_notshot.Remove(disk_notshot[i]);
                user_gui.ReduceBlood();
            }
        }
    }

    public void Hit(Vector3 pos)
    {
        Ray ray = Camera.main.ScreenPointToRay(pos);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray);
        bool not_hit = false;
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            if (hit.collider.gameObject.GetComponent<DiskComponent>() != null)
            {
                for (int j = 0; j < disk_notshot.Count; j++)
                {
                    if (hit.collider.gameObject.GetInstanceID() == disk_notshot[j].gameObject.GetInstanceID())
                    {
                        not_hit = true;
                    }
                }
                if (!not_hit)
                {
                    return;
                }
                disk_notshot.Remove(hit.collider.gameObject);
                user_gui.Record(hit.collider.gameObject);
                //回收飞碟
                StartCoroutine(WaitingParticle(0.08f, hit, disk_factory, hit.collider.gameObject));
                break;
            }
        }
    }

    public int GetScore()
    {
        return user_gui.score;
    }

    public void ReStart()
    {
        game_over = false;
        playing_game = false;
        user_gui.score = 0;
        round = 1;
        speed = 2f;
    }

    public void GameOver()
    {
        game_over = true;
    }

    IEnumerator WaitingParticle(float wait_time, RaycastHit hit, DiskFactory disk_factory, GameObject obj)
    {
        yield return new WaitForSeconds(wait_time);
        hit.collider.gameObject.transform.position = new Vector3(0, -9, 0);
        disk_factory.FreeDisk(obj);
    }
    public void BeginGame()
    {
        game_start = true;
    }
}