using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolCollide : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            Singleton<EventManager>.Instance.HeroCaughted();
        }
        else
        {
            Singleton<EventManager>.Instance.PatrolSwicth(this.gameObject.GetComponent<PatrolData>().ID);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            Singleton<EventManager>.Instance.CatchingHero(this.gameObject.GetComponent<PatrolData>().ID, collider.gameObject);
        }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Singleton<EventManager>.Instance.StopCatching(this.gameObject.GetComponent<PatrolData>().ID);
        }        
    }

}
