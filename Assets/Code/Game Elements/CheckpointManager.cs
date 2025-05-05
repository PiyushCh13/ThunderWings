using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [Header("Checkpoint Properties")]
    public Checkpoint[] checkpoints;
    public int collectedCheckpoints = 0;
    public InGameNetworkManager inGameNetworkManager;
    void Start()
    {
        checkpoints = new Checkpoint[transform.childCount];
        for (int i = 0; i < checkpoints.Length; i++)
        {
            checkpoints[i] = transform.GetChild(i).GetComponent<Checkpoint>();
            checkpoints[i].OnCheckpointPassed += OnCheckpointPassed;
        }

        GameManager.Instance.totalcheckpointNumber = checkpoints.Length;
        inGameNetworkManager = FindFirstObjectByType<InGameNetworkManager>();
    }

    public void OnCheckpointPassed(Checkpoint checkpoint)
    {
        if (collectedCheckpoints == (checkpoint.checkpointIndex - 1))
        {
            collectedCheckpoints++;
        }

        if (collectedCheckpoints == checkpoints.Length)
        {
            inGameNetworkManager.PlayerFinishedRace(PhotonNetwork.LocalPlayer);
        }

    }
}
