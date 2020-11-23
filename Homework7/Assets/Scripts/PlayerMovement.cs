using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Collider2D coll;
    Animator anim;

    public float speed;
    Vector2 movement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //coll = GetComponent<Collider2D>();
        //anim = GetComponent<Animator>();
        //rb.velocity = new Vector2(1,1);
    }

    private void Update()
    {
        Movement();
        //rb.velocity = new Vector2(1, 1);
        //SwitchAnim();
    }

    void Movement()//移动
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        //rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
        rb.velocity = new Vector2(movement.x, movement.y);
    }

    void SwitchAnim()//切换动画
    {
        if (movement != Vector2.zero)//保证Horizontal归0时，保留movment的值来切换idle动画的blend tree
        {
            anim.SetFloat("horizontal", movement.x);
            anim.SetFloat("vertical", movement.y);
        }
        anim.SetFloat("speed", movement.magnitude);//magnitude 也可以用 sqrMagnitude 具体可以参考Api 默认返回值永远>=0
    }
}