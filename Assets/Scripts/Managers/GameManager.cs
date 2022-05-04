using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static Camera mainCamera { get; private set; }

    [SerializeField] private CinemachineVirtualCamera m_playerCam;
    private PlayerManager m_playerManager;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Start()
    {
        var _playerManager = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), Vector3.zero, Quaternion.identity);
        m_playerManager = _playerManager.GetComponent<PlayerManager>();
        m_playerManager.SetCamera(m_playerCam);
    }

    public void RespawnPlayer() => m_playerManager.Respawn();

}
