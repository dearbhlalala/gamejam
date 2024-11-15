
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    [SerializeField] private float grappleLength;
    [SerializeField] private LayerMask grappleLayer;
    [SerializeField] private LineRenderer rope;

    public bool hookCooldown = false;
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
        if (Input.GetButtonDown("Fire1")) // space is now grapple button
        {
            RaycastHit2D hit = Physics2D.Raycast(
                origin: myPos,
                direction: dir,
                distance: 15,
                layerMask: grappleLayer
            );

            if (hit.collider != null && !hookCooldown)
            {
                grapplePoint = hit.point;
                grapplePoint.z = 0;
                joint.connectedAnchor = grapplePoint;
                joint.enabled = true;
                joint.distance = grappleLength;
                rope.SetPosition(0, grapplePoint);
                rope.SetPosition(1, transform.position);
                rope.enabled = true;
                hookCooldown = true;

                // Start the cooldown coroutine
                StartCoroutine(ResetCooldown());
            }
        }

        if (Input.GetButtonUp("Fire1")) // changed this input.
        {
            joint.enabled = false;
            rope.enabled = false;
        }

        if (rope.enabled)
        {
            rope.SetPosition(1, transform.position);
        }
    }

    private IEnumerator ResetCooldown()
    {
        yield return new WaitForSeconds((float)1.3); // Waits for 5 seconds
        hookCooldown = false;
        Debug.Log("Cooldown Completed");
    }
}