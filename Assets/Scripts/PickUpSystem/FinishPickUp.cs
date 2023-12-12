using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPickUp : MonoBehaviour
{
    public Vector3 boxsize = new Vector3(5, 0, 2f);
    public bool Detect;
    public LayerMask layerMask;
    private RaycastHit hitinfo;
    private ParticleSystem[] particleSystems;
    private LineRenderer[] lineRenderers;
    public bool IsSend;
    public Light PickUpLight;
    public MeshRenderer[] MeshRenderers;
    private bool HasShine;

    void Start()
    {
        HasShine = false;
        IsSend = false;
        particleSystems = GetComponentsInChildren<ParticleSystem>();
        lineRenderers = GetComponentsInChildren<LineRenderer>();
        DisableEmit();
    }
    public void Shine()
    {
        foreach (var meshRenderer in MeshRenderers)
        {
            foreach (var material in meshRenderer.materials)
            {
                material.color = Color.white * 20;
            }
        }
    }
    void DisableEmit()
    {
        //PickUpLight.color = Color.white;
        //PickUpLight.intensity = 0.9f;
        if (!HasShine)
        {
            Shine();
            StartCoroutine(StopShine());
        }
        foreach (var particle in particleSystems)
        {
            if (particle.isPlaying)
                particle.Stop();
        }

        foreach (var lineRenderer in lineRenderers)
        {
            lineRenderer.enabled = false;
        }
    }

    private IEnumerator StopShine()
    {
        yield return new WaitForSeconds(0.25f);
        HasShine = true;
        foreach (var meshRenderer in MeshRenderers)
        {
            foreach (var material in meshRenderer.materials)
            {
                material.color = Color.white * 1;
            }
        }
    }

    void EnableEmiting()
    {
        //PickUpLight.color = Color.red;
        //PickUpLight.intensity = 9f;
        HasShine = false;
        foreach (var particle in particleSystems)
        {
            if (!particle.isPlaying)
                particle.Play();
        }
        foreach (var lineRenderer in lineRenderers)
        {
            lineRenderer.enabled = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Physics.BoxCast(transform.position+Vector3.down , boxsize / 2, Vector3.up,
                out hitinfo, transform.rotation, 1, layerMask))
        {
            MeshRenderers = hitinfo.transform.GetComponent<CanPickUpObject>().MeshRenderers;
            Detect = true;
            DisableEmit();
        }
        else
        {
            Detect = false;
            EnableEmiting();
        }

        if (!IsSend && Detect)
        {
            EventBusManager.Instance.NonParamPublish(EventBusManager.EventType.CarryBoxToPosAdd);
            IsSend = true;
        }

        if (IsSend && !Detect)
        {
            EventBusManager.Instance.NonParamPublish(EventBusManager.EventType.CarryBoxToPosMinus);
            IsSend = false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawRay(transform.position + Vector3.down, Vector3.up * 10);
        Gizmos.DrawWireCube(transform.position , boxsize);
    }
}
