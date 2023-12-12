using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyHealthUIManger : MonoBehaviour
{
    public UIDocument uIDocument;

    private VisualElement RootElement;
    private VisualElement healthbar;
    private VisualElement healthbarfill;
    private Camera maincamera;
    private bool enableupdate;
    private Label questionLabel;
    private Label chaseLabel;
    public bool playerCansee;
    private bool CanFreshChaseBar;
    private bool CanFreshQuestBar;

    // Start is called before the first frame update
    void Start()
    {
        RootElement = uIDocument.rootVisualElement;
        healthbar = RootElement.Q<VisualElement>("HealthBar");
        healthbarfill = RootElement.Q<VisualElement>("VisualElement");
        questionLabel = RootElement.Q<Label>("IsPlayer");
        chaseLabel = RootElement.Q<Label>("IsChasing");
        maincamera = Camera.main;
        questionLabel.style.display = DisplayStyle.None;
        chaseLabel.style.display = DisplayStyle.None;
    }
    public void OnHealthChange(float currenthealth)
    {
        
        healthbarfill.style.translate = new Translate(Length.Percent(currenthealth - 100), 0, 0);
        
    }


    public void SetQuestionBarPos()
    {
        //DisableHealthbar();
        CanFreshQuestBar = true;

        //Debug.Log("SetQuestionBarPos");
    }
    public void SetChaseBarPos()
    {
        CanFreshChaseBar = true;
    }
    public void UnSetQuestionBarPos()
    {
        //Debug.Log("UnSetQuestionBarPos");
        CanFreshQuestBar = false;

    }
    public void UnSetChaseBarPos()
    {
        //Debug.Log("UnSetQuestionBarPos");
        CanFreshChaseBar = false;


    }
    public void EnableHealthbar()
    {
        //Debug.Log("EnableHealthbar");
        playerCansee = true;
   
        

    }
    public void DisableHealthbar()
    {
        //Debug.Log("DisableHealthbar");

        playerCansee = false;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if (playerCansee)
        {
            Vector2 newPos1 = RuntimePanelUtils.CameraTransformWorldToPanel(healthbar.panel,
                transform.position + transform.up.normalized * 1.4f, maincamera);
            healthbar.transform.position = new Vector2(newPos1.x - healthbar.layout.width / 2, newPos1.y - healthbar.layout.height / 2);

            healthbar.style.display = DisplayStyle.Flex;
            if (CanFreshChaseBar)
            {

                Vector2 newPos = RuntimePanelUtils.CameraTransformWorldToPanel(questionLabel.panel,
                    transform.position + transform.up.normalized * 1.5f, maincamera);
                chaseLabel.transform.position = new Vector2(newPos.x - questionLabel.layout.width / 2, newPos.y - questionLabel.layout.height / 2);
                chaseLabel.style.display = DisplayStyle.Flex;

            }
            else
            {
                chaseLabel.style.display = DisplayStyle.None;

            }

            if (CanFreshQuestBar)
            {
                questionLabel.style.display = DisplayStyle.Flex;
                Vector2 newPos = RuntimePanelUtils.CameraTransformWorldToPanel(questionLabel.panel,
                    transform.position + transform.up.normalized * 1.5f, maincamera);
                questionLabel.transform.position = new Vector2(newPos.x - questionLabel.layout.width / 2, newPos.y - questionLabel.layout.height / 2);
                questionLabel.style.display = DisplayStyle.Flex;


            }
            else
            {
                questionLabel.style.display = DisplayStyle.None;

            }
        }
        else
        {
            healthbar.style.display = DisplayStyle.None;
            questionLabel.style.display = DisplayStyle.None;
            chaseLabel.style.display = DisplayStyle.None;

        }

    }



}
