using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoPatrolAction : SSAction
{
    private Vector2 target;
    private float speed;
    private enum targetSide { W, D, S, A };
    private targetSide direction;
    private PatrolData data;
    public static GoPatrolAction GetAction(float speed)
    {
        GoPatrolAction action = CreateInstance<GoPatrolAction>();
        action.speed = speed;
        //action.data = action.gameobject.GetComponent<PatrolData>();
        //action.direction = (targetSide)Random.Range(0, 4);
        //action.target = action.getTarget(action.direction);
        return action;
    }

    private Vector2 getTarget(targetSide dirction)
    {
        Vector2 target = new Vector2(0, 0);
        switch (direction)
        {
            case targetSide.W:
                target.x = Random.Range(data.Area_min_x - 0.2f, data.Area_max_x + 0.2f);
                target.y = data.Area_max_y + 0.3f;
                break;
            case targetSide.A:
                target.x = data.Area_min_x - 0.2f;
                target.y = Random.Range(data.Area_min_y - 0.2f, data.Area_max_y + 0.2f);
                break;
            case targetSide.S:
                target.x = Random.Range(data.Area_min_x - 0.2f, data.Area_max_x + 0.2f);
                target.y = data.Area_min_y - 0.2f;
                break;
            case targetSide.D:
                target.x = data.Area_max_x + 0.2f;
                target.y = Random.Range(data.Area_min_y - 0.2f, data.Area_max_y + 0.2f);
                break;
        }
        return target;
    }

    private bool isActive()
    {
        return data.active;
    }

    private bool outOfBounds()
    {
        Vector2 curr_pos = this.transform.position;
        return curr_pos.x <= data.Area_min_x || curr_pos.x >= data.Area_max_x || curr_pos.y <= data.Area_min_y || curr_pos.y >= data.Area_max_y;
    }

    public override void Update()
    {
        this.transform.position = Vector2.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
        if (outOfBounds())
        {
            SwitchDiection(data.ID);
        }
        if ((Vector2)this.transform.position == target || isActive())
        {
            this.destroy = true;
            this.callback.SSActionEvent(this, SSActionEventType.Competeted, 1, null);
        }
    }

    public void SwitchDiection(int id)
    {
        if(data.ID == id)
        {
            if(direction == targetSide.A)
            {
                direction = targetSide.W;
            }
            else
            {
                direction += 1;
            }
            target = getTarget(direction);
        }
    }

    public override void Start()
    {
        
        EventManager.PatrolSwitchChange += SwitchDiection;
        this.data = this.gameobject.GetComponent<PatrolData>();
        this.direction = (targetSide)Random.Range(0, 4);
        this.target = this.getTarget(this.direction);
    }
}
