using UnityEngine;
using UnityEngine.InputSystem;

namespace Airplane.PlayerControls
{
    public class AirplaneInputController : MonoBehaviour
    {

        #region Variables
        protected float _pitch;
        protected float _throttle;
        protected float _stickyThrottle;
        protected int _flaps;
        public int _maxFlaps;
        protected float _yaw;
        protected float _roll;
        protected float _brake;
        public float throttleSpeed = 1f;
        #endregion

        #region Properties
        public float Pitch => _pitch;
        public float StickyThrottle => _stickyThrottle;
        public float Throttle => _throttle;
        public float Brake => _brake;
        public float Yaw => _yaw;
        public float Roll => _roll;
        public int Flaps => _flaps;
        #endregion

        private void Awake()
        {
            if(GameManager.Instance.isMultiplayer)
            {
                ConnectionManager.Instance.AllPlayers.Add(this.gameObject);
            }
        }

        private void OnDestroy()
        {
            if(GameManager.Instance.isMultiplayer)
            {
                ConnectionManager.Instance.AllPlayers.Remove(this.gameObject);
            }

        }

        public void HandlePitch(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _pitch = context.ReadValue<float>();
            }

            if (context.canceled)
            {
                _pitch = 0;
            }
        }

        public void HandleYaw(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _yaw = context.ReadValue<float>();
            }

            if (context.canceled)
            {
                _yaw = 0;
            }

        }

        public void HandleRoll(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _roll = context.ReadValue<float>();
            }

            if (context.canceled)
            {
                _roll = 0;
            }
        }

        public void HandleThrottle(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _throttle = context.ReadValue<float>();
            }

            if (context.canceled)
            {
                _throttle = 0;
            }

        }


        public void HandleBrake(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _brake = 1;
            }

            if (context.canceled)
            {
                _brake = 0;
            }
        }

        public void HandleFlapsIncrease(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _flaps++;
                _flaps = Mathf.Clamp(_flaps, 0, _maxFlaps);
            }
        }

        public void HandleFlapsDecrease(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _flaps--;
                _flaps = Mathf.Clamp(_flaps, 0, _maxFlaps);
            }
        }

        void Update()
        {
            if (_throttle != 0)
            {
                _stickyThrottle += _throttle * throttleSpeed * Time.deltaTime;
                _stickyThrottle = Mathf.Clamp01(_stickyThrottle);
            }
        }

        public void ResetInput()
        {
            _pitch = 0;
            _throttle = 0;
            _stickyThrottle = 0;
            _yaw = 0;
            _roll = 0;
            _brake = 0;
            _flaps = 0;
        }
    }

}
