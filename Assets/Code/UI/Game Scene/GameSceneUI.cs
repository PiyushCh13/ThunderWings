using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Airplane.PlayerControls;
using Airplane.Characteristics;

public class GameSceneUI : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Text throttle;
    public TMP_Text speed;
    public TMP_Text flaps;
    public TMP_Text altitude;
    public TMP_Text checkPoint;

    [Header("Player UI ELements")]
    private AirplaneInputController airplaneInputController;
    private AirplaneCharacteristics airplaneCharacteristics;
    public CheckpointManager checkpointManager;
    public GameObject pauseMenu;

    [Header("Scene Names")]
    public string mainMenuSceneName = "StartScreen";
    void Start()
    {
        pauseMenu.SetActive(false);
        if (airplaneInputController == null && airplaneCharacteristics == null)
        {
            airplaneInputController = FindFirstObjectByType<AirplaneInputController>();
            airplaneCharacteristics = FindFirstObjectByType<AirplaneCharacteristics>();
        }

        UpdateUI();
    }

    void Update()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        UpdateUISinglePlayer();
    }

    public void UpdateUISinglePlayer()
    {
        if (airplaneInputController == null && airplaneCharacteristics == null)
            return;

        throttle.text = $"Throttle: {Mathf.RoundToInt(airplaneInputController.StickyThrottle * 100)}%";

        if (airplaneCharacteristics.KMPH > 1f)
        {
            speed.text = "SPEED: " + airplaneCharacteristics.KMPH.ToString("F1") + " KPH";
        }

        flaps.text = "FLAPS: " + airplaneInputController.Flaps.ToString();
        altitude.text = "ALTITUDE: " + airplaneCharacteristics.altitude.ToString("F1") + " M";
    }

    public void LoadMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(mainMenuSceneName);
    }

    public void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }
}
