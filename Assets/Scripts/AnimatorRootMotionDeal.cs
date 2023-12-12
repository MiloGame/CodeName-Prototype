using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class AnimatorRootMotionDeal: MonoBehaviour
{
    public Animator an;
    public AniParams StateParams;

    public string filename;
    // Start is called before the first frame update
    public void SetfileName(string name)
    {
        filename = name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnAnimatorMove()
    {
        //var clipinfo = an.GetCurrentAnimatorStateInfo(1)

        //var cliipname = an.GetCurrentAnimatorClipInfo(1)[0].clip.name;
        //var hs= an.GetCurrentAnimatorStateInfo(1).shortNameHash;
        
        DataSaveManager<AniParams>.Instance.LoadData("JsonData/animation",
            filename + ".json",
            ref StateParams);
        Debug.Log("namename " + filename + StateParams.needrootmotion);
        if (StateParams.needrootmotion)
        {
            an.ApplyBuiltinRootMotion();
        }
        
    }
}
