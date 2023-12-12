using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using System.Linq;

using UnityEngine.UIElements;


public partial class CustomTabMenu : VisualElement
    {
        //private readonly VisualElement _container;

        //public override VisualElement contentContainer => _container;
    public new class UxmlFactory : UxmlFactory<CustomTabMenu, UxmlTraits> { }
    //�������Ҫ�Լ���uibuilder��inspector�����һЩ������ô������ౣ��Ĭ�ϼ��ɣ�����init����Ҫoverarride
    //Ҳ����Ҫд IEnumerabkle
    public new class UxmlTraits : VisualElement.UxmlTraits
    {
        public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
        {
            get { yield return new UxmlChildElementDescription(typeof(VisualElement)); }
        }

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as CustomTabMenu;
        }

    }
    public static readonly string disabledUssClassName = "unity-disabled";
    public static readonly string enabledUssClassName = "unity-enabled";

    private VisualElement _iconElement;
    private VisualElement _fillElement;
    private Label _label;

    //�ڹ��캯������Ӷ�Ӧ��Ԫ�ز����г�ʼ��

    public CustomTabMenu()
    {
        //_container = new VisualElement();


        _fillElement = new VisualElement();
        Add(_fillElement);

        _iconElement = new VisualElement();
        Add(_iconElement);

        _label = new Label();
        Add(_label);

        _fillElement.name = "Fill";
        _iconElement.name = "Icon";
        _label.name = "Label";

        //preventing clicks
        _fillElement.pickingMode = PickingMode.Ignore;
        _label.pickingMode = PickingMode.Ignore;
        _iconElement.pickingMode = PickingMode.Ignore;

    }

}


