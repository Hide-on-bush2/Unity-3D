using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskFlyAction : SSAction
{
    
    public float gravity = -5;                                 
    private Vector3 start_vector;                             
    private Vector3 gravity_vector = Vector3.zero;             
    private float time;                                       
    private Vector3 current_angle = Vector3.zero;

    public static DiskFlyAction GetSSAction(Vector3 direction, float angle, float power, bool flag)
    {
        Vector3 delta_vector = new Vector3(0, -3, 0);
        DiskFlyAction action = CreateInstance<DiskFlyAction>();
        if (flag)
        {
            action.start_vector += delta_vector;
        }
        else
        {
            if (direction.x == -1)
            {
                action.start_vector = Quaternion.Euler(new Vector3(0, 0, -angle)) * Vector3.left * power;
            }
            else
            {
                action.start_vector = Quaternion.Euler(new Vector3(0, 0, angle)) * Vector3.right * power;
            }
        }
        return action;
        }



    public override void Update()
    {
        time += Time.fixedDeltaTime;
        gravity_vector.y = gravity * time;

        transform.position += (start_vector + gravity_vector) * Time.fixedDeltaTime;
        current_angle.z = Mathf.Atan((start_vector.y + gravity_vector.y) / start_vector.x) * Mathf.Rad2Deg;
        transform.eulerAngles = current_angle;

        if (this.transform.position.y < -10)
        {
            this.destroy = true;
            this.callback.SSActionEvent(this);
        }
    }

    public override void Start() { }
}