using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CustomerSlider : MonoBehaviour
{
    public UIDocument UiDocument;
    private VisualElement rootElement;

    // slider
    private VisualElement silderElement;

    private VisualElement draggerElement;

    private VisualElement fillbarElement;
    private VisualElement BarElement;
    private VisualElement NewDragger;

    // Start is called before the first frame update
    void Start()
    {
        rootElement = UiDocument.rootVisualElement;
        silderElement = rootElement.Q<Slider>("MySlider");
        draggerElement = rootElement.Q<VisualElement>("unity-dragger");
        draggerElement.style.alignSelf = Align.FlexStart;
        AddBarElements();
        //AddNewDraggerElements();
       // silderElement.RegisterCallback<ChangeEvent<float>>(SliderValueChanged);
    }

    private void SliderValueChanged(ChangeEvent<float> evt)
    {
        var offset = new Vector2((NewDragger.layout.width - draggerElement.layout.width) / 2,
            (NewDragger.layout.height - draggerElement.layout.height) / 2);
        var pos = draggerElement.parent.LocalToWorld(draggerElement.transform.position);
        NewDragger.transform.position = NewDragger.parent.WorldToLocal(pos - offset);
    }

    private void AddNewDraggerElements()
    {
        NewDragger = new VisualElement();
        NewDragger.name = "NewDragger";
        NewDragger.AddToClassList("newdragger");
        NewDragger.pickingMode = PickingMode.Ignore;
        silderElement.Add(NewDragger);
    }

    private void AddBarElements()
    {
        BarElement = new VisualElement();
        BarElement.name = "Bar";
        BarElement.AddToClassList("bar");
        draggerElement.Add(BarElement);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
