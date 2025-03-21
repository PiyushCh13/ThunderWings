using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Airplane.Physics
{
    public class AirplanePropeller : MonoBehaviour
    {
        #region Variables

        [Header("Propeller Game Objects")]
        public GameObject mainPropeller;
        public GameObject blurredPropeller;
        
        [Header("Blurred Propeller Properties")]
        public float minQuadRpm;
        public float minRotationRPM;
        public float minBlurRpm = 600f;

        [Header("Blurred Propeller Material")] 
        public Material blurredPropMat;

        [Header("Blurred Propeller Textures")] 
        public Texture2D blurredPropTexture2D1;
        public Texture2D blurredPropTexture2D2;
        
        #endregion

        #region BuiltIn Functions

        private void Start()
        {
            if (mainPropeller && blurredPropeller)
            {
                HandleSwapping(0f);
            }
        }

        #endregion
        
        #region Custom Functions

        public void HandlePropeller(float currentRPM)
        {
            float dps = ((currentRPM * 360f) / 60f) * Time.deltaTime;
            dps = Mathf.Clamp(dps, 0, minRotationRPM);
            transform.Rotate(Vector3.forward,dps);

            if (mainPropeller && blurredPropeller)
            {
                HandleSwapping(currentRPM);
            }
        }

        private void HandleSwapping(float currentRPM)
        {
            if (currentRPM > minQuadRpm)
            {
                mainPropeller.gameObject.SetActive(false);
                blurredPropeller.gameObject.SetActive(true);

                if (currentRPM > minBlurRpm)
                {
                    blurredPropMat.SetTexture("_MainTex" , blurredPropTexture2D2);
                }
                else
                {
                    blurredPropMat.SetTexture("_MainTex" , blurredPropTexture2D1);
                }
            }
            else
            {
                mainPropeller.gameObject.SetActive(true);
                blurredPropeller.gameObject.SetActive(false);
            }
                
        }

        #endregion
    }
}