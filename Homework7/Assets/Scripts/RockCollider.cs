using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockCollider : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            Singleton<EventManager>.Instance.CollectRock(this.gameObject.GetComponent<RockData>().ID);
        }
        
    }

    
}
