using System;
using Photon.Pun;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Action<Checkpoint> OnCheckpointPassed;
    public int checkpointIndex = 0;
    public bool isPassed = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPassed)
        {
            PhotonView pv = other.GetComponentInParent<PhotonView>();

            if (pv != null && pv.IsMine)
            {
                isPassed = true;
                OnCheckpointPassed?.Invoke(this);
            }
        }
    }
}
