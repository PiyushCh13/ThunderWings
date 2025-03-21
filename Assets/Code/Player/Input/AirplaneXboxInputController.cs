using UnityEngine;

namespace Airplane.PlayerControls
{
    public class AirplaneXboxInputController : AirplaneBaseInputController
    {
        #region Custom Function
        protected override void HandleInput()
        {
            _pitch = Input.GetAxis("Vertical");
            _roll = Input.GetAxis("Horizontal");
            _yaw = Input.GetAxis("X_Right_Horizontal_Stick");
            _throttle = Input.GetAxis("X_Right_Vertical_Stick");
            CalculateStickyThrottle();
            _brake = Input.GetAxis("Fire1");
            if (Input.GetButtonDown("X_Left_Bumper")) _flaps++;
            if (Input.GetButtonDown("X_Right_Bumper")) _flaps--;
            _flaps = Mathf.Clamp(_flaps, 0, _maxFlaps);
        }
        
        
        #endregion
    }
}

