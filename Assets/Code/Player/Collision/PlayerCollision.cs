using System.Collections;
using Airplane.PlanePhysics;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    public GameObject explosionPrefab;
    public PhotonView photonView;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Mountains") ||
        collision.gameObject.CompareTag("Player"))
        {
            if (GameManager.Instance.isMultiplayer)
            {
                if (photonView.IsMine)
                {
                    photonView.RPC("HandleCollision", RpcTarget.All);
                }
            }
        }

        if (!GameManager.Instance.isMultiplayer)
        {
            if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Mountains"))
            {
                GameManager.Instance.TriggerCollisionSinglePlayer(
                new Vector3(transform.position.x, transform.position.y - 3f, transform.position.z),
                explosionPrefab, gameObject);
            }
        }
    }

    [PunRPC]
    void HandleCollision()
    {
        foreach (var r in GetComponentsInChildren<Renderer>())
        {
            r.enabled = false;
        }

        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        SFXManager.Instance.PlaySound(SFXManager.Instance.explosionSound);
        Destroy(explosion, 2f);

        if (SFXManager.Instance != null)
        {
            SFXManager.Instance.PlaySound(SFXManager.Instance.explosionSound);
        }

        if (photonView.IsMine)
        {
            StartCoroutine(RespawnAfterDelay());
        }

        foreach (var r in GetComponentsInChildren<Renderer>())
        {
            r.enabled = true;
        }
    }

    IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(0.7f);
        gameObject.GetComponent<AirplaneController>().ResetAirplane();
        // foreach (var r in GetComponentsInChildren<Renderer>())
        // {
        //     r.enabled = true;
        // }
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }
}
