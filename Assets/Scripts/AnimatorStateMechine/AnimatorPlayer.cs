using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;


public class AnimatorPlayer : MonoBehaviour
{
    public Animator _animatorRef;


    private string _currentAnimationName;
    private AniParams StateParams;

    //private float _frame;
    public bool IsPlayFinish { get; private set; }

    //public AnimatorPlayer(Animator am)
    //{
    //    _animatorRef = am;
    //   // _animatorRef.enabled = false;//如果不关闭会影响下落动画,原因是手动调用update
    //    _animatorRef.applyRootMotion = false;
        
    //}

    public Animator GetAnimator
    {
        get { return _animatorRef; }
    }

    public void Play(string animationname, float rate, int layindex,bool needrootmotion)
    {
        Debug.Log("speed"+rate);
        _currentAnimationName = animationname;
        //if (needrootmotion)
        //{
            _animatorRef.enabled = true; 
            _animatorRef.applyRootMotion = true;
            _animatorRef.speed = rate;
            _animatorRef.Play(animationname, layindex);


        //}
        //else
        //{
        //    //  _animatorRef.enabled = false; //如果不关闭会影响下落动画,原因是手动调用update
        //    _animatorRef.applyRootMotion = false;

        //    _animatorRef.Update(rate * Time.deltaTime);
        //    // _animatorRef.speed = rate;

        //}
        var clipinfo = _animatorRef.GetCurrentAnimatorStateInfo(layindex);
       //  Debug.Log(_currentAnimationName+"normaltime"+clipinfo.normalizedTime);
        if (clipinfo.normalizedTime >= 1.0f)
        {
            IsPlayFinish = true;
        }
        else
        {
            IsPlayFinish = false;
        }
        // var clipinfo = _animatorRef.GetCurrentAnimatorClipInfo(layindex)[0];
        // _frame = clipinfo.weight * (clipinfo.clip.length * clipinfo.clip.frameRate);
        //bool over = _actionframes[_currentAnimation].NextFrame < 0 &&
        //            _frame >= _actionframes[_currentAnimation].End;
        //if (over)
        //{
        //    int indexOldFrame = _currentAnimation;
        //    _currentAnimation = _actionframes[_currentAnimation].NextFrame;
        //}切换逻辑交给状态机
    }

    public void Changeframe(string targetframename, float transpeed)
    {
        _currentAnimationName = targetframename;
        IsPlayFinish = false;

        // Debug.Log(_currentAnimationName + "transpeed"+ transpeed+ "IsPlayFinish" + IsPlayFinish);
        if (transpeed <= 0)
        {
            _animatorRef.PlayInFixedTime(targetframename);
        }
        else
        {
            _animatorRef.CrossFadeInFixedTime(targetframename, transpeed);
        }
    }

    public string GetcurrentAnimationName
    {
        get { return _currentAnimationName; }
    }
    void OnAnimatorMove()
    {
        //var clipinfo = an.GetCurrentAnimatorStateInfo(1)

        //var cliipname = an.GetCurrentAnimatorClipInfo(1)[0].clip.name;
        //var hs= an.GetCurrentAnimatorStateInfo(1).shortNameHash;
        var clipinfo = _animatorRef.GetCurrentAnimatorStateInfo(1);
        if (clipinfo.IsName("RotateInv") || clipinfo.IsName("RotatePlus"))
        {
            _animatorRef.ApplyBuiltinRootMotion();
        }
        //DataSaveManager<AniParams>.Instance.LoadData("JsonData/animation",
        //    _currentAnimationName + ".json",
        //    ref StateParams);
        //if (StateParams.needrootmotion)
        //{
        //    _animatorRef.ApplyBuiltinRootMotion();
        //}

    }
}