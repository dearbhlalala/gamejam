using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class GrapplingHook : MonoBehaviour
{
    [SerializeField] private float grappleLength;
    [SerializeField] private LayerMask grappleLayer;
    [SerializeField] private LineRenderer rope;
 
    private Vector3 grapplePoint;
    private DistanceJoint2D joint;
    // Start is called before the first frame update
    void Start()
    {
        joint = gameObject.GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        rope.enabled = false;
    }
 
    // Update is called once per frame
    void Update()
    {
 
        // this is the new logic
        //------------------------------------
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector3 myPos = transform.position;
        Vector3 dir = (new Vector3(myPos.x + x, myPos.y + y, 0) - myPos).normalized;
        Debug.DrawLine(myPos, myPos + dir * 10, Color.red);
        //---------------------------------
        if (Input.GetButtonDown("Jump")) // space is now grapple button
        {
            RaycastHit2D hit = Physics2D.Raycast(
            origin: myPos, //changed this 
            direction: dir, // and this
            distance: Mathf.Infinity,
            layerMask: grappleLayer);
 
            if(hit.collider !=null)
            {
                grapplePoint = hit.point;
                grapplePoint.z =0;
                joint.connectedAnchor = grapplePoint;
                joint.enabled = true;
                joint.distance = grappleLength;
                rope.SetPosition(0, grapplePoint);
                rope.SetPosition(1, transform.position);
                rope.enabled = true;
            }
        }
 
        if(Input.GetButtonUp("Jump")) // changed this input.
        {
           joint.enabled = false;
           rope.enabled = false;
        }
 
        if(rope.enabled == true)
        {
            rope.SetPosition(1, transform.position);
        }
    }
}
 