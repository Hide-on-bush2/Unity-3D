using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 工厂
public class DiskFactory : MonoBehaviour {
    private List<Disk> used = new List<Disk>();
    private List<Disk> free = new List<Disk>();

    public GameObject GetDisk(int type) {
        GameObject disk_prefab = null;
        //寻找空闲飞碟,如果无空闲飞碟则重新实例化飞碟
        if (free.Count>0) {
            for(int i = 0; i < free.Count; i++) {
                if (free[i].type == type) {
                    disk_prefab = free[i].gameObject;
                    free.Remove(free[i]);
                    break;
                }
            }     
        }

        if(disk_prefab == null) {
            if(type == 1) {
                disk_prefab = Instantiate(
                Resources.Load<GameObject>("Prefabs/disk1"),
                new Vector3(0, -10f, 0), Quaternion.identity);
            }
            else if (type == 2) {
                disk_prefab = Instantiate(
                Resources.Load<GameObject>("Prefabs/disk2"),
                new Vector3(0, -10f, 0), Quaternion.identity);
            }
            else {
                disk_prefab = Instantiate(
                Resources.Load<GameObject>("Prefabs/disk3"),
                new Vector3(0, -10f, 0), Quaternion.identity);
            }

            disk_prefab.GetComponent<Renderer>().material.color = disk_prefab.GetComponent<Disk>().color;
        }

        used.Add(disk_prefab.GetComponent<Disk>());
        disk_prefab.SetActive(true);
        return disk_prefab;
    }

    public void FreeDisk() {
        for(int i=0; i<used.Count; i++) {
            if (used[i].gameObject.transform.position.y <= -10f) {
                free.Add(used[i]);
                used.Remove(used[i]);
            }
        }          
    }

    public void Reset() {
        FreeDisk();
    }

}
