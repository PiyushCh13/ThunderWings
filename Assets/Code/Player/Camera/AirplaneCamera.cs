using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneCamera : BaseCameraFollow
{
    public float minGroundHeight = 2.0f;
    protected override void FollowTarget()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            if (hit.distance < minGroundHeight && hit.transform.tag == "Ground")
            {
                float wantedHeight = orignalHeight + (minGroundHeight - hit.distance);
                height = wantedHeight;
                
            }
        }
        
        base.FollowTarget();
    }
}
