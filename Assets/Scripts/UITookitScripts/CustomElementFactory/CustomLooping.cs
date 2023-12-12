using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

public class CustomLooping : VisualElement
{
    [Preserve]
    public new class UxmlFactory : UxmlFactory<CustomLooping, UxmlTraits> { }
    [Preserve]
    public new class UxmlTrails : VisualElement.UxmlTraits { }

    private string UssSource = "CustomUIToolket/Uss/CssAniToUss";
    private StyleSheet usstemplate;
    private VisualElement _customElement;
   
    private string _basesta = "hvr-back-pulse";
    private string hoversta = "hvr-back-pulse--hover";
    private string loopnext = "hvr-back-pulse--next";
    public CustomLooping()
    {
        usstemplate = Resources.Load<StyleSheet>(UssSource);
        styleSheets.Add(usstemplate);
        _customElement = new VisualElement();
        Add(_customElement);
        _customElement.AddToClassList(_basesta);
        _customElement.RegisterCallback<MouseEnterEvent>(OnMouseIn);
        _customElement.RegisterCallback<MouseLeaveEvent>(OnMouseOut);
    }

    private void OnMouseOut(MouseLeaveEvent evt)
    {
        _customElement.UnregisterCallback<TransitionEndEvent>(OnAniLoop);
        _customElement.ClearClassList();
        _customElement.ToggleInClassList(_basesta);
    }

    private void OnMouseIn(MouseEnterEvent evt)
    {
        _customElement.RegisterCallback<TransitionEndEvent>(OnAniLoop);

        Debug.Log("trigger in");
        _customElement.ToggleInClassList(hoversta);
        
    }

    private void OnAniLoop(TransitionEndEvent evt)
    {
        _customElement.ToggleInClassList(hoversta);
        _customElement.ToggleInClassList(loopnext);
    }
}
