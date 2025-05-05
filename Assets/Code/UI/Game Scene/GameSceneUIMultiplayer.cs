using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using Airplane.PlanePhysics;
using Airplane.PlayerControls;
using Airplane.Characteristics;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameSceneUIMultiplayer : MonoBehaviour
{
    [Header("UI Elements Player 1")]
    public TMP_Text player_NameText_P1;
    public TMP_Text throttle_P1;
    public TMP_Text speed_P1;
    public TMP_Text flaps_P1;
    public TMP_Text altitude_P1;
    public TMP_Text checkPoint_P1;

    [Header("UI Elements Player 2")]
    public TMP_Text player_NameText_P2;
    public TMP_Text throttle_P2;
    public TMP_Text speed_P2;
    public TMP_Text flaps_P2;
    public TMP_Text altitude_P2;
    public TMP_Text checkPoint_P2;

    [Header("Player Components")]
    public string player1Name;
    public string player2Name;

    private List<GameObject> players = new List<GameObject>();

    private GameObject player1GameObject;
    private GameObject player2GameObject;

    private PlaneStatSync planeStatSyncPlayer_1;
    private PlaneStatSync planeStatSyncPlayer_2;

    [Header("Scene Names")]
    public string mainMenuSceneName;
    public GameObject pauseMenu;

    void Awake()
    {
        if(GameManager.Instance.isMultiplayer == false) return;
        
        players = ConnectionManager.Instance.AllPlayers;
        ChangeName();
    }

    void Start()
    {
        pauseMenu.SetActive(false);
    }

    void ChangeName()
    {
        if (GameManager.Instance.isMultiplayer)
        {
            player1Name = PhotonNetwork.LocalPlayer.NickName;

            foreach (var player in PhotonNetwork.PlayerListOthers)
            {
                player2Name = player.NickName;
                break;
            }

            player_NameText_P1.text = "Player 1: " + player1Name;
            player_NameText_P2.text = "Player 2: " + player2Name;
        }
    }

    void Update()
    {
        if (player1GameObject == null && players.Count > 1)
        {
            foreach (var go in players)
            {
                PhotonView pv = go.GetComponent<PhotonView>();

                if (pv != null && pv.IsMine)
                    player1GameObject = go;
                else
                    player2GameObject = go;
            }

            planeStatSyncPlayer_1 = player1GameObject.GetComponent<PlaneStatSync>();
            planeStatSyncPlayer_2 = player2GameObject.GetComponent<PlaneStatSync>();
        }

        if (planeStatSyncPlayer_1 != null && planeStatSyncPlayer_2 != null)
        {
            UpdateStatsUI();
        }
    }

    void UpdateStatsUI()
    {
        throttle_P1.text = $"Throttle: {Mathf.RoundToInt(planeStatSyncPlayer_1.throttle * 100)}%";

        if (planeStatSyncPlayer_1.speed > 1f)
        {
            speed_P1.text = "SPEED: " + planeStatSyncPlayer_1.speed.ToString("F1") + " KPH";
        }

        flaps_P1.text = "FLAPS: " + planeStatSyncPlayer_1.flaps.ToString();
        altitude_P1.text = "ALTITUDE: " + planeStatSyncPlayer_1.altitude.ToString("F1") + " M";
        checkPoint_P1.text = "CHECKPOINT: " + planeStatSyncPlayer_1.checkPointIndex.ToString("F0") + "/" + GameManager.Instance.totalcheckpointNumber.ToString("F0");

        throttle_P2.text = $"Throttle: {Mathf.RoundToInt(planeStatSyncPlayer_2.throttle * 100)}%";

        if (planeStatSyncPlayer_2.speed > 1f)
        {
            speed_P2.text = "SPEED: " + planeStatSyncPlayer_2.speed.ToString("F1") + " KPH";
        }

        flaps_P2.text = "FLAPS: " + planeStatSyncPlayer_2.flaps.ToString();
        altitude_P2.text = "ALTITUDE: " + planeStatSyncPlayer_2.altitude.ToString("F1") + " M";
        checkPoint_P2.text = "CHECKPOINT: " + planeStatSyncPlayer_2.checkPointIndex.ToString("F0") + "/" + GameManager.Instance.totalcheckpointNumber.ToString("F0");
    }

    public void LoadMainMenu()
    {
        pauseMenu.SetActive(false);
        PhotonNetwork.Disconnect();
    }

    public void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }
}
