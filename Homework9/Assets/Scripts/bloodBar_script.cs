using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bloodBar_script : MonoBehaviour
{
    // 最大血量
    public float maxValue = 10.0f;
    // 当前血量
    public float curValue;
    private float step;
    public Slider BloodSlider;

    private void Start()
    {
        // 初始血量为1.0
        curValue = 1.0f;
        step = 1.0f;
    }

    void OnGUI()
    {
        GUI.HorizontalScrollbar(new Rect(25, 25, 300, 50), 0.0f, curValue, 0.0f, maxValue);

        if (GUI.Button(new Rect(45, 90, 50, 30), "补血"))
        {
            step += 1.0f;
            if (step > 10.0f)
            {
                step = 10.0f;
            }
        }
        if (GUI.Button(new Rect(225, 90, 50, 30), "扣血"))
        {
            step -= 1.0f;
            if (step < 0.0f)
            {
                step = 0.0f;
            }
        }
        curValue = Mathf.Lerp(curValue, step, 0.05f);
    }
}
