using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LoadingScreenViewModel : MonoBehaviour
{
    public UIDocument RootUiDocument;
    private VisualElement rootvisualEle;
    private Label LoadPersent;
    private VisualElement LoadingFill;
    private BindableTProperty<float> loadpersenTProperty;
    void Start()
    {
        rootvisualEle = RootUiDocument.rootVisualElement;
        LoadingFill = rootvisualEle.Q<VisualElement>("LoadingFill");
        LoadPersent = rootvisualEle.Q<Label>("LoadPersent");
        loadpersenTProperty = new BindableTProperty<float>();
        loadpersenTProperty.GetValue(EventBusManager.EventType.LoadingScreenPersent,OnLoadPersentChange);
    }

    private void OnLoadPersentChange(object sender, EventArgs e)
    {
        var data = e as CustomTEventData<float>;
        if (data!=null)
        {
            LoadPersent.text = data.message.ToString("0.0");
            LoadingFill.style.translate =new Translate(Length.Percent(data.message - 100), 0, 0);
        }
    }

    void OnDestroy()
    {
        loadpersenTProperty.UnScribe(EventBusManager.EventType.LoadingScreenPersent,OnLoadPersentChange);
    }
}
