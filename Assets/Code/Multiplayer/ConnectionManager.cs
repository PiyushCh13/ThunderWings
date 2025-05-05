using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectionManager : MonoBehaviourPunCallbacks
{
    public static ConnectionManager Instance { get; private set; }

    [Header("Scene Names")]
    public string mainMenuSceneName = "StartScreen";
    public string playerSelectSceneName = "PlaneSelectScreen";

    public string matchmakingSceneName = "MatchmakingScreen";

    [Header("Player GameObject")]
    public List<GameObject> AllPlayers = new List<GameObject>();


    void Awake()
    {
        if (Instance == null && GameManager.Instance.isMultiplayer)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }


    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = GameManager.Instance.playerName;

        PhotonNetwork.SerializationRate = 20;
        PhotonNetwork.SendRate = 30;

        Debug.Log("Connection Status" + PhotonNetwork.IsConnected);
    }

    public override void OnConnectedToMaster()
    {
        if(!PhotonNetwork.InRoom)
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        if (!PhotonNetwork.InRoom)
        {
            string roomName = "Room_" + UnityEngine.Random.Range(1000, 9999);
            RoomOptions options = new RoomOptions { MaxPlayers = 2, CleanupCacheOnLeave = true };
            PhotonNetwork.CreateRoom(roomName, options);
        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room: " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2 && PhotonNetwork.IsMasterClient)
        {
            PhotonView photonView = LobbyManager.Instance.GetComponent<PhotonView>();
            photonView.RPC("ShowVsText", RpcTarget.All);
            StartCoroutine(LobbyManager.Instance.LoadPlayerSelectScene());
        }
    }

    public void StartRace()
    {
        PhotonNetwork.LoadLevel("GameScene");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        StartCoroutine(DisconnectAndLoadMainMenu());
    }

    private IEnumerator DisconnectAndLoadMainMenu()
    {
        GameManager.Instance.isMultiplayer = false;
        AllPlayers.Clear();
        PhotonNetwork.LocalPlayer.CustomProperties.Clear();
        PhotonNetwork.LocalPlayer.NickName = string.Empty;

        if (!PhotonNetwork.IsConnected)
        {
            SceneManager.LoadScene(mainMenuSceneName);
            Destroy(gameObject);
            yield break;
        }

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.DestroyAll(true);
        }

        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
            while (PhotonNetwork.InRoom)
            {
                yield return null;
            }
        }

        PhotonNetwork.Disconnect();

        while (PhotonNetwork.IsConnected)
        {
            yield return null;
        }
        
        
        SceneManager.LoadScene(mainMenuSceneName);
    }

}
