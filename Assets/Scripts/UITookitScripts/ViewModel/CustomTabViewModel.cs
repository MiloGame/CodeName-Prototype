using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;

public class CustomTabViewModel : MonoBehaviour
{
    public UIDocument RootUiDocument;
    private VisualElement fill;
    private VisualElement ct;
    private VisualElement icon;
    private Label label;
    private VisualElement rootvisualEle;
    private bool iscapture=false;
    private VisualElement oth;
    public void OnEnable()
    {
        rootvisualEle = RootUiDocument.rootVisualElement;
        fill = rootvisualEle.Q<VisualElement>("Fill");
        ct = rootvisualEle.Q<VisualElement>("CustomTab");
        oth = rootvisualEle.Q<VisualElement>("other");
        //ct.RegisterCallback<MouseCaptureEvent>(OnCtCaptureMouse);
        //oth.RegisterCallback<MouseCaptureEvent>(OnOthCaptureMouse);
        //ct.RegisterCallback<MouseCaptureOutEvent>(OnCtReleaseMouse);
        //oth.RegisterCallback<MouseCaptureOutEvent>(OnOthReleaseMouse);
        //rootvisualEle.RegisterCallback<MouseCaptureEvent>(OnCaptureMouse);
        //rootvisualEle.RegisterCallback<MouseCaptureOutEvent>(OnReleaseMouse);

        ct.RegisterCallback<MouseEnterEvent>(OnMouseIn);
        ct.RegisterCallback<MouseLeaveEvent>(OnMouseOut);
        ct.RegisterCallback<ClickEvent>(OnClick);
        oth.RegisterCallback<ClickEvent>(OnOtherClick);
    }

    private void OnReleaseMouse(MouseCaptureOutEvent evt)
    {
        Debug.Log("click Other trigger" + evt.currentTarget + evt.target);

    }

    private void OnCaptureMouse(MouseCaptureEvent evt)
    {
        
        //if (evt.propagationPhase != PropagationPhase.AtTarget)
        //    return;
        var triggerelem = evt.currentTarget as VisualElement;
        //MouseCaptureController.ReleaseMouse(ct);
        if (triggerelem == oth)
        {
            Debug.Log("click Other trigger" + evt.currentTarget + evt.target);
            MouseCaptureController.ReleaseMouse(ct);
            MouseCaptureController.CaptureMouse(triggerelem);
        }
        else
        {
            Debug.Log("click Other trigger" + evt.currentTarget + evt.target);

            MouseCaptureController.ReleaseMouse(oth);
        }
    }

    private void OnOthReleaseMouse(MouseCaptureOutEvent evt)
    {
        Debug.Log("release mouse " + evt.target);

    }

    private void OnCtReleaseMouse(MouseCaptureOutEvent evt)
    {
        Debug.Log("release mouse " + evt.target);
        //fill.ToggleInClassList("fill-none");
        //fill.ToggleInClassList("fill-click");
    }

    private void OnOthCaptureMouse(MouseCaptureEvent evt)
    {
        Debug.Log("capture mouse" + evt.target);
    }

    private void OnCtCaptureMouse(MouseCaptureEvent evt)
    {
        Debug.Log("capture mouse" + evt.target);
        fill.ClearClassList();
        fill.ToggleInClassList("sweep-to-right--base");
        fill.ToggleInClassList("sweep-to-right--enable");

    }

    private void OnOtherClick(ClickEvent evt)
    {
        Debug.Log("click Other trigger" + evt.currentTarget + evt.target);
        if (evt.propagationPhase != PropagationPhase.AtTarget)
            return;
        var triggerelem = evt.currentTarget as VisualElement;
        //MouseCaptureController.ReleaseMouse(ct);
        //if (triggerelem == oth)
        //{
        //    MouseCaptureController.ReleaseMouse(ct);
        //    MouseCaptureController.CaptureMouse(triggerelem);
        //}
        //else
        //{
        //    MouseCaptureController.ReleaseMouse(oth);
        //}
        //fill.ClearClassList();
        fill.ToggleInClassList("sweep-to-right--enable");
    }




    private void OnClick(ClickEvent evt)
    {
        Debug.Log("click trigger"+evt.currentTarget+evt.target);
        if (evt.propagationPhase != PropagationPhase.AtTarget)
            return;
        var triggerele = evt.currentTarget as VisualElement;
        //if (triggerele == ct)
        //{
        //    MouseCaptureController.ReleaseMouse(oth);
        //    MouseCaptureController.CaptureMouse(triggerele);
        //}
        //else
        //{
        //    MouseCaptureController.ReleaseMouse(ct);
        //}
        fill.ToggleInClassList("sweep-to-right--enable");
        //fill.ToggleInClassList("sweep-to-right--base");
    }

    private void OnMouseOut(MouseLeaveEvent evt)
    {
        Debug.Log("mouse leave"+evt.currentTarget + evt.target);
      //  fill.ToggleInClassList("fill-normal");
        evt.StopPropagation();
    }

    private void OnMouseIn(MouseEnterEvent evt)
    {
        Debug.Log("enter tigger"+evt.currentTarget + evt.target);
      //  fill.ToggleInClassList("fill-normal");
        
        evt.StopPropagation();
    }



    public void OnDisable()
    {

    }

}
