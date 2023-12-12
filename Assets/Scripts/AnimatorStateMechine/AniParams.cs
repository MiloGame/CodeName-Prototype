using System;


[Serializable]
public class AniParams
{
    public int layer = 0; //动画位于哪层
    public float rate = 1; //动画播放速率
    public string FrameName; //设计的动画树的名字
    public float TransitionSpeed = 0; //切换到本动画的速度，0代表立即切换，1代表很慢，很想animator转台转换的max num属性
    public bool needrootmotion;
}