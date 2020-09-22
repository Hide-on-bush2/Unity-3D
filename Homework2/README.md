# Homework2

## 简答题 

### 解释游戏对象（GameObjects）和资源（Assets）的区别与联系

区别 ：
* 资源（asset）就是可以在我们的项目中使用的文件，包括图像、视频、脚本文件、预制文件等，它们的存在不依赖于Unity
* 对象（Object）是Unity创建的实例，你可以在inspector窗口中调整它们的属性。其中游戏对象（GameObject），出现在场景中的所有物体都是GameObject，GameObject按照一定的层次结构组织起来，显示在Hierarchy窗口中

联系：
* 对象可以通过资源来保存起来，资源可以用来创建对象实例，一个资源可以创建多个对象

### 下载几个游戏案例，分别总结资源、对象组织的结构（指资源的目录组织结构与游戏对象树的层次结构）

每个Unity的项目包含一个资源文件夹。此文件夹的内容呈现在项目视图。这里存放着游戏的所有资源，在资源文件夹中，通常有对象、材质、场景、声音、预设、贴图、脚本、动作，在这些文件夹下可以继续进行划分。  

游戏对象树层次视图包含了每一个当前场景的所有游戏对象。其中一些是资源文件的实例，如3D模型和其他预制物体的实例。可以在层次结构视图中选择对象或者生成对象。当在场景中增加或者删除对象，层次结构视图中相应的对象则会出现或消失。想让一个游戏对象成为另一个的子对象，只需在层次视图中把它拖到另一个上即可。一个子对象将继承其父对象的移动和旋转属性

### 编写一个代码，使用debug语句来验证MonoBehaviour基本行为或事件触发的条件

* 基本行为包括`Awake()`,`Start()`,`Update()`,`FixedUpdate()` ,`LateUpdate()`
* 常用事件包括`OnGUI()`,`OnDisable()`,`OnEnable()`

## 计算器代码

```
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caculator : MonoBehaviour
{

    private string res = "";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool reset = false;
    
    void OnGUI()
    {
        // 创建背景板 
        GUI.Box(new Rect(200, 10, 230, 310), "");
        
        // 创建显示区域
        GUI.TextField(new Rect(215, 15, 205, 25), res);
        
        // 创建数字 
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (GUI.Button(new Rect(210 + 50 * j + j * 5, 260 - 50 * (i + 1) - i * 5, 50, 50),
                    "" + (i * 3 + j + 1)))
                {
                    if (reset)
                    {
                        res = "";
                        reset = false;
                    }
                    res += "" + (i * 3 + j + 1);
                }
            }
        }
        
        // 创建C, <, /, X
        if (GUI.Button(new Rect(210, 45, 50, 50), "C"))
        {
            res = "";
        }

        if (GUI.Button(new Rect(265, 45, 50, 50), "<"))
        {
            res = res.Substring(0, res.Length-1);
        }

        if (GUI.Button(new Rect(320, 45, 50, 50), "/"))
        {
            res += "/";
        }

        if (GUI.Button(new Rect(375, 45, 50, 50), "X"))
        {
            res += "X";
        }

        // 创建-, +, =
        if (GUI.Button(new Rect(375, 100, 50, 50), "-"))
        {
            res += "-";
        }

        if (GUI.Button(new Rect(375, 155, 50, 50), "+"))
        {
            res += "+";
        }

        if (GUI.Button(new Rect(375, 210, 50, 105), "="))
        {
            int tmp = Cal(res);
            res = Convert.ToString(tmp);
            reset = true;
        }
        
        // 创建0, .
        if (GUI.Button(new Rect(210, 265, 100, 50), "0"))
        {
            if (reset)
            {
                res = "";
                reset = false;
            }
            res += "0";
        }

        if (GUI.Button(new Rect(320, 265, 50, 50), "."))
        {
            res += ".";
        }
    }

    private bool isNumber(string str)
    {
        foreach (char c in str)
        {
            if ((c < '0' || c > '9') && c != '.')
            {
                return false;
            }
        }

        return true;
    }


    private class node
    {
        public string val;
        public node left;
        public node right;

        public node(string v)
        {
            val = v;
            left = null;
            right = null;
        }
    }

    private node createTree(string str)
    {
        
        if (isNumber(str))
        {
            return new node(str);
        }

        if (str == "")
        {
            return null;
        }

        string left = "";
        string right = "";

        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] == '+' || str[i] == '-')
            {
                for (int j = 0; j < i; j++)
                {
                    left += str[j];
                }

                for (int j = i + 1; j < str.Length; j++)
                {
                    right += str[j];
                }

                node new_node = new node(str[i].ToString());
                new_node.left = createTree(left);
                new_node.right = createTree(right);
                return new_node;
            }
        }
        
        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] == 'X' || str[i] == '/')
            {
                for (int j = 0; j < i; j++)
                {
                    left += str[j];
                }

                for (int j = i + 1; j < str.Length; j++)
                {
                    right += str[j];
                }

                node new_node = new node(str[i].ToString());
                new_node.left = createTree(left);
                new_node.right = createTree(right);
                return new_node;
            }
        }

        return null;
    }

    
    
    private int calculate(node root)
    {
        if (root == null)
        {
            return 0;
        }

        if (isNumber(root.val))
        {
            return int.Parse(root.val);
        }

        if (root.val == "+")
        {
            return calculate(root.left) + calculate(root.right);
        }

        if (root.val == "-")
        {
            return calculate(root.left) - calculate(root.right);
        }

        if (root.val == "X")
        {
            return calculate(root.left) * calculate(root.right);
        }

        if (root.val == "/")
        {
            return calculate(root.left) / calculate(root.right);
        }

        return 0;
    }

    private int Cal(string str)
    {
        return calculate(createTree(str));
    }
}

```