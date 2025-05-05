using System;
using Airplane.PlanePhysics;
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

   void Update()
   {
      if (target == null && GameManager.Instance.isMultiplayer)
      {
         foreach (var player in FindObjectsByType<AirplaneController>(FindObjectsSortMode.None))
         {
            if (player.photonViewComponent.IsMine)
            {
               target = player.transform;
               break;
            }
         }
      }
   }

   private void FixedUpdate()
   {
      if (target == null) return;
      FollowTarget();
   }

   protected virtual void FollowTarget()
   {
      Vector3 wantedPosition = target.position + (target.forward * distance) + (Vector3.up * height);
      transform.position = Vector3.SmoothDamp(transform.position, wantedPosition, ref currentVelocity, smoothTime);
      transform.LookAt(target);
   }

}
