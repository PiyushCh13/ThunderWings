using Airplane.PlayerControls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Airplane.Physics
{
    public class AirplaneWheels : MonoBehaviour
    {
        #region  Variables
        
        [Header("Components")]
        private WheelCollider wheelCollider;
        public Transform wheelGraphic;

        private Vector3 worldPos;
        private Quaternion worldRot;

        public bool isBrakingWheel;
        public bool isSteeringWheel;

        public float brakeTorqueForce;
        private float finalBrakePower;
        private float finalSteeringPower;
        private float maxsteeringAngle = 20f;

        private float smoothSpeed = 2.0f;

        #endregion

        #region  BuiltInFunctions

        void Start()
        {
            wheelCollider = GetComponent<WheelCollider>();
        }

        #endregion

        #region Custom Functions

        public void WheelMotorTorque()
        {
            if (wheelCollider != null)
            {
                wheelCollider.motorTorque = 0.0000001f;
            }
        }

        public void HandleWheel(AirplaneBaseInputController input) 
        {
            if (wheelCollider) 
            {
                wheelCollider.GetWorldPose(out worldPos, out worldRot);

                if (wheelGraphic) 
                {
                    wheelGraphic.position = worldPos;
                    wheelGraphic.rotation = worldRot;
                }

                if (isBrakingWheel) 
                {
                    if (input.Brake > 0.1f)
                    {
                        finalBrakePower = Mathf.Lerp(finalBrakePower, input.Brake * brakeTorqueForce, Time.deltaTime);
                        wheelCollider.brakeTorque = finalBrakePower;
                    }
                    else
                    {
                        finalBrakePower = 0;
                        wheelCollider.brakeTorque = 0;
                        wheelCollider.motorTorque = 0.0000001f;
                    }
                }

                if (isSteeringWheel)
                {
                    finalSteeringPower = Mathf.Lerp(finalSteeringPower, -input.Yaw * maxsteeringAngle, Time.deltaTime * smoothSpeed);
                    wheelCollider.steerAngle = finalSteeringPower;
                }
            }
        }

        #endregion
    }
}
