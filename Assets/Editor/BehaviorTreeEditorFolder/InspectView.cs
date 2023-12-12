using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InspectView : VisualElement
{
    private TextField infoTextField; // 使用 TextField 来显示信息

    public new class UxmlFactory : UxmlFactory<InspectView, VisualElement.UxmlTraits>
    {

    }

    public InspectView()
    {
        // 创建 TextField 并设置其多行显示
        infoTextField = new TextField();
        infoTextField.multiline = true;
        infoTextField.style.flexGrow = 1; // 设置 TextField 占满可用空间
        infoTextField.style.whiteSpace = WhiteSpace.Normal;
     
        Add(infoTextField); // 添加到视图中
    }



    public void UpdateInfo(string info)
    {
        infoTextField.value = "";
        Debug.Log("InspectView trigger! message"+info);
        infoTextField.value = info;
    }
}
