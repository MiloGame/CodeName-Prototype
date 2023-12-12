using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InspectView : VisualElement
{
    private TextField infoTextField; // ʹ�� TextField ����ʾ��Ϣ

    public new class UxmlFactory : UxmlFactory<InspectView, VisualElement.UxmlTraits>
    {

    }

    public InspectView()
    {
        // ���� TextField �������������ʾ
        infoTextField = new TextField();
        infoTextField.multiline = true;
        infoTextField.style.flexGrow = 1; // ���� TextField ռ�����ÿռ�
        infoTextField.style.whiteSpace = WhiteSpace.Normal;
     
        Add(infoTextField); // ��ӵ���ͼ��
    }



    public void UpdateInfo(string info)
    {
        infoTextField.value = "";
        Debug.Log("InspectView trigger! message"+info);
        infoTextField.value = info;
    }
}
