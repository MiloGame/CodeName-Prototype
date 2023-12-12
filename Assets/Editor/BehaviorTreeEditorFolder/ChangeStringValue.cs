using System.ComponentModel.Design;
using UnityEditor;
using UnityEngine;

class ChangeStringValue : EditorWindow
{
    private string inputText = "";
    // 创建对话框的入口方法
    public static void OpenDialog()
    {

        ChangeStringValue window = CreateInstance<ChangeStringValue>();
        window.titleContent = new GUIContent("Set message string");
        window.ShowUtility();
    }

    // 绘制对话框的 UI
    private void OnGUI()
    {
        // 在对话框中创建文本输入框，用于收集用户输入
        GUILayout.Label("New Message:");
        inputText = GUILayout.TextField(inputText);

        // 在对话框中创建按钮，用于提交用户输入
        if (GUILayout.Button("OK"))
        {
            EditorPrefs.SetString("NewMessage", inputText);
            Close();
        }
    }

  
}