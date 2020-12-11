using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTut : MonoBehaviour
{

    public Camera cam;
    public LineRenderer lineRenderer;
    public Transform firePoint;
    public GameObject startVFX;
    public GameObject endVFX;

    private Quaternion rotation;
    private List<ParticleSystem> particles = new List<ParticleSystem>();

    void Start()
    {

        FillLists();
        DisableLaser();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            EnableLaser();

        }
        
        if (Input.GetMouseButton(0))
        {
            UpdateLaser();

        }

        if (Input.GetMouseButtonUp(0))
        {
            DisableLaser();

        }
    }

    void EnableLaser()
    {
        lineRenderer.enabled = true;

        for(int i = 0; i<particles.Count; i++)
        {
            particles[i].Play();
        }
    }

    void UpdateLaser()
    {
        lineRenderer.SetPosition(0, firePoint.position);
        startVFX.transform.position = firePoint.position;  
         
        lineRenderer.SetPosition(1, firePoint.position + firePoint.right * 100);

        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right);

        if (hitInfo)
        {
            lineRenderer.SetPosition(1, hitInfo.point); 
        }

        endVFX.transform.position = lineRenderer.GetPosition(1);
    }

    void DisableLaser()
    {
        lineRenderer.enabled = false;

        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].Stop();
        }
    }

    void FillLists()
    {
        for(int i = 0; i < startVFX.transform.childCount; i++)
        {
            var ps = startVFX.transform.GetChild(i).GetComponent<ParticleSystem>();
            if(ps != null)
            {
                particles.Add(ps);
            }
        }
        
        for(int i = 0; i < endVFX.transform.childCount; i++)
        {
            var ps = endVFX.transform.GetChild(i).GetComponent<ParticleSystem>();
            if(ps != null)
            {
                particles.Add(ps);
            }
        }
    }
}
