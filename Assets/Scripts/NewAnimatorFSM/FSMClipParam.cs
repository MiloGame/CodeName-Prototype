

using System;

[Serializable]
public class FSMClipParam 
{
    public FSMClipParam(int layer,float rate,int id,float transitionSpeed,bool needRootmotion)
    {
        this.layer = layer;
        this.rate = rate;
        this.clipid = id;
        this.needRootmotion = needRootmotion;
        this.TransitionTime = transitionSpeed;
    }
    public int layer ; //动画位于哪层
    public float rate; //动画播放速率
    public int clipid; //设计的动画树的hash
    public bool needRootmotion;
    public float TransitionTime = 0; //切换到本动画的时间
    
}
