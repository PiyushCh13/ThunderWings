using System;
using System.Collections.Generic;
using Airplane.Characteristics;
using Airplane.ControlSurfaces;
using Airplane.PlayerControls;
using UnityEngine;
using UnityEngine.Rendering;

namespace Airplane.Physics
{
    [RequireComponent(typeof(AirplaneCharacteristics))]
    public class AirplaneController : AirplaneRigidBodyController
    {
        #region Variables

        [Header("Components")]
        public AirplaneBaseInputController baseInputController;
        private AirplaneCharacteristics _airplaneCharacteristics;

        [Header("Airplane Properties")]
        public Transform centerOfGravity;
        public float airplaneMass;

        [Header("Objects")]
        public List<AirplaneEngine> engines = new List<AirplaneEngine>();
        public List<AirplaneWheels> wheels = new List<AirplaneWheels>();
        public List<AirplaneControlSurfaces> controlSurface = new List<AirplaneControlSurfaces>();

        #endregion

        #region Builtin Methods

        protected override void Start()
        {
            base.Start();
            _rigidbodyrb.mass = airplaneMass;

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
                _airplaneCharacteristics.InitCharacteristics(_rigidbodyrb, baseInputController);
            }
        }

        #endregion

        #region CustomFuntions

        public override void HandlePhysics()
        {
            if (baseInputController)
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
            if(controlSurface.Count > 0) 
            {
                foreach(AirplaneControlSurfaces surface in controlSurface) 
                {
                    surface.HandleControlSurface(baseInputController);
                }
            }
        }

        private void HandleAltitude()
        {

        }

        private void HandleWheel()
        {
            if(wheels.Count > 0) 
            {
                foreach(AirplaneWheels wheel in wheels) 
                {
                    wheel.HandleWheel(baseInputController);
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

            if (engines != null)
            {
                if (engines.Count > 0)
                {
                    foreach (AirplaneEngine engine in engines)
                    {
                        _rigidbodyrb.AddForce(engine.CalculateForce(baseInputController.StickyThrottle));

                    }
                }
            }
        }

        #endregion
    }
}

