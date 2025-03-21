using UnityEngine;

namespace Airplane.PlayerControls
{
    public class AirplaneBaseInputController : MonoBehaviour
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
        protected float throttleSpeed = 0.2f;
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

        #region BuiltinFunctions
        void Start()
        {
        
        }
    
        void Update()
        {
          HandleInput();
        }
        #endregion

        #region CustomFunctions
        
        protected virtual void HandleInput()
        {
            _pitch = Input.GetAxis("Vertical");
            _roll = Input.GetAxis("Horizontal");
            _yaw = Input.GetAxis("Yaw");
            _throttle = Input.GetAxis("Throttle");
            
            CalculateStickyThrottle();
            
            _brake = Input.GetKey(KeyCode.Space) ? 1f : 0f;
            if (Input.GetKey(KeyCode.F)) _flaps++;
            if (Input.GetKey(KeyCode.G)) _flaps--;
            _flaps = Mathf.Clamp(_flaps, 0, _maxFlaps);
        }
        
        protected void CalculateStickyThrottle()
        {
            _stickyThrottle = _stickyThrottle + (_throttle * throttleSpeed * Time.deltaTime);
            _stickyThrottle = Mathf.Clamp01(_stickyThrottle);
        }
        
        #endregion
    }
}