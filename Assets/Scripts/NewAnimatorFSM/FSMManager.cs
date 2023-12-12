using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(FSMData))]
public class FSMManager : MonoBehaviour
{
    [SerializeField]
    public FSMBaseState aniRunState;
    public FSMBaseState PreState;
    
    public FSMData FsmData;

    private string loadpath = "JsonData/AniFSM";
    [SerializeField]
    private bool IsTransfer = true;

    // Start is called before the first frame update
    public void FreshStart()
    {
        string dirname = Path.Combine(Application.dataPath, loadpath);
        if (Directory.Exists(dirname))
        {
            var fileslist = Directory.GetFiles(dirname, "*");
            foreach (var s in fileslist)
            {
                File.Delete(s);
            }
           
           Directory.Delete(dirname);
           Directory.CreateDirectory(dirname);
        }
        else
        {
            Directory.CreateDirectory(dirname);

        }

        //´´½¨×´Ì¬
        aniRunState = FsmData.CreateState(FSMData.ClipType.Spawn, "Spawn");

    }

    //Update is called once per frame
    public void FreshUpdate()
    {
        
        //if (FsmData.PlayerModel.ShortPressFire && IsTransfer)
        //{
        //    Debug.Log("aim layer");
        //    FsmData.NaPlayer.animatorRef.SetLayerWeight(1,1);
        //    FsmData.NaPlayer.animatorRef.SetLayerWeight(0,0);
        //    PreState = aniRunState;
        //    aniRunState = FsmData.CreateState(FSMData.ClipType.NewState, "NewState");
        //    IsTransfer = false;
        //}
        //else if(!IsTransfer && !FsmData.PlayerModel.ShortPressFire)
        //{
        //    Debug.Log("mormal layer");
        //    FsmData.NaPlayer.animatorRef.SetLayerWeight(1, 0);
        //    FsmData.NaPlayer.animatorRef.SetLayerWeight(0, 1);
        //    IsTransfer = true;
        //    aniRunState = PreState;
        //    //aniRunState = FsmData.CreateState(FSMData.ClipType.Idle, "Idle");
        //}
        UpdateState(ref aniRunState);
    }

    private void UpdateState(ref FSMBaseState ani)
    {
        var nextState = ani.Exit();
        if (nextState != null)
        {
            ani = nextState;
        }

        ani.Enter();
        ani.RunUpdate();
    }
}
