using UnityEditor;
using UnityEngine;

public class ObjectGrabbable : MonoBehaviour
{
    private Rigidbody rb;
    private Transform objectGrapPointTransform;
    private float lerpSpeed = 10f;
    private void Awake()
    {
        rb=GetComponent<Rigidbody>();
    }
    public void Grab(Transform objectGrappPoint)
    {
        this.objectGrapPointTransform=objectGrappPoint;
        rb.useGravity = false;
    }

    public void Drop()
    {
        this.objectGrapPointTransform = null;
        rb.useGravity = true;
        rb.velocity = Vector3.zero; // Hýzý sýfýrla
        rb.angularVelocity = Vector3.zero; // Dönme hýzýný sýfýrla
    }

    private void FixedUpdate()
    {
        if(objectGrapPointTransform!=null) 
        {
           Vector3 newPosition = Vector3.Lerp(transform.position, objectGrapPointTransform.position, Time.deltaTime * lerpSpeed);
            rb.MovePosition(newPosition);
        }
    }
}
