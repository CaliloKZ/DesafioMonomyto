using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class PlayerListItem : MonoBehaviourPunCallbacks
{
    private TextMeshProUGUI m_playerNameText;

    private Player m_player;

    private void Awake() => m_playerNameText = GetComponent<TextMeshProUGUI>();

    public void SetUp(Player player)
    {
        m_player = player;
        m_playerNameText.text = m_player.NickName;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if(m_player == otherPlayer)
        {
            Destroy(gameObject);
        }
    }

    private void OnDisable()
    {
        Destroy(gameObject);
    }

}
