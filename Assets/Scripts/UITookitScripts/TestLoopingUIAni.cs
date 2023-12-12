using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TestLoopingUIAni : VisualElement
{
    private VisualElement rootElement;

    void LoopFun()
    {
        var tmp = rootElement.Q<VisualElement>("Background");
        //tmp.schedule.Execute()
    }
}
