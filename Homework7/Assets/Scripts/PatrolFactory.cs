using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PatrolFactory : MonoBehaviour
{
    private Dictionary<int, GameObject> patrols;
    private SSActionManager action_manager;
    private int max_id = 0;
    
    public void Awake()
    {
        patrols = new Dictionary<int, GameObject>();
        action_manager = new SSActionManager();

        EventManager.StartCatchingChange += startCatching;
        EventManager.StopCatchingChange += stopCatching;
        
    }

    public void Update()
    {
        while(max_id < 4){
            createPatrol();
            
        }
        this.action_manager.Update();
    }

    public void DestroyAll()
    {
        for (int i = 0; i < max_id; i++)
        {
            GameObject patrol = patrols[i];
            if (patrol != null)
            {
                Destroy(patrol);
            }
        }
        action_manager.DestroyAll();
    }

    void createPatrol()
    {
        GameObject patrol = Instantiate(Resources.Load<GameObject>("prefabs/patrol"));
        float minX = 0, minY = 0, maxX = 0, maxY = 0;
        
        if(max_id == 0)//左上角区域
        {
           
            
            minX = -1.7f;
            maxX = -0.5f;
            minY = 0.2f;
            maxY = 1.8f;
        }
        else if(max_id == 1)//右上角区域
        {

            minX = 0.1f;
            maxX = 1.1f;
            minY = 0.3f;
            maxY = 1.8f;
        }
        else if(max_id == 2)//左下角区域
        {
            
            minX = -3.0f;
            maxX = -2.6f;
            minY = -2.4f;
            maxY = -0.47f;
        }
        else//右下角区域
        {
            
            minX = -0.11f;
            maxX = 1.1f;
            minY = -2.4f;
            maxY = -0.47f;
        }
        float randX = Random.Range(minX, maxX);
        float randY = Random.Range(minY, maxY);
        patrol.transform.position = new Vector2(randX, randY);

        patrol.GetComponent<PatrolData>().Area_min_x = minX;
        patrol.GetComponent<PatrolData>().Area_max_x = maxX;
        patrol.GetComponent<PatrolData>().Area_min_y = minY;
        patrol.GetComponent<PatrolData>().Area_max_y = maxY;

        patrol.GetComponent<PatrolData>().ID = max_id;
        patrol.GetComponent<PatrolData>().active = false;

        GoPatrolAction action = GoPatrolAction.GetAction(0.3f);
        action_manager.RunAction(patrol, action, this.action_manager);

        patrols[max_id] = patrol;
        max_id += 1;
    }

    private void OnEnable()
    {
        //EventManager.StartCatchingChange += startCatching;
    }

    private void startCatching(int id, GameObject hero)
    {
        PatrolFollowAction action = PatrolFollowAction.GetAction(hero, 0.3f);
        patrols[id].GetComponent<PatrolData>().active = true;
        action_manager.RunAction(patrols[id], action, this.action_manager);
        //Debug.Log(id);
    }

    private void stopCatching(int id)
    {
        patrols[id].GetComponent<PatrolData>().active = false;
        GoPatrolAction action = GoPatrolAction.GetAction(0.3f);
        action_manager.RunAction(patrols[id], action, this.action_manager);
    }

    

}
