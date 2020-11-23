using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolFollowAction : SSAction
{
    public GameObject target;
    public float speed;

    public static PatrolFollowAction GetAction(GameObject target, float speed)
    {
        PatrolFollowAction action = ScriptableObject.CreateInstance<PatrolFollowAction>();
        action.target = target;
        action.speed = speed;
        return action;
    }

    private bool isActive()
    {
        return this.gameobject.GetComponent<PatrolData>().active;
    }

    public override void Update()
    {
        this.transform.position = Vector2.MoveTowards(this.transform.position, target.transform.position, speed * Time.deltaTime);
        if((Vector2)this.transform.position == (Vector2)target.transform.position || !isActive())
        {
            this.destroy = true;
            this.callback.SSActionEvent(this);
        }
    }

    public override void Start()
    {
        
    }
}
