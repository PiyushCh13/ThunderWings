using System.Collections;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameNetworkManager : MonoBehaviour
{
    [Header("MultiPlayer Settings")]
    public Transform[] spawnPoints;
    public GameObject multiplayerPrefab;
    public PhotonView photonView;

    private string endSceneName = "EndScreen";

    public static string winnerPlayerName;

    void Start()
    {
        if (GameManager.Instance.isMultiplayer)
        {
            photonView = GetComponent<PhotonView>();
            multiplayerPrefab.gameObject.SetActive(true);
            PhotonNetwork.AutomaticallySyncScene = true;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (GameManager.Instance.isMultiplayer == false) return;

        if (scene.name != "GameScene") return;

        StartCoroutine(GameManager.Instance.SetupPlayerInstantiation(spawnPoints));
    }
    public void PlayerFinishedRace(Player player)
    {
        photonView.RPC("RPC_DeclareWinner", RpcTarget.AllBuffered, player.NickName);
    }

    [PunRPC]
    void RPC_DeclareWinner(string winnerName)
    {
        winnerPlayerName = winnerName;
        SceneManager.LoadScene(endSceneName);
    }
}
