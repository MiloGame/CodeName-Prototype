using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NPCUImanager : MonoBehaviour
{
    public Camera MainCamera;

    public UIDocument UiDocument;
    private VisualElement TabElement;


    private Label TaskIcon;
    private VisualElement RootEle;
    public float TaskIconHorOffset;
    public float TaskIconVerOffset;

    // Start is called before the first frame update
    void Start()
    {
        MainCamera = Camera.main;
        RootEle = UiDocument.rootVisualElement;
        TabElement = RootEle.Q<VisualElement>("Tab");
        TabElement.style.display = DisplayStyle.None;
        TaskIcon = RootEle.Q<Label>("TaskIcon");
        TaskIcon.style.display = DisplayStyle.None;
    }

    public void DisplayF()
    {
        TabElement.style.display = DisplayStyle.None;
    }
    public void SetFBarPos()
    {
        Debug.Log("SetBarPos");
        TabElement.style.display = DisplayStyle.Flex;
        Vector2 newPos = RuntimePanelUtils.CameraTransformWorldToPanel(TabElement.panel,
            transform.position + Vector3.up * 0.8f + Vector3.right * (-0.4f), MainCamera);
        TabElement.transform.position = new Vector2(newPos.x, newPos.y);
    }
    public void SetTaskIconPos()
    {
        //Debug.Log("SetBarPos");
        TaskIcon.style.display = DisplayStyle.Flex;
        //var screenpos = MainCamera.WorldToScreenPoint(transform.position + Vector3.up * TaskIconVerOffset + Vector3.right * TaskIconHorOffset);
        Vector2 newPos = RuntimePanelUtils.CameraTransformWorldToPanel(TaskIcon.panel,
            transform.position + Vector3.up * TaskIconVerOffset + Vector3.right * TaskIconHorOffset, MainCamera);
        TaskIcon.transform.position = new Vector2(newPos.x, newPos.y)/TaskIcon.transform.scale;

        //TaskIcon.transform.position = new Vector2(screenpos.x, Screen.height - screenpos.y);
        Debug.Log(TaskIcon.transform.position+"npc");
    }

    public void DisableTaskIcon()
    {
        TaskIcon.style.display = DisplayStyle.None;

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        TabElement.style.display = DisplayStyle.None;

    }
}
