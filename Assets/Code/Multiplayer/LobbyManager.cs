using System.Collections;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager Instance;

    [Header("UI Elements")]
    public TMP_Text playerFoundText;
    public TMP_Text lookingForPlayerText;
    public TMP_Text vsText;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if(PhotonNetwork.IsConnected)
        {
            GameManager.Instance.isMultiplayer = true;
            Debug.Log("Connected to Photon Network");
        }
    }

    [PunRPC]
    public void ShowVsText()
    {
        lookingForPlayerText.gameObject.SetActive(false);
        playerFoundText.gameObject.SetActive(true);
        vsText.gameObject.SetActive(true);

        var players = PhotonNetwork.PlayerList;
        vsText.text = $"{players[0].NickName} vs {players[1].NickName}";
    }

    public IEnumerator LoadPlayerSelectScene()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("PlaneSelectScreen");
    }
}
