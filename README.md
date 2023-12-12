# CodeName-Prototype
&amp;emsp;&amp;emsp;《代号：原型》是一个非商用、自创背景的TPS箱庭式探险游戏，游戏内所有编辑器扩展和程序框架均由本人开发完成，**未使用任何插件**。开发的框架涵盖了**工具管线、3C、UI、武器MVP、动画状态机、交互系统、程序化Boss运动、异步时钟以及基建**等方面，游戏Gameplay流程完整、构建了较为完整的多人协作工作流。网络功能也在后续的计划之中。
本游戏自主开发的框架工具简介如下：
1. 敌人AI：构建了节点可视化的行为树编辑器，利用Json进行本地持久化存储，支持Editor模式下新建节点，可视
化配置节点参数；Runtime模式下可视化观察行为树节点运行状态：通过节点运行的不同状态显示不同颜色。支
持自定义新节点，只需修改4个文件即可添加。实现了BossAi，巡逻Ai，守卫Ai以及Boss自动刷小怪的Ai。
2. 技能编辑器：类似UE蓝图系统的节点可视化的技能编辑器，除了支持行为树编辑器的全部功能之外，基于反射
实现了数据流的绑定，用户可以自定义数据Port的数目；支持自定义新节点，仅需修改4个文件。利用技能编辑
器实现了4个技能。
3. 对话系统：实现了基于Excel配表的对话系统，支持任意数量的对话和选项按钮
4. 任务系统：自主构建了Excel转json的脚本，结合Visitor设计模式实现了Excel配表的任务系统
5. 基于UITookit的MVVM架构的UI框架：将UI界面和Model彻底解耦，通过数据劫持实现了数据的双向绑定，支
持CSS动画，并显著减小了DrawCall
6. 基于运动学的PlayerControler：在不使用Unity的物理系统（射线除外）下，基于纯运动学开发了角色控制
器。支持斜坡运动、障碍检测、跳跃、蹲下及蹲下后运动，以及射击时的Locomotion和自由运动时的
Locomotion
7. 基于向量旋转的Camera Controler：实现类似Cinemachine的FreeLook相机的功能以及释放技能大招的相机
切换（第一人称相机也有实现，留作后续开镜画面开发的预留）
8. 基于MVP的切枪武器系统：利用IK实现了手枪和步枪的切换，并且使用对象池管理子弹实例
9. 基于发布-订阅模式的单例总线系统：基于C#标准委托EventHandler和EventArgs，利用泛型扩展了无参订阅和
有参订阅，作为模块间解耦后通信的强力基建
10. 基于UniTask的异步时钟系统：异步检测作为替代协程延时的一种方案，在本游戏中主要用于按键长短按的检测
和部分延时任务
11. 基于IK的Boss程序化运动：受限于Boss的动画不好找，采用程序化动画进行替代
12. 基于状态机的动画系统：为了避免Animator的“蜘蛛网”地狱，自主实现了状态机，并利用CrossFade进行动画间
的过渡
13. 角色交互系统：支持捡起物体和丢弃物体、支持触发对话等
