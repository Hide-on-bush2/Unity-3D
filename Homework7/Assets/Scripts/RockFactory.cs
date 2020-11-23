using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockFactory : MonoBehaviour
{
    private Dictionary<int, GameObject> rocks;
    
    private int max_id = 0;
    private int curr_rocks = 10;

    public void Awake()
    {
        rocks = new Dictionary<int, GameObject>();
        EventManager.CollectRockChange += CollectRock;
        for(int i = 0;i < 10; i++)
        {
            createRock();
        }

    }

    private void CollectRock(int id)
    {
        Destroy(rocks[id]);
        if (--curr_rocks == 0)
        {
            Singleton<EventManager>.Instance.CollectAllRocks();
        }

    }

    public void Update()
    {
        //while (rocks.Count < 6)
        //{
        //    createRock();
            
        //}
        
    }

    void createRock()
    {
        GameObject rock = Instantiate(Resources.Load<GameObject>("prefabs/rock"));
        float minX = 0, minY = 0, maxX = 0, maxY = 0;
        int areaID = Random.Range(0, 10);

        switch (areaID)
        {
            case 0:
                minX = -3.05f;
                minY = 0.24f;
                maxX = -2.26f;
                maxY = 0.71f;
                break;
            case 1:
                minX = -1.7f;
                minY = 0.27f;
                maxX = -0.6f;
                maxY = 1.88f;
                break;
            case 2:
                minX = 0.08f;
                minY = 0.24f;
                maxX = 1.09f;
                maxY = 1.83f;
                break;
            case 3:
                minX = 3.15f;
                minY = 0.12f;
                maxX = 3.15f;
                maxY = 1.88f;
                break;
            case 4:
                minX = -3.05f;
                minY = 1.76f;
                maxX = -2.66f;
                maxY = 0.52f;
                break;
            case 5:
                minX = -3.05f;
                minY = -2.44f;
                maxX = -0.58f;
                maxY = -2.14f;
                break;
            case 6:
                minX = -1f;
                minY = 1.56f;
                maxX = -0.6f;
                maxY = -0.49f;
                break;
            case 7:
                minX = -0.09f;
                minY = -2.44f;
                maxX = -0.98f;
                maxY = -1.14f;
                break;
            case 8:
                minX = 0.13f;
                minY = -0.76f;
                maxX = 3.2f;
                maxY = -0.41f;
                break;
            case 9:
                minX = 2.82f;
                minY = 2.13f;
                maxX = 3.24f;
                maxY = -1.05f;
                break;
        }
        float randX = Random.Range(minX, maxX);
        float randY = Random.Range(minY, maxY);
        rock.transform.position = new Vector2(randX, randY);
        rock.GetComponent<RockData>().ID = max_id;

        

        rocks[max_id] = rock;
        max_id += 1;
    }
    public void DestroyAll()
    {
        for(int i = 0;i < max_id; i++)
        {
            GameObject rock = rocks[i];
            if(rock != null)
            {
                Destroy(rock);
            }
        }
    }

    private void OnEnable()
    {
        
    }

    
}
