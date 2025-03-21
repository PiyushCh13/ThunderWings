using System;
using UnityEngine;

public class BaseCameraFollow : MonoBehaviour
{
   [Header("Camera Properties")]
   public Transform target;

   public float distance;
   public float height;
   private Vector3 currentVelocity;
   public float smoothTime;
   protected float orignalHeight;

   private void Start()
   {
      orignalHeight = height;
   }

   private void FixedUpdate()
   {
      FollowTarget();
   }

   protected virtual void FollowTarget()
   {
      Vector3 wantedPosition = target.position + (-target.forward * distance) + (Vector3.up * height);
      transform.position = Vector3.SmoothDamp(transform.position , wantedPosition , ref currentVelocity , smoothTime );
      transform.LookAt(target);
   }

}
