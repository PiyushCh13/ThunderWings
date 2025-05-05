using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenUI : MonoBehaviour
{
    public TMP_Text player_NameText;

    void Start()
    {
        if (GameManager.Instance.isMultiplayer)
        {
            player_NameText.text = InGameNetworkManager.winnerPlayerName + " Wins!";
        }

        else
        {

            player_NameText.text = "Game Over!";
        }
    }

    public void GoToMainMenu()
    {
        if (GameManager.Instance.isMultiplayer)
        {
            PhotonNetwork.Disconnect();
        }
        else
        {
            SceneManager.LoadScene("StartScreen");
        }

    }
}
