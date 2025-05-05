using System;
using System.Collections.Generic;
using Airplane.Characteristics;
using Airplane.ControlSurfaces;
using Airplane.PlayerControls;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

namespace Airplane.PlanePhysics
{
    [RequireComponent(typeof(AirplaneCharacteristics))]
    public class AirplaneController : AirplaneRigidBodyController
    {
        #region Variables

        [Header("Components")]
        public AirplaneInputController inputController;
        private AirplaneCharacteristics _airplaneCharacteristics;
        private PlayerAudioHandler playerAudioHandler;

        [Header("Airplane Properties")]
        public Transform centerOfGravity;
        public float airplaneMass;

        [Header("Objects")]
        public List<AirplaneEngine> engines = new List<AirplaneEngine>();
        public List<AirplaneWheels> wheels = new List<AirplaneWheels>();
        public List<AirplaneControlSurfaces> controlSurface = new List<AirplaneControlSurfaces>();

        [Header("UI Properties")]
        public TMP_Text player_NameText;

        #endregion

        #region Builtin Methods

        protected override void Start()
        {
            base.Start();

            if (GameManager.Instance.isMultiplayer && photonViewComponent.Owner != null)
            {
                player_NameText.text = photonViewComponent.Owner.NickName;
            }
            else if (!GameManager.Instance.isMultiplayer)
            {
                player_NameText.text = "";
            }


            _rigidbodyrb.mass = airplaneMass;

            if (MusicManager.Instance.musicSource.isPlaying)
            {
                MusicManager.Instance.musicSource.Stop();
            }

            playerAudioHandler = GetComponent<PlayerAudioHandler>();
            playerAudioHandler.PlayEngineSound();

            if (wheels != null)
            {
                if (wheels.Count > 0)
                {
                    foreach (AirplaneWheels wheel in wheels)
                    {
                        wheel.WheelMotorTorque();
                    }
                }
            }
            _airplaneCharacteristics = GetComponent<AirplaneCharacteristics>();

            if (_airplaneCharacteristics)
            {
                _airplaneCharacteristics.InitCharacteristics(_rigidbodyrb, inputController);
            }
        }

        #endregion

        #region CustomFuntions
        public override void HandlePhysics()
        {
            if (!photonViewComponent.IsMine && GameManager.Instance.isMultiplayer) return;

            if (inputController)
            {
                HandleEngine();
                HandleCharacteristics();
                HandleControlSurface();
                HandleWheel();
                HandleAltitude();
            }
        }

        private void HandleControlSurface()
        {
            if (controlSurface.Count > 0)
            {
                foreach (AirplaneControlSurfaces surface in controlSurface)
                {
                    surface.HandleControlSurface(inputController);
                }
            }
        }

        private void HandleAltitude()
        {

        }

        private void HandleWheel()
        {
            if (wheels.Count > 0)
            {
                foreach (AirplaneWheels wheel in wheels)
                {
                    wheel.HandleWheel(inputController);
                }
            }
        }

        private void HandleCharacteristics()
        {
            if (_airplaneCharacteristics)
            {
                _airplaneCharacteristics.UpdateCharacteristics();
            }
        }

        private void HandleEngine()
        {
            if (centerOfGravity) _rigidbodyrb.centerOfMass = centerOfGravity.localPosition;

            if (playerAudioHandler)
            {
                playerAudioHandler.EngineSoundPitchModifier(inputController.StickyThrottle, 1.5f);
            }

            float throttle = inputController.StickyThrottle;

            if (Mathf.Abs(throttle) > 0.01f)
            {
                if (engines != null)
                {

                    if (engines.Count > 0)
                    {
                        foreach (AirplaneEngine engine in engines)
                        {
                            _rigidbodyrb.AddForce(engine.CalculateForce(inputController.StickyThrottle));
                        }
                    }
                }

            }
        }

        public void ResetAirplane()
        {
            _rigidbodyrb.linearVelocity = Vector3.zero;
            _rigidbodyrb.angularVelocity = Vector3.zero;
            transform.position = startPosition;
            transform.rotation = Quaternion.identity;
            _rigidbodyrb.isKinematic = true;

            inputController.ResetInput();
            playerAudioHandler.SetEnginePitch(1f);
        }
        #endregion
    }
}

