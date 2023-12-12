using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class StartGameViewModel : MonoBehaviour
{
    public UIDocument RootUiDocument;
    public VisualElement RootVisualElement;
    private VisualElement ChooseLevelEle;
    private VisualElement playtab;
    private string clickcss = "custom-tab-fill-click";
    string playtabinit = "custom-playtab-init";
    string shoptabinit = "custom-shoptab-init";
    string settabinit = "custom-settingtab-init";
    string herotabinit = "custom-herotab-init";
    private VisualElement shoptab;
    private VisualElement settingtab;
    private VisualElement herotab;
    private VisualElement playfill;
    private VisualElement shopfill;
    private VisualElement herofill;
    private VisualElement settingfill;
    private Dictionary<VisualElement,string> tabs=new Dictionary<VisualElement, string>();
    public UnityEvent<int,int> OnPlayEvent;
    void Start()
    {
        RootVisualElement = RootUiDocument.rootVisualElement;
        ChooseLevelEle = RootVisualElement.Q<VisualElement>("ChosseLevel");
        
        playtab = ChooseLevelEle.Q<VisualElement>("playtab");
        playtab.RegisterCallback<ClickEvent>(OnClickPlay);
        shoptab = ChooseLevelEle.Q<VisualElement>("shoptab");
        shoptab.RegisterCallback<ClickEvent>(OnClickShop);
        settingtab = ChooseLevelEle.Q<VisualElement>("settingtab");
        settingtab.RegisterCallback<ClickEvent>(OnClickSetting);
        herotab = ChooseLevelEle.Q<VisualElement>("herotab");
        herotab.RegisterCallback<ClickEvent>(OnClickHero);
        playfill = playtab.Q<VisualElement>("fill");

        shopfill = shoptab.Q<VisualElement>("fill");
        herofill = herotab.Q<VisualElement>("fill");
        settingfill = settingtab.Q<VisualElement>("fill");
        tabs[playtab] = playtabinit;
        tabs[shoptab] = shoptabinit;
        tabs[settingtab] = settabinit;
        tabs[herotab] = herotabinit;
    }

    private void OnClickHero(ClickEvent evt)
    {
        AddCss(ref herofill);
        RemoveCss(ref settingfill);
        RemoveCss(ref shopfill);
        RemoveCss(ref playfill);
        evt.StopPropagation();
    }

    private void OnClickShop(ClickEvent evt)
    {
        RemoveCss(ref herofill);
        RemoveCss(ref settingfill);
        AddCss(ref shopfill);
        RemoveCss(ref playfill);
        evt.StopPropagation();
    }

    private void OnClickSetting(ClickEvent evt)
    {
        RemoveCss(ref herofill);
        AddCss(ref settingfill);
        RemoveCss(ref shopfill);
        RemoveCss(ref playfill);
        evt.StopPropagation();
    }

    private void OnClickPlay(ClickEvent evt)
    {
        RemoveCss(ref herofill);
        RemoveCss(ref settingfill);
        RemoveCss(ref shopfill);
        AddCss(ref playfill);
        OnPlayEvent.Invoke(1,2);
        evt.StopPropagation();
    }

    void AddCss(ref VisualElement m)
    {
        m.AddToClassList(clickcss);
        
    }

    void RemoveCss(ref VisualElement m)
    {
        m.RemoveFromClassList(clickcss);
    }

    public void OnEnterExit(UIManager.UIType t)
    {
        
        StartCoroutine(Wait(0.5f));

    }

    private IEnumerator Wait(float f)
    {
        yield return new WaitForSeconds(f);
        foreach (var keyTab in tabs.Keys)
        {
            
            keyTab.ToggleInClassList(tabs[keyTab]);
            yield return new WaitForSeconds(f);
        }
        
    }
}
