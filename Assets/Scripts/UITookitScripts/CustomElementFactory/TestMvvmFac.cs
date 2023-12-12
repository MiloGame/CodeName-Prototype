using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

public class TestMvvmFac : VisualElement
{
    [Preserve]
    public new class UxmlFactory : UxmlFactory<TestMvvmFac> { }
    //[Preserve]
    //public new class UxmlTrails : VisualElement.UxmlTraits { }
    private string UxmlSource = "CustomUIToolket/Uxml/TestMVVMTemplate";
    private readonly TemplateContainer templateContainer;
    private VisualTreeAsset uxml;
    public TestMvvmFac()
    {
        uxml = Resources.Load<VisualTreeAsset>(UxmlSource);
        var uxmlinstanceContainer = uxml.Instantiate();
        
        Add(uxmlinstanceContainer);


    }


}
