

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
    public int layer ; //����λ���Ĳ�
    public float rate; //������������
    public int clipid; //��ƵĶ�������hash
    public bool needRootmotion;
    public float TransitionTime = 0; //�л�����������ʱ��
    
}
