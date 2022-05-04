using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class PlayerListItem : MonoBehaviourPunCallbacks
{
    private TextMeshProUGUI m_playerNameText;

    public Player player { get; private set; }

    private void Awake() => m_playerNameText = GetComponent<TextMeshProUGUI>();

    public void SetUp(Player playerToSetUp)
    {
        player = playerToSetUp;
        m_playerNameText.text = playerToSetUp.NickName;
    }

}
