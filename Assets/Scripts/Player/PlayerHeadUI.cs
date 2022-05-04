using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerHeadUI : MonoBehaviour
{
    private PhotonView m_photonView;

    [SerializeField] private TextMeshProUGUI m_nickNameText;
    [SerializeField] private Canvas m_playerCanvas;

    private void Awake()
    {
        m_photonView = GetComponent<PhotonView>();
        m_playerCanvas.worldCamera = GameManager.mainCamera;
        m_nickNameText.text = m_photonView.Owner.NickName;

        if (m_photonView.IsMine)
            m_playerCanvas.gameObject.SetActive(false);
    }
}
