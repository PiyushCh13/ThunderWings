using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Runtime.InteropServices.WindowsRuntime;
using Airplane.PlanePhysics;
using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : Singleton<GameManager>
{
    [Header("Player Properties")]
    public string playerName;
    public bool isPlayerNameAdded;
    public GameObject selectedPlaneName;

    [Header("Game Properties")]
    public bool isMultiplayer;
    public bool isMobile;

    [Header("Level Elements")]
    public int totalcheckpointNumber;

    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        if (IsMobile())
        {
            isMobile = true;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }
        else
        {
            isMobile = false;
        }
    }

    public GameObject SpawnPlayer()
    {
        string path = "R_Planes/" + selectedPlaneName.name;
        GameObject prefab = Resources.Load<GameObject>(path);
        GameObject playerInstance = Instantiate(prefab);
        return playerInstance;
    }

    #region SinglePlayerFunctions

    public IEnumerator SetupPlayerInstantiation(Transform[] spawnPoints)
    {
        yield return null;
        string localPlane = PhotonNetwork.LocalPlayer.CustomProperties["PlaneName"].ToString();

        int playerIndex = PhotonNetwork.LocalPlayer.ActorNumber % spawnPoints.Length;
        Vector3 spawnPosition = spawnPoints[playerIndex].position;

        GameObject player = PhotonNetwork.Instantiate("R_Planes/" + localPlane, spawnPosition, Quaternion.identity);
    }

    public void TriggerCollisionSinglePlayer(Vector3 spawnPosition, GameObject explosionPrefab,
    GameObject player)
    {
        player.SetActive(false);
        StartCoroutine(HandleExplosionSinglePlayer(spawnPosition, explosionPrefab, player));
    }

    public IEnumerator HandleExplosionSinglePlayer(Vector3 spawnPosition, GameObject explosionPrefab, GameObject player)
    {
        GameObject explosion = Instantiate(explosionPrefab, spawnPosition, Quaternion.identity);
        SFXManager.Instance.PlaySound(SFXManager.Instance.explosionSound);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("EndScreen");
        yield return new WaitForSeconds(0.8f);
        Destroy(explosion);
        Destroy(player);
    }
    #endregion
    #region MultiPlayerFunctions

    #region MobileCheck
    bool IsMobile()
    {
#if UNITY_ANDROID || UNITY_IOS
        return true;
#elif UNITY_WEBGL
        return IsMobileWebGL();
#else
        return false;
#endif
    }

bool IsMobileWebGL()
{
#if UNITY_WEBGL && !UNITY_EDITOR
    return DetectMobileDevice();
#else
    return false;
#endif
}

#if UNITY_WEBGL && !UNITY_EDITOR
    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern bool DetectMobileDevice();
#endif

    #endregion

    #endregion
}
