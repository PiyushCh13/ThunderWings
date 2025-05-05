using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;


namespace Airplane.PlanePhysics
{
    [RequireComponent(typeof(Rigidbody))]
    public class AirplaneRigidBodyController : MonoBehaviour
    {
        #region Variables

        protected Rigidbody _rigidbodyrb;
        public PhotonView photonViewComponent;

        public Vector3 startPosition;



        #endregion

        #region Builtin Functions

        protected virtual void Start()
        {
            _rigidbodyrb = GetComponent<Rigidbody>();
            photonViewComponent = GetComponent<PhotonView>();

            if (GameManager.Instance.isMultiplayer)
            {
                if (photonViewComponent.IsMine)
                {
                    startPosition = transform.position;
                }
            }

        }

        void FixedUpdate()
        {
            HandlePhysics();
        }
        #endregion

        #region Custom Functions

        public virtual void HandlePhysics() { }

        #endregion

    }
}

