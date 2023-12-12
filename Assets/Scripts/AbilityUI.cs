using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
    [SerializeField] public Image coolDownImage;
    [SerializeField] private bool _iscoolen=false;
    [SerializeField] public float coolenSpeed;

    public void OnTrigger()
    {
        coolDownImage.fillAmount = 0;
        _iscoolen = true;
    }

    public void Update()
    {
        if (_iscoolen&& Math.Abs(coolDownImage.fillAmount - 1f) > 1e-5)
        {
            coolDownImage.fillAmount = Mathf.MoveTowards(coolDownImage.fillAmount, 1f, Time.deltaTime * coolenSpeed);
        }
    }
}
