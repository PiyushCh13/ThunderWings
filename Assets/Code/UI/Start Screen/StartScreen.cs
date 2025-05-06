using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    public string planeSelectScreen;
    public string matchingMakingScreen;

    public TMP_InputField playerInputField;
    public GameObject playerButtons;
    public GameObject playerNameWindow;

    public GameObject playerControlField;

    public TMP_Text playerNameDisplayText;

    void Start()
    {
        MusicManager.Instance.PlayMusic();

        if (PlayerPrefs.HasKey("PlayerName"))
        {
            GameManager.Instance.playerName = PlayerPrefs.GetString("PlayerName", "");
            playerNameDisplayText.gameObject.SetActive(true);
            GameManager.Instance.isPlayerNameAdded = true;
            playerNameDisplayText.text = GameManager.Instance.playerName;
            playerNameWindow.SetActive(false);
            playerButtons.SetActive(true);
            playerControlField.SetActive(false);
        }

        else
        {
            GameManager.Instance.playerName = "";
            playerNameWindow.SetActive(true);
            playerButtons.SetActive(false);
            playerNameDisplayText.gameObject.SetActive(true);
            playerControlField.SetActive(false);
        }
    }

    public void AddPlayerName()
    {
        if(PlayerPrefs.HasKey("PlayerName"))
        {
            PlayerPrefs.DeleteKey("PlayerName");
        }
        
        SFXManager.Instance.PlaySound(SFXManager.Instance.buttonClick);
        GameManager.Instance.playerName = playerInputField.text;
        GameManager.Instance.isPlayerNameAdded = true;
        playerNameWindow.SetActive(false);
        playerButtons.SetActive(true);
        playerNameDisplayText.gameObject.SetActive(true);
        playerNameDisplayText.text = GameManager.Instance.playerName;

        PlayerPrefs.SetString("PlayerName", GameManager.Instance.playerName);
        PlayerPrefs.Save();
    }

    public void SinglePlayer()
    {
        SFXManager.Instance.PlaySound(SFXManager.Instance.buttonClick);
        SceneManager.LoadScene(planeSelectScreen);
    }

    public void MultiPlayer()
    {
        SFXManager.Instance.PlaySound(SFXManager.Instance.buttonClick);
        GameManager.Instance.isMultiplayer = true;
        SceneManager.LoadScene(matchingMakingScreen);
    }

    public void ChangePlayerName()
    {
        SFXManager.Instance.PlaySound(SFXManager.Instance.buttonClick);
        playerNameWindow.SetActive(true);
        playerButtons.SetActive(false);
        playerNameDisplayText.gameObject.SetActive(false);
    }

    public void OpenControlField()
    {
        SFXManager.Instance.PlaySound(SFXManager.Instance.buttonClick);
        playerButtons.SetActive(false);
        playerControlField.SetActive(true);
    }

        public void CloseControlField()
    {
        SFXManager.Instance.PlaySound(SFXManager.Instance.buttonClick);
        playerButtons.SetActive(true);
        playerControlField.SetActive(false);
    }

    public void QuitGame()
    {
        SFXManager.Instance.PlaySound(SFXManager.Instance.buttonClick);
        Application.Quit();
    }

}
