using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;


public class NewAnimatorPlayer : MonoBehaviour
{
    public Animator animatorRef;

    public int currentclipid;
    public int layer;
    public bool IsneedRootmotion;
    private AniParams StateParams;

    public bool IsInTrans;
    //private float _frame;
    public bool IsPlayFinish;
    public float mulval = 1f;
    public float normalizedtranstime = 0.1f;
    public AnimatorStateInfo nextclipinfo;
    public AnimatorTransitionInfo currenttransinfo;

    public void Play(int animationid, float rate, int layindex,bool needrootmotion)
    {
        IsneedRootmotion = needrootmotion;


        //Debug.Log("status"+animatorRef.IsInTransition(layindex)+animatorRef.GetCurrentAnimatorStateInfo(layindex).shortNameHash);
        if (animatorRef.IsInTransition(layindex))
        {
            IsInTrans = true;
            return;
        }
        else
            IsInTrans = false;
        layer = layindex;
        animatorRef.enabled = true; 
        animatorRef.applyRootMotion = true;
        animatorRef.speed = rate*mulval;
        animatorRef.Play(animationid, layindex);
        var clipinfo = animatorRef.GetCurrentAnimatorStateInfo(layindex);
        //Debug.Log(clipinfo.normalizedTime+"loop"+clipinfo.loop);
        var lasting = clipinfo.normalizedTime;
        if (clipinfo.loop) lasting %= 1;
        //  Debug.Log(_currentAnimationName+"normaltime"+clipinfo.normalizedTime);
        if (lasting>= 1.0f)
        {
            IsPlayFinish = true;
            
        }
        else
        {
            IsPlayFinish = false;
        }
      
    }

    public void Changeframe(int targetfameid, float transtime,bool needrootmotion,int layidx)
    {

        //animatorRef.PlayInFixedTime(targetfameid, layidx);
        IsneedRootmotion = needrootmotion;

        layer = layidx;
        IsPlayFinish = false;
        animatorRef.CrossFade(targetfameid, normalizedtranstime, layidx);
        currentclipid = targetfameid;
        //if (transtime <= 0)
        //{

        //    animatorRef.PlayInFixedTime(targetfameid, layidx);
        //}
        //else
        //{
        //    //animatorRef.CrossFadeInFixedTime(targetfameid, normalizedtranstime, layidx);
        //    animatorRef.CrossFade(targetfameid, normalizedtranstime, layidx);
        //    //animatorRef.CrossFade(targetfameid, currenttransinfo.duration, layidx, nextclipinfo.normalizedTime, normalizedtranstime);
        //}

        //if (animatorRef.IsInTransition(layidx)) return;
        //else currentclipid = targetfameid;
    }


    public int GetCilpId
    {
        get
        {
            return currentclipid;
        }
    }

    void OnAnimatorMove()
    {
        //var clipinfo = an.GetCurrentAnimatorStateInfo(1)

        //var cliipname = an.GetCurrentAnimatorClipInfo(1)[0].clip.name;
        //var hs= an.GetCurrentAnimatorStateInfo(1).shortNameHash;
        var clipinfo = animatorRef.GetCurrentAnimatorStateInfo(layer);
        if (clipinfo.shortNameHash == currentclipid && IsneedRootmotion)
        {
            
            animatorRef.ApplyBuiltinRootMotion(); 
        }

        //Debug.Log("clipinfo.shortNameHash euqal"+ (clipinfo.shortNameHash == currentclipid)+clipinfo.shortNameHash+"needroot?"+IsneedRootmotion);
        //if (clipinfo.IsName("RotateInv") || clipinfo.IsName("RotatePlus"))
        //{
        //    _animatorRef.ApplyBuiltinRootMotion();
        //}
        //DataSaveManager<AniParams>.Instance.LoadData("JsonData/animation",
        //    _currentAnimationName + ".json",
        //    ref StateParams);
        //if (StateParams.needrootmotion)
        //{
        //    _animatorRef.ApplyBuiltinRootMotion();
        //}

    }
}