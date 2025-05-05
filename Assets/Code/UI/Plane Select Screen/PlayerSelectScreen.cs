using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using ExitGames.Client.Photon;
using Photon.Realtime;

public class PlayerSelectScreen : MonoBehaviourPunCallbacks
{
    public GameObject mitsubushiPlane;
    public GameObject f15plane;
    public GameObject f16plane;
    public string gameplayScene;
    public GameObject waitingForPlayerText;
    private bool raceStarting = false;

    public void SelectMitsubushi() => OnPlayerSelect(mitsubushiPlane);
    public void SelectF15() => OnPlayerSelect(f15plane);
    public void SelectF16() => OnPlayerSelect(f16plane);

    void Start()
    {
        waitingForPlayerText.SetActive(false);
    }

    private void OnPlayerSelect(GameObject selectedPlane)
    {
        SFXManager.Instance.PlaySound(SFXManager.Instance.buttonClick);

        if (GameManager.Instance.isMultiplayer)
        {
            GameManager.Instance.selectedPlaneName = selectedPlane;
            int planeIndex = GetPlaneIndex(selectedPlane);
            SelectPlane(planeIndex, selectedPlane.name);
            waitingForPlayerText.SetActive(true);
        }

        else
        {
            GameManager.Instance.selectedPlaneName = selectedPlane;
            SceneManager.LoadScene(gameplayScene);
        }
    }

    public void SelectPlane(int planeIndex, string planeName)
    {
        Hashtable props = new Hashtable
        {
            { "PlaneIndex", planeIndex },
            { "PlaneName", planeName }
        };

        PhotonNetwork.LocalPlayer.SetCustomProperties(props);

    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (raceStarting) return;

        if (changedProps.ContainsKey("PlaneIndex"))
        {
            Debug.Log($"{targetPlayer.NickName} selected {changedProps["PlaneName"]}");

            if (PhotonNetwork.IsMasterClient && AllPlayersReady())
            {
                raceStarting = true;
                ConnectionManager.Instance.StartRace();
            }
        }
    }

    private bool AllPlayersReady()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount != 2) return false;

        foreach (var player in PhotonNetwork.PlayerList)
        {
            if (!player.CustomProperties.ContainsKey("PlaneIndex"))
                return false;
        }

        return true;
    }

    private int GetPlaneIndex(GameObject plane)
    {
        if (plane == mitsubushiPlane) return 0;
        if (plane == f15plane) return 1;
        if (plane == f16plane) return 2;
        return 0;
    }
}
