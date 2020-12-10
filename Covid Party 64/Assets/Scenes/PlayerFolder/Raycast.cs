using System.Collections;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public Transform shootPoint;

    public Animator animator;
    //  public GameObject impactEffect;
    public LineRenderer lineRenderer;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            StartCoroutine(Shoot());
        } 
    }

    IEnumerator Shoot()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(shootPoint.position, shootPoint.right);
        
        
        if (hitInfo)
        {
            

            // Instantiate(impactEffect, hitInfo.point, Quaternion.identity);

            lineRenderer.SetPosition(0, shootPoint.position);
            lineRenderer.SetPosition(1, hitInfo.point);
        }
        else
        { 

            lineRenderer.SetPosition(0, shootPoint.position);
            lineRenderer.SetPosition(1, shootPoint.position + shootPoint.right *1000);
        }

        lineRenderer.enabled = true;

        yield return new WaitForSeconds(3f);

        lineRenderer.enabled = false;

        
    } 


}
