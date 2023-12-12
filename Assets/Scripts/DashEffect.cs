using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class DashEffect : MonoBehaviour
{
    public List<Material> trail_material;
    private bool isdash = false;
    public PostProcessVolume m_Volume;
    public float lendistortionIntensity = -50;
    [SerializeField]private LensDistortion m_lensdistortion;
    [SerializeField]private ChromaticAberration m_ChromaticAberration;
    public AbilityUI AbilityUi;
    public ParticleSystem speedlines;
    public GameObject _DashGameObject;
    // Start is called before the first frame update
    void Start()
    {
        m_Volume.profile.TryGetSettings(out m_lensdistortion);
        m_Volume.profile.TryGetSettings(out m_ChromaticAberration);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            speedlines.Emit(100);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isdash = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isdash = false;
        }

        if (isdash)
        {
            AbilityUi?.OnTrigger();
            _DashGameObject.SetActive(true);
            //m_lensdistortion.enabled.Override(true);
            m_lensdistortion.intensity.value=
                Mathf.MoveTowards(m_lensdistortion.intensity, lendistortionIntensity, 5f);
            m_ChromaticAberration.enabled.Override(true);
            
        }
        else
        {
            _DashGameObject.SetActive(false);
            //m_lensdistortion.enabled.Override(false);
            m_lensdistortion.intensity.value =
                Mathf.MoveTowards(m_lensdistortion.intensity, 0, 5f);
            m_ChromaticAberration.enabled.Override(false);
        }
        foreach (var material in trail_material)
        {
            material.SetInt("_IsDash",isdash?1:0);
        }
    }
    private void GetDash()
    {
        _DashGameObject.SetActive(true);

        StartCoroutine(Dash(2f));
    }

    IEnumerator Dash(float healtime)
    {
        while (healtime > 0)
        {
            healtime -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        _DashGameObject.SetActive(false);
    }
}
