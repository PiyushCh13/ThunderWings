using UnityEngine;


namespace Airplane.Physics
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(AudioSource))]
    public class AirplaneRigidBodyController : MonoBehaviour
    {
        #region Variables

        protected Rigidbody _rigidbodyrb;
        protected AudioSource _audioSource;
    
        #endregion

        #region Builtin Functions
        protected virtual void Start()
        {
            _rigidbodyrb = GetComponent<Rigidbody>();
            _audioSource = GetComponent<AudioSource>();

            if (_audioSource)
            {
                _audioSource.playOnAwake = false;
            }
        }
    
        void FixedUpdate()
        {
            HandlePhysics();
        }
        #endregion

        #region Custom Functions

        public virtual void HandlePhysics(){}

        #endregion
    
    }
}

