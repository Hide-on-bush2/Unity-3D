using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskFactory : MonoBehaviour
{
    public GameObject disk_prefab = null;                 //飞碟预制
    private static DiskFactory _instance;
    private List<DiskComponent> used = new List<DiskComponent>();   
    private List<DiskComponent> free = new List<DiskComponent>();   

    public static DiskFactory getInstance()
    {
        if (_instance == null)
        {
            _instance = new DiskFactory();
        }
        return _instance;
    }

    public GameObject GetDisk(int round)
    {
        float start_y = 7;                           
        string tag;
        disk_prefab = null;

        if (round == 1)
        {
            tag = "disk1";
        }
        else if (round == 2)
        {
            tag = "disk2";
        }
        else
        {
            tag = "disk3";
        }
        
        for (int i = 0; i < free.Count; i++)
        {
            if (free[i].tag == tag)
            {
                disk_prefab = free[i].gameObject;
                free.Remove(free[i]);
                break;
            }
        }

        if (disk_prefab == null)
        {
            if (tag == "disk1")
            {
                disk_prefab = Instantiate(Resources.Load<GameObject>("Prefabs/disk1"), new Vector3(0, start_y, 10), Quaternion.identity);
            }
            else if (tag == "disk2")
            {
                disk_prefab = Instantiate(Resources.Load<GameObject>("Prefabs/disk2"), new Vector3(0, start_y, 10), Quaternion.identity);
            }
            else
            {
                disk_prefab = Instantiate(Resources.Load<GameObject>("Prefabs/disk3"), new Vector3(0, start_y, 10), Quaternion.identity);
            }
        }
        used.Add(disk_prefab.GetComponent<DiskComponent>());
        return disk_prefab;
    }

    public void FreeDisk(GameObject disk)
    {
        for (int i = 0; i < used.Count; i++)
        {
            if (disk.GetInstanceID() == used[i].gameObject.GetInstanceID())
            {
                used[i].gameObject.SetActive(false);
                free.Add(used[i]);
                used.Remove(used[i]);
                break;
            }
        }
    }
}