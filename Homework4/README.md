# Homework4

## 基本操作演练

### 下载Fantasy Skybox FREE，构建自己的游戏场景

### 写一个简单的总结，总结游戏对象的使用

* GameObject的创建 
  * GameObject->3D Object->选择要创建的游戏对象
  * 在脚本中实例化：GameObject.CreatPrimitive
* GameObject的获取
  * 在Inspector中直接将游戏对象拖到对应的被设为public的对象栏中
  * 在脚本中用`Find`，`FindWithTag`等函数来获取对象
* 添加组件和修改组件
  * 在Inspectot中手动添加和删除，以及进行赋值、修改
  * 利用函数`AddComponent<>()`来添加组件
* 发送广播和信息
  * 主要是三个函数：
    * `SendMessage`
    * `BroadcastMessage`
    * `SendMessageUpwards`
* 克隆和预制
  * 使用`GameObject.Instantiate()`函数来用预制来实例化一个游戏对象
  * 制作预制，将游戏对象拖到Assets栏
* 运动、缩放、旋转
  * 直接改变物体的position
  * `transform.Translate(Vector3)`
  * `transform.position += Vector3`
* 销毁游戏对象：
  * `GameObject.Destroy`
  * `Destroy(gameObject)`

## 编程实践

