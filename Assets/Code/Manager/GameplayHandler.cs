using UnityEngine;
using Photon.Pun;

public class GameplayHandler : MonoBehaviourPun
{
    [Header("SinglePlayer Settings")]
    public GameObject singlePlayerParent;
    public GameObject singlePlayerPrefab;

    [Header("Multiplayer Settings")]
    public GameObject multiplayerPrefab;

    [Header("Components")]
    private BaseCameraFollow cameraFollow;

    void Start()
    {
        cameraFollow = Camera.main.GetComponent<BaseCameraFollow>();

        if(!GameManager.Instance.isMultiplayer)
        {
            singlePlayerPrefab.gameObject.SetActive(true);
            multiplayerPrefab.gameObject.SetActive(false);
            GameObject player = GameManager.Instance.SpawnPlayer();
            player.transform.SetParent(singlePlayerParent.transform, false);
            cameraFollow.target = player.transform;
        }
    }
}
