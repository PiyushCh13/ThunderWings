using System.Collections;
using System.Collections.Generic;
using Airplane.PlayerControls;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Airplane.Characteristics
{
    public class AirplaneCharacteristics : MonoBehaviour
    {
        #region Varaibles
        [FormerlySerializedAs("maxMPH")]

        [Header("Characteristics Properties")]
        public float maxKMPH = 177f;
        public float rbLerpSpeed = 0.01f;

        [Header("Lift Properties")]
        public float maxLiftPower = 800f;
        public AnimationCurve liftCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
        public float flapLiftPower = 100f;


        [Header("Drag Properties")]
        public float dragFactor = 0.01f;
        public float flapDragFactor = 0.005f;


        [Header("Control Properties")]
        public float pitchSpeed = 1000f;
        public float rollSpeed = 1000f;
        public float yawSpeed = 1000f;
        public float bankSpeed = 1000f;
        public AnimationCurve controlSurfaceEfficiency = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

        private float forwardSpeed;
        public float ForwardSpeed
        {
            get { return forwardSpeed; }
        }

        private float kmph;
        public float KMPH
        {
            get { return kmph; }
        }

        private AirplaneInputController input;
        private PlayerAudioHandler playerAudioHandler;
        private Rigidbody rb;
        private float startDrag;
        private float startAngularDrag;

        private float maxMPS;
        private float normalizeKMPH;

        private float angleOfAttack;
        private float pitchAngle;
        private float rollAngle;

        public Vector3 velocitySmooth;

        private float csEfficiencyValue;

        [Header("Wheel Animation")]
        public float maxGroundHeight;
        public float altitude;
        public LayerMask groundLayer;

        private Animator animator;

        public bool isWheelClosed;

        [Header("VFX")]
        public TrailRenderer[] wingTrails;
        public Transform[] thrustObject;
        public float thrustscaleValue;

        public ParticleSystem[] smokeParticles;

        #endregion

        #region Constants
        const float mpsToKMph = 3.6f;
        #endregion

        void Start()
        {
            animator = GetComponent<Animator>();
            playerAudioHandler = GetComponent<PlayerAudioHandler>();
        }

        #region Custom Methods

        public void InitCharacteristics(Rigidbody curRB, AirplaneInputController curInput)
        {
            input = curInput;
            rb = curRB;
            startDrag = rb.linearDamping;
            startAngularDrag = rb.angularDamping;

            maxMPS = maxKMPH / mpsToKMph;
        }

        public void UpdateCharacteristics()
        {
            if (rb)
            {
                CalculateForwardSpeed();
                CalculateLift();
                CalculateDrag();

                HandleControlSurfaceEfficiency();
                HandleYaw();
                HandlePitch();
                HandleRoll();
                HandleBanking();
                HandleWheelAnimation();

                HandleRigidbodyTransform();
                HandleThrustObject();
            }
        }

        void CalculateForwardSpeed()
        {
            Vector3 localVelocity = transform.InverseTransformDirection(rb.linearVelocity);
            forwardSpeed = Mathf.Max(0f, localVelocity.z);

            kmph = forwardSpeed * mpsToKMph;

            normalizeKMPH = Mathf.InverseLerp(0f, maxKMPH, kmph);
        }

        void CalculateLift()
        {
            angleOfAttack = Vector3.Dot(rb.linearVelocity.normalized, transform.forward);
            angleOfAttack *= angleOfAttack;

            Vector3 liftDir = transform.up;
            float liftPower = liftCurve.Evaluate(normalizeKMPH) * maxLiftPower;

            float finalLiftPower = flapLiftPower * input.Flaps;

            Vector3 finalLiftForce = liftDir * (liftPower + finalLiftPower) * angleOfAttack;
            rb.AddForce(finalLiftForce);
        }
        void CalculateDrag()
        {
            float speedDrag = forwardSpeed * dragFactor;

            float flapDrag = input.Flaps * flapDragFactor;

            float finalDrag = startDrag + speedDrag + flapDrag;

            rb.linearDamping = finalDrag;
            rb.angularDamping = startAngularDrag * forwardSpeed;
        }


        void HandleRigidbodyTransform()
        {
            float velocityMagnitude = rb.linearVelocity.magnitude;

            // Only apply this logic if the airplane is flying or moving significantly
            if (velocityMagnitude > 5f)
            {
                Vector3 targetVelocity = transform.forward * forwardSpeed;
                Vector3 updatedVelocity = Vector3.Lerp(rb.linearVelocity, targetVelocity, forwardSpeed * angleOfAttack * Time.deltaTime * rbLerpSpeed);
                rb.linearVelocity = updatedVelocity;

                // Avoid LookRotation issues at low speed
                if (rb.linearVelocity.sqrMagnitude > 0.1f)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(rb.linearVelocity.normalized, transform.up);
                    Quaternion updatedRotation = Quaternion.Slerp(rb.rotation, targetRotation, Time.deltaTime * rbLerpSpeed);
                    rb.MoveRotation(updatedRotation);
                }
            }
        }


        void HandleControlSurfaceEfficiency()
        {
            csEfficiencyValue = controlSurfaceEfficiency.Evaluate(normalizeKMPH);
        }

        void HandlePitch()
        {
            if (Mathf.Abs(input.Pitch) > 0.01f)
            {
                Vector3 flatForward = transform.forward;
                flatForward.y = 0f;
                flatForward = flatForward.normalized;
                pitchAngle = Vector3.Angle(transform.forward, flatForward);

                Vector3 pitchTorque = input.Pitch * pitchSpeed * transform.right * csEfficiencyValue;
                rb.AddTorque(pitchTorque);
            }
        }

        void HandleRoll()
        {
            Vector3 flatRight = transform.right;
            flatRight.y = 0f;
            flatRight = flatRight.normalized;
            rollAngle = Vector3.SignedAngle(transform.right, flatRight, transform.forward);

            Vector3 rollTorque = -input.Roll * rollSpeed * transform.forward * csEfficiencyValue;
            rb.AddTorque(rollTorque);
        }

        void HandleYaw()
        {
            Vector3 yawTorque = input.Yaw * yawSpeed * transform.up * csEfficiencyValue;
            rb.AddTorque(yawTorque);
        }

        void HandleBanking()
        {
            float bankSide = Mathf.InverseLerp(-90f, 90f, rollAngle);
            float bankAmount = Mathf.Lerp(-1f, 1f, bankSide);
            Vector3 bankTorque = bankAmount * bankSpeed * transform.up;
            rb.AddTorque(bankTorque);
        }

        public void HandleWheelAnimation()
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, groundLayer))
            {
                altitude = hit.distance;

                if (hit.distance > maxGroundHeight)
                {
                    animator.Play("WheelClose");
                    playerAudioHandler.PlayLandingGearSound();
                    isWheelClosed = true;
                }

                else if (hit.distance < maxGroundHeight && isWheelClosed)
                {
                    isWheelClosed = false;
                    playerAudioHandler.PlayLandingGearSound();
                    animator.Play("WheelOpen");
                }
            }
        }

        public void HandleThrustObject()
        {
            if (thrustObject != null)
            {
                float thrustValue = input.StickyThrottle * thrustscaleValue;

                for (int i = 0; i < thrustObject.Length; i++)
                {
                    float newXScale = Mathf.Lerp(thrustObject[i].localScale.x, thrustValue, Time.deltaTime * 5f);
                    thrustObject[i].localScale = new Vector3(newXScale, thrustObject[i].localScale.y, thrustObject[i].localScale.z);
                }

            }
        }
        #endregion
    }
}
