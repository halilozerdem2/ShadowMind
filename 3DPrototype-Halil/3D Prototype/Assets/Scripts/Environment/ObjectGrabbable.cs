using UnityEditor;
using UnityEngine;

public class ObjectGrabbable : MonoBehaviour
{
    private Rigidbody rb;
    private Transform objectGrapPointTransform;
    private MeshCollider boxCollider;

    private float lerpSpeed = 10f;
    private void Awake()
    {
        rb=GetComponent<Rigidbody>();
        boxCollider = GetComponent<MeshCollider>();
    }
    public void Grab(Transform objectGrappPoint)
    {
        this.objectGrapPointTransform=objectGrappPoint;
        boxCollider.isTrigger = true;
        rb.useGravity = false;
    }

    public void Drop()
    {
        this.objectGrapPointTransform = null;
        boxCollider.isTrigger = false;
        rb.useGravity = true;

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
