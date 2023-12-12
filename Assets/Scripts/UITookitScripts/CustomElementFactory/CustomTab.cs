using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

public class CustomTab : VisualElement
{
    [Preserve]
    public new class  UxmlFactory : UxmlFactory<CustomTab,UxmlTraits> { }
    [Preserve]
    public new class UxmlTrails : VisualElement.UxmlTraits { }

    private string UssSource = "CustomUIToolket/Uss/CustomTab";
    private StyleSheet usstemplate;
    private VisualElement _fill;
    private VisualElement _icon;
    private Label _label;
    private string _fillstylesheet = "fill-normal";
    private string _iconstylesheet = "icon-normal";
    private string _labelstylesheet = "label-normal";

    public CustomTab()
    {
        usstemplate = Resources.Load<StyleSheet>(UssSource);
        styleSheets.Add(usstemplate);
        
        _fill = new VisualElement();
        _fill.AddToClassList("sweep-to-right--base");
        Add(_fill);
        _icon = new VisualElement();
        _icon.AddToClassList(_iconstylesheet);
        Add(_icon);
        _label = new Label();
        _label.AddToClassList(_labelstylesheet);
        Add(_label);

        _fill.name = "Fill";
        _icon.name = "Icon";
        _label.name = "Label";
        _label.text = "This is menu word";
        _fill.pickingMode = PickingMode.Ignore;
        _label.pickingMode = PickingMode.Ignore;
        _icon.pickingMode = PickingMode.Ignore;
        

    }

 
}
