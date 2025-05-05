using Airplane.Characteristics;
using Airplane.PlayerControls;
using Photon.Pun;
using UnityEngine;

public class PlaneStatSync : MonoBehaviourPun, IPunObservable
{
    public float throttle;
    public float speed;
    public float flaps;
    public float altitude;

    public float checkPointIndex;

    private AirplaneInputController inputController;
    private AirplaneCharacteristics characteristics;

    private CheckpointManager checkpointManager;

    void Awake()
    {
        inputController = GetComponent<AirplaneInputController>();
        characteristics = GetComponent<AirplaneCharacteristics>();
        checkpointManager = FindFirstObjectByType<CheckpointManager>();
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            throttle = inputController.StickyThrottle;
            speed = characteristics.KMPH;
            flaps = inputController.Flaps;
            altitude = characteristics.altitude;
            checkPointIndex = checkpointManager.collectedCheckpoints;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(throttle);
            stream.SendNext(speed);
            stream.SendNext(flaps);
            stream.SendNext(altitude);
            stream.SendNext(checkPointIndex);
        }
        else
        {
            throttle = (float)stream.ReceiveNext();
            speed = (float)stream.ReceiveNext();
            flaps = (float)stream.ReceiveNext();
            altitude = (float)stream.ReceiveNext();
            checkPointIndex = (float)stream.ReceiveNext();
        }
    }
}
