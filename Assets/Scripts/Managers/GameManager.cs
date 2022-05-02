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

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Start()
    {
        var _playerManager = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), Vector3.zero, Quaternion.identity);
        _playerManager.GetComponent<PlayerManager>().SetCamera(m_playerCam);
    }


}
