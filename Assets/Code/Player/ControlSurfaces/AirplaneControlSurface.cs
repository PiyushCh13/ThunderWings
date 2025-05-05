using Airplane.PlayerControls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Airplane.ControlSurfaces
{
    public enum ControlSurface
    {
        Rudder,
        Elevator,
        R_Flap_1,
        R_Flap_2,
        L_Flap_1,
        L_Flap_2,
        Aileron
    }

    public class AirplaneControlSurfaces : MonoBehaviour
    {
        #region Varaibles

        public ControlSurface type = ControlSurface.Rudder;
        public float maxAngle = 30.0f;
        public Vector3 axis = Vector3.right;
        private float wantedAngle;
        public Transform controlSurfaceGraphic;
        public float smoothSpeed;

        public PlayerAudioHandler playerAudioHandler;

        #endregion

        #region BuitlInMethods

        void Start()
        {
             playerAudioHandler = GetComponentInParent<PlayerAudioHandler>();
        }

        void Update()
        {
            if (controlSurfaceGraphic) 
            {
                Vector3 angleAxis = (axis * wantedAngle);
                controlSurfaceGraphic.localRotation = Quaternion.Slerp(controlSurfaceGraphic.localRotation, Quaternion.Euler(angleAxis), Time.deltaTime * smoothSpeed);                            
            }
        }

        #endregion

        #region CustomMethods

        public void HandleControlSurface(AirplaneInputController input)
        {
            if(playerAudioHandler == null)
            {
                playerAudioHandler = GetComponentInParent<PlayerAudioHandler>();
            }

            float inputValue = 0;

            switch (type)
            {
                case ControlSurface.Rudder:
                    inputValue = input.Yaw;
                    playerAudioHandler.PlayRudderSound(inputValue);
                    break;
                case ControlSurface.Elevator:
                    inputValue = input.Pitch;
                    playerAudioHandler.PlayElevatorSound(inputValue);
                    break;
                case ControlSurface.R_Flap_1:
                    inputValue = input.Flaps;
                    break;
                case ControlSurface.R_Flap_2:
                    inputValue = input.Flaps;
                    break;
                case ControlSurface.L_Flap_1:
                    inputValue = input.Flaps;
                    break;
                case ControlSurface.L_Flap_2:
                    inputValue = input.Flaps;
                    break;
                case ControlSurface.Aileron:
                    inputValue = input.Roll;
                    playerAudioHandler.PlayAelironSound(inputValue);
                    break;

                default:
                    break;

            }

            wantedAngle = inputValue * maxAngle;
        }
        #endregion
    }

}