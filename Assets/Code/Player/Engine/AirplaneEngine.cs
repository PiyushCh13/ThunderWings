using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Airplane.PlanePhysics
{
    public class AirplaneEngine : MonoBehaviour
    {

        #region  Variables

        [Header("Engine Properties")]
        public float maxForce;
        public float maxRPM;
        public AnimationCurve animationCurve = AnimationCurve.Linear(0, 0f, 1f, 1f);

        [Header("Components")]
        public AirplanePropeller airplanePropeller;
        
        #endregion

        #region  BuiltInFunctions

        void Start()
        {
        
        }
        void Update()
        {
        
        }

        #endregion

        #region Custom Functions

        public Vector3 CalculateForce(float throttle)
        {
            throttle = animationCurve.Evaluate(throttle);

            float finalRPM = throttle * maxRPM;

            if (airplanePropeller)
            {
                airplanePropeller.HandlePropeller(finalRPM);
            }
            
            float finalEnginePower = maxForce * throttle;
            //Vector3 engineForce = transform.TransformDirection(transform.forward) * finalEnginePower;
            Vector3 engineForce = transform.forward * finalEnginePower;
;           return engineForce;
        }

        #endregion
    }
}
