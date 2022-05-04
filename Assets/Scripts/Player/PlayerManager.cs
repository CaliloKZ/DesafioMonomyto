using System.IO;
using UnityEngine;
using Cinemachine;
using Photon.Pun;

public class PlayerManager : MonoBehaviour
{
    private PhotonView m_photonView;
    private CinemachineVirtualCamera m_playerCam;

    [SerializeField] private float m_minX;
    [SerializeField] private float m_maxX;
    [SerializeField] private float m_minY;
    [SerializeField] private float m_maxY;

    private void Awake()
    {
        m_photonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if (m_photonView.IsMine)
            CreateController();
    }

    private void CreateController()
    {
        Vector2 _randomPos = new Vector2(Random.Range(m_minX, m_maxX), Random.Range(m_minY, m_maxY));
        var _playerObj = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"), _randomPos, Quaternion.identity);
        m_playerCam.Follow = _playerObj.transform;
    }

    public void SetCamera(CinemachineVirtualCamera newPlayerCam) => m_playerCam = newPlayerCam;

    public void OnPlayerDeath()
    {
        m_playerCam.Follow = null;
    }

    public void Respawn()
    {
        if (m_photonView.IsMine)
            CreateController();
    }
}
