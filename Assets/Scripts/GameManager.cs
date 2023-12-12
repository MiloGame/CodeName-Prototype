using System;
using System.Collections;
using System.Collections.Generic;using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameStatus Status;
    public PrefabManger PbManger;
    public CameraControler CameraControler;
    public GameUIManager GameUiManager;
    public FSMManager FsmManager;
    public PlayerControl PlayerControl;
    public MVP_GunPresenter MvpGunPresenter;
    public SkillManager SkillManager;
    void Awake()
    {
        GameUiManager.FreshAwake();
    }
    void Start()
    {
        Status = GameStatus.Initialization;
        EventBusManager.Instance.ParamScribe(EventBusManager.EventType.ChangeGameState,ChangeGameState);
        MvpGunPresenter.FreshStart();
        FsmManager.FreshStart();
        PbManger.FreshStart();
        GameUiManager.FreshStart();
        SkillManager.FreshStart();
    }

    private void ChangeGameState(object sender, EventArgs e)
    {
        var newGameStatus = e as CustomTEventData<GameStatus>;
        if (newGameStatus != null)
        {
            Status = newGameStatus.message;
        }

    }

    void Update()
    {
        switch (Status)
        {
            case GameStatus.Initialization:
                PbManger.FreshUpdate();
                break;
            case GameStatus.Running:
                
                PlayerControl.FreshUpdate();
                CameraControler.FreshUpdate();
                MvpGunPresenter.FreshUpdate();
                FsmManager.FreshUpdate();
                SkillManager.FreshUpdate();
                break;
            case GameStatus.GameOver:
                break;
            case GameStatus.ReStart:
                break;
            case GameStatus.Quit:
                break;
            case GameStatus.Victory:
                break;
            case GameStatus.Failure:
                break;
            case GameStatus.Pause:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        GameUiManager.FreshUpdate();

    }

    void LateUpdate()
    {

    }
}

public enum GameStatus
{
    Initialization,
    Running,
    GameOver,
    ReStart,
    Quit,
    Victory,
    Failure,
    Pause

}