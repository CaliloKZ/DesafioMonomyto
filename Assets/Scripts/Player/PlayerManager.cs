using System.IO;
using UnityEngine;
using Cinemachine;
using Photon.Pun;

public class PlayerManager : MonoBehaviour
{
    private PhotonView m_photonView;
    private CinemachineVirtualCamera m_playerCam;

    private GameObject m_player;

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
        m_player = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"), _randomPos, Quaternion.identity);
        m_playerCam.Follow = m_player.transform;
    }

    public void SetCamera(CinemachineVirtualCamera newPlayerCam) => m_playerCam = newPlayerCam;

    public void OnPlayerDeath()
    {
        m_playerCam.Follow = null;
    }

    public void Respawn() //TODO jeito de outro player ver o setactive.
    {
        Vector2 _randomPos = new Vector2(Random.Range(m_minX, m_maxX), Random.Range(m_minY, m_maxY));
        m_player.SetActive(true);
        m_player.transform.position = _randomPos;
        m_playerCam.Follow = m_player.transform;
    }
}
